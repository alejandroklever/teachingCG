using GMath;
using Rendering;
using System;
using Renderer.Scene;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer
{
    internal static class Program
    {
        static void CreateScene(Scene<float3> scene)
        {
            // Adding elements of the scene
            scene.Add(Raycasting.UnitarySphere, Transforms.Translate(0, 1, 0));
            scene.Add(Raycasting.PlaneXZ, Transforms.Identity);
        }

        static void CreateScene(Scene<PositionNormal> scene)
        {
            // Adding elements of the scene
            scene.Add(
                Raycasting.UnitarySphere.AttributesMap(a => new PositionNormal {Position = a, Normal = normalize(a)}),
                Transforms.Translate(0, 1, 0));

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormal {Position = a, Normal = float3(0, 1, 0)}),
                Transforms.Identity);
        }

        static void SimpleRaycast(Texture2D texture)
        {
            Raytracer<RayPayload, float3> raycaster = new Raytracer<RayPayload, float3>();

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(float3(2, 1f, 4), float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<float3> scene = new Scene<float3>();
            CreateScene(scene);

            raycaster.OnClosestHit += delegate(IRaycastContext context, float3 attribute, ref RayPayload payload)
            {
                payload.Color = attribute;
            };

            for (float px = 0.5f; px < texture.Width; px++)
            for (float py = 0.5f; py < texture.Height; py++)
            {
                RayDescription ray = RayDescription.FromScreen(px, py, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                RayPayload coloring = new RayPayload();

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write((int) px, (int) py, float4(coloring.Color, 1));
            }
        }

        static void LitRaycast(Texture2D texture)
        {
            // Scene Setup
            float3 CameraPosition = float3(3, 2f, 4);
            float3 LightPosition = float3(3, 5, -2);

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 1, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<PositionNormal> scene = new Scene<PositionNormal>();
            CreateScene(scene);

            // Raycaster to trace rays and check for shadow rays.
            Raytracer<ShadowRayPayload, PositionNormal> shadower = new Raytracer<ShadowRayPayload, PositionNormal>();
            shadower.OnAnyHit += delegate(IRaycastContext context, PositionNormal attribute,
                ref ShadowRayPayload payload)
            {
                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No neccessary to continue checking other objects
                return HitResult.Stop;
            };

            // Raycaster to trace rays and lit closest surfaces
            Raytracer<RayPayload, PositionNormal> raycaster = new Raytracer<RayPayload, PositionNormal>();
            raycaster.OnClosestHit +=
                delegate(IRaycastContext context, PositionNormal attribute, ref RayPayload payload)
                {
                    // Move geometry attribute to world space
                    attribute = attribute.Transform(context.FromGeometryToWorld);

                    float3 V = normalize(CameraPosition - attribute.Position);
                    float3 L = normalize(LightPosition - attribute.Position);
                    float lambertFactor = max(0, dot(attribute.Normal, L));

                    // Check ray to light...
                    ShadowRayPayload shadow = new ShadowRayPayload();
                    shadower.Trace(scene,
                        RayDescription.FromTo(
                            attribute.Position +
                            attribute.Normal * 0.001f, // Move an epsilon away from the surface to avoid self-shadowing 
                            LightPosition), ref shadow);

                    payload.Color = shadow.Shadowed ? float3(0, 0, 0) : float3(1, 1, 1) * lambertFactor;
                };
            raycaster.OnMiss += delegate(IRaycastContext context, ref RayPayload payload)
            {
                payload.Color = float3(0, 0, 1); // Blue, as the sky.
            };

            // Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                RayPayload coloring = new RayPayload();

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4(coloring.Color, 1));
            }
        }

        static float3 EvalBezier(float3[] control, float t)
        {
            // DeCasteljau
            if (control.Length == 1)
                return control[0]; // stop condition
            float3[] nestedPoints = new float3[control.Length - 1];
            for (int i = 0; i < nestedPoints.Length; i++)
                nestedPoints[i] = lerp(control[i], control[i + 1], t);
            return EvalBezier(nestedPoints, t);
        }

        static Mesh<PositionNormal> CreateModel()
        {
            // Revolution Sample with Bezier
            float3[] contourn =
            {
                float3(0, -.5f, 0),
                float3(0.8f, -0.5f, 0),
                float3(1f, -0.2f, 0),
                float3(0.6f, 1, 0),
                float3(0, 1, 0)
            };

            // Creates the model using a revolution of a bezier.
            // Only Positions are updated.
            Mesh<PositionNormal> model;
            model = Manifold<PositionNormal>.Revolution(10, 10,
                    t => EvalBezier(contourn, t),
                    float3(1, 0, 0))
                .Weld();

            model.ComputeNormals();
            return model;
        }

        static void CreateMeshScene(Scene<PositionNormal> scene)
        {
            Mesh<PositionNormal> mesh;

            var transform = new Transform
            {
                Position = float3.zero, // 2 * float3.up
                // Scale = .5f * float3.one
            };

            var cube = new Microphone<PositionNormal>(transform);
            mesh = cube.Mesh;

            var model = mesh; // CreateModel();
            scene.Add(cube.RaycastGeometry, cube.TransformMatrix);

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormal {Position = a, Normal = float3(0, 1, 0)}),
                Transforms.Identity);
        }

        static void RaycastingMesh(Texture2D texture)
        {
            // Scene Setup
            float3 CameraPosition = 2 * float3(0, 5, -5);
            float3 LightPosition = 3 * float3(3, 5, -2);

            // CameraPosition = LightPosition = 5 * float3(1, 1, -1);
            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, 2 * float3.up, float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<PositionNormal> scene = new Scene<PositionNormal>();
            CreateMeshScene(scene);

            // Raycaster to trace rays and check for shadow rays.
            var shadower = new Raytracer<ShadowRayPayload, PositionNormal>();
            shadower.OnAnyHit += delegate(IRaycastContext context, PositionNormal attribute,
                ref ShadowRayPayload payload)
            {
                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No neccessary to continue checking other objects
                return HitResult.Stop;
            };

            // Raycaster to trace rays and lit closest surfaces
            Raytracer<RayPayload, PositionNormal> raycaster = new Raytracer<RayPayload, PositionNormal>();
            raycaster.OnClosestHit +=
                delegate(IRaycastContext context, PositionNormal attribute, ref RayPayload payload)
                {
                    // Move geometry attribute to world space
                    attribute = attribute.Transform(context.FromGeometryToWorld);

                    float3 V = normalize(CameraPosition - attribute.Position);
                    float3 L = normalize(LightPosition - attribute.Position);
                    float lambertFactor = max(0, dot(attribute.Normal, L));

                    // Check ray to light...
                    ShadowRayPayload shadow = new ShadowRayPayload();
                    shadower.Trace(scene,
                        RayDescription.FromTo(
                            attribute.Position +
                            attribute.Normal * 0.001f, // Move an epsilon away from the surface to avoid self-shadowing 
                            LightPosition), ref shadow);

                    payload.Color = shadow.Shadowed ? float3(0, 0, 0) : float3(1, 1, 1) * lambertFactor;
                };
            raycaster.OnMiss += delegate(IRaycastContext context, ref RayPayload payload)
            {
                payload.Color = float3(0, 0, 1); // Blue, as the sky.
            };

            // Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                int progress = px * texture.Height + py;
                if (progress % 100 == 0)
                {
                    Console.Write("\r" + progress * 100 / (float) (texture.Width * texture.Height) + "%            ");
                }

                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                RayPayload coloring = new RayPayload();

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4(coloring.Color, 1));
            }

            Console.Write("\r" + 100 + "%            ");
        }

        private static void Main(string[] args)
        {
            // Texture2D texture = new Texture2D(256, 256);
            //
            // SimpleRaycast(texture);
            // LitRaycast(texture);
            // RaycastingMesh(texture);
            //
            // texture.Save("test.rbm");
            // Console.WriteLine("Done.");

            RaycastProcess.StartProcess(new TestConfig{
                width = 256,
                height = 256,
                camera = 2 * float3(5, 5, 0),
                light = 3 * float3(3, 5, -2),
                target = 2 * float3.up});
            Processing.ShowImageProcess();
        }
    }
}
