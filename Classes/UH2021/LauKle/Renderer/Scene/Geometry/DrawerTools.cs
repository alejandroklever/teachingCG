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
                points[i] = randomInCylinder();// randomInBox();

            return points;
        }
    }

    public enum DrawerType
    {
        Guitar,
        Amp
    }
}