using System;
using System.Collections.Generic;
using System.Text;
using GMath;
using static GMath.Gfx;

namespace Rendering
{
    /// <summary>
    /// Represents a rasterizer object allowing to draw primitives in a specific texture.
    /// </summary>
    public class Raster
    {
        /// <summary>
        /// Gets the specific texture this renderer is drawing at.
        /// </summary>
        public Texture2D RenderTarget { get; private set; }

        /// <summary>
        /// Initializes a rasterizer to use a specific texture to draw at.
        /// </summary>
        public Raster (Texture2D renderTarget)
        {
            this.RenderTarget = renderTarget;
        }

        /// <summary>
        /// Initializes a rasterizer to use a new texture to draw at with specific dimensions.
        /// </summary>
        public Raster(int width, int height) : this(new Texture2D(width, height))
        {
        }

        /// <summary>
        /// Clears the render target with a specific color
        /// </summary>
        public void ClearRT(float4 color)
        {
            for (int i = 0; i < RenderTarget.Height; i++)
                for (int j = 0; j < RenderTarget.Width; j++)
                    RenderTarget.Write(j, i, color);
        }

        /// <summary>
        /// Render a set of points. Points are assumed to be in Normalized Device Coordinates.
        /// I.e. visible points have x and y between -1 and 1, and z between 0 and 1.
        /// Corresponding colors are used for each point.
        /// </summary>
        public void DrawPoints(float3[] points, float4[] colors)
        {
            if (RenderTarget == null)
                return;

            for (int i = 0; i < points.Length; i++)
            {
                float3 pto = points[i];
                if (pto.x < -1 || pto.x >= 1 || pto.y <= -1 || pto.y > 1 || pto.z < 0 || pto.z >= 1)
                    continue; // clip no-visible point
                              // Convert to image coordinates (0,0) - (Width, Height)
                int px = (int)((pto.x * 0.5f + 0.5f) * RenderTarget.Width);
                int py = (int)((0.5f - pto.y * 0.5f) * RenderTarget.Height);

                RenderTarget.Write(px, py, colors[i]);
            }
        }

        /// <summary>
        /// Render a set of points. Points are assumed to be in Normalized Device Coordinates.
        /// I.e. visible points have x and y between -1 and 1, and z between 0 and 1.
        /// Color is used for every point.
        /// </summary>
        public void DrawPoints(float3[] points, float4 color)
        {
            float4[] colors = new float4[points.Length];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = color;
            DrawPoints(points, colors);
        }

        /// <summary>
        /// Render a set of points. Points are assumed to be in Normalized Device Coordinates.
        /// I.e. visible points have x and y between -1 and 1, and z between 0 and 1.
        /// White color is used for every point.
        /// </summary>
        public void DrawPoints(float3[] points)
        {
            DrawPoints(points, float4(1, 1, 1, 1));
        }
    }
}
