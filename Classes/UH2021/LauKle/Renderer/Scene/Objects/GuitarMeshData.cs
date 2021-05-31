using System.Collections.Generic;
using GMath;
using Rendering;
using Renderer.Scene;
using Renderer.Scene.Structs;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class GuitarMeshData
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

        private static Mesh<T> GetFrontMesh<T>(Topology topology = Topology.Lines) where T : struct, INormalVertex<T>
        {
            var upperMesh = Manifold<T>.Surface(10, 10, (u, v) =>
            {
                var left = upperLeftCurve.GetPoint(u);
                var right = upperRightCurve.GetPoint(u);
                return lerp(left, right, v);
            });

            var bottomMesh = Manifold<T>.Surface(10, 10,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(left, right, v);
                }
            );

            var mesh = bottomMesh.Concat(upperMesh).Weld();
            mesh.ComputeNormals();
            return mesh.ConvertTo(topology);
        }

        private static Mesh<T> GetBackMesh<T>(Topology topology = Topology.Lines) where T : struct, INormalVertex<T>
        {
            var upperMesh = Manifold<T>.Surface(10, 10, (u, v) =>
            {
                var left = upperLeftCurve.GetPoint(u);
                var right = upperRightCurve.GetPoint(u);
                return lerp(left, right, v) + .5f * float3.back;
            });

            var bottomMesh = Manifold<T>.Surface(10, 10,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(left, right, v) + .5f * float3.back;
                }
            );

            var mesh = bottomMesh.Concat(upperMesh).Weld();
            mesh.ComputeNormals();
            return mesh.ConvertTo(topology);
        }

        private static Mesh<V> GetBorderMeshes<V>(int slices, int stacks, Topology topology = Topology.Lines)
            where V : struct, INormalVertex<V>
        {
            var meshes = new List<Mesh<V>>(
                new[]
                {
                    Manifold<V>.Lofted(slices, stacks,
                        t => bottomLeftCurve.GetPoint(t),
                        t => bottomLeftCurve.GetPoint(t) + .5f * float3.back),
                    Manifold<V>.Lofted(slices, stacks,
                        t => bottomRightCurve.GetPoint(t),
                        t => bottomRightCurve.GetPoint(t) + .5f * float3.back),
                    Manifold<V>.Lofted(slices, stacks,
                        t => upperLeftCurve.GetPoint(t),
                        t => upperLeftCurve.GetPoint(t) + .5f * float3.back),
                    Manifold<V>.Lofted(slices, stacks,
                        t => upperRightCurve.GetPoint(t),
                        t => upperRightCurve.GetPoint(t) + .5f * float3.back)
                }
            );

            var mesh = meshes[0].Concat(meshes[1]).Concat(meshes[2]).Concat(meshes[3]).Weld();
            mesh.ComputeNormals();
            return mesh.ConvertTo(topology);
        }

        public static void DrawMesh(Texture2D texture)
        {
            var render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            var CameraPosition = float3(5f, 0f, 5f);
            var (viewMatrix, projectionMatrix) = CameraTools.GetViewAndProjection(CameraPosition, texture);

            var frontMesh = GetFrontMesh<PositionNormal>();
            var borderMesh = GetBorderMeshes<PositionNormal>(10, 1);
            var backMesh = GetBackMesh<PositionNormal>();

            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                var hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex {Homogeneous = hPosition};
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p => float4(p.Homogeneous.x / 512.0f, p.Homogeneous.y / 512.0f, 1, 1);

            render.DrawMesh(frontMesh);
            render.DrawMesh(borderMesh);
            render.DrawMesh(backMesh);
        }
    }
}