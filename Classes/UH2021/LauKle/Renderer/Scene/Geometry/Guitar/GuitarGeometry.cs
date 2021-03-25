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
        private float4 Color => _colorType switch
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

            var viewMatrix = Transforms.LookAtLH(float3(0f, 8f, -2f), float3.zero, float3.up);
            var projMatrix = Transforms.PerspectiveFovLH(pi_over_4,
                render.RenderTarget.Height / (float) render.RenderTarget.Width, 0.01f, 20);

            
            var transform = mul(viewMatrix, projMatrix);
            transform = mul(Transforms.Translate(0, 0, -4f), transform);
            
            DrawGuitarBody(render, points, transform);

            
            SetColor(ColorType.Red);
            var boxPoints = DrawerTools.RandomPositionsInBoxSurface(N);


            var position = 3.5f * float3.forward + float3(-.3f, 0, 0f); // 1.75f * float3(-1, 0, -1) + float3(-.5f, 0, .5f);
            var scale = float3(.6f, .4f, 5f);
            var rotation = float3(0, 0, 0);
            TransformAndDrawFretboard(render, boxPoints, transform, position, scale, rotation, useGrad: true);
        }

        private void DrawGuitarBody(Raster render, float3[] cylinderPoints, float4x4 transform)
        {
            TransformAndDrawCylinder(render, cylinderPoints, transform, float3.zero, float3(2.2f, .2f, 2.2f));
            
            TransformAndDrawCylinder(render, cylinderPoints.Take(cylinderPoints.Length / 2).ToArray(), transform,
                2.8f * float3.forward, float3(1.6f, .2f, 1.6f));
        }

        private void TransformAndDrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
            float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
                rotDirection, angle, useGrad);
            DrawCylinder(render, cylinderPoints, desiredTransform);
        }

        private void DrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform)
        {
            var pointsToDraw = ApplyTransform(cylinderPoints, transform);
            render.DrawPoints(pointsToDraw, Color);
        }

        private void TransformAndDrawFretboard(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
            float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
                rotDirection, angle, useGrad);
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

            var pointsToDraw = ApplyTransform(boxPoints, transformingIntoBox);
            render.DrawPoints(pointsToDraw, Color);
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
             float3? eulerRotation = null, float3? rotCenter = null, float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = transform;

            if (position != null) 
                desiredTransform = mul(Transforms.Translate(position.Value), desiredTransform);

            if (scale != null) 
                desiredTransform = mul(Transforms.Scale(scale.Value), desiredTransform);

            
            Func<float3, float3, float, float4x4> rotateRespectTo = Transforms.RotateRespectTo;
            Func<float, float3, float4x4> rotate = Transforms.Rotate;
            Func<float,  float4x4> rotateX = Transforms.RotateX;
            Func<float,  float4x4> rotateY = Transforms.RotateY;
            Func<float,  float4x4> rotateZ = Transforms.RotateZ;
            
            if (useGrad)
            {
                rotateRespectTo = Transforms.RotateRespectTo;
                rotate = Transforms.RotateGrad;
                rotateX = Transforms.RotateXGrad;
                rotateY = Transforms.RotateYGrad;
                rotateZ = Transforms.RotateZGrad;
            }

            if(rotCenter != null && rotDirection != null && angle != null)
                desiredTransform = mul(rotateRespectTo(rotCenter.Value, rotDirection.Value, angle.Value), desiredTransform);
            
            if (rotDirection != null && angle != null)
                desiredTransform = mul(rotate(angle.Value, rotDirection.Value), desiredTransform);

            if (eulerRotation != null)
            {
                desiredTransform = mul(rotateX(eulerRotation.Value.x), desiredTransform);
                desiredTransform = mul(rotateY(eulerRotation.Value.y), desiredTransform);
                desiredTransform = mul(rotateZ(eulerRotation.Value.z), desiredTransform);
            }
            
            return desiredTransform;
        }
    }
}