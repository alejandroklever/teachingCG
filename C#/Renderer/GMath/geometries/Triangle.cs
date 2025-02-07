﻿using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public struct Triangle3D
    {
        public float3 V0;
        public float3 V1;
        public float3 V2;

        public Triangle3D(float3 v0, float3 v1, float3 v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }

        public bool Intersect(Ray3D ray, out float t, out float3 baricenter)
        {
            var P = V0;
            var N = cross(V1 - V0, V2 - V0);

            var den = dot(ray.D, N);

            if (den == 0)
            {
                t = float.NaN;
                baricenter = float3(0, 0, 0);
                return false;
            }

            t = dot(P - ray.X, N) / den;
            baricenter = GTools.barycenter(ray.X + ray.D * t, V0, V1, V2);
            return all(baricenter >= 0);
        }
    }
}
