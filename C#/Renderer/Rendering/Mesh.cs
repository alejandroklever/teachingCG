using GMath;
using Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace Renderer
{
    public class Mesh<V> where V : struct, IVertex<V>
    {
        /// <summary>
        /// Gets the vertices of this mesh.
        /// </summary>
        public V[] Vertices { get; private set; }

        /// <summary>
        /// Gets the indices of this mesh. Depending on the topology, this array is grouped by 1, 2 or 3 to form the mesh points, edges or faces.
        /// </summary>
        public int[] Indices { get; private set; }

        /// <summary>
        /// Gets the topology of this mesh. Points will use an index per point. Lines will use two indices per line. Triangles will use three indices per triangle.
        /// </summary>
        public Topology Topology { get; private set; }

        /// <summary>
        /// Creates a mesh object using vertices, indices and the desired topology.
        /// </summary>
        public Mesh (V[] vertices, int[] indices, Topology topology = Topology.Triangles)
        {
            this.Vertices = vertices;
            this.Indices = indices;
            this.Topology = topology;
        }

        /// <summary>
        /// Gets a new mesh instance with vertices and indices clone.
        /// </summary>
        /// <returns></returns>
        public Mesh<V> Clone()
        {
            V[] newVertices = Vertices.Clone() as V[];
            int[] newIndices = Indices.Clone() as int[];
            return new Mesh<V>(newVertices, newIndices, this.Topology);
        }

        #region Mesh Vertices Transforms

        public Mesh<T> Transform<T>(Func<V, T> transform) where T : struct, IVertex<T>
        {
            T[] newVertices = new T[Vertices.Length];

            for (int i = 0; i < newVertices.Length; i++)
                newVertices[i] = transform(Vertices[i]);

            return new Mesh<T>(newVertices, Indices, Topology);
        }

        public Mesh<V> Transform(Func<V, V> transform)
        {
            return Transform<V>(transform);
        }

        public Mesh<V> Transform(float4x4 transform)
        {
            return Transform(v =>
            {
                float4 hP = float4(v.Position, 1);
                hP = mul(hP, transform);
                V newVertex = v;
                newVertex.Position = hP.xyz / hP.w;
                return newVertex;
            });
        }

        #endregion

        /// <summary>
        /// Changes a mesh to another object with different topology. For instance, from a triangle mesh to a wireframe (lines).
        /// </summary>
        public Mesh<V> ConvertTo(Topology topology)
        {
            switch (topology)
            {
                case Topology.Triangles:
                    switch (this.Topology)
                    {
                        case Topology.Triangles:
                            return this.Clone(); // No necessary change
                        case Topology.Lines:
                            // This problem is NP.
                            // Try to implement a greedy, that means, recognize the small triangle and so on...
                            throw new NotImplementedException("Missing implementing line-to-triangle conversion.");
                        case Topology.Points:
                            throw new NotImplementedException("Missing implementing point-to-triangle conversion.");
                    }
                    break;
                case Topology.Lines:
                    switch (this.Topology)
                    {
                        case Topology.Points:
                            // Get the wireframe from surface reconstruction
                            return ConvertTo(Topology.Triangles).ConvertTo(Topology.Lines);
                        case Topology.Lines:
                            return this.Clone(); // nothing to do
                        case Topology.Triangles:
                            {
                                // This is repeating edges for adjacent triangles.... use a hash table to prevent for double linking vertices.
                                V[] newVertices = Vertices.Clone() as V[];
                                int[] newIndices = new int[Indices.Length * 2];
                                int index = 0;
                                for (int i = 0; i < Indices.Length / 3; i++)
                                {
                                    newIndices[index++] = Indices[i * 3 + 0];
                                    newIndices[index++] = Indices[i * 3 + 1];

                                    newIndices[index++] = Indices[i * 3 + 1];
                                    newIndices[index++] = Indices[i * 3 + 2];
                                    
                                    newIndices[index++] = Indices[i * 3 + 2];
                                    newIndices[index++] = Indices[i * 3 + 0];
                                }
                                return new Mesh<V>(newVertices, newIndices, Topology.Lines);
                            }
                    }
                    break;
                case Topology.Points:
                    {
                        V[] newVertices = Vertices.Clone() as V[];
                        int[] indices = new int[newVertices.Length];
                        for (int i = 0; i < indices.Length; i++)
                            indices[i] = i;
                        return new Mesh<V>(newVertices, indices, Topology.Points);
                    }
            }

            throw new ArgumentException("Wrong topology.");
        }
    }

    /// <summary>
    /// Tool class to create different mesh from parametric methods.
    /// </summary>
    public class Manifold<V> where V : struct, IVertex<V>
    {
        public static Mesh<V> Surface(int slices, int stacks, Func<float, float, float3> generating)
        {
            V[] vertices = new V[(slices + 1) * (stacks + 1)];
            int[] indices = new int[slices * stacks * 6];

            // Filling vertices for the manifold.
            // A manifold with x,y,z mapped from (0,0)-(1,1)
            for (int i = 0; i <= stacks; i++)
                for (int j = 0; j <= slices; j++)
                    vertices[i * (slices + 1) + j] = new V { Position = generating(j / (float)slices, i / (float)stacks) };

            // Filling the indices of the quad. Vertices are linked to adjacent.
            int index = 0;
            for (int i = 0; i < stacks; i++)
                for (int j = 0; j < slices; j++)
                {
                    indices[index++] = i * (slices + 1) + j;
                    indices[index++] = (i + 1) * (slices + 1) + j;
                    indices[index++] = (i + 1) * (slices + 1) + (j + 1);

                    indices[index++] = i * (slices + 1) + j;
                    indices[index++] = (i + 1) * (slices + 1) + (j + 1);
                    indices[index++] = i * (slices + 1) + (j + 1);
                }

            return new Mesh<V>(vertices, indices);
        }

        public static Mesh<V> Generative(int slices, int stacks, Func<float, float3> g, Func<float3, float, float3> f)
        {
            return Surface(slices, stacks, (u, v) => f(g(u), v));
        }

        public static Mesh<V> Extrude(int slices, int stacks, Func<float, float3> g, float3 direction)
        {
            return Generative(slices, stacks, g, (v, t) => v + direction * t);
        }

        public static Mesh<V> Revolution(int slices, int stacks, Func<float, float3> g, float3 axis)
        {
            return Generative(slices, stacks, g, (v, t) => mul(float4(v, 1), Transforms.Rotate(t * 2 * pi, axis)).xyz);
        }

        public static Mesh<V> Lofted(int slices, int stacks, Func<float, float3> g1, Func<float, float3> g2)
        {
            return Surface(slices, stacks, (u, v) => g1(u) * (1 - v) + g2(u) * v);
        }
    }

    /// <summary>
    /// Represents different topologies to connect vertices.
    /// </summary>
    public enum Topology
    {
        /// <summary>
        /// Every vertex is a different point.
        /// </summary>
        Points,
        /// <summary>
        /// Every two vertices there is a line in between.
        /// </summary>
        Lines,
        /// <summary>
        /// Every three vertices form a triangle
        /// </summary>
        Triangles
    }
}
