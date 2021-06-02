using System;
using System.Linq;
using Renderer.Scene.Structs;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class HumbuckerMeshData
    {
        private static BezierCurve verticalGuide = new BezierCurve(
            float3(0f, 0f, -1f),
            float3(0f, 0.5f, -1f),
            float3(0f, 0.5f, 1f),
            float3(0f, 0f, 1f));

        private static BezierCurve horizontalGuide = new BezierCurve(float3(-2f, 0f, 0f),
            float3(-2f, 0.5f, 0f),
            float3(2f, 0.5f, 0f),
            float3(2f, 0f, 0f));

        private static Mesh<V> GetMesh<V>(int slices, int stacks, Topology topology = Topology.Lines)
            where V : struct, INormalVertex<V>
        {
            var t = .2f;
            var meshes = new[]
            {
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(lerp(-2 + t , 2 - t, u), lerp(-1 + t, 1 - t, v), -1)),
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(lerp(-2 + t, 2 - t, u), lerp(-1 + t, 1 - t, v), 1)),
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(2, lerp(-1 + t, 1 - t, u), lerp(-1 + t, 1 - t, v))),
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(-2, lerp(-1 + t, 1 - t, u), lerp(-1 + t, 1 - t, v))),
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(lerp(-2 + t, 2 - t, u), 1, lerp(-1 + t, 1 - t, v))),
                Manifold<V>.Surface(slices, stacks,
                    (u, v) => float3(lerp(-2 + t, 2 - t, u), -1, lerp(-1 + t, 1 - t, v)))
            };
            
            var curveA = new BezierCurve(
                float3(2 - t, 1f, 1 - t),
                float3(2 - t, 1, 1), 
                float3(2 - t, 1, 1),
                float3(2 - t, 1 - t, 1));
            
            var curveB = new BezierCurve(
                float3(-2 + t, 1f, 1 - t),
                float3(-2 + t, 1, 1),
                float3(-2 + t, 1, 1),
                float3(-2 + t, 1 - t, 1));

            var cyl1 = Manifold<V>.Surface(3, stacks, (u, v) =>
            {
                var a = curveA.GetPoint(u);
                var b = curveB.GetPoint(u);
                return lerp(a, b, v);
            });
            
            curveA = new BezierCurve(
                float3(2 - t, 1 - t, 1), 
                float3(2, 1 - t, 1), 
                float3(2, 1 - t, 1),
                float3(2, 1 - t, 1 - t));
            
            curveB = new BezierCurve(
                float3(2 - t, -1 + t, 1), 
                float3(2, -1 + t, 1), 
                float3(2, -1 + t, 1),
                float3(2, -1 + t, 1 - t));

            var cyl2 = Manifold<V>.Surface(3, stacks, (u, v) =>
            {
                var a = curveA.GetPoint(u);
                var b = curveB.GetPoint(u);
                return lerp(a, b, v);
            });

            curveA = new BezierCurve(
                float3(2 - t, 1f, 1 - t),
                float3(2, 1, 1 - t), 
                float3(2, 1, 1 - t),
                float3(2, 1 - t, 1 - t));
            
            curveB = new BezierCurve(
                float3(2 - t, 1f, -1 + t),
                float3(2, 1, -1 + t), 
                float3(2, 1, -1 + t),
                float3(2, 1 - t, -1 + t));

            var cyl3 = Manifold<V>.Surface(3, stacks, (u, v) =>
            {
                var a = curveA.GetPoint(u);
                var b = curveB.GetPoint(u);
                return lerp(a, b, v);
            });

            var cyls = cyl1.Concat(cyl2).Concat(cyl3);
            
            var mesh = meshes.Aggregate((accumulate, current) => accumulate.Concat(current)).Concat(cyls).Weld();
            mesh.ComputeNormals();
            return mesh.ConvertTo(topology);
        }

        public static void DrawMesh(Texture2D texture)
        {
            var render = new Raster<PositionNormal, MyProjectedVertex>(texture);

            // Scene Setup
            var CameraPosition = 1f * float3(5f, 5f, -5f);
            var (viewMatrix, projectionMatrix) = CameraTools.GetViewAndProjection(CameraPosition, texture);

            
            var mesh = GetMesh<PositionNormal>(1, 1);
        
            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                var hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex {Homogeneous = hPosition};
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p => float4(p.Homogeneous.x / 512.0f, p.Homogeneous.y / 512.0f, 1, 1);
            
            
            // render.DrawMesh(mesh);
            var t = .2f;
            var curve = new BezierCurve(
                float3(2 - t, 1f, 1 - t),
                float3(2 - t, 1, 1), 
                float3(2 - t, 1, 1),
                float3(2 - t, 1 - t, 1));
            
            
            render.DrawBezier(curve);
            curve = new BezierCurve(
                float3(2 - t, 1 - t, 1), 
                float3(2, 1 - t, 1), 
                float3(2, 1 - t, 1),
                float3(2, 1 - t, 1 - t));
            
            
            render.DrawBezier(curve);
            curve = new BezierCurve(
                float3(2 - t, 1f, 1 - t),
                float3(2, 1, 1 - t), 
                float3(2, 1, 1 - t),
                float3(2, 1 - t, 1 - t));
            
            render.DrawBezier(curve);
        }
    }
}