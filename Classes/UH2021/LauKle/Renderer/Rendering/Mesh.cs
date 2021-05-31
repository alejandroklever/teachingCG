using GMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static GMath.Gfx;
using float3 = GMath.float3;

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
        public Mesh(V[] vertices, int[] indices, Topology topology = Topology.Triangles)
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
            V[] newVertices = Vertices.Clone() as V[];
            int[] newIndices = Indices.Clone() as int[];
            return new Mesh<V>(newVertices, newIndices, Topology);
        }
    }

    public static class MeshTools
    {
        #region Mesh Vertices Transforms

        public static Mesh<T> Transform<V, T>(this Mesh<V> mesh, Func<V, T> transform)
            where V : struct, IVertex<V> where T : struct, IVertex<T>
        {
            T[] newVertices = new T[mesh.Vertices.Length];

            for (int i = 0; i < newVertices.Length; i++)
                newVertices[i] = transform(mesh.Vertices[i]);

            return new Mesh<T>(newVertices, mesh.Indices, mesh.Topology);
        }

        public static Mesh<V> Transform<V>(this Mesh<V> mesh, Func<V, V> transform) where V : struct, IVertex<V>
        {
            return Transform<V, V>(mesh, transform);
        }

        public static Mesh<V> Transform<V>(this Mesh<V> mesh, float4x4 transform) where V : struct, IVertex<V>
        {
            return Transform<V>(mesh, v =>
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
        public static Mesh<V> ConvertTo<V>(this Mesh<V> mesh, Topology topology) where V : struct, IVertex<V>
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
                            V[] newVertices = mesh.Vertices.Clone() as V[];
                            int[] newIndices = new int[mesh.Indices.Length * 2];
                            int index = 0;
                            for (int i = 0; i < mesh.Indices.Length / 3; i++)
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
                    V[] newVertices = mesh.Vertices.Clone() as V[];
                    int[] indices = new int[newVertices.Length];
                    for (int i = 0; i < indices.Length; i++)
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

            Dictionary<int3, int> uniqueVertices = new Dictionary<int3, int>();
            int[] mappedVertices = new int[mesh.Vertices.Length];
            List<V> newVertices = new List<V>();

            for (int i = 0; i < mesh.Vertices.Length; i++)
            {
                V vertex = mesh.Vertices[i];
                float3 p = vertex.Position;
                int3 cell = (int3) (p / epsilon); // convert vertex position in a discrete cell.
                if (!uniqueVertices.ContainsKey(cell))
                {
                    uniqueVertices.Add(cell, newVertices.Count);
                    newVertices.Add(vertex);
                }

                mappedVertices[i] = uniqueVertices[cell];
            }

            int[] newIndices = new int[mesh.Indices.Length];
            for (int i = 0; i < mesh.Indices.Length; i++)
                newIndices[i] = mappedVertices[mesh.Indices[i]];

            return new Mesh<V>(newVertices.ToArray(), newIndices, mesh.Topology);
        }

        public static Mesh<V> Concat<V>(this Mesh<V> mesh1, Mesh<V> mesh2) where V : struct, IVertex<V>
        {
            var vertices = mesh1.Vertices.Concat(mesh2.Vertices)
                .ToArray();
            var indexes = mesh1.Indices.Concat(mesh2.Indices.Select(i => i + mesh1.Vertices.Length))
                .ToArray();
            return new Mesh<V>(vertices,
                indexes);
        }

        public static void ComputeNormals<V>(this Mesh<V> mesh) where V : struct, INormalVertex<V>
        {
            if (mesh.Topology != Topology.Triangles)
                return;

            float3[] normals = new float3[mesh.Vertices.Length];

            for (int i = 0; i < mesh.Indices.Length / 3; i++)
            {
                float3 p0 = mesh.Vertices[mesh.Indices[i * 3 + 0]].Position;
                float3 p1 = mesh.Vertices[mesh.Indices[i * 3 + 1]].Position;
                float3 p2 = mesh.Vertices[mesh.Indices[i * 3 + 2]].Position;

                // Compute the normal of the triangle.
                float3 N = cross(p1 - p0, p2 - p0);

                // Add the normal to the vertices involved
                normals[mesh.Indices[i * 3 + 0]] += N;
                normals[mesh.Indices[i * 3 + 1]] += N;
                normals[mesh.Indices[i * 3 + 2]] += N;
            }

            // Update per-vertex normal using normal accumulation normalized.
            for (int i = 0; i < mesh.Vertices.Length; i++)
                mesh.Vertices[i].Normal = normalize(normals[i]);
        }


        private static (List<int[]>, List<(int, int)>) GetFacesAndEdges<V>(this Mesh<V> mesh)
            where V : struct, IVertex<V>
        {
            var faces = new List<int[]>();
            var edges = new HashSet<(int, int)>();

            for (var i = 0; i < mesh.Indices.Length / 6; i++)
            {
                var tris1 = new List<int> {mesh.Indices[6 * i], mesh.Indices[6 * i + 1], mesh.Indices[6 * i + 2]};
                var tris2 = new List<int> {mesh.Indices[6 * i + 3], mesh.Indices[6 * i + 4], mesh.Indices[6 * i + 5]};

                var intersection = new HashSet<int>(tris1);
                intersection.IntersectWith(tris2);

                var p1 = tris1.First(j => !intersection.Contains(j));
                var p2 = tris2.First(j => !intersection.Contains(j));

                var index = tris1.IndexOf(p1);
                var face = new[] {p1, tris1[(index + 1) % 3], p2, tris1[(index + 2) % 3]};
                faces.Add(face);

                if (!edges.Contains((face[0], face[1])) && !edges.Contains((face[1], face[0])))
                    edges.Add((face[0], face[1]));

                if (!edges.Contains((face[1], face[2])) && !edges.Contains((face[2], face[1])))
                    edges.Add((face[1], face[2]));

                if (!edges.Contains((face[2], face[3])) && !edges.Contains((face[3], face[2])))
                    edges.Add((face[2], face[3]));

                if (!edges.Contains((face[3], face[0])) && !edges.Contains((face[0], face[3])))
                    edges.Add((face[3], face[0]));
            }

            return (faces, edges.ToList());
        }

        private static List<int[]> FacesInvolvingPoint<V>(this Mesh<V> mesh, int i, IEnumerable<int[]> faces)
            where V : struct, IVertex<V> => faces.Where(quad => quad.Contains(i)).ToList();

        private static List<(int, int)> EdgesInvolvingPoint<V>(this Mesh<V> mesh, int i, IEnumerable<(int, int)> edges)
            where V : struct, IVertex<V> => edges.Where(edge => edge.Item1 == i || edge.Item2 == i).ToList();

        private static List<(int, int)> EdgesOfFace(IReadOnlyList<int> face) =>
            face.Select((t, i) => (t, face[(i + 1) % face.Count])).ToList();

        public static Mesh<V> CatmullClark<V>(this Mesh<V> mesh, int iterations = 1) where V : struct, IVertex<V>
        {
            if (iterations == 0)
                return mesh;

            var (faces, edges) = GetFacesAndEdges(mesh);

            var facePoints = new Dictionary<int[], float3>();
            foreach (var face in faces)
            {
                var point = face.Select(i => mesh.Vertices[i].Position)
                    .Aggregate((t, x) => t + x) / 4;
                facePoints[face] = point;
            }

            var edgePoints = new Dictionary<(int, int), float3>();
            foreach (var edge in edges)
            {
                var (p1, p2) = edge;

                var (e1, e2) = (mesh.Vertices[p1].Position, mesh.Vertices[p2].Position);
                var facesInvolvingP1 = mesh.FacesInvolvingPoint(p1, faces);
                var facesInvolvingP2 = mesh.FacesInvolvingPoint(p2, faces);
                var correctFaces = facesInvolvingP1.Intersect(facesInvolvingP2).ToArray();
                var (f1, f2) = (correctFaces[0], correctFaces[1]);

                var point = (facePoints[f1] + facePoints[f2] + e1 + e2) / 4;
                edgePoints[edge] = edgePoints[(edge.Item2, edge.Item1)] = point;
            }

            var visited = new bool[mesh.Vertices.Length];
            foreach (var index in mesh.Indices)
            {
                if (visited[index])
                    continue;

                visited[index] = true;
                var p = mesh.Vertices[index].Position;
                var facesInvolvingP = mesh.FacesInvolvingPoint(index, faces);
                var edgesInvolvingP = mesh.EdgesInvolvingPoint(index, edges);

                var f = facesInvolvingP.Select(face => facePoints[face]).Aggregate((t, x) => t + x) /
                        facesInvolvingP.Count;
                var r = edgesInvolvingP.Select(edge => edgePoints[edge]).Aggregate((t, x) => t + x) /
                        edgesInvolvingP.Count;

                var n = facesInvolvingP.Count;
                var pos = (f + 2 * r + (n - 3) * p) / n;
                mesh.Vertices[index].Position = pos;
            }

            var vertices = new List<V>();
            var indices = new List<int>();
            foreach (var face in faces)
            {
                var o = facePoints[face];

                var p0 = mesh.Vertices[face[0]].Position;
                var e01 = edgePoints[(face[0], face[1])];
                var p1 = mesh.Vertices[face[1]].Position;
                var e30 = edgePoints[(face[3], face[0])];
                var e12 = edgePoints[(face[1], face[2])];
                var p3 = mesh.Vertices[face[3]].Position;
                var e23 = edgePoints[(face[2], face[3])];
                var p2 = mesh.Vertices[face[2]].Position;

                var index = vertices.Count;
                vertices.AddRange(new[] {p0, e01, p1, e30, o, e12, p3, e23, p2}.Select(p => new V {Position = p}));
                indices.AddRange(new[]
                {
                    index + 0, index + 1, index + 3,
                    index + 4, index + 3, index + 1,

                    index + 1, index + 2, index + 4,
                    index + 5, index + 4, index + 2,

                    index + 3, index + 4, index + 6,
                    index + 7, index + 6, index + 4,

                    index + 4, index + 5, index + 7,
                    index + 8, index + 7, index + 5
                });
            }

            return new Mesh<V>(vertices.ToArray(), indices.ToArray()).Weld().CatmullClark(iterations - 1);
        }

        public static float3 Average(this IEnumerable<float3> vertices)
        {
            var array = vertices as float3[] ?? vertices.ToArray();
            return array.Aggregate((t, x) => t + x) / array.Count();
        }

        public static IEnumerable<float3> Positions<V>(this IEnumerable<IVertex<V>> enumerable) where V : struct =>
            enumerable.Select(v => v.Position);

        public static Mesh<V> DooSabin<V>(this Mesh<V> mesh) where V : struct, IVertex<V>
        {
            var (faces, edges) = GetFacesAndEdges(mesh);

            var newVertices = new List<float3>();
            var newIndices = new List<int>();
            var newFaces = new List<int[]>();

            foreach (var face in faces)
            {
                var faceAverage = face.Select(index => mesh.Vertices[index].Position).Average();
                var edgesOfFace = EdgesOfFace(face);

                var innerSurface = new float3[face.Length];
                var expandedPoints = new Dictionary<int, List<float3>>();

                for (var i = 0; i < face.Length; i++)
                {
                    var index = face[i];
                    var point = mesh.Vertices[index].Position;
                    var edgesInvolvingP = mesh.EdgesInvolvingPoint(index, edgesOfFace);
                    var middle0 = (edgesInvolvingP[0].Item1 + edgesInvolvingP[0].Item2) / 2;
                    var middle1 = (edgesInvolvingP[1].Item1 + edgesInvolvingP[1].Item2) / 2;

                    if (!expandedPoints.ContainsKey(index))
                        expandedPoints[index] = new List<float3>();

                    var newPoint = innerSurface[i] = (point + middle0 + middle1 + faceAverage) / 4;
                    expandedPoints[index].Add(newPoint);
                }

                var n = newVertices.Count;
                newVertices.AddRange(innerSurface);
                newIndices.AddRange(new[]
                {
                    n, n + 1, n + 3,
                    n + 2, n + 3, n + 1
                });
                newFaces.Add(new[] {n, n + 1, n + 2, n + 3});
            }

            foreach (var edge in edges)
            {
                var (p0, p1) = edge;
                var facesInvolvingP0 = FacesInvolvingPoint(mesh, p0, faces);
                var facesInvolvingP1 = FacesInvolvingPoint(mesh, p1, faces);
                var facesAroundEdge = facesInvolvingP0.Intersect(facesInvolvingP1).ToList();

                var face0 = facesAroundEdge[0];
                var face1 = facesAroundEdge[1];
                
                
            }
            
            return null;
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
                vertices[i * (slices + 1) + j] = new V {Position = generating(j / (float) slices, i / (float) stacks)};

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
        
        public static Mesh<V> Sphere(int slices, int stacks, float3 pos, float r = 1f)
        {
            return Surface(slices, stacks, (u, v) =>
            {
                float alpha = u * 2 * pi;
                float beta = pi / 2 - v * pi;
                return pos + r * float3(cos(alpha) * cos(beta), sin(beta), sin(alpha) * cos(beta));
            });
        }

        public static Mesh<V> Cube(float3 pos)
        {
            var vertex = new[]
            {
                new V {Position = float3(0, 0, 0)},
                new V {Position = float3(1, 0, 0)},
                new V {Position = float3(1, 1, 0)},
                new V {Position = float3(0, 1, 0)},
                new V {Position = float3(0, 1, 1)},
                new V {Position = float3(1, 1, 1)},
                new V {Position = float3(1, 0, 1)},
                new V {Position = float3(0, 0, 1)}
            };
            
            var index = new[]
            {
                0, 2, 1, //face front
                0, 3, 2,
                2, 3, 4, //face top
                2, 4, 5,
                1, 2, 5, //face right
                1, 5, 6,
                0, 7, 4, //face left
                0, 4, 3,
                5, 4, 7, //face back
                5, 7, 6,
                0, 6, 7, //face bottom
                0, 1, 6
            };
            return new Mesh<V>(vertex, index).Transform(Transforms.Translate(pos));
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
