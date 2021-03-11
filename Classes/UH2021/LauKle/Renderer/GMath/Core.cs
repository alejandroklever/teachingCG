using System;

namespace GMath
{
    /// <summary>
    /// Represents a volume implicitly using a ray-cast function
    /// </summary>
    public interface IVolume
    {
        /// <summary>
        /// Determines every distance along the ray where the surface is hit.
        /// Distances are sorted
        /// </summary>
        /// <returns>Distance array. 0-sized array is returned if there is no hit to the volume</returns>
        float[] Raycast(float3 x, float3 w);
    }

    public static class VolumeX
    {
        /// <summary>
        /// Determines the distance along the ray where the surface is hit
        /// </summary>
        /// <returns>Distance value if exists (always positive). NaN if there is no hit to the volume</returns>
        public static float ClosestHit(this IVolume volume, float3 x, float3 w)
        {
            return ClosestHit(volume, x, w, out bool _);
        }

        /// <summary>
        /// Determines the distance along the ray where the surface is hit
        /// </summary>
        /// <returns>Distance value if exists (always positive). NaN if there is no hit to the volume</returns>
        public static float ClosestHit(this IVolume volume, float3 x, float3 w, out bool isInside)
        {
            var hits = volume.Raycast(x, w);
            isInside = hits.Length % 2 == 1;
            if (hits.Length == 0)
                return float.NaN;
            return hits[0];
        }

        /// <summary>
        /// Determines whenever a specific position is inside the volume.
        /// </summary>
        public static bool IsInside(this IVolume volume, float3 x)
        {
            var hits = volume.Raycast(x, Gfx.normalize(new float3(3.001f, 5.003f, 7.021f)));
            return hits.Length % 2 == 1;
        }
    }
}
