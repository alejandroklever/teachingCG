using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class GuitarBody<V> : SceneObject<V> where V : struct, INormalVertex<V>
    {
        public GuitarBody(Transform transform, int slices, int stacks) : base(transform)
        {
            Mesh = GuitarMeshData<V>.GetMesh(slices, stacks).Weld();
            UpdateTranslation();
        }
    }

    public static class GuitarMeshData<V> where V : struct, INormalVertex<V>
    {
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

        public static readonly BezierCurve topLeftCurve = new BezierCurve(
            float3(-1.6f, 0f, 0f),
            float3(-0.95f, 0.42f, 0f),
            float3(-1.7f, 0.89f, 0f),
            float3(-1.7f, 1.4f, 0f),
            float3(-1.7f, 2f, 0f),
            float3(-0.8f, 2f, 0f),
            float3(0f, 2f, 0f)
        );

        public static readonly BezierCurve topRightCurve = new BezierCurve(
            float3(1.6f, 0f, 0f),
            float3(0.95f, 0.42f, 0f),
            float3(1.7f, 0.89f, 0f),
            float3(1.7f, 1.4f, 0f),
            float3(1.7f, 2f, 0f),
            float3(0.8f, 2f, 0f),
            float3(0f, 2f, 0f)
        );

        public static Mesh<V> GetMesh(int slices, int stacks)
        {
            return MeshTools.Join(
                GetFrontMesh(slices, stacks),
                GetBorderMeshes(slices, 1),
                GetBackMesh(slices, stacks));
        }

        private static Mesh<V> GetFrontMesh(int slices, int stacks)
        {
            var upperMesh = MyManifold<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = topLeftCurve.GetPoint(u);
                var right = topRightCurve.GetPoint(u);
                return lerp(left, right, v);
            });

            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
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
            var upperMesh = MyManifold<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = topLeftCurve.GetPoint(u);
                var right = topRightCurve.GetPoint(u);
                return lerp(right, left, v) + float3.back;
            });

            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(right, left, v) + float3.back;
                }
            );

            return bottomMesh.Concat(upperMesh).Weld();
        }

        private static Mesh<V> GetBorderMeshes(int slices, int stacks)
        {
            var meshes = new[]
            {
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomLeftCurve.GetPoint(1 - t),
                    t => bottomLeftCurve.GetPoint(1 - t) + float3.back),
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomRightCurve.GetPoint(t),
                    t => bottomRightCurve.GetPoint(t) + float3.back),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topLeftCurve.GetPoint(1 - t),
                    t => topLeftCurve.GetPoint(1 - t) + float3.back),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topRightCurve.GetPoint(t),
                    t => topRightCurve.GetPoint(t) + float3.back)
            };

            return MeshTools.Join(meshes).Weld();
        }
    }
}