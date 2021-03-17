using System;
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
    }

    public enum DrawerType
    {
        Guitar,
        Amp
    }
}