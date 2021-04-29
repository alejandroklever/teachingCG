using System;
using System.Collections.Generic;
using GMath;
using Rendering;
using static GMath.Gfx;

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

        private int LoopIndex(int i)
        {
            return (i + _points.Count) % _points.Count;
        }
        
        public static float3 Eval(float3 p0, float3 p1, float3 p2, float3 p3, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return (1 - t) * (1 - t) * (1 - t) * p0 +
                   3 * (1 - t) * (1 - t) * t * p1 +
                   3 * (1 - t) * t * t * p2 +
                   t * t * t * p3;
        }

        public float3 GetPoint(float t)
        {
            int i;
            if (t >= 1f) {
                t = 1f;
                i = _points.Count - 4;
            }
            else {
                t = Math.Clamp(t, 0, 1) * CountOfSegments;
                i = (int)t;
                t -= i;
                i *= 3;
            }
            return Eval(_points[i], _points[i + 1], _points[i + 2], _points[i + 3], t);
        }
    }
}