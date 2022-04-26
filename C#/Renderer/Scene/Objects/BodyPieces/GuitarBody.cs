using System;
using Rendering;
using static GMath.Gfx;
using static Renderer.Scene.MeshTools;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class GuitarBody<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public GuitarBody(Transform transform, int slices, int stacks, int roundness, bool renderBottom = false) :
            base(transform)
        {
            var redParts = GuitarMeshData<V>.GetFacesMesh(slices, stacks);
            var edgesParts = GuitarMeshData<V>.GetEdgeMesh(slices, roundness);
            var mastilBack = GuitarMeshData<V>.GenerateBackMastilMesh(slices, stacks, float3.zero);

            AddMesh(edgesParts, Materials.Default);

            AddMesh(redParts
                    .Concat(mastilBack)
                    .Weld(),
                Materials.GuitarRedMaterial);

            GenerateBridge(renderBottom);
            GenerateRails(renderBottom);
            GenerateMicrophones(renderBottom);
            GenerateSwitch(renderBottom);
            GeneratePotentiometers();

            UpdateTranslation(zero);
        }

        private void GenerateBridge(bool renderBottom)
        {
            var bridge = new Bridge<V>(
                new Transform
                {
                    Position = 1.4f * down,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = float3(.22f, .21f, .15f)
                }, renderBottom);
            bridge.ApplyTransform();
            bridge.Log("Bridge");

            meshes.AddRange(bridge.meshes);
            materials.AddRange(bridge.materials);
        }

        private void GenerateRails(bool renderBottom)
        {
            var rails = new Rails<V>(
                new Transform
                {
                    Position = .75f * down,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = float3(.21f, .21f, .15f)
                }, 10, renderBottom);
            rails.ApplyTransform();
            rails.Log("Rails");

            meshes.AddRange(rails.meshes);
            materials.AddRange(rails.materials);
        }

        private void GenerateMicrophones(bool renderBottom)
        {
            var bridgeMicro = new Microphone<V>(
                new Transform
                {
                    Position = .3f * down,
                    Rotation = float3(pi / 2, pi / 2, 0),
                    Scale = .06f * one
                }, renderBottom);
            bridgeMicro.ApplyTransform();
            bridgeMicro.Log("Bridge Micro");
            meshes.AddRange(bridgeMicro.meshes);
            materials.AddRange(bridgeMicro.materials);

            var fretboardMicro = new Microphone<V>(
                new Transform
                {
                    Position = .9f * up,
                    Rotation = float3(pi / 2, pi / 2, 0),
                    Scale = .06f * one
                }, renderBottom);
            fretboardMicro.ApplyTransform();
            fretboardMicro.Log("Fretboard Micro");

            meshes.AddRange(fretboardMicro.meshes);
            materials.AddRange(fretboardMicro.materials);
        }

        private void GeneratePotentiometers()
        {
            var roundness = 8;

            var potentiometer1 = new Potentiometer<V>(
                new Transform
                {
                    Position = 1.5f * down + 1.15f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, roundness);
            potentiometer1.ApplyTransform();
            potentiometer1.Log("Potentiometer 1");
            meshes.AddRange(potentiometer1.meshes);
            materials.AddRange(potentiometer1.materials);

            var potentiometer2 = new Potentiometer<V>(
                new Transform
                {
                    Position = 2.5f * down + 1.15f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, roundness);
            potentiometer2.ApplyTransform();
            potentiometer2.Log("Potentiometer 2");
            meshes.AddRange(potentiometer2.meshes);
            materials.AddRange(potentiometer2.materials);

            var potentiometer3 = new Potentiometer<V>(
                new Transform
                {
                    Position = 1f * down + 1.8f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, roundness);
            potentiometer3.ApplyTransform();
            potentiometer3.Log("Potentiometer 3");
            meshes.AddRange(potentiometer3.meshes);
            materials.AddRange(potentiometer3.materials);

            var potentiometer4 = new Potentiometer<V>(
                new Transform
                {
                    Position = 2f * down + 1.8f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, roundness);
            potentiometer4.ApplyTransform();
            potentiometer4.Log("Potentiometer 4");
            meshes.AddRange(potentiometer4.meshes);
            materials.AddRange(potentiometer4.materials);
        }

        private void GenerateSwitch(bool renderBottom)
        {
            var @switch = new Switch<V>(
                new Transform
                {
                    Position = 1.15f * left + 1.42f * up,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .15f * one
                }, 8, renderBottom);
            @switch.ApplyTransform();
            @switch.Log("Switch");
            meshes.AddRange(@switch.meshes);
            materials.AddRange(@switch.materials);
        }
    }

    public static class GuitarMeshData<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private static float guitarDepth => 1f;

        private static readonly BezierCurve bottomLeftCurve = new(
            float3(0f, -4f, 0f),
            float3(-1.714658f, -4.024096f, 0f),
            float3(-2.419511f, -2.990311f, 0f),
            float3(-2.5f, -2.25f, 0f),
            float3(-2.57303f, -1.578298f, 0f),
            float3(-2.53f, -0.67f, 0f),
            float3(-1.6f, 0f, 0f)
        );

        private static readonly BezierCurve bottomRightCurve = new(
            float3(0f, -4f, 0f),
            float3(1.714658f, -4.024096f, 0f),
            float3(2.419511f, -2.990311f, 0f),
            float3(2.5f, -2.25f, 0f),
            float3(2.57303f, -1.578298f, 0f),
            float3(2.53f, -0.67f, 0f),
            float3(1.6f, 0f, 0f)
        );

        private static readonly BezierCurve topLeftCurve = new(
            float3(-1.6f, 0f, 0f),
            float3(-1f, 0.35f, 0f),
            float3(-1.69f, 0.92f, 0f),
            float3(-1.68f, 1.42f, 0f),
            float3(-1.69f, 2f, 0f),
            float3(-1f, 2.5f, 0f),
            float3(-0.4f, 2.5f, 0f)
        );


        private static readonly BezierCurve topRightInnerCurve = new(
            float3(1.6f, 0f, 0f),
            float3(0.6339321f, 0.0130834f, 0f),
            float3(1.25f, 1.435222f, 0f),
            float3(0.94f, 1.42f, 0f),
            float3(0.5300064f, 1.399868f, 0f),
            float3(0.5f, 1.59f, 0f),
            float3(0.4f, 2.5f, 0f)
        );

        private static readonly BezierCurve topRightPicCurve = new(
            float3(1.6f, 0f, 0f),
            float3(0.9517487f, 0.4689771f, 0f),
            float3(1.825669f, 1.012423f, 0f),
            float3(1.671447f, 1.555869f, 0f),
            float3(1.587696f, 1.850992f, 0f),
            float3(1.211327f, 1.428498f, 0f),
            float3(0.94f, 1.42f, 0f)
        );

        public static Mesh<V> GetFacesMesh(int slices, int stacks)
        {
            return Join(
                GetLateralMeshes(slices, stacks),
                GetBackMesh(slices, stacks),
                GetFrontMesh(slices, stacks)
            ).Weld();
        }

        private static Mesh<V> GetBackMesh(int slices, int stacks)
        {
            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = bottomLeftCurve;
                    var r = bottomRightCurve;

                    var left = l.GetPoint(u);
                    var right = r.GetPoint(u);

                    return lerp(left, right, v) + guitarDepth * float3.forward;
                }
            );

            var upperMesh0 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topLeftCurve.GetCurve(0);
                    var r = topRightInnerCurve.GetCurve(0);

                    var left = l.GetPoint(u);
                    var right = r.GetPoint(u);

                    return lerp(left, right, v) + guitarDepth * float3.forward;
                }
            );

            var upperMesh1 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topLeftCurve.GetCurve(1);
                    var r = topRightInnerCurve.GetCurve(1);

                    var left = l.GetPoint(u);
                    var right = r.GetPoint(u);

                    return lerp(left, right, v) + guitarDepth * float3.forward;
                }
            );

            var upperMesh2 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topRightInnerCurve.GetCurve(0);
                    var r = topRightPicCurve;

                    var left = l.GetPoint(u);
                    var right = r.GetPoint(u);

                    return lerp(left, right, v) + guitarDepth * float3.forward;
                }
            );

            return Join(
                upperMesh0,
                upperMesh1,
                upperMesh2,
                bottomMesh
            ).Weld();
        }

        private static Mesh<V> GetFrontMesh(int slices, int stacks)
        {
            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = bottomLeftCurve;
                    var r = bottomRightCurve;

                    var left = NormalPos(l, u);
                    var right = NormalPos(r, u);

                    return lerp(left, right, 1 - v);
                }
            );

            var upperMesh0 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topLeftCurve.GetCurve(0);
                    var r = topRightInnerCurve.GetCurve(0);

                    var left = NormalPos(l, u);
                    var right = NormalPos(r, u);

                    if (right.y > 2.4f)
                        right.y = 2.4f;

                    return lerp(left, right, 1 - v);
                }
            );

            var upperMesh1 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topLeftCurve.GetCurve(1);
                    var r = topRightInnerCurve.GetCurve(1);

                    var left = NormalPos(l, u);
                    var right = NormalPos(r, u);

                    if (right.y > 2.4f)
                        right.y = 2.4f;

                    return lerp(left, right, 1 - v);
                }
            );

            var upperMesh2 = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var l = topRightInnerCurve.GetCurve(0);
                    var r = topRightPicCurve;

                    var left = NormalPos(l, u);
                    var right = NormalPos(r, u);

                    if (right.y > 2.4f)
                        right.y = 2.4f;

                    return lerp(left, right, 1 - v);
                }
            );
            return Join(
                upperMesh0, upperMesh1, upperMesh2,
                bottomMesh
            ).Weld();
        }

        private static Mesh<V> GetLateralMeshes(int slices, int stacks)
        {
            var c = topRightInnerCurve.GetCurve(1);
            var meshes = new[]
            {
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomLeftCurve.GetPoint(1 - t) + guitarDepth * float3.forward,
                    t => bottomLeftCurve.GetPoint(1 - t) + .1f * float3.forward),
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomRightCurve.GetPoint(t) + guitarDepth * float3.forward,
                    t => bottomRightCurve.GetPoint(t) + .1f * float3.forward),

                MyManifold<V>.Lofted(slices, stacks,
                    t => topLeftCurve.GetCurve(0).GetPoint(1 - t) + guitarDepth * float3.forward,
                    t => topLeftCurve.GetCurve(0).GetPoint(1 - t) + .1f * float3.forward),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topLeftCurve.GetCurve(1).GetPoint(1 - t) + guitarDepth * float3.forward,
                    t => topLeftCurve.GetCurve(1).GetPoint(1 - t) + .1f * float3.forward),

                MyManifold<V>.Lofted(slices, stacks,
                    t => topRightPicCurve.GetPoint(t) + guitarDepth * float3.forward,
                    t => topRightPicCurve.GetPoint(t) + .1f * float3.forward),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topRightInnerCurve.GetCurve(1).GetPoint(t) + guitarDepth * float3.forward,
                    t => topRightInnerCurve.GetCurve(1).GetPoint(t) + .1f * float3.forward),
            };

            return Join(meshes).Weld();
        }

        public static Mesh<V> GetEdgeMesh(int slices, int stacks)
        {
            var c = topRightInnerCurve.GetCurve(1);

            var f = (Func<BezierCurve, float, float, float3>)((curve, u, v) =>
           {
               var p = curve.GetPoint(u);
               var posNormal = NormalPos(curve, u);
               var posForward = curve.GetPoint(u) + .1f * float3.forward;
               return new BezierCurve(posNormal, p, p, posForward).GetPoint(v);
           });

            var meshes = new[]
            {
                MyManifold<V>.Surface(slices, stacks,
                    (u, v) => f(bottomLeftCurve, u, v)),
                MyManifold<V>.Surface(slices, stacks,
                    (u, v) => f(bottomRightCurve, u, 1 - v)),

                MyManifold<V>.Surface(slices, stacks,
                    (u, v) => f(topLeftCurve.GetCurve(0), u, v)),
                MyManifold<V>.Surface(slices, stacks,
                    (u, v) => f(topLeftCurve.GetCurve(1), u, v)),

                MyManifold<V>.Surface(2 * slices, stacks,
                    (u, v) => f(topRightPicCurve, u, 1 - v)),
                MyManifold<V>.Surface(2 * slices, stacks,
                    (u, v) => f(topRightInnerCurve.GetCurve(1), u, 1 - v)),
            };

            return Join(meshes).Weld();
        }

        public static Mesh<V> GenerateBackMastilMesh(int slices, int stacks, float3 scale)
        {
            var (x, y, z) = scale;
            var width = .4f;

            var bTop = new BezierCurve(
                float3(width, 7.2f, 0),
                float3(width, 7, .3f),
                float3(-width, 7, .3f),
                float3(-width, 7.2f, 0)
            );

            var bBottom = new BezierCurve(
                float3(width, 3.5f, 0),
                float3(width, 4f, .5f),
                float3(-width, 4f, .5f),
                float3(-width, 3.5f, 0)
            );

            var yy = -0.0520792f;
            var zz = 0.12146461f;
            var t = float3(0, 0.0520792f, -0.12146461f);

            var bTaco = new BezierCurve(
                float3(width, 2.5f, guitarDepth) + t,
                float3(.5f * width, 2.5f + .5f, guitarDepth) + t,
                float3(.5f * -width, 2.5f + .5f, guitarDepth) + t,
                float3(-width, 2.5f, guitarDepth) + t
            );

            return MyManifold<V>.Lofted(slices, stacks,
                    u =>
                    {
                        var l = topLeftCurve.GetCurve(1);
                        var r = topRightInnerCurve.GetCurve(1);

                        var left = l.GetPoint(1);
                        var right = r.GetPoint(1);

                        var x = lerp(left, right, 1 - u).x;

                        return float3(x, 2.5f, guitarDepth) + t;
                    },
                    u => bTaco.GetPoint(u))
                .Transform(Transforms.RotateX(-pi / 64))
                .Concat(
                    MyManifold<V>.ImprovedLofted(slices, stacks,
                            u => bTaco.GetPoint(u),
                            v => bBottom.GetPoint(v),
                            (u, v, t) =>
                            {
                                var d = distance(u, v);
                                var b = new BezierCurve(
                                    u,
                                    u + .25f * d * float3.back,
                                    u + .25f * d * float3.back,
                                    v);
                                return b.GetPoint(t);
                            }
                        )
                        .Transform((Transforms.RotateX(-pi / 64)))
                )
                .Concat(
                    MyManifold<V>.Lofted(slices, stacks,
                            u => float3(-width, 2.5f, lerp(1, 0, u)),
                            v =>
                            {
                                var d = distance(float3(-width, 2.5f, 1), float3(-width, 3.5f, 0));
                                var b = new BezierCurve(
                                    float3(-width, 2.5f, 1),
                                    float3(-width, 2.5f, 1) - t + .25f * d * float3.back,
                                    float3(-width, 2.5f, 1) - t + .25f * d * float3.back,
                                    float3(-width, 3.5f, 0));
                                return b.GetPoint(v);
                            })
                        .Transform(Transforms.RotateX(-pi / 64))
                )
                .Concat(
                    MyManifold<V>.Lofted(slices, stacks,
                            u => float3(width, 2.5f, lerp(1, 0, 1 - u)),
                            v =>
                            {
                                var d = distance(float3(width, 2.5f, 1), float3(width, 3.5f, 0));
                                var b = new BezierCurve(
                                    float3(width, 2.5f, 1),
                                    float3(width, 2.5f, 1) + .25f * d * float3.back,
                                    float3(width, 2.5f, 1) + .25f * d * float3.back,
                                    float3(width, 3.5f, 0));
                                return b.GetPoint(1 - v);
                            })
                        .Transform(Transforms.RotateX(-pi / 64))
                )
                .Concat(
                    MyManifold<V>.Lofted(slices, 1,
                            u => bBottom.GetPoint(u),
                            v => bTop.GetPoint(v))
                        .Transform(Transforms.RotateX(-pi / 64))
                )
                .Concat(
                    MyManifold<V>.Centred(slices, 1, 7.2f * float3.up, u => bTop.GetPoint(1 - u))
                        .Transform(Transforms.RotateX(-pi / 64))
                )
                .Weld();
        }


        private static float3 NormalPos(BezierCurve curve, float t)
        {
            var p = curve.GetPoint(t);
            var n = curve.GetNormal(t);

            var minus = p - .1f * n;
            var max = p + .1f * n;
            return distance(float3.zero, max) < distance(float3.zero, minus) ? max : minus;
        }
    }
}