using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using static Renderer.Scene.MeshTools;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Microphone<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private  const int xSize = 8;
        private  const int ySize = 3;
        private  const int zSize = 15;
        
        public Microphone(Transform transform, bool renderBottom = true) : base(transform)
        {
            var centerCube = new Cube<V>(transform, xSize, ySize, zSize, 1, true, renderBottom);
            var center = centerCube.Mesh;

            var x = centerCube.xSize;
            var y = centerCube.ySize;
            var z = centerCube.zSize;

            var ringMeshes = new[]
            {
                LateralMesh(x, y, z + 1, true, renderBottom).Transform(Transforms.Translate(float3(x, 0, -1))),
                LateralMesh(x, y, z + 1, false, renderBottom).Transform(Transforms.Translate(float3(-1, 0, -1))),
                ConnectionMesh(x, y, z, renderBottom).Transform(Transforms.Translate(float3(0, 0, z))),
                ConnectionMesh(x, y, z, renderBottom).Transform(Transforms.Translate(float3(0, 0, -1)))
            };

            var ring = Join(ringMeshes)
                .Weld()
                .Transform(
                    Transforms.Translate(.5f * y * float3.up
                        // Transforms.Scale(float3(1.1f, 1f, 1.1f))
                    )
                )
                ;
            
            var (tx, _, tz) = ring.Center() - center.Center();
            center = center.Transform(Transforms.Translate(float3(tx, 0, tz)));
            
            // This delete meshes bellow ySize - 1 in center
            if (!renderBottom)
            {
                var tris = 0;
                var v = new List<float3>();
                var i = new List<int>();
                for (int j = 0; j < center.Indices.Length / 3; j++)
                {
                    var p0 = center.Vertices[center.Indices[3 * j + 0]].Position;
                    var p1 = center.Vertices[center.Indices[3 * j + 1]].Position;
                    var p2 = center.Vertices[center.Indices[3 * j + 2]].Position;

                    if (p0.y <  ySize - 1 || p1.y < ySize - 1 || p2.y < ySize - 1)
                        continue;
                    AddTriangle(v, i, p0, p1, p2, ref tris);
                }

                center = new Mesh<V>(v.Select(p => new V {Position = p}).ToArray(), i.ToArray());
            }
            
            // Mesh = ring.Concat(center);
            // Mesh = Mesh.Transform(Transforms.Translate(-Mesh.Center()));

            ring = ring.Transform(Transforms.Translate(-ring.Center()));
            center = center.Transform(Transforms.Translate(-center.Center()));
            
            Add(ring, Materials.Black);
            Add(center, Materials.Default);
            
            UpdateTranslation(zero);
        }

        private Mesh<V> LateralMesh(int x, int y, int z, bool right, bool renderBottom)
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();

            // right
            if (right)
            {
                AddFace(vertices, indices, float3(1, 1, 0), float3(1, 1, 1), float3(1, 0, 0), float3(1, 0, 1),
                    ref tris);
                AddFace(vertices, indices, float3(1, 1, z), float3(1, 1, z + 1), float3(1, 0, z), float3(1, 0, z + 1),
                    ref tris);
            }

            AddFace(vertices, indices, float3(1, 1, 1), float3(1, 1, z), float3(1, 0, 1), float3(1, 0, z), ref tris);

            // left
            if (!right)
            {
                AddFace(vertices, indices, float3(0, 1, 0), float3(0, 1, 1), float3(0, 0, 0), float3(0, 0, 1), ref tris,
                    true);
                AddFace(vertices, indices, float3(0, 1, z), float3(0, 1, z + 1), float3(0, 0, z), float3(0, 0, z + 1),
                    ref tris, true);

            }

            AddFace(vertices, indices, float3(0, 1, 1), float3(0, 1, z), float3(0, 0, 1), float3(0, 0, z), ref tris,
                true);


            // top
            AddFace(vertices, indices, float3(0, 1, 0), float3(0, 1, 1), float3(1, 1, 0), float3(1, 1, 1), ref tris);
            AddFace(vertices, indices, float3(0, 1, 1), float3(0, 1, z), float3(1, 1, 1), float3(1, 1, z), ref tris);
            AddFace(vertices, indices, float3(0, 1, z), float3(0, 1, z + 1), float3(1, 1, z), float3(1, 1, z + 1),
                ref tris);

            // bottom
            if (renderBottom)
            {
                AddFace(vertices, indices, float3(0, 0, 0), float3(0, 0, 1), float3(1, 0, 0), float3(1, 0, 1), ref tris,
                    true);
                AddFace(vertices, indices, float3(0, 0, 1), float3(0, 0, z), float3(1, 0, 1), float3(1, 0, z), ref tris,
                    true);
                AddFace(vertices, indices, float3(0, 0, z), float3(0, 0, z + 1), float3(1, 0, z), float3(1, 0, z + 1),
                    ref tris, true);
            }

            AddFace(vertices, indices, float3(0, 1, 0), float3(1, 1, 0), float3(0, 0, 0), float3(1, 0, 0), ref tris);
            AddFace(vertices, indices, float3(0, 1, z + 1), float3(1, 1, z + 1), float3(0, 0, z + 1),
                float3(1, 0, z + 1), ref tris, true);
            
            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(),
                indices.ToArray()).Weld();
        }

        private Mesh<V> ConnectionMesh(int x, int y, int z, bool deleteBottom)
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();
            
            AddFace(vertices, indices, float3(0, 1, 0), float3(x, 1, 0), float3(0, 0, 0), float3(x, 0, 0), ref tris);
            AddFace(vertices, indices, float3(0, 1, 1), float3(x, 1, 1), float3(0, 0, 1), float3(x, 0, 1), ref tris, true);

            AddFace(vertices, indices, float3(0, 1, 1), float3(x, 1, 1), float3(0, 1, 0), float3(x, 1, 0), ref tris);
            if (!deleteBottom)
                AddFace(vertices, indices, float3(0, 0, 1), float3(x, 0, 1), float3(0, 0, 0), float3(x, 0, 0), ref tris,
                    true);

            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(),
                indices.ToArray()).Weld();
        }
    }
}