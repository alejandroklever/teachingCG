using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public class Switch<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public Switch(Transform transform, int roundness, bool renderBottom) : base(transform)
        {
            var washer = MyManifold<V>.Cylinder(roundness, 1, zero, 1f, .2f).Weld();
            var input = MyManifold<V>.HoledCylinder(roundness, 1, .2f * up, .3f, .4f, .2f, createDiscs: true,
                createTopDiscOnly: !renderBottom).Weld();
            var stick = MyManifold<V>.Cylinder(roundness, 1, .2f * up, .2f, .8f, createDiscs: false).Weld();

            var bezier = new BezierCurve(
                float3(0.2f, 1f, 0),
                float3(0.3f, 1.1f, 0),
                float3(0.6f, 2f, 0),
                float3(0, 2f, 0)
            );
            var head = MyManifold<V>.Revolution(4, roundness, t => bezier.GetPoint(t), up).Weld();

            AddMesh(washer, Materials.Metallic);
            AddMesh(input, Materials.Metallic);
            AddMesh(stick, Materials.Metallic);
            AddMesh(head, Materials.SandyMaterial);
            
            UpdateTranslation(zero);
        }
    }
}