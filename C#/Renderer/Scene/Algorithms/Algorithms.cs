using System;
using System.Diagnostics;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class Algorithms
    {
        private static readonly Action<Scene<PositionNormalCoordinate, Material>, float3, float3> CreateScene =
            Scenes.CreateMeshScene;

        public static void Raytracing(Texture2D texture, float3 CameraPosition, float3 Target, float3 LightPosition,
            float3 LightIntensity)
        {
            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, Target, float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<PositionNormalCoordinate, Material> scene = new Scene<PositionNormalCoordinate, Material>();
            CreateScene(scene, LightPosition, LightIntensity);

            // Raycaster to trace rays and check for shadow rays.
            Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material> shadower =
                new Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material>();
            shadower.OnAnyHit += delegate(IRaycastContext context, PositionNormalCoordinate attribute,
                Material material, ref ShadowRayPayload payload)
            {
                if (any(material.Emissive))
                    return HitResult.Discard; // Discard light sources during shadow test.

                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No neccessary to continue checking other objects
                return HitResult.Stop;
            };

            // Raycaster to trace rays and lit closest surfaces
            Raytracer<RTRayPayload, PositionNormalCoordinate, Material> raycaster =
                new Raytracer<RTRayPayload, PositionNormalCoordinate, Material>();
            raycaster.OnClosestHit += delegate(IRaycastContext context, PositionNormalCoordinate attribute,
                Material material, ref RTRayPayload payload)
            {
                // Move geometry attribute to world space
                attribute = attribute.Transform(context.FromGeometryToWorld);

                float3 V = -normalize(context.GlobalRay.Direction);

                float3 L = (LightPosition - attribute.Position);
                float d = length(L);
                L /= d; // normalize direction to light reusing distance to light

                attribute.Normal = normalize(attribute.Normal);

                if (material.BumpMap != null)
                {
                    float3 T, B;
                    createOrthoBasis(attribute.Normal, out T, out B);
                    float3 tangentBump =
                        material.BumpMap.Sample(material.TextureSampler, attribute.Coordinates).xyz * 2 - 1;
                    float3 globalBump = tangentBump.x * T + tangentBump.y * B + tangentBump.z * attribute.Normal;
                    attribute.Normal = globalBump; // normalize(attribute.Normal + globalBump * 5f);
                }

                float lambertFactor = max(0, dot(attribute.Normal, L));

                // Check ray to light...
                ShadowRayPayload shadow = new ShadowRayPayload();
                shadower.Trace(scene,
                    RayDescription.FromDir(
                        attribute.Position +
                        attribute.Normal * 0.001f, // Move an epsilon away from the surface to avoid self-shadowing 
                        L), ref shadow);

                float3 Intensity = (shadow.Shadowed ? 0.2f : 1.0f) * LightIntensity / (d * d);

                payload.Color =
                    material.Emissive +
                    material.EvalBRDF(attribute, V, L) * Intensity * lambertFactor; // direct light computation

                // Recursive calls for indirect light due to reflections and refractions
                if (payload.Bounces > 0)
                    foreach (var impulse in material.GetBRDFImpulses(attribute, V))
                    {
                        float3 D = impulse.Direction; // recursive direction to check
                        float3 facedNormal =
                            dot(D, attribute.Normal) > 0
                                ? attribute.Normal
                                : -attribute.Normal; // normal respect to direction

                        RayDescription ray = new RayDescription
                        {
                            Direction = D, Origin = attribute.Position + facedNormal * 0.001f, MinT = 0.0001f,
                            MaxT = 10000
                        };

                        RTRayPayload newPayload = new RTRayPayload
                        {
                            Bounces = payload.Bounces - 1
                        };

                        raycaster.Trace(scene, ray, ref newPayload);

                        payload.Color += newPayload.Color * impulse.Ratio;
                    }
            };
            raycaster.OnMiss += delegate(IRaycastContext context, ref RTRayPayload payload)
            {
                payload.Color = float3(0, 0, 0); // Blue, as the sky.
            };

            /// Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                int progress = (px * texture.Height + py);
                if (progress % 1000 == 0)
                {
                    Console.Write("\r" + progress * 100 / (float) (texture.Width * texture.Height) + "%            ");
                }

                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                RTRayPayload coloring = new RTRayPayload();
                coloring.Bounces = 3;

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4(coloring.Color, 1));
            }

            Console.Write("\r" + 100 + "%            ");
        }

        public static void Pathtracing(Texture2D texture, int pass, float3 CameraPosition, float3 Target,
            float3 LightPosition, float3 LightIntensity)
        {
            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, Target, float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<PositionNormalCoordinate, Material> scene = new Scene<PositionNormalCoordinate, Material>();
            CreateScene(scene, LightPosition, LightIntensity);

            // Raycaster to trace rays and lit closest surfaces
            Raytracer<PTRayPayload, PositionNormalCoordinate, Material> raycaster =
                new Raytracer<PTRayPayload, PositionNormalCoordinate, Material>();
            raycaster.OnClosestHit += delegate(IRaycastContext context, PositionNormalCoordinate attribute,
                Material material, ref PTRayPayload payload)
            {
                // Move geometry attribute to world space
                attribute = attribute.Transform(context.FromGeometryToWorld);

                float3 V = -normalize(context.GlobalRay.Direction);

                attribute.Normal = normalize(attribute.Normal);

                if (material.BumpMap != null)
                {
                    float3 T, B;
                    createOrthoBasis(attribute.Normal, out T, out B);
                    float3 tangentBump =
                        material.BumpMap.Sample(material.TextureSampler, attribute.Coordinates).xyz * 2 - 1;
                    float3 globalBump = tangentBump.x * T + tangentBump.y * B + tangentBump.z * attribute.Normal;
                    attribute.Normal = globalBump; // normalize(attribute.Normal + globalBump * 5f);
                }

                ScatteredRay outgoing = material.Scatter(attribute, V);

                float lambertFactor = max(0, dot(attribute.Normal, outgoing.Direction));

                payload.Color += payload.Importance * material.Emissive;

                // Recursive calls for indirect light due to reflections and refractions
                if (payload.Bounces > 0)
                {
                    float3 D = outgoing.Direction; // recursive direction to check
                    float3 facedNormal =
                        dot(D, attribute.Normal) > 0
                            ? attribute.Normal
                            : -attribute.Normal; // normal respect to direction

                    RayDescription ray = new RayDescription
                    {
                        Direction = D, Origin = attribute.Position + facedNormal * 0.001f, MinT = 0.0001f, MaxT = 10000
                    };

                    payload.Importance *= outgoing.Ratio / outgoing.PDF;
                    payload.Bounces--;

                    raycaster.Trace(scene, ray, ref payload);
                }
            };
            raycaster.OnMiss += delegate(IRaycastContext context, ref PTRayPayload payload)
            {
                payload.Color = float3(0, 0, 0); // Blue, as the sky.
            };

            /// Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                int progress = (px * texture.Height + py);
                if (progress % 10000 == 0)
                {
                    Console.Write("\r" + progress * 100 / (float) (texture.Width * texture.Height) + "%            ");
                }

                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                float4 accum = texture.Read(px, py) * pass;
                PTRayPayload coloring = new PTRayPayload();
                coloring.Importance = float3(1, 1, 1);
                coloring.Bounces = 3;

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4((accum.xyz + coloring.Color) / (pass + 1), 1));
            }

            Console.Write("\r" + 100 + "%            ");
        }
    }
}