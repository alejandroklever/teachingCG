using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Potentiometer<V> : SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Potentiometer(Transform transform) : base(transform)
        {
            Mesh = PotentiometerMeshData<V>.GetMesh(6, 4).Weld();
            UpdateTranslation();
        }
    }

    public static class PotentiometerMeshData<V> where V : struct, INormalVertex<V>
    {
        private static readonly BezierCurve potentiometerOutline = new BezierCurve(
            float3(-0.9888976f, -1f, 0f),
            float3(-0.4800489f, -0.7274024f, 0f),
            float3(-0.5f, -0.6272122f, 0f),
            float3(-0.5f, -0.5f, 0f),
            float3(-0.5f, -0.1347239f, 0f),
            float3(-0.5181732f, -0.3998096f, 0f),
            float3(-0.5f, 0f, 0f),
            float3(-0.4939923f, 0.1321662f, 0f),
            float3(-0.1999048f, -0.1897819f, 0f),
            float3(0f, -0.1897819f, 0f)
        );

        public static Mesh<V> GetMesh(int slices, int stacks)
        {
            return MyManifold<V>.Revolution(slices, stacks, t => potentiometerOutline.GetPoint(t), float3.up);
        }
    }
}