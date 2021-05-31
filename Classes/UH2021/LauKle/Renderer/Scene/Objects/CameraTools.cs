using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer
{
    public static class CameraTools
    {
        public static (float4x4, float4x4) GetViewAndProjection(float3 cameraPosition, Texture2D texture)
        {
            // View and projection matrices
            var viewMatrix = Transforms.LookAtLH(cameraPosition, float3(0, 0, 0), float3(0, 1, 0));
            var projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);
            return (viewMatrix, projectionMatrix);
        }
    }
}