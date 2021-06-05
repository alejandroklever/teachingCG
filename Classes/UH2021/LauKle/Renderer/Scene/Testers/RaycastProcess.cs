using System;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class RaycastProcess
    {
        public static void StartProcess(TestConfig config)
        {
            var texture = new Texture2D(config.width, config.height);
            RaycastingMesh(texture, config.camera, config.light, config.target);
            texture.Save("test.rbm");
            Console.WriteLine("Done.");
        }

        private static void CreateMeshScene(Scene<PositionNormal> scene)
        {
            var transform = new Transform
            {
                Position = float3.zero, // 2 * float3.up
                // Scale = .5f * float3.one
            };
            var cube = new Microphone<PositionNormal>(transform);
            scene.Add(cube.RaycastGeometry, cube.TransformMatrix);

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormal {Position = a, Normal = float3(0, 1, 0)}),
                Transforms.Identity);
        }

        private static void RaycastingMesh(Texture2D texture, float3 cameraPosition, float3 lightPosition, float3 target)
        {
            // View and projection matrices
            var viewMatrix = Transforms.LookAtLH(cameraPosition, target, float3.up);
            var projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            var scene = new Scene<PositionNormal>();
            CreateMeshScene(scene);

            // Raycaster to trace rays and check for shadow rays.
            var shadower = new Raytracer<ShadowRayPayload, PositionNormal>();
            shadower.OnAnyHit += delegate(IRaycastContext context, PositionNormal attribute,
                ref ShadowRayPayload payload)
            {
                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No necessary to continue checking other objects
                return HitResult.Stop;
            };

            // Raycaster to trace rays and lit closest surfaces
            var raycaster = new Raytracer<RayPayload, PositionNormal>();
            raycaster.OnClosestHit +=
                delegate(IRaycastContext context, PositionNormal attribute, ref RayPayload payload)
                {
                    // Move geometry attribute to world space
                    attribute = attribute.Transform(context.FromGeometryToWorld);

                    var V = normalize(cameraPosition - attribute.Position);
                    var L = normalize(lightPosition - attribute.Position);
                    var lambertFactor = max(0, dot(attribute.Normal, L));

                    // Check ray to light...
                    var shadow = new ShadowRayPayload();
                    shadower.Trace(scene,
                        RayDescription.FromTo(
                            attribute.Position +
                            attribute.Normal * 0.001f, // Move an epsilon away from the surface to avoid self-shadowing 
                            lightPosition), ref shadow);

                    payload.Color = shadow.Shadowed ? float3(0, 0, 0) : float3(1, 1, 1) * lambertFactor;
                };
            raycaster.OnMiss += delegate(IRaycastContext context, ref RayPayload payload)
            {
                payload.Color = float3(0, 0, 1); // Blue, as the sky.
            };

            // Render all points of the screen
            for (var px = 0; px < texture.Width; px++)
            for (var py = 0; py < texture.Height; py++)
            {
                var progress = px * texture.Height + py;
                if (progress % 100 == 0)
                {
                    Console.Write("\r" + progress * 100 / (float) (texture.Width * texture.Height) + "%            ");
                }

                var ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);
                
                var coloring = new RayPayload();

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4(coloring.Color, 1));
            }
            Console.Write("\r" + 100 + "%            ");
        }
    }
}