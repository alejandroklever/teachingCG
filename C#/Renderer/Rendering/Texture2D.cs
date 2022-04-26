using GMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static GMath.Gfx;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Rendering
{
    public class Texture2D
    {
        public readonly int Width;
        public readonly int Height;

        private float4[,] data;

        /// <summary>
        /// Creates an empty texture 2D with a specific size.
        /// </summary>
        public Texture2D(int width, int height)
        {
            Width = width;
            Height = height;
            data = new float4[height, width];
        }

        /// <summary>
        /// Gets or sets a value in the texture at specific position.
        /// </summary>
        public float4 this[int px, int py]
        {
            get { return data[py, px]; }
            set { data[py, px] = value; }
        }

        /// <summary>
        /// Writes a value in the texture at specific position.
        /// </summary>
        public void Write(int x, int y, float4 value)
        {
            data[y, x] = value;
        }

        /// <summary>
        /// Reads a value from a specific position.
        /// </summary>
        public float4 Read(int x, int y)
        {
            return data[y, x];
        }

        public void Save(string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(Width);
            binaryWriter.Write(Height);
            for (var py = 0; py < Height; py++)
            for (var px = 0; px < Width; px++)
            {
                binaryWriter.Write(data[py, px].x);
                binaryWriter.Write(data[py, px].y);
                binaryWriter.Write(data[py, px].z);
                binaryWriter.Write(data[py, px].w);
            }

            binaryWriter.Close();
        }

        private float4 PointSampleCoord(float2 uv, WrapMode mode, float4 border)
        {
            // Wrapping uv
            switch (mode)
            {
                case WrapMode.Border:
                    if (any(uv < 0) || any(uv >= 1))
                        return border;
                    break;
                case WrapMode.Clamp:
                    uv = clamp(uv, 0.0f, 0.9999f);
                    break;
                case WrapMode.Repeat:
                    uv = (uv % 1 + 1) % 1;
                    break;
            }

            uv *= float2(Width, Height);
            return data[(int) uv.y, (int) uv.x];
        }

        public float4 Sample(Sampler sampler, float2 uv)
        {
            switch (sampler.MinMagFilter)
            {
                case Filter.Point:
                    return PointSampleCoord(uv, sampler.Wrap, sampler.Border);
                case Filter.Linear:
                    var uv00 = uv + float2(-0.5f, -0.5f) / float2(Width, Height);
                    var uv01 = uv + float2(-0.5f, +0.5f) / float2(Width, Height);
                    var uv10 = uv + float2(+0.5f, -0.5f) / float2(Width, Height);
                    var uv11 = uv + float2(+0.5f, +0.5f) / float2(Width, Height);

                    var sample00 = PointSampleCoord(uv00, sampler.Wrap, sampler.Border);
                    var sample01 = PointSampleCoord(uv01, sampler.Wrap, sampler.Border);
                    var sample10 = PointSampleCoord(uv10, sampler.Wrap, sampler.Border);
                    var sample11 = PointSampleCoord(uv11, sampler.Wrap, sampler.Border);

                    var w = ((uv * float2(Width, Height) + 0.5f) % 1 + 1) % 1; // gets the weights for the cells.
                    return
                        sample00 * (1 - w.x) * (1 - w.y) +
                        sample01 * (1 - w.x) * w.y +
                        sample10 * w.x * (1 - w.y) +
                        sample11 * w.x * w.y;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public static Texture2D LoadFromFile(string path)
        {
            using (var image = Image.Load(path).CloneAs<Rgba32>())
            {
                var texture = new Texture2D(image.Width, image.Height);
                for (var i = 0; i < image.Height; i++)
                for (var j = 0; j < image.Width; j++)
                {
                    var color = image[j, i];
                    texture[j, i] = float4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
                }

                return texture;
            }
        }
    }

    public enum WrapMode
    {
        /// <summary>
        /// Outside values are a border constant
        /// </summary>
        Border,

        /// <summary>
        /// Outside values are map back to the interior
        /// </summary>
        Repeat,

        /// <summary>
        /// Outside values are clamped to the limits
        /// </summary>
        Clamp
    }

    public enum Filter
    {
        /// <summary>
        /// Nearest sample is assumed
        /// </summary>
        Point,

        /// <summary>
        /// A linear interpolation is assumed
        /// </summary>
        Linear
    }

    public struct Sampler
    {
        /// <summary>
        /// Gets or sets the wrap mode of this sampler
        /// </summary>
        public WrapMode Wrap;

        /// <summary>
        /// Gets or sets the interpolation mode for the minification and magnification reconstructions
        /// </summary>
        public Filter MinMagFilter;

        /// <summary>
        /// Gets or sets the interpolation between two consecutive mips.
        /// </summary>
        public Filter MipFilter;

        /// <summary>
        /// Gets or sets the border color used.
        /// </summary>
        public float4 Border;
    }
}
