using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public static partial class GTools
    {
        /// <summary>
        /// Distance from point to point
        /// </summary>
        public static float distanceP2P(float3 a, float3 b)
        {
            return distance(a, b);
        }

        /// <summary>
        /// Distance from point to line (given by two points)
        /// </summary>
        public static float distanceP2L(float3 p, float3 a, float3 b, out float3 closest)
        {
            float3 b_a = a - b;
            float alpha = dot(p - b, b_a) / dot(b_a, b_a);
            closest = lerp(b, a, alpha);
            return distanceP2P(p, closest);
        }

        /// <summary>
        /// Distance from point to line (given by two points)
        /// </summary>
        public static float distanceP2L(float3 p, float3 a, float3 b)
        {
            return distanceP2L(p, a, b, out float3 _);
        }

        /// <summary>
        /// Distance from point to ray (given by a position and direction)
        /// </summary>
        public static float distanceP2R(float3 p, float3 pos, float3 dir, out float3 closest)
        {
            float3 b_a = dir;
            float alpha = dot(p - pos, b_a) / dot(b_a, b_a);
            closest = pos + dir * max(0, alpha);
            return distanceP2P(p, closest);
        }

        /// <summary>
        /// Distance from point to ray (given by a position and direction)
        /// </summary>
        public static float distanceP2R(float3 p, float3 pos, float3 dir)
        {
            return distanceP2R(p, pos, dir, out float3 _);
        }

        /// <summary>
        /// Distance from point to segment
        /// </summary>
        public static float distanceP2S(float3 p, float3 a, float3 b, out float3 closest)
        {
            float3 b_a = a - b;
            float alpha = dot(p - b, b_a) / dot(b_a, b_a);
            closest = lerp(b, a, saturate(alpha));
            return distanceP2P(p, closest);
        }

        /// <summary>
        /// Distance from point to segment
        /// </summary>
        public static float distanceP2S(float3 p, float3 a, float3 b)
        {
            return distanceP2S(p, a, b, out float3 _);
        }

        /// <summary>
        /// Distance from point to a plane (given by a position and normal)
        /// </summary>
        public static float distanceP2X(float3 p, float3 P, float3 N, out float3 closest)
        {
            closest = p - N * dot(p - P, N);
            return absdot(p - closest, N);
        }

        /// <summary>
        /// Distance from point to a plane (given by a position and normal)
        /// </summary>
        public static float distanceP2X(float3 p, float3 P, float3 N)
        {
            return distanceP2X(p, P, N, out float3 _);
        }

        /// <summary>
        /// Distance from point to a triangle (given by 3 points)
        /// </summary>
        public static float distanceP2T(float3 p, float3 a, float3 b, float3 c, out float3 closest)
        {
            float3 N = normalize(cross(c - a, b - a));
            float3 P = a;

            float distance = distanceP2X(p, P, N, out closest);

            float3x3 M = float3x3(
                cross(b - c, closest - c),
                cross(c - a, closest - a),
                cross(a - b, closest - b)
                );

            float3 bary = mul(N, transpose(M));
            bary /= (bary.x + bary.y + bary.z);

            if (all(bary >= 0))
                return distance;

            if (bary.x < 0) // 
                return distanceP2S(p, c, b, out closest);
            if (bary.y < 0)
                return distanceP2S(p, c, a, out closest);
            return distanceP2S(p, b, a, out closest);
        }

        /// <summary>
        /// Distance from point to a triangle (given by 3 points)
        /// </summary>
        public static float distanceP2T(float3 p, float3 a, float3 b, float3 c)
        {
            return distanceP2T(p, a, b, c, out float3 _);
        }

        /// <summary>
        /// Distance between segments
        /// </summary>
        public static float distanceS2S(float3 a1, float3 b1, float3 a2, float3 b2, out float3 closest1, out float3 closest2)
        {
            float3 u = b1 - a1;
            float3 v = b2 - a2;
            float3 w = a1 - a2;
            float a = dot(u, u);         // always >= 0
            float b = dot(u, v);
            float c = dot(v, v);         // always >= 0
            float d = dot(u, w);
            float e = dot(v, w);
            float D = a * c - b * b;        // always >= 0
            float sc, sN, sD = D;       // sc = sN / sD, default sD = D >= 0
            float tc, tN, tD = D;       // tc = tN / tD, default tD = D >= 0

            // compute the line parameters of the two closest points
            if (D < 0.00001)
            { // the lines are almost parallel
                sN = 0.0f;         // force using point P0 on segment S1
                sD = 1.0f;         // to prevent possible division by 0.0 later
                tN = e;
                tD = c;
            }
            else
            {                 // get the closest points on the infinite lines
                sN = (b * e - c * d);
                tN = (a * e - b * d);
                if (sN < 0.0)
                {        // sc < 0 => the s=0 edge is visible
                    sN = 0.0f;
                    tN = e;
                    tD = c;
                }
                else if (sN > sD)
                {  // sc > 1  => the s=1 edge is visible
                    sN = sD;
                    tN = e + b;
                    tD = c;
                }
            }

            if (tN < 0.0)
            {            // tc < 0 => the t=0 edge is visible
                tN = 0.0f;
                // recompute sc for this edge
                if (-d < 0.0)
                    sN = 0.0f;
                else if (-d > a)
                    sN = sD;
                else
                {
                    sN = -d;
                    sD = a;
                }
            }
            else if (tN > tD)
            {      // tc > 1  => the t=1 edge is visible
                tN = tD;
                // recompute sc for this edge
                if ((-d + b) < 0.0)
                    sN = 0;
                else if ((-d + b) > a)
                    sN = sD;
                else
                {
                    sN = (-d + b);
                    sD = a;
                }
            }
            // finally do the division to get sc and tc
            sc = (abs(sN) < 0.00001 ? 0.0f : sN / sD);
            tc = (abs(tN) < 0.00001 ? 0.0f : tN / tD);

            // get the two closest points
            closest1 = a1 + (sc * u);
            closest2 = a2 + (tc * v);

            return distanceP2P(closest1, closest2);   // return the closest distance
        }

        /// <summary>
        /// Distance between segments
        /// </summary>
        public static float distanceS2S(float3 a1, float3 b1, float3 a2, float3 b2)
        {
            return distanceS2S(a1, b1, a2, b2, out float3 _, out float3 _);
        }

        // TODO: Implement line to line, segment to line

        /// <summary>
        /// Distance segment (given by two points) to triangle (given by 3 points)
        /// </summary>
        public static float distanceS2T(float3 a, float3 b, float3 t1, float3 t2, float3 t3, out float3 closest1, out float3 closest2)
        {
            // Assume is in an edge of the triangle
            float distance = distanceP2P(a, t1);
            closest1 = a; closest2 = t1;

            float3 cl1, cl2;
            float d;
            
            d = distanceS2S(a, b, t1, t2, out cl1, out cl2);
            if (d < distance)
            {
                closest1 = cl1;
                closest2 = cl2;
                distance = d;
            }

            d = distanceS2S(a, b, t2, t3, out cl1, out cl2);
            if (d < distance)
            {
                closest1 = cl1;
                closest2 = cl2;
                distance = d;
            }

            d = distanceS2S(a, b, t3, t1, out cl1, out cl2);
            if (d < distance)
            {
                closest1 = cl1;
                closest2 = cl2;
                distance = d;
            }

            // Assume is an interior point

            d = distanceP2T(a, t1, t2, t3, out cl2);
            if (d < distance)
            {
                closest1 = a;
                closest2 = cl2;
                distance = d;
            }

            d = distanceP2T(b, t1, t2, t3, out cl2);
            if (d < distance)
            {
                closest1 = b;
                closest2 = cl2;
                distance = d;
            }

            return distance;
        }

        /// <summary>
        /// Distance segment (given by two points) to triangle (given by 3 points)
        /// </summary>
        public static float distanceS2T(float3 a, float3 b, float3 t1, float3 t2, float3 t3)
        {
            return distanceS2T(a, b, t1, t2, t3, out float3 _, out float3 _);
        }


        /// <summary>
        /// Distance from a quad C, C+U, C+U+R, C+R to a triangle
        /// </summary>
        public static float distanceQ2T(float3 C, float3 U, float3 R, float3 N, float3 t1, float3 t2, float3 t3)
        {
            float3 p00 = C;
            float3 p01 = C + R;
            float3 p10 = C + U;
            float3 p11 = C + U + R;

            float3[] ed = new float3[] { p00, p01, p11, p10 };

            float dist = 1000000;
            for (int i = 0; i < 4; i++) // Assume, the closest point is in an edge.
                dist = min(dist, distanceS2T(ed[i], ed[(i + 1) % 4], t1, t2, t3));

            float3[] t = new float3[] { t1, t2, t3 };
            for (int i = 0; i < 3; i++) // Assume the closest point is inside, i.e. a vertex of the triangle
            {
                // Project t[i] on plane C, N
                float3 tp;
                distanceP2X(t[i], C, N, out tp);
                float cx = dot(tp - C, R);
                float cy = dot(tp - C, U);
                if (cx >= 0 && cy >= 0 && cx <= 1 && cy <= 1) // inside side
                    dist = min(dist, distanceP2T(tp, t1, t2, t3));
            }

            return dist;
        }
    }
}
