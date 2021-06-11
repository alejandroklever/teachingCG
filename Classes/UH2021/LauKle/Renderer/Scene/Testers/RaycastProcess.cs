using System;
using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class RaycastProcess
    {
        public static void StartProcess(TestConfig config, Texture2D texture = null)
        {
            texture ??= new Texture2D(config.width, config.height);
            RaycastingMesh(texture, config.camera, config.light, config.target);
            texture.Save("test.rbm");
            Console.WriteLine("Done.");
        }

        private static void CreateMeshScene(Scene<PositionNormal> scene)
        {
            var transform = new Transform
            {
                Position = 3f * float3.up,
                Scale = float3.one,
                Rotation = float3(0, 0, 0)
            };
            var guitarBody = new GuitarBody<PositionNormal>(transform, 5, 5);
            guitarBody.ComputeNormals();
            // scene.Add(guitarBody.RaycastGeometry, guitarBody.TransformMatrix);

            var transform2 = new Transform
            {
                Position = 4f * float3.up,
                Scale = float3.one,
                Rotation = float3.zero // float3(0, pi / 2, 0)
            };
            var microphone1 = new Microphone<PositionNormal>(transform2);
            microphone1.ComputeNormals();
            scene.Add(microphone1.RaycastGeometry, microphone1.TransformMatrix);
            
            // var transform3 = new Transform
            // {
            //     Position = 2.5f * float3.up + 5* float3.back,
            //     Scale = .5f * float3.one,
            //     Rotation = float3(0, pi / 2, 0)
            // };
            // var microphone2 = new Microphone<PositionNormal>(transform3);
            // microphone2.ComputeNormals();
            // scene.Add(microphone2.RaycastGeometry, microphone2.TransformMatrix);
            

            // var transform5 = new Transform
            // {
            //     Parent = guitarBody.Transform,
            //     Position =  float3.up + .25f * float3.back,
            //     Scale = float3(.5f, 1, 1)
            //     // Rotation = float3(-pi / 32, 0, 0)
            // };
            // var fretboard = new Fretboard<PositionNormal>(transform5, 5, 22, 5);
            // fretboard.ComputeNormals();
            // scene.Add(fretboard.RaycastGeometry, fretboard.TransformMatrix);
            
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
            shadower.OnAnyHit += (IRaycastContext context, PositionNormal attribute, ref ShadowRayPayload payload) =>
            {
                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No necessary to continue checking other objects
                return HitResult.Stop;
            };

            // Raycaster to trace rays and lit closest surfaces
            var raycaster = new Raytracer<RayPayload, PositionNormal>();
            raycaster.OnClosestHit += (IRaycastContext context, PositionNormal attribute, ref RayPayload payload) =>
            {
                // payload.Color = attribute.Normal;
                // return;
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

                payload.Color = shadow.Shadowed ? float3.zero : float3.one * lambertFactor;
            };
            
            raycaster.OnMiss += (IRaycastContext context, ref RayPayload payload) =>
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