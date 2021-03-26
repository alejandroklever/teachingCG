using GMath;
using Rendering;
using System;
using static GMath.Gfx;

namespace Renderer
{
    class Program
    {
        struct MyVertex : IVertex<MyVertex>
        {
            public float3 Position { get; set; }

            public MyVertex Add(MyVertex other)
            {
                return new MyVertex
                {
                    Position = this.Position + other.Position,
                };
            }

            public MyVertex Mul(float s)
            {
                return new MyVertex
                {
                    Position = this.Position * s,
                };
            }
        }

        struct MyProjectedVertex : IProjectedVertex<MyProjectedVertex>
        {
            public float4 Homogeneous { get; set; }

            public MyProjectedVertex Add(MyProjectedVertex other)
            {
                return new MyProjectedVertex
                {
                    Homogeneous = this.Homogeneous + other.Homogeneous
                };
            }

            public MyProjectedVertex Mul(float s)
            {
                return new MyProjectedVertex
                {
                    Homogeneous = this.Homogeneous * s
                };
            }
        }

        static void Main(string[] args)
        {
            Raster<MyVertex, MyProjectedVertex> render = new Raster<MyVertex, MyProjectedVertex>(1024, 512);
            GeneratingMeshes(render);
            //DrawRoomTest(render);
            render.RenderTarget.Save("test.rbm");
            Console.WriteLine("Done.");
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


        static Mesh<MyVertex> CreateModel()
        {
            // Parametric representation of a sphere.
            //return Manifold<MyVertex>.Surface(30, 30, (u, v) =>
            //{
            //    float alpha = u * 2 * pi;
            //    float beta = pi / 2 - v * pi;
            //    return float3(cos(alpha) * cos(beta), sin(beta), sin(alpha) * cos(beta));
            //});

            // Generative model
            //return Manifold<MyVertex>.Generative(30, 30,
            //    // g function
            //    u => float3(cos(2 * pi * u), 0, sin(2 * pi * u)),
            //    // f function
            //    (p, v) => p + float3(cos(v * pi), 2*v-1, 0)
            //);

            // Revolution Sample with Bezier
            float3[] contourn =
            {
                float3(0, -.5f,0),
                float3(0.8f, -0.5f,0),
                float3(1f, -0.2f,0),
                float3(0.6f,1,0),
                float3(0,1,0)
            };
            return Manifold<MyVertex>.Revolution(30, 30, t => EvalBezier(contourn, t), float3(0, 1, 0));
        }
        
        private static void GeneratingMeshes(Raster<MyVertex, MyProjectedVertex> render)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1)); // clear with color dark blue.

            var primitive = CreateModel();

            /// Convert to a wireframe to render. Right now only lines can be rasterized.
            primitive = primitive.ConvertTo(Topology.Lines);

            #region viewing and projecting

            float4x4 viewMatrix = Transforms.LookAtLH(float3(2, 1f, 4), float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix = Transforms.PerspectiveFovLH(pi_over_4, render.RenderTarget.Height / (float)render.RenderTarget.Width, 0.01f, 20);

            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex { Homogeneous = hPosition };
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p =>
            {
                return float4(p.Homogeneous.x / 1024.0f, p.Homogeneous.y / 512.0f, 1, 1);
            };

            #endregion

            // Draw the mesh.
            render.DrawMesh(primitive);
        }
    }
}
