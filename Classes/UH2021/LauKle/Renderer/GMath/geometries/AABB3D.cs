using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public struct AABB3D
    {
        public float3 Minimum;
        public float3 Maximum;

        public AABB3D (float3 min, float3 max)
        {
            this.Minimum = min;
            this.Maximum = max;
        }

        public bool Intersect(Ray3D ray, out float minT, out float maxT)
        {
            float xtmin = ray.D.x == 0 ? -10000000 : (Minimum.x - ray.X.x) / ray.D.x;
            float ytmin = ray.D.y == 0 ? -10000000 : (Minimum.y - ray.X.y) / ray.D.y;
            float ztmin = ray.D.z == 0 ? -10000000 : (Minimum.z - ray.X.z) / ray.D.z;
            float xtmax = ray.D.x == 0 ? 10000000 : (Maximum.x - ray.X.x) / ray.D.x;
            float ytmax = ray.D.y == 0 ? 10000000 : (Maximum.y - ray.X.y) / ray.D.y;
            float ztmax = ray.D.z == 0 ? 10000000 : (Maximum.z - ray.X.z) / ray.D.z;

            minT = max(max(xtmin, ytmin), ztmin);
            maxT = min(min(xtmax, ytmax), ztmax);

            return maxT >= minT;
        }

        public bool Intersect(Ray3D ray)
        {
            return Intersect(ray, out _, out _);
        }
    }
}
