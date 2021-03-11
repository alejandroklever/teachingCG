using System;
using System.Collections.Generic;
using System.Text;

namespace GMath
{
    /// <summary>
    /// Represents a line in space
    /// </summary>
    public struct Line3D
    {
        /// <summary>
        /// Gets the first sample
        /// </summary>
        public readonly float3 A;
        /// <summary>
        /// Gets the second sample
        /// </summary>
        public readonly float3 B;

        /// <summary>
        /// Direction from a to b
        /// </summary>
        public float3 D { get { return Gfx.normalize(B - A); } }

        public Ray3D FromA2B { get { return Ray3D.CreateFromTo(A, B); } }
        public Ray3D FromB2A { get { return Ray3D.CreateFromTo(B, A); } }

        /// <summary>
        /// Creates a line given by two positions in space
        /// </summary>
        public Line3D(float3 a, float3 b)
        {
            this.A = a;
            this.B = b;
        }

        public float Distance(float3 pto, out float3 closest)
        {
            return GTools.distanceP2L(pto, this.A, this.B, out closest);
        }

        public float Distance(float3 pto)
        {
            return Distance(pto, out float3 _);
        }
    }
}
