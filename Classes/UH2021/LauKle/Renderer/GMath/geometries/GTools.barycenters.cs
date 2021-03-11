using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;
namespace GMath
{
    public static partial class GTools
    {
        /// <summary>
        /// Gets the barycentric coordinates for a point in a plane
        /// </summary>
        public static float3 barycenter(float3 P, float3 t1, float3 t2, float3 t3)
        {
            float3 v0 = t2 - t1, v1 = t3 - t1, v2 = P - t1;
            float d00 = dot(v0, v0);
            float d01 = dot(v0, v1);
            float d11 = dot(v1, v1);
            float d20 = dot(v2, v0);
            float d21 = dot(v2, v1);
            float denom = d00 * d11 - d01 * d01;
            float v = (d11 * d20 - d01 * d21) / denom;
            float w = (d00 * d21 - d01 * d20) / denom;
            return float3(1 - v - w, v, w);
        }

        /// <summary>
        /// Gets the barycentric coordinates for a point in a line
        /// </summary>
        public static float2 barycenter(float3 P, float3 a, float3 b)
        {
            float3 v = b - a;
            float alpha = dot(P - a, v) / dot(v, v);
            return float2(1 - alpha, alpha);
        }
    }
}
