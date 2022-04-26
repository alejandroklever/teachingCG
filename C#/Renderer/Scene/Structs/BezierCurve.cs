using System;
using System.Collections.Generic;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class BezierCurve
    {
        private readonly List<float3> _points;

        public int Count => _points.Count;
        public int CountOfSegments => _points.Count / 3;
        public List<float3> Points => _points;

        public BezierCurve()
        {
            _points = new List<float3>();
        }

        public BezierCurve(params float3[] points)
        {
            _points = new List<float3>(points);
        }

        public float3 this[int index]
        {
            get => _points[index];
            set => _points[index] = value;
        }

        public float3[] GetPointsInSegment(int segmentIndex)
        {
            segmentIndex = Math.Clamp(segmentIndex, 0, CountOfSegments - 1);
            return new[]
            {
                this[segmentIndex * 3],
                this[segmentIndex * 3 + 1],
                this[segmentIndex * 3 + 2],
                this[LoopIndex(segmentIndex * 3 + 3)]
            };
        }

        public BezierCurve GetCurve(int i)
        {
            var p = GetPointsInSegment(i);
            return new BezierCurve(p[0], p[1], p[2], p[3]);
        }

        private int LoopIndex(int i)
        {
            return (i + _points.Count) % _points.Count;
        }

        public float3 GetPoint(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = _points.Count - 4;
            }
            else
            {
                t = Math.Clamp(t, 0, 1) * CountOfSegments;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            return Eval(_points[i], _points[i + 1], _points[i + 2], _points[LoopIndex(i + 3)], t);
        }

        public float3 GetNormal(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = _points.Count - 4;
            }
            else
            {
                t = Math.Clamp(t, 0, 1) * CountOfSegments;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            return Normal(_points[i], _points[i + 1], _points[i + 2], _points[LoopIndex(i + 3)], t);
        }

        public static float3 Eval(float3 p0, float3 p1, float3 p2, float3 p3, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return (1 - t) * (1 - t) * (1 - t) * p0 +
                   3 * (1 - t) * (1 - t) * t * p1 +
                   3 * (1 - t) * t * t * p2 +
                   t * t * t * p3;
        }

        /// Returns a vector tangent to the point at time 't'
        /// This is the vector tangent to the curve at that point
        public static float3 EvaluateCurveDerivative(float3[] points, float t)
        {
            return EvaluateCurveDerivative(points[0], points[1], points[2], points[3], t);
        }

        /// Calculates the derivative of the curve at time 't'
        /// This is the vector tangent to the curve at that point
        public static float3 EvaluateCurveDerivative(float3 a1, float3 c1, float3 c2, float3 a2, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return 3 * (1 - t) * (1 - t) * (c1 - a1) + 6 * (1 - t) * t * (c2 - c1) + 3 * t * t * (a2 - c2);
        }

        public static float3 EvaluateCurveSecondDerivative(float3 a1, float3 c1, float3 c2, float3 a2, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return 6 * (1 - t) * (c2 - 2 * c1 + a1) + 6 * t * (a2 - 2 * c2 + c1);
        }

        /// Calculates the normal vector (vector perpendicular to the curve) at specified time
        public static float3 Normal(float3 a1, float3 c1, float3 c2, float3 a2, float t)
        {
            var tangent = EvaluateCurveDerivative(a1, c1, c2, a2, t);
            var nextTangent = EvaluateCurveSecondDerivative(a1, c1, c2, a2, t);
            var c = cross(nextTangent, tangent);
            return normalize(cross(c, tangent));
        }
    }
}