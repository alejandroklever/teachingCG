using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Guitar<V> : SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Guitar(Transform transform) : base(transform)
        {
            Mesh = GuitarMeshData<V>.GetMesh(10, 10);
            UpdateTranslation();
        }
    }

    public static class GuitarMeshData<V> where V : struct, INormalVertex<V>
    {
        public static readonly BezierCurve GuitarOutline = new BezierCurve(
            float3(-0.04884124f, -2.970556f, 0f),
            float3(-3.124903f, -2.962161f, 0f),
            float3(-2.721841f, -0.7429778f, 0f),
            float3(-1.6f, -0.005768061f, 0f),
            float3(-0.9554807f, 0.417773f, 0f),
            float3(-1.717579f, 0.8927901f, 0f),
            float3(-1.707786f, 1.40761f, 0f),
            float3(-1.697563f, 1.945039f, 0f),
            float3(-0.7979001f, 2.036842f, 0f),
            float3(-0.007782161f, 2.010925f, 0f),
            float3(0.2717567f, 2.001756f, 0f),
            float3(0.4391647f, 1.99094f, 0f),
            float3(0.5791342f, 1.99094f, 0f),
            float3(0.5791342f, 1.825696f, 0f),
            float3(0.5883141f, 1.632911f, 0f),
            float3(0.5791342f, 1.375865f, 0f),
            float3(0.6066751f, 0.8709524f, 0f),
            float3(1.322733f, 0.8342313f, 0f),
            float3(1.313553f, 1.366685f, 0f),
            float3(1.295192f, 1.522749f, 0f),
            float3(1.476111f, 1.761416f, 0f),
            float3(1.634861f, 1.284063f, 0f),
            float3(1.810262f, 0.7566362f, 0f),
            float3(0.9574833f, 0.4208248f, 0f),
            float3(1.6f, -0.002990961f, 0f),
            float3(2.721841f, -0.7429778f, 0f),
            float3(3.11974f, -2.979204f, 0f)
        );

        public static readonly BezierCurve bottomLeftCurve = new BezierCurve(
            float3(0f, -3f, 0f),
            float3(-1.6f, -3f, 0f),
            float3(-2.3f, -2.1f, 0f),
            float3(-2.4f, -1.6f, 0f),
            float3(-2.5f, -1f, 0f),
            float3(-2.1f, -0.3f, 0f),
            float3(-1.6f, 0f, 0f)
        );

        public static readonly BezierCurve bottomRightCurve = new BezierCurve(
            float3(0f, -3f, 0f),
            float3(1.6f, -3f, 0f),
            float3(2.3f, -2.1f, 0f),
            float3(2.4f, -1.6f, 0f),
            float3(2.5f, -1f, 0f),
            float3(2.1f, -0.3f, 0f),
            float3(1.6f, 0f, 0f)
        );

        public static readonly BezierCurve upperLeftCurve = new BezierCurve(
            float3(-1.6f, -0.005768061f, 0f),
            float3(-0.9554807f, 0.4177729f, 0f),
            float3(-1.717579f, 0.8927901f, 0f),
            float3(-1.707786f, 1.40761f, 0f),
            float3(-1.697563f, 1.945039f, 0f),
            float3(-0.7979001f, 2.036842f, 0f),
            float3(-0.007782161f, 2.010925f, 0f)
        );

        public static readonly BezierCurve upperRightCurve = new BezierCurve(
            float3(1.6f, -0.005768061f, 0f),
            float3(0.9554807f, 0.4177729f, 0f),
            float3(1.717579f, 0.8927901f, 0f),
            float3(1.707786f, 1.40761f, 0f),
            float3(1.697563f, 1.945039f, 0f),
            float3(0.7979001f, 2.036842f, 0f),
            float3(0.007782161f, 2.010925f, 0f)
        );

        public static Mesh<V> GetMesh(int slices, int stacks)
        {
            return MeshTools.Join(GetFrontMesh(slices, stacks), GetBorderMeshes(slices, 1),
                GetBackMesh(slices, stacks));
        }

        private static Mesh<V> GetFrontMesh(int slices, int stacks)
        {
            var upperMesh = Manifold2<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = upperLeftCurve.GetPoint(u);
                var right = upperRightCurve.GetPoint(u);
                return lerp(left, right, v);
            });

            var bottomMesh = Manifold2<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(left, right, v);
                }
            );

            return bottomMesh.Concat(upperMesh).Weld();
        }

        private static Mesh<V> GetBackMesh(int slices, int stacks)
        {
            var upperMesh = Manifold2<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = upperLeftCurve.GetPoint(u);
                var right = upperRightCurve.GetPoint(u);
                return lerp(left, right, v) + .5f * float3.back;
            });

            var bottomMesh = Manifold2<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(left, right, v) + .5f * float3.back;
                }
            );

            return bottomMesh.Concat(upperMesh).Weld();
        }

        private static Mesh<V> GetBorderMeshes(int slices, int stacks)
        {
            var meshes = new[]
            {
                Manifold2<V>.Lofted(slices, stacks,
                    t => bottomLeftCurve.GetPoint(t),
                    t => bottomLeftCurve.GetPoint(t) + .5f * float3.back),
                Manifold2<V>.Lofted(slices, stacks,
                    t => bottomRightCurve.GetPoint(t),
                    t => bottomRightCurve.GetPoint(t) + .5f * float3.back),
                Manifold2<V>.Lofted(slices, stacks,
                    t => upperLeftCurve.GetPoint(t),
                    t => upperLeftCurve.GetPoint(t) + .5f * float3.back),
                Manifold2<V>.Lofted(slices, stacks,
                    t => upperRightCurve.GetPoint(t),
                    t => upperRightCurve.GetPoint(t) + .5f * float3.back)
            };

            return MeshTools.Join(meshes).Weld();
        }
    }
}