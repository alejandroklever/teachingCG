using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public class RasterProcess
    {
        public static void RasterizeMesh<V>(int width, int height, float3 camera, SceneObject<V> sceneObject)
            where V : struct, INormalVertex<V>
        {
            var texture = new Texture2D(width, height);
            var render = new Raster<V, ProjectedVertex>(texture);

            // Scene Setup
            var (viewMatrix, projectionMatrix) = CameraTools.GetViewAndProjection(camera, texture);


            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                var hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new ProjectedVertex {Homogeneous = hPosition};
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p => float4(p.Homogeneous.x / 512.0f, p.Homogeneous.y / 512.0f, 1, 1);

            render.DrawMesh(sceneObject.Mesh.ConvertTo(Topology.Lines));
        }
    }
}