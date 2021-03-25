using System;
using GMath;
using static GMath.Gfx;
using Renderer.Scene.Geometry;

namespace Renderer.Scene
{
    public static class DrawerTools
    {
        public static IDrawer GetDrawer(DrawerType type)
        {
            return type switch
            {
                DrawerType.Guitar => new GuitarGeometry(),
                DrawerType.Amp => new AmpGeometry(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
        
        public static float3[] RandomPositionsInBoxSurface(int n)
        {
            var points = new float3[n];

            for (var i = 0; i < n; i++)
                points[i] = randomInBox();

            return points;
        }
        
        public static float3[] RandomPositionInCylinderSurface(int n)
        {
            var points = new float3[n];

            for (var i = 0; i < n; i++)
                points[i] = randomInCylinder();

            return points;
        }
        
        private static float3[] ApplyTransform(float3[] points, float4x4 matrix)
        {
            float3[] result = new float3[points.Length];

            // Transform points with a matrix
            // Linear transform in homogeneous coordinates
            for (int i = 0; i < points.Length; i++)
            {
                float4 h = float4(points[i], 1);
                h = mul(h, matrix);
                result[i] = h.xyz / h.w;
            }

            return result;
        }
    }

    public enum DrawerType
    {
        Guitar,
        Amp
    }
}