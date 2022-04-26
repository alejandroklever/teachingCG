using GMath;
using Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace Rendering
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
            Vertices = vertices;
            Indices = indices;
            Topology = topology;
        }

        /// <summary>
        /// Gets a new mesh instance with vertices and indices clone.
        /// </summary>
        /// <returns></returns>
        public Mesh<V> Clone()
        {
            var newVertices = Vertices.Clone() as V[];
            var newIndices = Indices.Clone() as int[];
            return new Mesh<V>(newVertices, newIndices, Topology);
        }
    }

    public static class MeshTools
    {
        #region Mesh Vertices Transforms

        public static Mesh<T> Transform<V, T>(this Mesh<V> mesh, Func<V, T> transform) where V : struct, IVertex<V> where T : struct, IVertex<T>
        {
            var newVertices = new T[mesh.Vertices.Length];

            for (var i = 0; i < newVertices.Length; i++)
                newVertices[i] = transform(mesh.Vertices[i]);

            return new Mesh<T>(newVertices, mesh.Indices, mesh.Topology);
        }

        public static Mesh<V> Transform<V>(this Mesh<V> mesh, Func<V, V> transform) where V: struct, IVertex<V>
        {
            return Transform<V, V>(mesh, transform);
        }

        public static Mesh<V> Transform<V>(this Mesh<V> mesh, float4x4 transform) where V: struct, IVertex<V>
        {
            return Transform<V>(mesh, v =>
            {
                var hP = float4(v.Position, 1);
                hP = mul(hP, transform);
                var newVertex = v;
                newVertex.Position = hP.xyz / hP.w;
                return newVertex;
            });
        }

        #endregion

        /// <summary>
        /// Changes a mesh to another object with different topology. For instance, from a triangle mesh to a wireframe (lines).
        /// </summary>
        public static Mesh<V> ConvertTo<V>(this Mesh<V> mesh, Topology topology) where V:struct, IVertex<V>
        {
            switch (topology)
            {
                case Topology.Triangles:
                    switch (mesh.Topology)
                    {
                        case Topology.Triangles:
                            return mesh.Clone(); // No necessary change
                        case Topology.Lines:
                            // This problem is NP.
                            // Try to implement a greedy, that means, recognize the small triangle and so on...
                            throw new NotImplementedException("Missing implementing line-to-triangle conversion.");
                        case Topology.Points:
                            throw new NotImplementedException("Missing implementing point-to-triangle conversion.");
                    }
                    break;
                case Topology.Lines:
                    switch (mesh.Topology)
                    {
                        case Topology.Points:
                            // Get the wireframe from surface reconstruction
                            return mesh.ConvertTo(Topology.Triangles).ConvertTo(Topology.Lines);
                        case Topology.Lines:
                            return mesh.Clone(); // nothing to do
                        case Topology.Triangles:
                            {
                                // This is repeating edges for adjacent triangles.... use a hash table to prevent for double linking vertices.
                                var newVertices = mesh.Vertices.Clone() as V[];
                                var newIndices = new int[mesh.Indices.Length * 2];
                                var index = 0;
                                for (var i = 0; i < mesh.Indices.Length / 3; i++)
                                {
                                    newIndices[index++] = mesh.Indices[i * 3 + 0];
                                    newIndices[index++] = mesh.Indices[i * 3 + 1];

                                    newIndices[index++] = mesh.Indices[i * 3 + 1];
                                    newIndices[index++] = mesh.Indices[i * 3 + 2];

                                    newIndices[index++] = mesh.Indices[i * 3 + 2];
                                    newIndices[index++] = mesh.Indices[i * 3 + 0];
                                }
                                return new Mesh<V>(newVertices, newIndices, Topology.Lines);
                            }
                    }
                    break;
                case Topology.Points:
                    {
                        var newVertices = mesh.Vertices.Clone() as V[];
                        var indices = new int[newVertices.Length];
                        for (var i = 0; i < indices.Length; i++)
                            indices[i] = i;
                        return new Mesh<V>(newVertices, indices, Topology.Points);
                    }
            }

            throw new ArgumentException("Wrong topology.");
        }
        /// <summary>
        /// Welds different vertices with positions close to each other using an epsilon decimation.
        /// </summary>
        public static Mesh<V> Weld<V>(this Mesh<V> mesh, float epsilon = 0.0001f) where V : struct, IVertex<V>
        {
            // Method using decimation...
            // TODO: Implement other methods

            var uniqueVertices = new Dictionary<int3, int>();
            var mappedVertices = new int[mesh.Vertices.Length];
            var newVertices = new List<V>();

            for (var i = 0; i < mesh.Vertices.Length; i++)
            {
                var vertex = mesh.Vertices[i];
                var p = vertex.Position;
                var cell = (int3)(p / epsilon); // convert vertex position in a discrete cell.
                if (!uniqueVertices.ContainsKey(cell))
                {
                    uniqueVertices.Add(cell, newVertices.Count);
                    newVertices.Add(vertex);
                }
                mappedVertices[i] = uniqueVertices[cell];
            }

            var newIndices = new int[mesh.Indices.Length];
            for (var i = 0; i < mesh.Indices.Length; i++)
                newIndices[i] = mappedVertices[mesh.Indices[i]];

            return new Mesh<V>(newVertices.ToArray(), newIndices, mesh.Topology);
        }

        /// <summary>
        /// Computes the normals of the mesh vertices using the positions and the orientation of the triangles.
        /// </summary>
        public static void ComputeNormals<V>(this Mesh<V> mesh) where V:struct, INormalVertex<V>
        {
            if (mesh.Topology != Topology.Triangles)
                return;

            var normals = new float3[mesh.Vertices.Length];

            for (var i=0; i<mesh.Indices.Length/3; i++)
            {
                var p0 = mesh.Vertices[mesh.Indices[i * 3 + 0]].Position;
                var p1 = mesh.Vertices[mesh.Indices[i * 3 + 1]].Position;
                var p2 = mesh.Vertices[mesh.Indices[i * 3 + 2]].Position;

                // Compute the normal of the triangle.
                var N = cross(p1 - p0, p2 - p0);

                // Add the normal to the vertices involved
                normals[mesh.Indices[i * 3 + 0]] += N;
                normals[mesh.Indices[i * 3 + 1]] += N;
                normals[mesh.Indices[i * 3 + 2]] += N;
            }

            // Update per-vertex normal using normal accumulation normalized.
            for (var i = 0; i < mesh.Vertices.Length; i++)
                mesh.Vertices[i].Normal = normalize(normals[i]);
        }

        /// <summary>
        /// Computes the Axis-aligned boundary box of the mesh using the vertex positions.
        /// </summary>
        public static AABB3D ComputeAABB<V>(this Mesh<V> mesh) where V:struct, IVertex<V>
        {
            var minimum = float3(float.MaxValue, float.MaxValue, float.MaxValue);
            var maximum = float3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var v in mesh.Vertices)
            {
                minimum = min(minimum, v.Position);
                maximum = max(maximum, v.Position);
            }
            return new AABB3D { Minimum = minimum, Maximum = maximum };
        }
    }


    /// <summary>
    /// Tool class to create different mesh from parametric methods.
    /// </summary>
    public class Manifold<V> where V : struct, ICoordinatesVertex<V>
    {
        public static Mesh<V> Surface(int slices, int stacks, Func<float, float, float3> generating)
        {
            var vertices = new V[(slices + 1) * (stacks + 1)];
            var indices = new int[slices * stacks * 6];

            // Filling vertices for the manifold.
            // A manifold with x,y,z mapped from (0,0)-(1,1)
            for (var i = 0; i <= stacks; i++)
                for (var j = 0; j <= slices; j++)
                    vertices[i * (slices + 1) + j] = new V { Position = generating(j / (float)slices, i / (float)stacks), Coordinates = float2(j / (float)slices, i / (float)stacks) };

            // Filling the indices of the quad. Vertices are linked to adjacent.
            var index = 0;
            for (var i = 0; i < stacks; i++)
                for (var j = 0; j < slices; j++)
                    if ((i + j) % 2 == 0)
                    {
                        indices[index++] = i * (slices + 1) + j;
                        indices[index++] = (i + 1) * (slices + 1) + j;
                        indices[index++] = (i + 1) * (slices + 1) + j + 1;

                        indices[index++] = i * (slices + 1) + j;
                        indices[index++] = (i + 1) * (slices + 1) + j + 1;
                        indices[index++] = i * (slices + 1) + j + 1;
                    }
                    else
                    {
                        indices[index++] = i * (slices + 1) + j;
                        indices[index++] = (i + 1) * (slices + 1) + j;
                        indices[index++] = i * (slices + 1) + j + 1;

                        indices[index++] = i * (slices + 1) + j + 1;
                        indices[index++] = (i + 1) * (slices + 1) + j;
                        indices[index++] = (i + 1) * (slices + 1) + j + 1;
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
