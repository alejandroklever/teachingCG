using GMath;

namespace Renderer.Scene
{
    /// <summary>
    /// Payload used to pick a color from a hit intersection
    /// </summary>
    public struct RTRayPayload
    {
        public float3 Color;
        public int Bounces; // Maximum value of allowed bounces
    }

    public struct PTRayPayload
    {
        public float3 Color; // Accumulated color to the viewer
        public float3 Importance; // Importance of the ray to the viewer
        public int Bounces; // Maximum value of allowed bounces
    }

    /// <summary>
    /// Payload used to flag when a ray was shadowed.
    /// </summary>
    public struct ShadowRayPayload
    {
        public bool Shadowed;
    }
}