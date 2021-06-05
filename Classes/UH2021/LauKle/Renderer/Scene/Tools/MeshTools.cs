using System.Collections.Generic;
using System.Linq;
using GMath;
using Rendering;

namespace Renderer.Scene
{
    public static class MeshTools
    {
        public static Mesh<V> Concat<V>(this Mesh<V> mesh1, Mesh<V> mesh2) where V : struct, IVertex<V>
        {
            var vertices = mesh1.Vertices.Concat(mesh2.Vertices)
                .ToArray();
            var indexes = mesh1.Indices.Concat(mesh2.Indices.Select(i => i + mesh1.Vertices.Length))
                .ToArray();
            return new Mesh<V>(vertices, indexes);
        }

        public static Mesh<V> Join<V>(params Mesh<V>[] meshes) where V : struct, IVertex<V>
        {
            return meshes.Aggregate((t, x) => t.Concat(x));
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
    }

}