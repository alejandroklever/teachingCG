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
            var d00 = dot(v0, v0);
            var d01 = dot(v0, v1);
            var d11 = dot(v1, v1);
            var d20 = dot(v2, v0);
            var d21 = dot(v2, v1);
            var denom = d00 * d11 - d01 * d01;
            var v = (d11 * d20 - d01 * d21) / denom;
            var w = (d00 * d21 - d01 * d20) / denom;
            return float3(1 - v - w, v, w);
        }

        /// <summary>
        /// Gets the barycentric coordinates for a point in a line
        /// </summary>
        public static float2 barycenter(float3 P, float3 a, float3 b)
        {
            var v = b - a;
            var alpha = dot(P - a, v) / dot(v, v);
            return float2(1 - alpha, alpha);
        }
    }
}
