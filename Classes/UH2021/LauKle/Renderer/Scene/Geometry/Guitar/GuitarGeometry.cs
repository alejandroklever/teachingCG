using System;
using System.Collections.Generic;
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
            White = 0,
            Red,
            Blue,
            Black
        }

        public enum CamPosition
        {

            Free,
            FromUp,
            FromRight,
            FromLeft,
            FromBottom,
        }

        private ColorType _colorType = ColorType.White;
        private float4 Color => _colorType switch
        {
            ColorType.White => float4(1, 1, 1, 1),
            ColorType.Red => float4(.68f, .25f, .25f, 1),
            ColorType.Blue => float4(.25f, .25f, .68f, 1),
            ColorType.Black => float4(.24f, .24f, .24f, 1),
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


            var camPos = CamPosition.Free;
            var cameraPosition = camPos switch
            {
               CamPosition.Free => float3(5f, 4.6f, 2),
               CamPosition.FromUp => float3(0f, 12f, -1f),
               CamPosition.FromBottom => float3(0f, 1f, -5f),
               CamPosition.FromRight => float3(8f, 0f, 0f),
               CamPosition.FromLeft => float3(-8f, 1f, 0f),
            };

            var viewMatrix = Transforms.LookAtLH(cameraPosition, float3.zero, float3.up);
            var projMatrix = Transforms.PerspectiveFovLH(pi_over_4,
                render.RenderTarget.Height / (float) render.RenderTarget.Width, 0.01f, 20);

            var transform = mul(viewMatrix, projMatrix);
            transform = mul(Transforms.Translate(-1 * float3(5f, 9.2f, 2)), transform);
            transform = mul(Transforms.RotateXGrad(120), transform);
            transform = mul(Transforms.RotateYGrad(-40), transform);
            transform = mul(Transforms.RotateZGrad(150), transform);

            const int n = 100000;
            var points = DrawerTools.RandomPositionInCylinderSurface(n);


            SetColor(ColorType.Red);
            // Create the bottom part of the guitar body
            var bottomCylinderScale = float3(2.2f, .2f, 2f);
            var bottomCylinderPosition = float3.zero;
            var bottomCylinderRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, bottomCylinderPosition, bottomCylinderScale,
                bottomCylinderRotation, useGrad: true);

            // Create the top part of the guitar body
            var (x, _, z) =  .8f * bottomCylinderScale;
            var topCylinderScale = float3(x, .2f, z);
            var topCylinderPosition =  .5f * bottomCylinderScale.z * float3.forward;
            var topCylinderRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, topCylinderPosition, topCylinderScale,
                topCylinderRotation, useGrad: true);

            // Create potentiometer
            SetColor(ColorType.White);
            var potentiometerScale = float3(.2f, .1f, .2f);
            var potentiometerPosition = float3(.5f, .1f, 0f);
            var potentiometerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
                potentiometerRotation, useGrad: true);

            potentiometerScale = float3(.2f, .1f, .2f);
            potentiometerPosition = float3(.8f, .1f, .25f);
            potentiometerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
                potentiometerRotation, useGrad: true);

            potentiometerScale = float3(.2f, .1f, .2f);
            potentiometerPosition = float3(.5f, .1f, -.35f);
            potentiometerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
                potentiometerRotation, useGrad: true);

            potentiometerScale = float3(.2f, .1f, .2f);
            potentiometerPosition = float3(.8f, .1f, -.1f);
            potentiometerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
                potentiometerRotation, useGrad: true);

            // Create Switch
            var switchScale = float3(.1f, .15f, .1f);
            var switchPosition = float3(topCylinderPosition.x - .25f * topCylinderScale.x, switchScale.y,
                topCylinderPosition.z + .25f * topCylinderScale.z);
            var switchRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, switchPosition, switchScale, switchRotation,
                useGrad: true);

            points = DrawerTools.RandomPositionsInBoxSurface(n);

            // Create Humbucker
            SetColor(ColorType.White);
            var humbuckerScale = float3(.5f, .1f, .25f);
            var humbuckerPosition = float3(0f, .1f, .4f);
            var humbuckerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, humbuckerPosition, humbuckerScale,
                humbuckerRotation, useGrad: true);

            (_, _, z) = topCylinderPosition;
            humbuckerScale = float3(.5f, .1f, .25f);
            humbuckerPosition = float3(0f, .1f, z + .05f * bottomCylinderScale.z);
            humbuckerRotation = float3.zero;
            TransformAndDrawCylinder(render, points, transform, humbuckerPosition, humbuckerScale,
                humbuckerRotation, useGrad: true);

            // Create FretBoard
            SetColor(ColorType.White);
            (_, _, z) = topCylinderPosition;
            var fretBoardScale = float3(.3f, .1f, 2.5f);
            var fretBoardPosition = (z + .25f * bottomCylinderScale.z + .4f * fretBoardScale.z) * float3.forward +
                                    fretBoardScale.y * float3.up;
            var fretBoardRotation = float3(-.5f, 0, 0);
            TransformAndDrawFretboard(render, points, transform, fretBoardPosition, fretBoardScale, fretBoardRotation,
                useGrad: true);

            // Create Head
            SetColor(ColorType.Black);
            var headScale = float3(.4f, .1f, .7f);
            var headPosition = (fretBoardPosition.z + .5f * (fretBoardScale.z +  .9f * headScale.z)) * float3.forward +
                .5f * headScale.y * float3.up;
            var headRotation = float3(-6f, 0, 0);
            TransformAndDrawHead(render, points, transform, headPosition, headScale, headRotation, useGrad: true);
        }

        private float4x4 TransformAndDrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
            float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
                rotDirection, angle, useGrad);
            DrawCylinder(render, cylinderPoints, desiredTransform);
            return desiredTransform;
        }

        private void DrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform)
        {
            var scaledTransform = mul(float4x4(
                0.5f, 0, 0, 0,
                0, 0.5f, 0, 0,
                0, 0, 0.5f, 0,
                0, 0, 0, 1
            ), transform);
            
            var pointsToDraw = ApplyTransform(cylinderPoints, scaledTransform);
            render.DrawPoints(pointsToDraw, Color);
        }

        private float4x4 TransformAndDrawFretboard(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
            float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
                rotDirection, angle, useGrad);
            DrawFretboard(render, cylinderPoints, desiredTransform);
            return desiredTransform;
        }

        private void DrawFretboard(Raster render, float3[] boxPoints, float4x4 transform)
        {
            var scaledTransform = mul(float4x4(
                0.5f, 0, 0, 0,
                0, 0.5f, 0, 0,
                0, 0, 0.5f, 0,
                0, 0, 0, 1
            ), transform);

            var pointsToDraw = ApplyTransform(boxPoints, scaledTransform);
            render.DrawPoints(pointsToDraw, Color);
        }

        private float4x4 TransformAndDrawHead(Raster render, float3[] cylinderPoints, float4x4 transform,
            float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
            float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            var desiredTransform = GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
                rotDirection, angle, useGrad);
            DrawHead(render, cylinderPoints, desiredTransform);
            return desiredTransform;
        }

        private void DrawHead(Raster render, float3[] boxPoints, float4x4 transform)
        {
            var scaledTransform = mul(float4x4(
                0.5f, 0, 0, 0,
                0, 0.5f, 0, 0,
                0, 0, 0.5f, 0,
                0, 0, 0, 1
            ), transform);

            var pointsToDraw = ApplyTransform(boxPoints, scaledTransform);
            render.DrawPoints(pointsToDraw, Color);
        }

        public static float3[] ApplyTransform(IReadOnlyList<float3> points, float4x4 matrix)
        {
            var result = new float3[points.Count];

            // Transform points with a matrix
            // Linear transform in homogeneous coordinates
            for (var i = 0; i < points.Count; i++)
            {
                var h = float4(points[i], 1);
                h = mul(h, matrix);
                result[i] = h.xyz / h.w;
            }

            return result;
        }

        private void SetColor(ColorType colorType) => _colorType = colorType;

        private static float4x4 GetDesiredTransform(float4x4 transform, float3? position = null, float3? scale = null,
             float3? eulerRotation = null, float3? rotCenter = null, float3? rotDirection = null, float? angle = null, bool useGrad = false)
        {
            /*
             * The order of the operations is important
             * First we need to Translate, then Rotate, then Scale
            */
            var desiredTransform = transform;

            if (position != null)
                desiredTransform = mul(Transforms.Translate(position.Value), desiredTransform);

            Func<float3, float3, float, float4x4> rotateRespectTo = Transforms.RotateRespectTo;
            Func<float, float3, float4x4> rotate = Transforms.Rotate;
            Func<float, float4x4> rotateX = Transforms.RotateX;
            Func<float, float4x4> rotateY = Transforms.RotateY;
            Func<float, float4x4> rotateZ = Transforms.RotateZ;

            if (useGrad)
            {
                rotateRespectTo = Transforms.RotateRespectTo;
                rotate = Transforms.RotateGrad;
                rotateX = Transforms.RotateXGrad;
                rotateY = Transforms.RotateYGrad;
                rotateZ = Transforms.RotateZGrad;
            }

            if (eulerRotation != null)
            {
                desiredTransform = mul(rotateX(eulerRotation.Value.x), desiredTransform);
                desiredTransform = mul(rotateY(eulerRotation.Value.y), desiredTransform);
                desiredTransform = mul(rotateZ(eulerRotation.Value.z), desiredTransform);
            }

            else if (rotCenter != null && rotDirection != null && angle != null)
                desiredTransform = mul(rotateRespectTo(rotCenter.Value, rotDirection.Value, angle.Value), desiredTransform);

            else if (rotDirection != null && angle != null)
                desiredTransform = mul(rotate(angle.Value, rotDirection.Value), desiredTransform);

            if (scale != null)
                desiredTransform = mul(Transforms.Scale(scale.Value), desiredTransform);

            return desiredTransform;
        }
    }
}