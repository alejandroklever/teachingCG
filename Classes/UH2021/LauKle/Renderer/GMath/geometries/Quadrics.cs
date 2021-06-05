using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public struct Quadric
    {
        public readonly float3x3 Q;
        public readonly float3 P;
        public readonly float R;

        public Quadric(float3x3 Q, float3 P, float R)
        {
            this.Q = Q;
            this.P = P;
            this.R = R;
        }

        public bool Intersect(Ray3D ray, out float minT, out float maxT)
        {
            float a = dot(mul(ray.D, Q), ray.D);
            float b = 2 * dot(mul(ray.X, Q), ray.D) + dot(ray.D, P);
            float c = dot(mul(ray.X, Q), ray.X) + dot(ray.X, P) + R;

            float D = b * b - 4 * a * c;
            if (D < 0)
            {
                minT = maxT = float.NaN;
                return false;
            }

            minT = (-b - sqrt(D)) / (2 * a);
            maxT = (-b + sqrt(D)) / (2 * a);
            return true;
        }

        public static Quadric Sphere(float radius = 1)
        {
            return new Quadric(
                float3x3(
                    1, 0, 0,
                    0, 1, 0,
                    0, 0, 1
                    ),
                float3(0, 0, 0),
                -radius * radius
                );
        }
    }
}
