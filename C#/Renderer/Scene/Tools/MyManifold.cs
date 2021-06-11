using System;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class MyManifold<V>: Manifold<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public static Mesh<V> SurfaceDiscrete(int slices, int stacks, Func<int, int, float3> generating)
        {
            var vertices = new V[(slices + 1) * (stacks + 1)];
            var indices = new int[slices * stacks * 6];

            // Filling vertices for the manifold.
            // A manifold with x,y,z mapped from (0,0)-(1,1)
            for (var i = 0; i <= stacks; i++)
            for (var j = 0; j <= slices; j++)
                vertices[i * (slices + 1) + j] = new V {Position = generating(j, i)};

            // Filling the indices of the quad. Vertices are linked to adjacent.
            var index = 0;
            for (var i = 0; i < stacks; i++)
            for (var j = 0; j < slices; j++)
            {
                indices[index++] = i * (slices + 1) + j;
                indices[index++] = (i + 1) * (slices + 1) + j;
                indices[index++] = (i + 1) * (slices + 1) + (j + 1);

                indices[index++] = i * (slices + 1) + j;
                indices[index++] = (i + 1) * (slices + 1) + (j + 1);
                indices[index++] = i * (slices + 1) + (j + 1);
            }

            return new Mesh<V>(vertices, indices);
        }

        public static Mesh<V> Sphere(int slices, int stacks, float3 pos, float r = 1f)
        {
            return Surface(slices, stacks, (u, v) =>
            {
                var alpha = u * 2 * pi;
                var beta = pi / 2 - v * pi;
                return pos + r * float3(cos(alpha) * cos(beta), sin(beta), sin(alpha) * cos(beta));
            });
        }

        public static Mesh<V> Centred(int slices, int stacks, float3 pos, Func<float, float3> f)
        {
            return Surface(slices, stacks, (u, v) => lerp(pos, f(u), v));
        }

        /// <summary>
        /// The third Function receive the previous two function results 
        /// </summary>
        public static Mesh<V> ImprovedLofted(int slices, int stacks, Func<float, float3> g1, Func<float, float3> g2,
            Func<float3, float3, float, float3> h)
        {
           return Surface(slices, stacks, (u, v) => h(g1(u), g2(u), v));
        }

        public static Mesh<V> HoledCylinder(int slices, int stacks, float3 pos, float innerRadio, float outerRadio,
            float height,
            (float a, float b)? interval = null, bool createDiscs = true, bool createTopDiscOnly = false)
        {
            interval ??= (0, two_pi);

            var (a, b) = interval.Value;

            var g = (Func<float, float, float3>) ((t, r) =>
            {
                var angle = lerp(a, b, t);
                return pos + float3(r * cos(angle), 0, r * sin(angle));
            });

            var m1 = Lofted(slices, stacks,
                u => g(u, outerRadio),
                v => g(v, outerRadio) + height * float3.up);

            var m2 = Lofted(slices, stacks,
                v => g(v, innerRadio) + height * float3.up,
                u => g(u, innerRadio));

            if (!createDiscs)
                return MeshTools.Join(m1, m2);
            
            var m3 = Lofted(slices, stacks,
                u => g(u, outerRadio) + height * float3.up,
                v => g(v, innerRadio) + height * float3.up);
            
            if (createTopDiscOnly) return MeshTools.Join(m1, m2, m3);

            var m4 = Lofted(slices, stacks,
                v => g(v, innerRadio),
                u => g(u, outerRadio));

            return MeshTools.Join(m1, m2, m3, m4);
        }
        
        public static Mesh<V> Cylinder(int slices, int stacks, float3 pos, float radio, float height,
            (float a, float b)? interval = null, bool createDiscs = true, bool createTopDiscOnly = false)
        {
            interval ??= (0, two_pi);

            var (a, b) = interval.Value;
            var mesh = Lofted(slices, stacks,
                u =>
                {
                    var angle = lerp(a, b, u);
                    return pos + float3(radio * cos(angle), 0, radio * sin(angle));
                },
                v =>
                {
                    var angle = lerp(a, b, v);
                    return pos + float3(radio * cos(angle), height, radio * sin(angle));
                }).Weld();

            if (!createDiscs) return mesh;

            if (createTopDiscOnly)
                return mesh.Concat(Disc(slices, stacks, pos + height * float3.up, radio, interval));
            
            return mesh.Concat(
                Disc(slices, stacks, pos + height * float3.up, radio, interval)).Concat(
                Disc(slices, stacks, pos, radio, (b, a))
            );
        }

        public static Mesh<V> Cone(int slices, int stacks, float3 pos, float bottomRadio, float topRadio,float height,
            (float a, float b)? interval = null, bool createDiscs = true)
        {
            interval ??= (0, two_pi);

            var (a, b) = interval.Value;
            var mesh = Lofted(slices, stacks,
                u =>
                {
                    var angle = lerp(a, b, u);
                    return pos + float3(bottomRadio * cos(angle), 0, bottomRadio * sin(angle));
                },
                v =>
                {
                    var angle = lerp(a, b, v);
                    return pos + float3(topRadio * cos(angle), height, topRadio * sin(angle));
                }).Weld();

            if (!createDiscs) return mesh;

            return mesh.Concat(
                Disc(slices, stacks, pos + height * float3.up, topRadio, interval)).Concat(
                Disc(slices, stacks, pos, bottomRadio, interval)
                    .Transform(
                        Transforms.DesiredTransform(
                            eulerRotation: float3(0, 0, -pi)
                        )
                    )
            );
        }
        
        public static Mesh<V> Disc(int slices, int stacks, float3 pos, float radio, (float a, float b)? interval = null)
        {
            interval ??= (0, two_pi);

            var (a, b) = interval.Value;
            return Surface(slices, stacks,
                (u, v) =>
                {
                    var angle = lerp(a, b, u);
                    return lerp(pos, pos + float3(radio * cos(angle), 0, radio * sin(angle)), 1 - v);
                }).Weld();
        }

        public static Mesh<V> UnitaryCube(int roundness)
        {
            if (roundness > 0)
                return new Cube<V>(Transform.Default, 4 * roundness, 4 * roundness, 4 * roundness, roundness)
                    .Mesh.Transform(Transforms.Scale(1f / (4 * roundness) * float3.one));
            return new Cube<V>(Transform.Default, 1, 1, 1).Mesh;
        }
        
        public static Mesh<V> UnitaryCube(float3 pos, int roundness)
        {
            Mesh<V> mesh;
            if (roundness > 0)
                mesh = new Cube<V>(Transform.Default, 4 * roundness, 4 * roundness, 4 * roundness, roundness)
                    .Mesh.Transform(Transforms.Scale(1f / (4 * roundness) * float3.one));
            else mesh = new Cube<V>(Transform.Default, 1, 1, 1).Mesh;
            return mesh.Transform(Transforms.Translate(pos - mesh.Center()));
        }

        public static Mesh<V> Cube(float3 position, int width, int height, int depth, int roundness)
        {
            var mesh =  new Cube<V>(Transform.Default, width, height, depth, roundness)
                .Mesh;

            return mesh.Transform(mul(Transforms.Translate(position - mesh.Center()), Transforms.Scale(float3.one)));
        }
    }
}