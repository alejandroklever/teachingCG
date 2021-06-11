using System;
using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Fretboard<V> : SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Fretboard(Transform transform, float height, int frets, int roundness) : base(transform)
        {
            float xScale = .75f;
            float zScale = .5f;

            var scale = float3(xScale, height, zScale);

            var back = GenerateBackMesh(roundness, height, scale);

            var front = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    lerp(xScale * float3.left, xScale * float3.right, u) +
                    lerp(float3.zero, height * float3.up, v) + .15f * float3.back);

            var right = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    xScale * float3.right +
                    lerp(float3.zero, height * float3.up, v) +
                    lerp(float3.zero, .15f * float3.back, 1 - u));

            var left = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    xScale * float3.left +
                    lerp(float3.zero, height * float3.up, v) +
                    lerp(float3.zero, .15f * float3.back, u));

            var bottom = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    float3(
                        lerp(-xScale, xScale, v),
                        0,
                        lerp(-.15f, 0, u)));

            // var top = MyManifold<V>.Surface(1, 1,
            //     (u, v) =>
            //         float3(
            //             lerp(-xScale, xScale, v),
            //             height,
            //             lerp(-.15f, 0, 1 - u)));

            var fretMeshes = MeshTools.Join(BuildFrets(xScale, height, frets).ToArray());
            Mesh = MeshTools.Join(back, front, left, right, bottom).Weld();

            Console.WriteLine($"{Mesh.Vertices.Length} points");
            Console.WriteLine($"{Mesh.Indices.Length / 3} polygons");
            UpdateTranslation();
        }

        private List<Mesh<V>> BuildFrets(float width, float height, int frets)
        {
            var meshes = new List<Mesh<V>>();
            var total = frets + 2;
            var step = height / total;

            var scale = 1.36f * height;
            const float radio = 17.817f;
            var position = new float[24];
            for (var i = 1; i < total - 1; i++)
            {
                position[i] = position[i - 1] + (scale - position[i - 1]) / radio;
            }

            position = position.Reverse().ToArray();

            for (var i = 1; i < total - 1; i++)
            {
                // Console.WriteLine($"{(i, position[i])} => {height - position[i]}");
                var mesh = MyManifold<V>.Cylinder(3, 1, float3.zero, 1, 1, (pi, two_pi))
                    .Transform(
                        Transforms.DesiredTransform(
                            translation: float3(width, height - position[i], 0),
                            scale: float3(.03f, 2 * width, .03f),
                            eulerRotation: float3(0, 0, -pi / 2)
                        )
                    );

                meshes.Add(mesh);
            }

            return meshes;
        }

        private Mesh<V> GenerateBackMesh(int roundness, float height, float3 scale)
        {
            var (x, y, z) = scale;
            var bTop = new BezierCurve(
                x * float3.right + height * float3.up,
                x * float3.right + (height - .25f) * float3.up + .35f * float3.forward,
                x * float3.left + (height - .25f) * float3.up + .35f * float3.forward,
                x * float3.left + height * float3.up);

            var bBottom = new BezierCurve(
                x * float3.right,
                float3.up + (z + .05f) * float3.forward,
                float3.up + (z + .05f) * float3.forward,
                x * float3.left);

            var bTaco = new BezierCurve(
                .5f * float3.right + z * float3.forward,
                .2f * float3.right + .6f * float3.up + z * float3.forward,
                .2f * float3.left + .6f * float3.up + z * float3.forward,
                .5f * float3.left + z * float3.forward);
            
            var heatTopDecoration = new BezierCurve(
                float3(-1f, 0f, 0f),
                float3(-0.5f, 0f, 0f),
                float3(-0.05f, 0.25f, 0f),
                float3(0f, 0f, 0f),
                float3(0.05f, 0.25f, 0f),
                float3(0.5f, 0f, 0f),
                float3(1f, 0f, 0f));

            return MyManifold<V>.Lofted(roundness, 1,
                    u => bBottom.GetPoint(u),
                    v => bTop.GetPoint(v))
                .Concat(
                    MyManifold<V>.ImprovedLofted(roundness, 5,
                        u => bTaco.GetPoint(u),
                        v => bBottom.GetPoint(v),
                        (u, v, t) =>
                        {
                            var d = distance(u, v);
                            var b = new BezierCurve(
                                u,
                                u + .25f * d * float3.back,
                                u + .25f * d * float3.back,
                                v);
                            return b.GetPoint(t);
                        }
                    )
                )
                .Concat(
                    MyManifold<V>.Centroid(roundness, 1, z * float3.forward,
                        t => bTaco.GetPoint(t)))
                .Concat(
                    // Head Front
                    MyManifold<V>.Lofted(10, 10,
                        v => lerp(float3(-x, height, -.15f), float3(x, height, -.15f), v),
                        u => heatTopDecoration.GetPoint(u) + (height + 1.5f) * float3.up))
                .Concat(
                    // Head Back
                    MyManifold<V>.Lofted(10, 10,
                        u => heatTopDecoration.GetPoint(u) + (height + 1.5f) * float3.up + .15f * float3.forward,
                        v => lerp(float3(-x, height, 0f), float3(x, height, 0f), v)))
                .Concat(
                    // Head Right
                    MyManifold<V>.Lofted(1, 10,
                        u => lerp(float3(x, height, -.15f), float3(x, height, 0f), u),
                        v => lerp(float3(1, height + 1.5f, 0f), float3(1, height + 1.5f, .15f), v)))
                .Concat(
                    // Head Left
                    MyManifold<V>.Lofted(1, 10,
                        v => lerp(float3(-1, height + 1.5f, 0f), float3(-1, height + 1.5f, .15f), v),
                        u => lerp(float3(-x, height, -.15f), float3(-x, height, 0f), u)))
                .Concat(
                    // Head Top
                    MyManifold<V>.Lofted(10, 1,
                        u => heatTopDecoration.GetPoint(u) + (height + 1.5f) * float3.up,
                        v => heatTopDecoration.GetPoint(v) + (height + 1.5f) * float3.up + .15f * float3.forward))
                .Weld();
        }
    }
}