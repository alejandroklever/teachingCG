// using System;
// using System.Collections.Generic;
// using System.Linq;
// using GMath;
// using Rendering;
// using static GMath.Gfx;
// using float3 = GMath.float3;
// using float4x4 = GMath.float4x4;
//
// namespace Renderer.Scene.Geometry
// {
//     public class GuitarGeometry : IDrawer
//     {
//         public enum ColorType
//         {
//             White = 0,
//             Red,
//             Blue,
//             CreamGold,
//             Gray,
//             Brown,
//             Yellow,
//             Black
//         }
//
//         public enum CamPosition
//         {
//
//             Free,
//             FromUp,
//             FromRight,
//             FromLeft,
//             FromBottom,
//         }
//
//         private ColorType _colorType = ColorType.White;
//         private float4 Color => _colorType switch
//         {
//             ColorType.White => float4(1, 1, 1, 1),
//             ColorType.Red => float4(.68f, .25f, .25f, 1),
//             ColorType.Blue => float4(.25f, .25f, .68f, 1),
//             ColorType.CreamGold => float4(1f, .99f, .82f, 1),
//             ColorType.Gray => float4(.75f, .75f, .75f, 1),
//             ColorType.Brown => float4(.28f, .24f, .2f, 1),
//             ColorType.Yellow => float4(1f, .65f, 0f, 1),
//             ColorType.Black => float4(.24f, .24f, .24f, 1),
//             _ => throw new ArgumentOutOfRangeException()
//         };
//
//         public float4x4 Draw(Raster render)
//         {
//             DrawGuitar(render);
//             return new float4x4();
//         }
//
//         private void DrawGuitar(Raster render)
//         {
//             render.ClearRT(float4(0, 0, 0.2f, 1)); // clear with color dark blue.
//
//
//             var camPos = CamPosition.Free;
//             var cameraPosition = camPos switch
//             {
//                CamPosition.Free => float3(5f, 4.6f, 2),
//                CamPosition.FromUp => float3(0f, 12f, -1f),
//                CamPosition.FromBottom => float3(0f, 0f, -5f),
//                CamPosition.FromRight => float3(12f, 0f, 0f),
//                CamPosition.FromLeft => float3(-8f, 0f, 0f),
//             };
//
//             var viewMatrix = Transforms.LookAtLH(cameraPosition, float3.zero, float3.up);
//             var projMatrix = Transforms.PerspectiveFovLH(pi_over_4,
//                 render.RenderTarget.Height / (float) render.RenderTarget.Width, 0.01f, 20);
//
//             var transform = mul(viewMatrix, projMatrix);
//             
//             if (camPos == CamPosition.Free)
//                 transform = Transforms.GetDesiredTransform(transform, -1 * float3(1f, 4.6f, 1f),
//                     eulerRotation: float3(110, -45, 150), useGrad: true);
//
//             const int n = 100000;
//
//
//             var bottomCylinderScale = float3(2.2f, .2f, 2f);
//             var bottomCylinderPosition = float3.zero;
//
//
//             var points = DrawerTools.RandomPositionsInBoxSurface(n);
//             // Create Floor
//             SetColor(ColorType.Yellow);
//             var floorScale = float3(7, .001f, 10);
//             var floorPosition =  1.6f * float3.back + 2 * float3.right + float3.down;
//             var floorRotation = float3(90, 45, 0);
//             TransformAndDrawBox(render, points, transform, floorPosition, floorScale, floorRotation, useGrad: true);
//
//
//             points = DrawerTools.RandomPositionsInBoxSurface(n);
//             // Create Amp
//             SetColor(ColorType.Black);
//             var ampScale = float3(4f, 2f, 3f);
//             var ampPosition = float3(.25f * ampScale.x, -.5f * (ampScale.y + bottomCylinderScale.y),
//                 bottomCylinderPosition.z + .25f * bottomCylinderScale.z);
//             var ampRotation = float3.zero;
//             TransformAndDrawBox(render, points, transform, ampPosition, ampScale, ampRotation, useGrad: true);
//
//             SetColor(ColorType.CreamGold);
//             var ampControlsScale = float3(3.2f, .0001f, .3f);
//             var ampControlsPosition = float3(.25f * ampControlsScale.x,
//                 -.5f * (ampControlsScale.y + bottomCylinderScale.y),
//                 bottomCylinderPosition.z + .25f * bottomCylinderScale.z + .5f * ampScale.z - .4f);
//             var ampControlsRotation = float3.zero;
//             TransformAndDrawBox(render, points, transform, ampControlsPosition, ampControlsScale, ampControlsRotation, useGrad: true);
//
//             SetColor(ColorType.Gray);
//             var ampCoverScale = float3(3.2f, .0001f, 2.2f);
//             var ampCoverPosition = float3(.25f * ampControlsScale.x,
//                 -.5f * (ampControlsScale.y + bottomCylinderScale.y),
//                 bottomCylinderPosition.z + .2f);
//             var ampCoverRotation = float3.zero;
//             TransformAndDrawBox(render, points, transform, ampCoverPosition, ampCoverScale, ampCoverRotation, useGrad: true);
//
//             points = DrawerTools.RandomPositionInCylinderSurface(n);
//
//             // Draw Amp Potentiometer
//             SetColor(ColorType.Black);
//
//             for (var i = 0; i < 5; i++)
//             {
//                 var controlScale = float3(.2f, .1f, .2f);
//                 var controlPosition = float3(ampControlsPosition.x + i * .3f, controlScale.y, ampControlsPosition.z);
//                 var controlRotation = float3.zero;
//                 TransformAndDrawCylinder(render, points, transform, controlPosition, controlScale,
//                     controlRotation, useGrad: true);
//             }
//
//             for (var i = 1; i < 5; i++)
//             {
//                 var controlScale = float3(.2f, .1f, .2f);
//                 var controlPosition = float3(ampControlsPosition.x - i * .3f, controlScale.y, ampControlsPosition.z);
//                 var controlRotation = float3.zero;
//                 TransformAndDrawCylinder(render, points, transform, controlPosition, controlScale,
//                     controlRotation, useGrad: true);
//             }
//
//
//             // Create Guitar
//
//             points = DrawerTools.RandomPositionInCylinderSurface(n);
//
//             var guitarGlobalPos = float3(0, .3f, -.35f);
//             var guitarGlobalRot = float3(-10, 5, 0);
//             transform = Transforms.GetDesiredTransform(transform, guitarGlobalPos, eulerRotation: guitarGlobalRot,
//                 useGrad: true);
//
//             SetColor(ColorType.Red);
//             // Create the bottom part of the guitar body
//             bottomCylinderScale = float3(2.2f, .2f, 2f);
//             bottomCylinderPosition = float3.zero;
//             var bottomCylinderRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, bottomCylinderPosition, bottomCylinderScale,
//                 bottomCylinderRotation, useGrad: true);
//
//             // Create the top part of the guitar body
//             var (x, _, z) =  .8f * bottomCylinderScale;
//             var topCylinderScale = float3(x, .2f, z);
//             var topCylinderPosition =  .5f * bottomCylinderScale.z * float3.forward;
//             var topCylinderRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, topCylinderPosition, topCylinderScale,
//                 topCylinderRotation, useGrad: true);
//
//             // Create potentiometer
//             SetColor(ColorType.White);
//             var potentiometerScale = float3(.2f, .1f, .2f);
//             var potentiometerPosition = float3(.5f, .1f, 0f);
//             var potentiometerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
//                 potentiometerRotation, useGrad: true);
//
//             potentiometerScale = float3(.2f, .1f, .2f);
//             potentiometerPosition = float3(.8f, .1f, .25f);
//             potentiometerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
//                 potentiometerRotation, useGrad: true);
//
//             potentiometerScale = float3(.2f, .1f, .2f);
//             potentiometerPosition = float3(.5f, .1f, -.35f);
//             potentiometerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
//                 potentiometerRotation, useGrad: true);
//
//             potentiometerScale = float3(.2f, .1f, .2f);
//             potentiometerPosition = float3(.8f, .1f, -.1f);
//             potentiometerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, potentiometerPosition, potentiometerScale,
//                 potentiometerRotation, useGrad: true);
//
//             // Create Switch
//             var switchScale = float3(.1f, .15f, .1f);
//             var switchPosition = float3(topCylinderPosition.x - .25f * topCylinderScale.x, switchScale.y,
//                 topCylinderPosition.z + .25f * topCylinderScale.z);
//             var switchRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, switchPosition, switchScale, switchRotation,
//                 useGrad: true);
//
//             points = DrawerTools.RandomPositionsInBoxSurface(n);
//
//             // Create Bridge Humbucker
//             SetColor(ColorType.White);
//             var humbuckerScale = float3(.5f, .1f, .25f);
//             var humbuckerPosition = float3(0f, .1f, .4f);
//             var humbuckerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, humbuckerPosition, humbuckerScale,
//                 humbuckerRotation, useGrad: true);
//
//             // Create Fretboard Humbucker
//             (_, _, z) = topCylinderPosition;
//             humbuckerScale = float3(.5f, .1f, .25f);
//             humbuckerPosition = float3(0f, .1f, z + .05f * bottomCylinderScale.z);
//             humbuckerRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, humbuckerPosition, humbuckerScale,
//                 humbuckerRotation, useGrad: true);
//
//             // Create Bridge
//             var bridgeScale = float3(.75f, .1f, .15f);
//             var bridgePosition = float3(0f, .1f, 0f);
//             var bridgeRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, bridgePosition, bridgeScale,
//                 bridgeRotation, useGrad: true);
//
//
//             bridgeScale = float3(.6f, .1f, .05f);
//             bridgePosition = float3(0f, .1f, .2f);
//             bridgeRotation = float3.zero;
//             TransformAndDrawCylinder(render, points, transform, bridgePosition, bridgeScale,
//                 bridgeRotation, useGrad: true);
//
//
//             // Create FretBoard
//             SetColor(ColorType.Brown);
//             (_, _, z) = topCylinderPosition;
//             var fretBoardScale = float3(.3f, .1f, 2.5f);
//             var fretBoardPosition = (z + .25f * bottomCylinderScale.z + .4f * fretBoardScale.z) * float3.forward +
//                                     fretBoardScale.y * float3.up;
//             var fretBoardRotation = float3(-.5f, 0, 0);
//             TransformAndDrawBox(render, points, transform, fretBoardPosition, fretBoardScale, fretBoardRotation,
//                 useGrad: true);
//
//             // Create Head
//             SetColor(ColorType.Black);
//             var headScale = float3(.4f, .1f, .5f);
//             var headPosition = (fretBoardPosition.z + .5f * (fretBoardScale.z + .9f * headScale.z)) * float3.forward +
//                 .5f * headScale.y * float3.up;
//             var headRotation = float3(-6f, 0, 0);
//             TransformAndDrawBox(render, points, transform, headPosition, headScale, headRotation, useGrad: true);
//
//             Transforms.GetDesiredTransform(transform, -1 * guitarGlobalPos, eulerRotation: -1 * guitarGlobalRot, useGrad: true);
//         }
//
//         private float4x4 TransformAndDrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform,
//             float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
//             float3? rotDirection = null, float? angle = null, bool useGrad = false)
//         {
//             var desiredTransform = Transforms.GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
//                 rotDirection, angle, useGrad);
//             DrawCylinder(render, cylinderPoints, desiredTransform);
//             return desiredTransform;
//         }
//
//         private void DrawCylinder(Raster render, float3[] cylinderPoints, float4x4 transform)
//         {
//             var scaledTransform = mul(float4x4(
//                 0.5f, 0, 0, 0,
//                 0, 0.5f, 0, 0,
//                 0, 0, 0.5f, 0,
//                 0, 0, 0, 1
//             ), transform);
//             
//             var pointsToDraw = Transforms.ApplyTransform(cylinderPoints, scaledTransform);
//             render.DrawPoints(pointsToDraw, Color);
//         }
//
//         private float4x4 TransformAndDrawBox(Raster render, float3[] cylinderPoints, float4x4 transform,
//             float3? position = null, float3? scale = null, float3? eulerRotation = null, float3? rotCenter = null,
//             float3? rotDirection = null, float? angle = null, bool useGrad = false)
//         {
//             var desiredTransform = Transforms.GetDesiredTransform(transform, position, scale, eulerRotation, rotCenter,
//                 rotDirection, angle, useGrad);
//             DrawBox(render, cylinderPoints, desiredTransform);
//             return desiredTransform;
//         }
//
//         private void DrawBox(Raster render, float3[] boxPoints, float4x4 transform)
//         {
//             var scaledTransform = mul(float4x4(
//                 0.5f, 0, 0, 0,
//                 0, 0.5f, 0, 0,
//                 0, 0, 0.5f, 0,
//                 0, 0, 0, 1
//             ), transform);
//
//             var pointsToDraw = Transforms.ApplyTransform(boxPoints, scaledTransform);
//             render.DrawPoints(pointsToDraw, Color);
//         }
//
//         private void SetColor(ColorType colorType) => _colorType = colorType;
//     }
// }