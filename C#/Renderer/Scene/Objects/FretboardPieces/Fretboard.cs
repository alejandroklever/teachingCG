using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Fretboard<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private const float lateralWidth = .1f;
        private const int fretRoundness = 3;

        public Fretboard(Transform transform, float width, float height, float depth, int fretsCount, int roundness, bool addFrets = true, bool deleteBack = false) :
            base(transform)
        {
            var scale = float3(width, height, depth);

            var nut = GenerateNut(scale);
            var backMesh = GenerateBackMesh(roundness, scale);
            var generateFrontMesh = GenerateFrontMesh(scale);
            var lateralMesh = GenerateLateralMesh(scale);
            var frets = GenerateFrets(width, height, fretsCount);

            Mesh = MeshTools.Join(generateFrontMesh, lateralMesh,nut);

            if (addFrets)
                Mesh = Mesh.Concat(frets);
            
            if (!deleteBack)
                Mesh = Mesh.Concat(backMesh);
            
            UpdateTranslation(zero);
        }

        private Mesh<V> GenerateNut(float3 scale)
        {
            var nut = MyManifold<V>.UnitaryCube( back+ 20 * scale.y * up, 1).Transform(Transforms.Scale(.6f, 0.05f, 0.11f));
            return nut;
        }

        private static Mesh<V> GenerateFrets(float width, float height, int frets)
        {
            var meshes = new List<Mesh<V>>();
            var total = frets + 2;

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
                var mesh = MyManifold<V>.Cylinder(fretRoundness, 1, float3.zero, 1, 1, (pi, two_pi))
                    .Transform(
                        Transforms.DesiredTransform(
                            translation: float3(.95f * width, height - position[i], -lateralWidth),
                            scale: float3(.02f, .95f * (2 * width), .02f),
                            eulerRotation: float3(0, 0, -pi / 2)
                        )
                    );

                meshes.Add(mesh);
            }

            return MeshTools.Join(meshes.ToArray());
        }

        private Mesh<V> GenerateFrontMesh(float3 scale)
        {
            var (x, y, _) = scale;
            return MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    lerp(x * float3.left, x * float3.right, u) + // x
                    lerp(float3.zero, y * float3.up, v) + // y
                    lateralWidth * float3.back // z
            );
        }

        private Mesh<V> GenerateLateralMesh(float3 scale)
        {
            var (xScale, height, _) = scale;
            var right = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    xScale * float3.right +
                    lerp(float3.zero, height * float3.up, v) +
                    lerp(float3.zero, lateralWidth * float3.back, 1 - u));

            var left = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    xScale * float3.left +
                    lerp(float3.zero, height * float3.up, v) +
                    lerp(float3.zero, lateralWidth * float3.back, u));

            var bottom = MyManifold<V>.Surface(1, 1,
                (u, v) =>
                    float3(
                        lerp(-xScale, xScale, v),
                        0,
                        lerp(-lateralWidth, 0, u)));

            return MeshTools.Join(right, left, bottom).Weld();
        }

        private Mesh<V> GenerateBackMesh(int roundness, float3 scale)
        {
            var (x, y, z) = scale;
            var bTop = new BezierCurve(
                x * float3.right + y * float3.up,
                x * float3.right + (y - .25f) * float3.up + .5f * z * float3.forward,
                x * float3.left + (y - .25f) * float3.up + .5f * z * float3.forward,
                x * float3.left + y * float3.up);

            var bBottom = new BezierCurve(
                x * float3.right,
                2f *  float3.up + (z + .05f) * float3.forward,
                2f * float3.up + (z + .05f) * float3.forward,
                x * float3.left);

            var zScale = 1.4f;
            var bTaco = new BezierCurve(
                .66f * x * float3.right + zScale * z * float3.forward,
                .33f * x * float3.right + 1.3f * float3.up + zScale * z * float3.forward,
                .33f * x * float3.left + 1.3f * float3.up + zScale * z * float3.forward,
                .66f * x * float3.left + zScale * z * float3.forward);

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
                    MyManifold<V>.Centred(roundness, 1,  zScale * z * float3.forward,
                        t => bTaco.GetPoint(t)))
                .Concat(
                    MyManifold<V>.Lofted(roundness, 1,
                        u => bTop.GetPoint(u),
                        v => x * lerp(float3.right, float3.left, v) + y * float3.up))
               .Weld();
        }
    }
}