using System;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class RasterProcess
    {
        private static SceneObject<V> CreateMeshScene<V>() where V : struct, INormalVertex<V>
        {
            var transform = new Transform
            {
                Position = 3f * float3.up,
                Scale = 2 * float3.one,
                Rotation = float3(0, 0 ,0)
            };
            var obj = new GuitarBody<V>(transform, 10,10);
            obj.ComputeNormals();
            obj.Mesh = obj.Mesh.Transform(obj.TransformMatrix);
            return obj;
        }

        public static void StartProcess<V>(TestConfig config, Texture2D texture = null) where V : struct, INormalVertex<V>
        {
            texture ??= new Texture2D(config.width, config.height);
            RasterizeMesh<V>(texture, config.camera, config.target, CreateMeshScene<V>());
            texture.Save("test.rbm");
            Console.WriteLine("Done.");
        }

        private static void RasterizeMesh<V>(Texture2D texture, float3 camera, float3 target, SceneObject<V> sceneObject)
            where V : struct, INormalVertex<V>
        {
            var render = new Raster<V, ProjectedVertex>(texture);

            // Scene Setup
            var (viewMatrix, projectionMatrix) = CameraTools.GetViewAndProjection(texture, camera, target);


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