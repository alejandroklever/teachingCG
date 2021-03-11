using System;
using System.Collections.Generic;
using System.Text;

namespace GMath
{
    /// <summary>
    /// Represents a segment
    /// </summary>
    public struct Segment3D
    {
        /// <summary>
        /// Gets the first endpoint
        /// </summary>
        public readonly float3 A;
        /// <summary>
        /// Gets the second endpoint
        /// </summary>
        public readonly float3 B;

        /// <summary>
        /// Position of the first endpoint
        /// </summary>
        public float3 X { get { return A; } }

        /// <summary>
        /// Vector from a to b
        /// </summary>
        public float3 W { get { return (B - A); } }

        /// <summary>
        /// Direction from a to b
        /// </summary>
        public float3 D { get { return Gfx.normalize(B - A); } }

        public Ray3D RayFromA2B { get { return Ray3D.CreateFromTo(A, B); } }
        public Ray3D RayFromB2A { get { return Ray3D.CreateFromTo(B, A); } }

        /// <summary>
        /// Length of the segment
        /// </summary>
        public float Length
        {
            get { return Gfx.length(W); }
        }

        /// <summary>
        /// Creates a segment given by two positions in space
        /// </summary>
        public Segment3D(float3 a, float3 b)
        {
            this.A = a;
            this.B = b;
        }

        public static explicit operator Line3D(Segment3D segment)
        {
            return new Line3D(segment.A, segment.B);
        }

        public float Distance(Segment3D other, out float3 closest1, out float3 closest2)
        {
            return GTools.distanceS2S(this.A, this.B, other.A, other.B, out closest1, out closest2);
        }
        public float Distance(Segment3D other)
        {
            return GTools.distanceS2S(this.A, this.B, other.A, other.B, out float3 _, out float3 _);
        }

        public float3 FromBaricenter(float alpha)
        {
            return A * (1 - alpha) + B * alpha;
        }

        public float3 RandomUniform(GRandom rnd)
        {
            return FromBaricenter(rnd.random());
        }
    }
}
