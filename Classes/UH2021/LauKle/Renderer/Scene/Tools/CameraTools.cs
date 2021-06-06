using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class CameraTools
    {
        public static (float4x4, float4x4) GetViewAndProjection(Texture2D texture, float3 cameraPosition, float3 target )
        {
            // View and projection matrices
            var viewMatrix = Transforms.LookAtLH(cameraPosition, target, float3(0, 1, 0));
            var projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);
            return (viewMatrix, projectionMatrix);
        }
    }
}