using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Head<V>: SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private readonly int topSlices; 
        private const float lateralWidth = .1f;

        public Head(Transform transform, int slices, float width, float height, float depth, bool setBackTuners = true, bool setFrontTuners = true, bool deleteBottom = false) : base(transform)
        {
            topSlices = slices;
            var scale = float3(width, height, depth);

             var  headMesh =   GenerateHeadMesh(scale);
             AddMesh(headMesh, Materials.GlossyBlack);
            
            if (setBackTuners)
                GenerateBackTuners();

            if (setFrontTuners)
                GenerateFrontTuners();
            
            UpdateTranslation(zero);
        }

        private void GenerateFrontTuners()
        {
            const float scaleFactor = .125f;
            var tb1 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .28f * right + 1.1f * up +  lateralWidth * back
                });
            tb1.ApplyTransform();
            tb1.Log("Front Tuner Part 1");
            meshes.AddRange(tb1.meshes);
            materials.AddRange(tb1.materials);

            var tb2 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .25f * right + .7f * up +  lateralWidth * back
                });
            tb2.ApplyTransform();
            tb2.Log("Front Tuner Part 2");
            meshes.AddRange(tb2.meshes);
            materials.AddRange(tb2.materials);
            
            var tb3 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .22f * right + .3f * up +  lateralWidth * back
                });
            tb3.ApplyTransform();
            tb3.Log("Front Tuner Part 3");
            meshes.AddRange(tb3.meshes);
            materials.AddRange(tb3.materials);
            
            var tb4 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .28f * left + 1.1f * up +  lateralWidth * back
                });
            tb4.ApplyTransform();
            tb4.Log("Front Tuner Part 4");
            meshes.AddRange(tb4.meshes);
            materials.AddRange(tb4.materials);
            
            var tb5 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .25f * left + .7f * up +  lateralWidth * back
                });
            tb5.ApplyTransform();
            tb5.Log("Front Tuner Part 5");
            meshes.AddRange(tb5.meshes);
            materials.AddRange(tb5.materials);
            
            var tb6 = new TunerUp<V>(
                new Transform
                {
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = scaleFactor * one,
                    Position = .22f * left + .3f * up +  lateralWidth * back
                });
            tb6.ApplyTransform();
            tb6.Log("Front Tuner Part 6");
            meshes.AddRange(tb6.meshes);
            materials.AddRange(tb6.materials);
        }

        private void GenerateBackTuners()
        {
            var tb1 = new TunerBackRight<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.28f - .1f) * right + (1.1f - .1f) * up // + lateralWidth * forward
                });
            tb1.ApplyTransform();
            tb1.Log("Back Tuner Part 1");
            meshes.AddRange(tb1.meshes);
            materials.AddRange(tb1.materials);

            var tb2 = new TunerBackRight<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.25f - .1f) * right + (.7f - .1f) * up // + lateralWidth * forward
                });
            tb2.ApplyTransform();
            tb2.Log("Back Tuner Part 2");
            meshes.AddRange(tb2.meshes);
            materials.AddRange(tb2.materials);

            var tb3 = new TunerBackRight<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.22f - .1f) * right + (.3f - .1f) * up // + lateralWidth * forward
                });
            tb3.ApplyTransform();
            tb3.Log("Back Tuner Part 3");
            meshes.AddRange(tb3.meshes);
            materials.AddRange(tb3.materials);

            var tb4 = new TunerBackLeft<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.28f + .1f) * left + (1.1f - .1f) * up // + .2f * forward
                });
            tb4.ApplyTransform();
            tb4.Log("Back Tuner Part 4");
            meshes.AddRange(tb4.meshes);
            materials.AddRange(tb4.materials);

            var tb5 = new TunerBackLeft<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.25f + .1f) * left + (.7f - .1f) * up // + .2f * forward
                });
            tb5.ApplyTransform();
            tb5.Log("Back Tuner Part 5");
            meshes.AddRange(tb5.meshes);
            materials.AddRange(tb5.materials);

            var tb6 = new TunerBackLeft<V>(
                new Transform
                {
                    Scale = .2f * one,
                    Position = (.22f + .1f) * left + (.3f - .1f) * up //  + .2f * forward
                });
            tb6.ApplyTransform();
            tb6.Log("Back Tuner Part 6");
            meshes.AddRange(tb6.meshes);
            materials.AddRange(tb6.materials);
        }

        private Mesh<V> GenerateHeadMesh(float3 scale)
        {
            var (x, y, _) = scale;

            const float xScale = 1.25f;
            var headTopDecoration = new BezierCurve(
                float3(xScale * x * -1f, 0f, 0f),
                float3(xScale * x * -0.25f, .1f, 0f),
                float3(xScale * x * -0.25f, 0f, 0f),
                float3(0f, 0f, 0f),
                float3(xScale * x * 0.25f, 0f, 0f),
                float3(xScale * x * 0.25f, .1f, 0f),
                float3(xScale * x * 1f, 0f, 0f));

            var c0 = new BezierCurve(headTopDecoration.GetPointsInSegment(0));
            var c1 = new BezierCurve(headTopDecoration.GetPointsInSegment(1));

            // Head Front Left
            return MyManifold<V>.Lofted(topSlices, topSlices,
                    v => lerp(float3(-x, 0, -lateralWidth), float3(0, 0, -lateralWidth), v),
                    u => c0.GetPoint(u) + y * float3.up + lateralWidth * back)
                .Concat(
                    // Head Front Right
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        v => lerp(float3(0, 0, -lateralWidth), float3(x, 0, -lateralWidth), v),
                        u => c1.GetPoint(u) + y * float3.up+ lateralWidth * back))
                .Concat(
                    // Head Back Left
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        u => c0.GetPoint(u) + y * float3.up,
                        v => lerp(float3(-x, 0, 0), float3(0, 0, 0), v)))
                .Concat(
                    // Head Back Right
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        u => c1.GetPoint(u) + y * float3.up,
                        v => lerp(float3(0, 0, 0), float3(x, 0, 0), v)))
                .Concat(
                    // Head Right
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        u => lerp(float3(x, 0, -lateralWidth), float3(x, 0, 0f), u),
                        v => lerp(float3(xScale * x, y, -lateralWidth), float3(xScale * x, y, 0), v)))
                .Concat(
                    // Head Left
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        v => lerp(float3(-xScale * x, y, -lateralWidth), float3(-xScale * x, y, 0), v),
                        u => lerp(float3(-x, 0, -lateralWidth), float3(-x, 0, 0f), u)))
                .Concat(
                    // Head Top Left
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        u => c0.GetPoint(u) + y * float3.up + lateralWidth * back,
                        v => c0.GetPoint(v) + y * float3.up))
                .Concat(
                    // Head Top Right
                    MyManifold<V>.Lofted(topSlices, topSlices,
                        u => c1.GetPoint(u) + y * float3.up + lateralWidth * back,
                        v => c1.GetPoint(v) + y * float3.up ))
                .Weld();
        }
    }
}