using System;
using System.Linq;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;
using float4x4 = GMath.float4x4;

namespace Renderer.Scene.Geometry
{
    public class GuitarGeometry : IDrawer
    {
        public enum ColorType
        {
            White = 0, Red
        }

        private ColorType _colorType = ColorType.White;
        private float4 color => _colorType switch
        {
            ColorType.White => float4(1, 1, 1, 1),
            ColorType.Red => float4(.68f, .25f, .25f, 1),
            _ => throw new ArgumentOutOfRangeException()
        };

        public float4x4 Draw(Raster render)
        {
            DrawGuitar(render);
            return new float4x4();
        }

        private void DrawGuitar(Raster render)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1)); // clear with color dark blue.

            const int N = 100000;
            // Create buffer with points to render
            var points = DrawerTools.RandomPositionInCylinderSurface(N);

            var viewMatrix = Transforms.LookAtLH(float3(5f, 4.6f, 2), float3.zero, float3.up);
            var projMatrix = Transforms.PerspectiveFovLH(pi_over_4,
                render.RenderTarget.Height / (float) render.RenderTarget.Width, 0.01f, 10);

            DrawGuitarBody(render, points, mul(viewMatrix, projMatrix));
        }

        private void DrawGuitarBody(Raster render, float3[] cylinderPoints, float4x4 transform)
        {
            SetColor(ColorType.White);
            TransformAndDrawCylinder(render, cylinderPoints, transform, float3.zero, float3(2.2f, .2f, 2.2f));
            
            // SetColor(ColorType.Red);
            TransformAndDrawCylinder(render, cylinderPoints.Take(cylinderPoints.Length / 2).ToArray(), transform,
                1.75f * float3(-1, 0, -1), float3(1.6f, .2f, 1.6f));

            SetColor(ColorType.Red);
            TransformAndDrawFretboard(render, cylinderPoints, transform, 1.75f * float3(-1, 0, -1),
                float3(.25f, .1f, 5f));
        }

        private void TransformAndDrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? rotation = null)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, rotation);
            DrawCylinder(render, cylinderPoints, desiredTransform);
        }

        private void DrawCylinder(Raster render, float3[] boxPoints, float4x4 transform)
        {
            var pointsToDraw = ApplyTransform(boxPoints, transform);
            render.DrawPoints(pointsToDraw, color);
        }
        
        private void TransformAndDrawFretboard(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? rotation = null)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, rotation);
            DrawFretboard(render, cylinderPoints, desiredTransform);
        }
        
        private void DrawFretboard(Raster render, float3[] boxPoints, float4x4 transform)
        {
            var transformingIntoBox = mul(float4x4(
                0.5f, 0, 0, 0,
                0, 0.5f, 0, 0,
                0, 0, 0.5f, 0,
                0.5f, 0.5f, 0.5f, 1
            ), transform);

            var pointsToDraw = ApplyTransform(boxPoints, transform);
            render.DrawPoints(pointsToDraw, color);
        }
        
        private float3[] ApplyTransform(float3[] points, float4x4 matrix)
        {
            var result = new float3[points.Length];

            // Transform points with a matrix
            // Linear transform in homogeneous coordinates
            for (var i = 0; i < points.Length; i++)
            {
                var h = float4(points[i], 1);
                h = mul(h, matrix);
                result[i] = h.xyz / h.w;
            }

            return result;
        }

        private void SetColor(ColorType colorType) => _colorType = colorType;

        private float4x4 GetDesiredTransform(float4x4 transform, float3? position = null, float3? scale = null,
            float3? rotation = null)
        {
            var desiredTransform = transform;

            if (position != null) desiredTransform = mul(Transforms.Translate(position.Value), desiredTransform);

            if (scale != null) desiredTransform = mul(Transforms.Scale(scale.Value), desiredTransform);

            if (rotation != null)
            {
            }

            return desiredTransform;
        }
    }
}