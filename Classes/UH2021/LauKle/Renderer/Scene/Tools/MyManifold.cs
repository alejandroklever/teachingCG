using System;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class MyManifold<V>: Manifold<V> where V : struct, IVertex<V>
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

        public static Mesh<V> Centroid(int slices, int stacks, float3 pos, Func<float, float3> f)
        {
            return Surface(slices, stacks, (u, v) => lerp(pos, f(u), v));
        }

        public static Mesh<V> ImprovedLofted(int slices, int stacks, Func<float, float3> g1, Func<float, float3> g2,
            Func<float3, float3, float, float3> h)
        {
           return Surface(slices, stacks, (u, v) => h(g1(u), g2(u), v));
        }

        public static Mesh<V> Cylinder(int slices, int stacks, float3 pos, float radio, float height,
            (float a, float b)? interval = null, bool createDiscs = true)
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

            return mesh.Concat(
                Disc(slices, stacks, pos + height * float3.up, radio, interval)).Concat(
                Disc(slices, stacks, pos, radio, interval)
                    .Transform(
                        Transforms.DesiredTransform(
                            eulerRotation: float3(0, 0, -pi)
                        )
                    )
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

        public static Mesh<V> Cube(float3 pos)
        {
            var vertex = new[]
            {
                new V {Position = float3(0, 0, 0)},
                new V {Position = float3(1, 0, 0)},
                new V {Position = float3(1, 1, 0)},
                new V {Position = float3(0, 1, 0)},
                new V {Position = float3(0, 1, 1)},
                new V {Position = float3(1, 1, 1)},
                new V {Position = float3(1, 0, 1)},
                new V {Position = float3(0, 0, 1)}
            };

            var index = new[]
            {
                0, 2, 1, //face front
                0, 3, 2,
                2, 3, 4, //face top
                2, 4, 5,
                1, 2, 5, //face right
                1, 5, 6,
                0, 7, 4, //face left
                0, 4, 3,
                5, 4, 7, //face back
                5, 7, 6,
                0, 6, 7, //face bottom
                0, 1, 6
            };
            return new Mesh<V>(vertex, index).Transform(Transforms.Translate(pos));
        }
    }
}