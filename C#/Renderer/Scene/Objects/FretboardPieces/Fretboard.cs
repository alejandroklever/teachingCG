using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Fretboard<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private const float lateralWidth = .15f;
        private const int fretRoundness = 3;

        public Fretboard(Transform transform, float width, float height, float depth, int fretsCount, int roundness, bool addFrets = true, bool deleteBack = false) :
            base(transform)
        {
            var scale = float3(width, height, depth);

            var nut = GenerateNut(scale);
            var frontMesh = GenerateFrontMesh(scale);
            var lateralMesh = GenerateLateralMesh(scale);
            var frets = GenerateFrets(width, height, fretsCount);

            AddMesh(frontMesh, Materials.Black);
            AddMesh(lateralMesh, Materials.White);
            AddMesh(nut, Materials.White);
            
            if (addFrets)
                AddMesh(frets, Materials.FretsColor);
            UpdateTranslation(zero);
        }

        private Mesh<V> GenerateNut(float3 scale)
        {
            var nut = MyManifold<V>.UnitaryCube(back+ 20 * scale.y * up, 1).Transform(Transforms.Scale(.8f, 0.05f, 0.11f));
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
    }
}