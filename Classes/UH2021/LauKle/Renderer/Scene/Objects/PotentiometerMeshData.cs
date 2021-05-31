using GMath;
using Renderer.Scene;
using Renderer.Scene.Structs;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class PotentiometerMeshData
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

        private static Mesh<V> GetMesh<V>(int slices, int stacks, Topology topology = Topology.Lines)
            where V : struct, INormalVertex<V>
        {
            var mesh = Manifold<V>.Revolution(slices, stacks, t => potentiometerOutline.GetPoint(t), float3.up).Weld();
            mesh.ComputeNormals();
            return mesh.ConvertTo(topology);
        }


        public static void DrawMesh(Texture2D texture)
        {
            var render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            var CameraPosition = float3(0f, 5f, 5f);
            var (viewMatrix, projectionMatrix) = CameraTools.GetViewAndProjection(CameraPosition, texture);

            var mesh = GetMesh<PositionNormal>(10, 10);

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

            render.DrawMesh(mesh);
        }
    }
}