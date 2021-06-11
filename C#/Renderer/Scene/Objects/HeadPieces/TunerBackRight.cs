using System;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class TunerBackRight<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private const float r = .15f;
        private const int crankSlices = 6;
        private const int roundness = 6;
        private const int stacks = 1;

        private readonly BezierCurve rightCurve = new(
            float3(0f, -2.25f, 0f),
            float3(0.15f, -2.25f, 0f),
            float3(0.2f, -2f, 0f),
            float3(0.3f, -2f, 0f),
            float3(1.15f, -2f, 0f),
            float3(1.15f, -0.5f, 0f),
            float3(1.25f, 0f, 0f),
            float3(1f, 0.18f, 0f),
            float3(0.9f, 0.23f, 0f),
            float3(0.6f, 0.42f, 0f),
            float3(0.4f, 0.42f, 0f),
            float3(0.16f, 0.42f, 0f),
            float3(0f, 0.42f, 0f));

        private readonly BezierCurve leftCurve = new(
            float3(0f, -2.25f, 0f),
            float3(-0.15f, -2.25f, 0f),
            float3(-0.2f, -2f, 0f),
            float3(-0.3f, -2f, 0f),
            float3(-1.15f, -2f, 0f),
            float3(-1.15f, -0.5f, 0f),
            float3(-1.25f, 0f, 0f),
            float3(-1f, 0.18f, 0f),
            float3(-0.9f, 0.23f, 0f),
            float3(-0.6f, 0.42f, 0f),
            float3(-0.4f, 0.42f, 0f),
            float3(-0.16f, 0.42f, 0f),
            float3(0f, 0.42f, 0f));

        public TunerBackRight(Transform transform) : base(transform)
        {
            var cube = MyManifold<V>.UnitaryCube(0);

            var cylinder = MyManifold<V>.Cylinder(roundness, 1, float3.zero, r, 1.2f, createDiscs: false)
                .Transform(Transforms.DesiredTransform(
                    translation: cube.Center(),
                    eulerRotation: float3(0, 0, pi / 2)));

            var crank = GetCrank()
                .Transform(Transforms.DesiredTransform(
                    translation: cylinder.Center() + float3.right,
                    eulerRotation: float3(0, 0, pi / 2),
                    scale: float3(.4f, .3f, 1)));

            Add(cube, Materials.Default);
            Add(cylinder, Materials.Default);
            Add(crank, Materials.Default);
            
            // Mesh = MeshTools.Join(cube, cylinder, crank);
            UpdateTranslation(zero);
        }

        private Mesh<V> GetCrank()
        {
            var center = float3.down;

            var leftFront = MyManifold<V>.Centred(crankSlices, stacks, center + r * float3.back,
                u => leftCurve.GetPoint(u) + r * float3.back);
            var rightFront = MyManifold<V>.Centred(crankSlices, stacks, center + r * float3.back,
                u => rightCurve.GetPoint(1f - u) + r * float3.back);

            var leftBack = MyManifold<V>.Centred(crankSlices, stacks, center + r * float3.forward,
                u => leftCurve.GetPoint(1f - u) + r * float3.forward);
            var rightBack = MyManifold<V>.Centred(crankSlices, stacks, center + r * float3.forward,
                u => rightCurve.GetPoint(u) + r * float3.forward);

            var leftLateral = MyManifold<V>.Lofted(crankSlices, stacks,
                u => leftCurve.GetPoint(1f - u) + r * float3.forward,
                v => leftCurve.GetPoint(1f - v) + r * float3.back);

            var rightLateral = MyManifold<V>.Lofted(crankSlices, 1,
                u => rightCurve.GetPoint(u) + r * float3.forward,
                v => rightCurve.GetPoint(v) + r * float3.back);

            return MeshTools.Join(leftFront, rightFront, leftBack, rightBack, leftLateral, rightLateral);
        }
    }
}