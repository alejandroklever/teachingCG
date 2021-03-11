using GMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            this.Width = width;
            this.Height = height;
            this.data = new float4[height, width];
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
            data[y , x] = value;
        }

        /// <summary>
        /// Reads a value from a specific position.
        /// </summary>
        public float4 Read(int x, int y)
        {
            return data[y , x];
        }

        public void Save(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(Width);
            binaryWriter.Write(Height);
            for(int py = 0; py < Height; py++)
                for (int px = 0; px < Width; px++)
                {
                    binaryWriter.Write(data[py, px].x);
                    binaryWriter.Write(data[py, px].y);
                    binaryWriter.Write(data[py, px].z);
                    binaryWriter.Write(data[py, px].w);
                }
            binaryWriter.Close();
        }
    }
}
