using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public struct Plane3D
    {
        public readonly float3 P;
        public readonly float3 N;

        public Plane3D(float3 P, float3 N)
        {
            this.P = P;
            this.N = N;
        }

        public bool Intersect (Ray3D ray, out float t)
        {
            float den = dot(ray.D, N);

            if (den == 0)
            {
                t = float.NaN;
                return false;
            }

            t = dot(P - ray.X, N) / den;
            return true;
        }
    }
}
