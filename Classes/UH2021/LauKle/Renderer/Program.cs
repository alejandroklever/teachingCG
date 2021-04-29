using GMath;
using Rendering;
using System;
using System.IO;
using System.Text;
using Renderer.Scene;
using Renderer.Scene.Structs;
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
            Raytracer<MyRayPayload, float3> raycaster = new Raytracer<MyRayPayload, float3>();

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(float3(2, 1f, 4), float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Scene<float3> scene = new Scene<float3>();
            CreateScene(scene);

            raycaster.OnClosestHit += delegate(IRaycastContext context, float3 attribute, ref MyRayPayload payload)
            {
                payload.Color = attribute;
            };

            for (float px = 0.5f; px < texture.Width; px++)
            for (float py = 0.5f; py < texture.Height; py++)
            {
                RayDescription ray = RayDescription.FromScreen(px, py, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                MyRayPayload coloring = new MyRayPayload();

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
            Raytracer<MyRayPayload, PositionNormal> raycaster = new Raytracer<MyRayPayload, PositionNormal>();
            raycaster.OnClosestHit +=
                delegate(IRaycastContext context, PositionNormal attribute, ref MyRayPayload payload)
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
            raycaster.OnMiss += delegate(IRaycastContext context, ref MyRayPayload payload)
            {
                payload.Color = float3(0, 0, 1); // Blue, as the sky.
            };

            // Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                MyRayPayload coloring = new MyRayPayload();

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
            Mesh<PositionNormal> model = CreateModel();
            scene.Add(model.AsRaycast(), Transforms.Identity);
        }

        static void RaycastingMesh(Texture2D texture)
        {
            // Scene Setup
            float3 CameraPosition = float3(3, 2f, 4);
            float3 LightPosition = float3(3, 5, -2);
            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 1, 0), float3(0, 1, 0));
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
            Raytracer<MyRayPayload, PositionNormal> raycaster = new Raytracer<MyRayPayload, PositionNormal>();
            raycaster.OnClosestHit +=
                delegate(IRaycastContext context, PositionNormal attribute, ref MyRayPayload payload)
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
            raycaster.OnMiss += delegate(IRaycastContext context, ref MyRayPayload payload)
            {
                payload.Color = float3(0, 0, 1); // Blue, as the sky.
            };

            // Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
            for (int py = 0; py < texture.Height; py++)
            {
                int progress = (px * texture.Height + py);
                if (progress % 100 == 0)
                {
                    Console.Write("\r" + progress * 100 / (float) (texture.Width * texture.Height) + "%            ");
                }

                RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height,
                    inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                MyRayPayload coloring = new MyRayPayload();

                raycaster.Trace(scene, ray, ref coloring);

                texture.Write(px, py, float4(coloring.Color, 1));
            }
        }

        static void DrawBezierCurve(Texture2D texture)
        {
            Raster<PositionNormal, MyProjectedVertex> render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            float3 CameraPosition = float3(0f, 0f, -10f);

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            var path = new BezierCurve(
                float3(-0.04884124f, -2.970556f, 0f),
                float3(-3.124903f, -2.962161f, 0f),
                float3(-2.721841f, -0.7429778f, 0f),
                float3(-1.6f, -0.005768061f, 0f),
                float3(-0.9554807f, 0.417773f, 0f),
                float3(-1.717579f, 0.8927901f, 0f),
                float3(-1.707786f, 1.40761f, 0f),
                float3(-1.697563f, 1.945039f, 0f),
                float3(-0.7979001f, 2.036842f, 0f),
                float3(-0.007782161f, 2.010925f, 0f),
                float3(0.2717567f, 2.001756f, 0f),
                float3(0.4391647f, 1.99094f, 0f),
                float3(0.5791342f, 1.99094f, 0f),
                float3(0.5791342f, 1.825696f, 0f),
                float3(0.5883141f, 1.632911f, 0f),
                float3(0.5791342f, 1.375865f, 0f),
                float3(0.6066751f, 0.8709524f, 0f),
                float3(1.322733f, 0.8342313f, 0f),
                float3(1.313553f, 1.366685f, 0f),
                float3(1.295192f, 1.522749f, 0f),
                float3(1.476111f, 1.761416f, 0f),
                float3(1.634861f, 1.284063f, 0f),
                float3(1.810262f, 0.7566362f, 0f),
                float3(0.9574833f, 0.4208248f, 0f),
                float3(1.6f, -0.002990961f, 0f),
                float3(2.721841f, -0.7429778f, 0f),
                float3(3.11974f, -2.979204f, 0f)
            );

            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex {Homogeneous = hPosition};
            };

            // Manifold<PositionNormal>.Surface(10, 10,
            //     (u, v) => { return float3.zero; });
            //
            // var slices = 10;
            // var stacks = 10;
            // for (int j = 0; j < stacks; j++)
            // {
            //     var step = j / (float) stacks;
            //     var p0 = step * float3.up;
            //     var p1 = p0 + float3.left;
            //     render.DrawLine(new PositionNormal {Position = p0}, new PositionNormal {Position = p1});
            // }

            const int steps = 1;
            for (var i = 1; i <= steps; i++)
            {
                var subPath = new BezierCurve(Transforms.ApplyTransform(path.Points,
                    Transforms.Scale((i / (float) steps) * float3.one)));
                render.DrawBezier(subPath, false);
            }
        }

        static void RasterizeMesh(Texture2D texture)
        {
            Raster<PositionNormal, MyProjectedVertex> render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            float3 CameraPosition = 2 * float3(0f, 0f, -5f);

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            Mesh<PositionNormal> model = CreateModel();
            model.ComputeNormals();
            model = model.ConvertTo(Topology.Lines);

            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex {Homogeneous = hPosition};
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p => float4(p.Homogeneous.x / 512.0f, p.Homogeneous.y / 512.0f, 1, 1);
            render.DrawMesh(model);
        }

        static void Main(string[] args)
        {
            Texture2D texture = new Texture2D(512, 512);

            // SimpleRaycast(texture);
            // LitRaycast(texture);
            // RaycastingMesh(texture);

            // RasterizeMesh(texture);

            // DrawBezierCurve(texture);

            DrawPlane(texture);
            
            texture.Save("test.rbm");
            Console.WriteLine("Done.");
            Processing.ShowImageProcess();
        }

        private static void DrawPlane(Texture2D texture)
        {
            var render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            float3 CameraPosition = float3(0f, 0f, -10f);

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            var slices = 10;
            var stacks = 10;
            var model = Manifold<PositionNormal>.Surface(10, 10, (u, v) => float3(lerp(-2, 2, u), lerp(-2, 2, v), 0))
                .Weld();
            model = model.ConvertTo(Topology.Lines);


            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex {Homogeneous = hPosition};
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p => float4(p.Homogeneous.x / 512.0f, p.Homogeneous.y / 512.0f, 1, 1);
            render.DrawMesh(model);


            for (int i = 0; i <= stacks; i++)
            {
                for (int j = 0; j <= slices; j++)
                {
                    if (i == 0 || j == 0 || i == stacks || j == slices)
                    {
                        var p = model.Vertices[j + i * (slices + 1)];
                        render.DrawMesh(Manifold<PositionNormal>.Sphere(p.Position, .1f).ConvertTo(Topology.Lines));
                    }
                }
            }
        }

        static void ValuesToTxt(int segment, int steps)
        {
            var writer = new StreamWriter("out.txt");
            var s = new StringBuilder();
            var curve = new BezierCurve(
                float3(-0.04884124f, -2.970556f, 0f),
                float3(-3.124903f, -2.962161f, 0f),
                float3(-2.721841f, -0.7429778f, 0f),
                float3(-1.6f, -0.005768061f, 0f),
                float3(-0.9554807f, 0.417773f, 0f),
                float3(-1.717579f, 0.8927901f, 0f),
                float3(-1.707786f, 1.40761f, 0f),
                float3(-1.697563f, 1.945039f, 0f),
                float3(-0.7979001f, 2.036842f, 0f),
                float3(-0.007782161f, 2.010925f, 0f),
                float3(0.2717567f, 2.001756f, 0f),
                float3(0.4391647f, 1.99094f, 0f),
                float3(0.5791342f, 1.99094f, 0f),
                float3(0.5791342f, 1.825696f, 0f),
                float3(0.5883141f, 1.632911f, 0f),
                float3(0.5791342f, 1.375865f, 0f),
                float3(0.6066751f, 0.8709524f, 0f),
                float3(1.322733f, 0.8342313f, 0f),
                float3(1.313553f, 1.366685f, 0f),
                float3(1.295192f, 1.522749f, 0f),
                float3(1.476111f, 1.761416f, 0f),
                float3(1.634861f, 1.284063f, 0f),
                float3(1.810262f, 0.7566362f, 0f),
                float3(0.9574833f, 0.4208248f, 0f),
                float3(1.6f, -0.002990961f, 0f),
                float3(2.721841f, -0.7429778f, 0f),
                float3(3.11974f, -2.979204f, 0f)
            );

            var pointsInSegment = curve.GetPointsInSegment(segment);
            for (var i = 0; i < steps; i++)
            {
                var (x, y, z) = BezierCurve.Eval(pointsInSegment[0], pointsInSegment[1], pointsInSegment[2],
                    pointsInSegment[3], i / (float) steps);
                s.Append($"{(x: x, y: y, z: z)}\n");
            }

            writer.Write(s.ToString());
            writer.Close();
        }

        private static void ClearDirectory()
        {
            File.Delete("out.txt");
            File.Delete("test.rbm");
        }
    }
}
