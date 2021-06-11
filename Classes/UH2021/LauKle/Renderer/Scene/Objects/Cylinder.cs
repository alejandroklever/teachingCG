using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public class Cylinder<V> : SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Cylinder(Transform transform, int slices, int stacks, float radio, float height) : base(transform)
        {
            Mesh = MyManifold<V>.Lofted(slices, stacks,
                u =>
                {
                    var angle = lerp(0, two_pi, u);
                    return float3(radio * cos(angle), 0, radio * sin(angle));
                },
                v =>
                {
                    var angle = lerp(0, two_pi, v);
                    return float3(radio * cos(angle), height , radio * sin(angle));
                }).Weld();
            UpdateTranslation();
        }
    }
}