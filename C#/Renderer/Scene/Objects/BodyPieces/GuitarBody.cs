using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class GuitarBody<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public GuitarBody(Transform transform, int slices, int stacks, bool renderBottom = false) : base(transform)
        {
            // Mesh = GuitarMeshData<V>.GetMesh(slices, stacks, renderBottom);
            var mesh = GuitarMeshData<V>.GetMesh(slices, stacks, renderBottom);
            Add(mesh, new Material());
            
            GenerateBridge(transform, renderBottom);
            GenerateRails(transform, renderBottom);
            GenerateMicrophones(transform, renderBottom);
            GenerateSwitch(transform, renderBottom);
            GeneratePotentiometers(transform);
            
            UpdateTranslation(zero);
        }

        private void GenerateBridge(Transform transform, bool renderBottom)
        {
            var bridge = new Bridge<V>(
                new Transform
                {
                    Parent = transform,
                    Position = .9f * down,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .15f * one
                }, renderBottom);
            bridge.ApplyTransform();
            bridge.Log("Bridge");
            
            meshes.AddRange(bridge.meshes);
            materials.AddRange(bridge.materials);
            // Mesh = Mesh.Concat(bridge.Mesh);

        }

        private void GenerateRails(Transform transform, bool renderBottom)
        {
            var rails = new Rails<V>(
                new Transform
                {
                    Parent = transform,
                    Position = .5f * down,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .15f * one
                }, 6, renderBottom);
            rails.ApplyTransform();
            rails.Log("Rails");
            
            meshes.AddRange(rails.meshes);
            materials.AddRange(rails.materials);
            // Mesh = Mesh.Concat(rails.Mesh);
        }

        private void GenerateMicrophones(Transform transform, bool renderBottom)
        {
            var bridgeMicro = new Microphone<V>(
                new Transform
                {
                    Parent = transform,
                    Position = .1f * down + (renderBottom ? zero : .05f * back),
                    Rotation = float3(pi / 2,  pi / 2, 0),
                    Scale = .05f * one
                },  renderBottom);
            bridgeMicro.ApplyTransform();
            bridgeMicro.Log("Bridge Micro");
            meshes.AddRange(bridgeMicro.meshes);
            materials.AddRange(bridgeMicro.materials);
            // Mesh = Mesh.Concat(bridgeMicro.Mesh);

            var fretboardMicro = new Microphone<V>(
                new Transform
                {
                    Parent = transform,
                    Position = .9f * up + (renderBottom ? zero : .05f * back),
                    Rotation = float3(pi / 2,  pi / 2, 0),
                    Scale = .05f * one
                }, renderBottom);
            fretboardMicro.ApplyTransform();
            fretboardMicro.Log("Fretboard Micro");
            
            meshes.AddRange(fretboardMicro.meshes);
            materials.AddRange(fretboardMicro.materials);
            // Mesh = Mesh.Concat(fretboardMicro.Mesh);
        }

        private void GeneratePotentiometers(Transform transform)
        {
            var potentiometer1 = new Potentiometer<V>(
                new Transform
                {
                    Parent = transform,
                    Position = down + right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, 6);
            potentiometer1.ApplyTransform();
            potentiometer1.Log("Potentiometer 1");
            meshes.AddRange(potentiometer1.meshes);
            materials.AddRange(potentiometer1.materials);
            // Mesh = Mesh.Concat(potentiometer1.Mesh);
            
            var potentiometer2 = new Potentiometer<V>(
                new Transform
                {
                    Parent = transform,
                    Position = 2f*  down + right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, 6);
            potentiometer2.ApplyTransform();
            potentiometer2.Log("Potentiometer 2");
            meshes.AddRange(potentiometer2.meshes);
            materials.AddRange(potentiometer2.materials);
            // Mesh = Mesh.Concat(potentiometer2.Mesh);
            
            var potentiometer3 = new Potentiometer<V>(
                new Transform
                {
                    Parent = transform,
                    Position = .5f * down + 1.5f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, 6);
            potentiometer3.ApplyTransform();
            potentiometer3.Log("Potentiometer 3");
            meshes.AddRange(potentiometer3.meshes);
            materials.AddRange(potentiometer3.materials);
            // Mesh = Mesh.Concat(potentiometer3.Mesh);
            
            var potentiometer4 = new Potentiometer<V>(
                new Transform
                {
                    Parent = transform,
                    Position = 1.5f*  down + 1.5f * right,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .2f * one
                }, 6);
            potentiometer4.ApplyTransform();
            potentiometer4.Log("Potentiometer 4");
            meshes.AddRange(potentiometer4.meshes);
            materials.AddRange(potentiometer4.materials);
            // Mesh = Mesh.Concat(potentiometer4.Mesh);
        }

        private void GenerateSwitch(Transform transform, bool renderBottom)
        {
            var @switch = new Switch<V>(
                new Transform
                {
                    Parent = transform,
                    Position = 1.15f * left + 1.2f * up,
                    Rotation = float3(pi / 2, 0, 0),
                    Scale = .15f * one
                }, 5, renderBottom);
            @switch.ApplyTransform();
            @switch.Log("Switch");
            meshes.AddRange(@switch.meshes);
            materials.AddRange(@switch.materials);
            // Mesh = Mesh.Concat(@switch.Mesh);
        }
    }

    public static class GuitarMeshData<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public static readonly BezierCurve bottomLeftCurve = new(
            float3(0f, -3f, 0f),
            float3(-1.6f, -3f, 0f),
            float3(-2.3f, -2.1f, 0f),
            float3(-2.4f, -1.6f, 0f),
            float3(-2.5f, -1f, 0f),
            float3(-2.1f, -0.3f, 0f),
            float3(-1.6f, 0f, 0f)
        );

        public static readonly BezierCurve bottomRightCurve = new(
            float3(0f, -3f, 0f),
            float3(1.6f, -3f, 0f),
            float3(2.3f, -2.1f, 0f),
            float3(2.4f, -1.6f, 0f),
            float3(2.5f, -1f, 0f),
            float3(2.1f, -0.3f, 0f),
            float3(1.6f, 0f, 0f)
        );

        public static readonly BezierCurve topLeftCurve = new(
            float3(-1.6f, 0f, 0f),
            float3(-0.95f, 0.42f, 0f),
            float3(-1.7f, 0.89f, 0f),
            float3(-1.7f, 1.4f, 0f),
            float3(-1.7f, 2f, 0f),
            float3(-0.8f, 2f, 0f),
            float3(0f, 2f, 0f)
        );

        public static readonly BezierCurve topRightCurve = new(
            float3(1.6f, 0f, 0f),
            float3(0.95f, 0.42f, 0f),
            float3(1.7f, 0.89f, 0f),
            float3(1.7f, 1.4f, 0f),
            float3(1.7f, 2f, 0f),
            float3(0.8f, 2f, 0f),
            float3(0f, 2f, 0f)
        );

        public static Mesh<V> GetMesh(int slices, int stacks, bool deleteBottom)
        {
            return deleteBottom
                ? MeshTools.Join(
                    GetFrontMesh(slices, stacks),
                    GetBorderMeshes(slices, 1))
                : MeshTools.Join(
                    GetFrontMesh(slices, stacks),
                    GetBorderMeshes(slices, 1),
                    GetBackMesh(slices, stacks)
                );
        }

        private static Mesh<V> GetBackMesh(int slices, int stacks)
        {
            var upperMesh = MyManifold<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = topLeftCurve.GetPoint(u);
                var right = topRightCurve.GetPoint(u);
                return lerp(left, right, v) + .5f * float3.forward;
            });

            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(left, right, v) + .5f * float3.forward;
                }
            );

            return bottomMesh.Concat(upperMesh).Weld();
        }

        private static Mesh<V> GetFrontMesh(int slices, int stacks)
        {
            var upperMesh = MyManifold<V>.Surface(slices, stacks, (u, v) =>
            {
                var left = topLeftCurve.GetPoint(u);
                var right = topRightCurve.GetPoint(u);
                return lerp(right, left, v);
            });

            var bottomMesh = MyManifold<V>.Surface(slices, stacks,
                (u, v) =>
                {
                    var left = bottomLeftCurve.GetPoint(u);
                    var right = bottomRightCurve.GetPoint(u);
                    return lerp(right, left, v);
                }
            );

            return bottomMesh.Concat(upperMesh).Weld();
        }

        private static Mesh<V> GetBorderMeshes(int slices, int stacks)
        {
            var meshes = new[]
            {
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomLeftCurve.GetPoint(1 - t) + .5f * float3.forward,
                    t => bottomLeftCurve.GetPoint(1 - t)),
                MyManifold<V>.Lofted(slices, stacks,
                    t => bottomRightCurve.GetPoint(t) + .5f * float3.forward,
                    t => bottomRightCurve.GetPoint(t)),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topLeftCurve.GetPoint(1 - t) + .5f * float3.forward,
                    t => topLeftCurve.GetPoint(1 - t)),
                MyManifold<V>.Lofted(slices, stacks,
                    t => topRightCurve.GetPoint(t) + .5f * float3.forward,
                    t => topRightCurve.GetPoint(t))
            };

            return MeshTools.Join(meshes).Weld();
        }
    }
}