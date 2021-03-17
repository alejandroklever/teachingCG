using GMath;
using Rendering;

namespace Renderer.Scene.Geometry
{
    public interface IDrawer
    {
        public float4x4 Draw(Raster render);
    }
}