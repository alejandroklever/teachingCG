using System;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Amplifier<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public Amplifier(Transform transform) : base(transform)
        {
            var upperBox = MyManifold<V>.Cube(10f * up, 20, 6, 8, 2)
                    .CatmullClark()
                    .CutBellowY(9)
                    .Transform(Transforms.Translate(.25f * float3.down))
                ;
            
            var bottomBox = MyManifold<V>.Cube(6 * up, 20, 12, 8, 2)
                .CatmullClark()
                .CutAboveY(10);

            var front = MyManifold<V>.Surface(1, 1, (u, v) => float3(
                lerp(-8f, 8f, u),
                lerp(-4f, 2.5f, v),
                -4 - .001f
            )).Transform(Transforms.Translate(0, 6 , 0));

            // var upFront = MyManifold<V>.Surface(1, 1, (u, v) => float3(
            //     lerp(-7f, 7f, u),
            //     lerp(-.5f, 1f, v),
            //     -4 - .001f
            // )).Transform(Transforms.Translate(0, 10f, 0));

            AddMesh(front, Materials.AmpMeshTexture);
            AddMesh(bottomBox, Materials.BrownLeatherTexture);
            // Add(upFront, Materials.BrownLeatherTexture);
            AddMesh(upperBox, Materials.DarkLeatherTexture);

            UpdateTranslation(zero);
        }
    }
}