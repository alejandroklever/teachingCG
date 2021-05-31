using System;
using GMath;
using Renderer.Scene.Structs;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class MeshManipulationTester
    {
        public static void TestMeshManipulation(Texture2D texture)
        {
            var render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            float3 CameraPosition = float3(0f, 0f, -8f);

            // View and projection matrices
            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition, float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix =
                Transforms.PerspectiveFovLH(pi_over_4, texture.Height / (float) texture.Width, 0.01f, 20);

            var slices = 10;
            var stacks = 10;
            var model = Manifold<PositionNormal>.Surface(10, 10,
                (u, v) => float3(lerp(-2, 2, u), lerp(-2, 2, v), 0));
            var guitarOutline = new BezierCurve(); // GuitarDataMesh.BezierCurveOutline;
            model = MeshManipulator.Adjust(stacks, slices, model, guitarOutline);
            model = model.Weld().ConvertTo(Topology.Lines);

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
            
            for (int i = 0; i < stacks + 1; i++)
            for (int j = 0; j < slices + 1; j++)
            {
                if (i != 0 && j != 0 && i != stacks && j != slices) continue;
                Console.WriteLine((i, j, j + i * (slices + 1), model.Vertices.Length));
                var p = model.Vertices[j + i * (slices + 1)];
                render.DrawMesh(Manifold<PositionNormal>.Sphere(30, 30, p.Position, .1f).ConvertTo(Topology.Lines));
            }
        }
    }
}