using System;
using System.Collections.Generic;
using System.Text;

namespace GMath
{
    /// <summary>
    /// Represents a ray in space
    /// </summary>
    public struct Ray3D
    {
        /// <summary>
        /// Gets the start point of the ray
        /// </summary>
        public readonly float3 X;
        /// <summary>
        /// Gets the direction of the ray
        /// </summary>
        public readonly float3 D;

        /// <summary>
        /// Creates a ray given by the start position and the direction
        /// </summary>
        public Ray3D(float3 X, float3 D)
        {
            this.X = X;
            this.D = D;
        }

        /// <summary>
        /// Creates a ray that goes from a in direction to b
        /// </summary>
        public static Ray3D CreateFromTo(float3 a, float3 b)
        {
            return new Ray3D(a, b - a);
        }

        public static explicit operator Line3D (Ray3D ray)
        {
            return new Line3D(ray.X, ray.X + ray.D);
        }
    }
}
