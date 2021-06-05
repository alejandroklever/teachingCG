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
    public class Raster<V, P> where V : struct ,IVertex<V> where P : struct, IProjectedVertex<P>
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
            if (renderTarget == null)
                throw new ArgumentNullException("Expected to receive a render target to draw to.");

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
        /// Gets or sets the vertex shader used by this rasterizer to draw primitives
        /// If null, a defacto Homogenous coordinate is assume from vertex position (x,y,z,1);
        /// </summary>
        public Func<V, P> VertexShader { get; set; }

        /// <summary>
        /// Gets or sets the pixel shader used by this rasterizer to shade every pixel drawn
        /// If null, a defacto white color is used to draw a pixel.
        /// </summary>
        public Func<P, float4> PixelShader { get; set; }

        /// <summary>
        /// Draws a primitive using a specific buffer of vertices.
        /// </summary>
        /// <param name="topology">Indicates how to interpretate vertices.</param>
        public void DrawPrimitive(Topology topology, V[] vertices)
        {
            switch (topology)
            {
                case Topology.Points:
                    for (int i = 0; i < vertices.Length; i++)
                        DrawPoint(vertices[i]);
                    break;
                case Topology.Lines:
                    for (int i = 0; i < vertices.Length / 2; i++)
                        DrawLine(vertices[i * 2 + 0], vertices[i * 2 + 1]);
                    break;
                default:
                    throw new NotSupportedException("Uff, too much work left...!");
            }
        }

        /// <summary>
        /// Draws a primitive using a specific buffer of vertices and indices.
        /// </summary>
        /// <param name="topology">Indicates how to interpretate vertices.</param>
        public void DrawIndexedPrimitive(Topology topology, V[] vertices, int[] indices)
        {
            switch (topology)
            {
                case Topology.Points:
                    for (int i = 0; i < indices.Length; i++)
                        DrawPoint(vertices[indices[i]]);
                    break;
                case Topology.Lines:
                    for (int i = 0; i < indices.Length / 2; i++)
                        DrawLine(vertices[indices[i * 2 + 0]], vertices[indices[i * 2 + 1]]);
                    break;
                default:
                    throw new NotSupportedException("Uff, too much work left...!");
            }
        }

        /// <summary>
        /// Draws a mesh.
        /// </summary>
        public void DrawMesh(Mesh<V> mesh)
        {
            DrawIndexedPrimitive(mesh.Topology, mesh.Vertices, mesh.Indices);
        }

        #region Private Tools for Clipling, Rasterization and Updating Buffers

        void FromNDCToScreen(float4 pPosition, out float px, out float py)
        {
            float3 hPosition = pPosition.xyz / pPosition.w;
            px = (hPosition.x * 0.5f + 0.5f) * RenderTarget.Width;
            py = (0.5f - 0.5f * hPosition.y) * RenderTarget.Height;
        }

        void UpdateBuffers(float px, float py, P pixelInput)
        {
            // Place pixel in screen before pixel shader
            pixelInput.Homogeneous = float4(px, py, pixelInput.Homogeneous.z, pixelInput.Homogeneous.w);
            RenderTarget[Math.Min(RenderTarget.Width - 1, (int)px), Math.Min(RenderTarget.Height - 1, (int)py)] = PerformPixelShader(pixelInput);
        }

        void DrawPoint(V vertex) 
        {
            P pixelInput = PerformVertexShader(vertex);

            float4 pPosition = pixelInput.Homogeneous;
            if (pPosition.x < -pPosition.w || pPosition.x >= pPosition.w || pPosition.y < -pPosition.w || pPosition.y >= pPosition.w || pPosition.z < 0 || pPosition.z >= pPosition.w)
                return;

            float px, py;
            FromNDCToScreen(pPosition, out px, out py);

            UpdateBuffers(px, py, pixelInput);
        }

        bool ClipSegmentHP(float4 P, float4 N, float4 pV1, float4 pV2, out float alpha1, out float alpha2) 
        {
            alpha1 = 0; alpha2 = 1;
            float d1 = dot(pV1 - P, N);
            float d2 = dot(pV2 - P, N);
            if (d1 < 0 && d2 < 0)
                return false;
            if (d1 >= 0 && d2 >= 0)
                return true;
            float alpha = -d1 / (d2 - d1);
            if (d1 < 0 && d2 >= 0)
                alpha1 = alpha;
            else
                alpha2 = alpha;
            return true;
        }

        bool ClipSegmentHP(ref P p1, ref P p2, ref float4 pV1, ref float4 pV2)
        {
            float alpha1, alpha2;

            // Clip to enforce z > 0 (0x +0y + 1z + 0w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(0, 0, 1, 0), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            // Clip to enforce -z > -w (0x +0y -1z 1w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(0, 0, -1, 1), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            // Clip to enforce x > -w (1x +0y + 0z + 1w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(1, 0, 0, 1), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            // Clip to enforce -x > -w (-1x +0y + 0z + 1w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(-1, 0, 0, 1), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            // Clip to enforce y > -w (0x +1y + 0z + 1w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(0, 1, 0, 1), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            // Clip to enforce -y > -w (0x -1y + 0z + 1w > 0)
            if (!ClipSegmentHP(float4(0, 0, 0, 0), float4(0, -1, 0, 1), pV1, pV2, out alpha1, out alpha2))
                return false;
            p1 = p1.Mul(1 - alpha1).Add(p2.Mul(alpha1));
            p2 = p1.Mul(1 - alpha2).Add(p2.Mul(alpha2));
            pV1 = lerp(pV1, pV2, alpha1);
            pV2 = lerp(pV1, pV2, alpha2);

            return true;
        }

        P PerformVertexShader (V v)
        {
            if (VertexShader == null)
            {
                P p = new P();
                p.Homogeneous = float4(v.Position, 1);
                return p;
            }

            return VertexShader(v);
        }

        float4 PerformPixelShader(P p)
        {
            if (PixelShader == null)
                return float4(1, 1, 1, 1);
            return PixelShader(p);
        }

        void DrawLine(V v1, V v2)
        {
            P p1 = PerformVertexShader(v1);
            P p2 = PerformVertexShader(v2);

            float4 pV1 = p1.Homogeneous; // Projected into homogeneous coordinates
            float4 pV2 = p2.Homogeneous; // Projected into homogeneous coordinates

            // Clip vertices against box in homogeneous coordinates!
            if (!ClipSegmentHP(ref p1, ref p2, ref pV1, ref pV2))
                return; // Clip every point in the segment

            // TODO: Implement Breshenham or Middle point approaches!

            // Transform to screen
            float x1, y1, x2, y2;
            FromNDCToScreen(pV1, out x1, out y1);
            FromNDCToScreen(pV2, out x2, out y2);

            int steps = (int)max(abs(x1 - x2), abs(y1 - y2)) * 2 + 1; // Is not the best, just to grant there is at least one sample for each pixel.

            for (int i=0; i<=steps; i++)
            {
                float alpha = i / (float)steps; // Not using Perspective Correction! Careful!
                P interpolatedValue = p1.Mul(1 - alpha).Add(p2.Mul(alpha));
                float x = lerp(x1, x2, alpha);
                float y = lerp(y1, y2, alpha);
                UpdateBuffers(x, y, interpolatedValue);
            }
        }

        #endregion
    }
}
