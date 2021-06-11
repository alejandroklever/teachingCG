using System;
using System.Collections.Generic;
using System.Linq;
using Rendering;
using GMath;
using static GMath.Gfx;
using static Renderer.Scene.MeshTools;

namespace Renderer.Scene
{
    public class Rails<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private readonly float3 leftScrewPoint = 2.5f * left;
        private readonly float3 rightScrewPoint = 2.5f * right;
        
        private int baseCylinderRoundness = 20;
        private const float baseCylinderHeight = .5f;
        private const float baseCylinderRadio = 1f;

        private int lateralRoundness = 20;
        private const float lateralRadio = .75f;
        private const float railsContainerHeight = .5f;
        private const float railsContainerBottomY = baseCylinderHeight;
        private const float railsContainerTopY = baseCylinderHeight + railsContainerHeight;

        private const float octaverWidth = .4f;
        private const float octaverDepth = .4f;
        private const float octaverHeight = .6f;

        private bool renderBottom;

        public Rails(Transform transform, int roundness, bool renderBottom) : base(transform)
        {
            this.renderBottom = renderBottom;
            baseCylinderRoundness = roundness;
            lateralRoundness = roundness / 2;

            var cylinderLeft = MyManifold<V>.Cylinder(baseCylinderRoundness, 1, leftScrewPoint, baseCylinderRadio,
                baseCylinderHeight, createTopDiscOnly: !renderBottom);
            
            var cylinderRight = MyManifold<V>.Cylinder(baseCylinderRoundness, 1, rightScrewPoint, baseCylinderRadio,
                baseCylinderHeight, createTopDiscOnly: !renderBottom);

            var leftLateral = MyManifold<V>.Cylinder(lateralRoundness, 2,
                leftScrewPoint + railsContainerBottomY * up,
                lateralRadio, railsContainerHeight,
                (pi / 2, 3 * pi / 2), createTopDiscOnly: !renderBottom);

            var rightLateral = MyManifold<V>.Cylinder(lateralRoundness, 2,
                rightScrewPoint + railsContainerBottomY * up,
                lateralRadio, railsContainerHeight,
                (3 * pi / 2, two_pi + pi / 2), createTopDiscOnly: !renderBottom);

            var lateralCenter = GenerateLateralCenterMesh();
            var lateralInside = GenerateLateralInsideMesh();
            
            var lateral = Join(leftLateral, rightLateral, lateralCenter, lateralInside).Weld();
            
            
            Add(cylinderLeft, Materials.Default);
            Add(cylinderRight,Materials.Default);
            Add(lateral, Materials.Default);

            var octavers = GenerateOctavers();
            foreach (var mesh in octavers)
                Add(mesh, Materials.Default);

            // Mesh = Join(
            //     cylinderLeft,
            //     cylinderRight,
            //     leftLateral,
            //     rightLateral,
            //     lateralCenter,
            //     Join(meshes.ToArray()),
            //     InsideMesh()
            // ).Weld();
            UpdateTranslation(zero);
        }

        private List<Mesh<V>> GenerateOctavers()
        {
            var start = leftScrewPoint + lateralRadio * right;

            var end = rightScrewPoint + lateralRadio * left;
            var d = distance(start, end) / 6;

            var meshes = new List<Mesh<V>>();
            for (var i = 0; i < 6; i++)
            {
                var _up = (railsContainerTopY - railsContainerBottomY) * up;
                var pos = lerp(start, end, i / 6f) + .5f * d * right + _up;

                const float h = 2.25f * lateralRadio;
                meshes.Add(Octaver(pos).Transform(Transforms.Translate(.2f * up)));
                meshes.Add(MyManifold<V>.Cylinder(8, 1, pos, .125f, h)
                    .Transform(mul(Transforms.Translate(.8f * back + .8f * h * down), Transforms.RotateX(-pi / 2)))
                );
            }

            return meshes;
        }

        private void AddFaces(List<float3> vertices, List<int> indices, (float m, float M) x, (float m, float M) y,
            (float m, float M) z, ref int tris, bool reverse = false)
        {
            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + z.M * forward + y.M * up + x.m * right,
                    leftScrewPoint + z.M * forward + y.M * up + x.M * right,
                    rightScrewPoint + z.M * forward + y.M * up + x.M * left,
                    rightScrewPoint + z.M * forward + y.M * up + x.m * left
                }, new[]
                {
                    leftScrewPoint + z.m * forward + y.m * up + x.m * right,
                    leftScrewPoint + z.m * forward + y.m * up + x.M * right,
                    rightScrewPoint + z.m * forward + y.m * up + x.M * left,
                    rightScrewPoint + z.m * forward + y.m * up + x.m * left
                }, ref tris, reverse);
        }

        private Mesh<V> Octaver(float3 pos)
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();

            var x = octaverWidth;
            var y = octaverHeight;
            var z = octaverDepth;

            AddFace(vertices, indices,
                y * up + z * forward,
                y * up + z * forward + x * right,
                zero,
                x * right, ref tris);
            AddTriangle(vertices, indices,
                y * up + z * forward,
                zero,
                z * forward, ref tris);
            AddTriangle(vertices, indices,
                x * right + y * up + z * forward,
                x * right + z * forward,
                x * right, ref tris);
            if(renderBottom)
                AddFace(vertices, indices,
                    x * right + z * forward,
                    z * forward,
                    x * right,
                    zero, ref tris);
            AddFace(vertices, indices,
                x * right + y * up + z * forward,
                y * up + z * forward,
                x * right + z * forward,
                zero, ref tris);

            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Transform(
                Transforms.Translate(pos - .5f * (x * right + x * forward)));
        }

        private Mesh<V> GenerateLateralCenterMesh()
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();

            // line 0
            AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerTopY),
                (-lateralRadio, -lateralRadio), ref tris);
            AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerTopY),
                (lateralRadio, lateralRadio), ref tris, true);

            // line 1
            AddFaces(vertices, indices, (0, lateralRadio), (railsContainerTopY, railsContainerTopY),
                (-lateralRadio, -.5f * lateralRadio), ref tris);

            if (renderBottom)
                AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerBottomY),
                    (-lateralRadio, -.5f * lateralRadio), ref tris, true);

            // line 2
            AddFace(vertices, indices,
                leftScrewPoint + railsContainerTopY * up,
                leftScrewPoint + railsContainerTopY * up + lateralRadio * right,
                leftScrewPoint + .5f * lateralRadio * back + railsContainerTopY * up,
                leftScrewPoint + .5f * lateralRadio * back + railsContainerTopY * up + lateralRadio * right,
                ref tris);

            AddFace(vertices, indices,
                rightScrewPoint + railsContainerTopY * up + lateralRadio * left,
                rightScrewPoint + railsContainerTopY * up,
                rightScrewPoint + .5f * lateralRadio * back + railsContainerTopY * up + lateralRadio * left,
                rightScrewPoint + .5f * lateralRadio * back + railsContainerTopY * up,
                ref tris);

            if (renderBottom)
                AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerBottomY),
                    (-.5f * lateralRadio, 0), ref tris, true);

            // line 3
            AddFace(vertices, indices,
                leftScrewPoint + .5f * lateralRadio * forward + railsContainerTopY * up,
                leftScrewPoint + .5f * lateralRadio * forward + railsContainerTopY * up + lateralRadio * right,
                leftScrewPoint + railsContainerTopY * up,
                leftScrewPoint + railsContainerTopY * up + lateralRadio * right,
                ref tris);

            AddFace(vertices, indices,
                rightScrewPoint + .5f * lateralRadio * forward + railsContainerTopY * up + lateralRadio * left,
                rightScrewPoint + .5f * lateralRadio * forward + railsContainerTopY * up,
                rightScrewPoint + railsContainerTopY * up + lateralRadio * left,
                rightScrewPoint + railsContainerTopY * up,
                ref tris);

            if (renderBottom)
                AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerBottomY),
                    (0, .5f * lateralRadio), ref tris, true);

            // line 4
            AddFaces(vertices, indices, (0, lateralRadio), (railsContainerTopY, railsContainerTopY),
                (.5f * lateralRadio, lateralRadio), ref tris);

            if (renderBottom)
                AddFaces(vertices, indices, (0, lateralRadio), (railsContainerBottomY, railsContainerBottomY),
                    (.5f * lateralRadio, lateralRadio), ref tris, true);

            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(), indices.ToArray());
        }

        private Mesh<V> GenerateLateralInsideMesh()
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();

            var yMin = railsContainerBottomY + .25f * (railsContainerTopY - railsContainerBottomY);
            var yMax = railsContainerTopY;

            var zMin = -.5f * lateralRadio;
            var zMax = .5f * lateralRadio;

            var x = lateralRadio;
            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + x * right + yMax * up + zMin * forward,
                    leftScrewPoint + x * right + yMax * up,
                    leftScrewPoint + x * right + yMax * up + zMax * forward
                }, new[]
                {
                    leftScrewPoint + x * right + yMin * up + zMin * forward,
                    leftScrewPoint + x * right + yMin * up,
                    leftScrewPoint + x * right + yMin * up + zMax * forward
                }, ref tris
            );

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    rightScrewPoint + x * left + yMax * up + zMax * forward,
                    rightScrewPoint + x * left + yMax * up,
                    rightScrewPoint + x * left + yMax * up + zMin * forward
                }, new[]
                {
                    rightScrewPoint + x * left + yMin * up + zMax * forward,
                    rightScrewPoint + x * left + yMin * up,
                    rightScrewPoint + x * left + yMin * up + zMin * forward
                }, ref tris
            );

            AddFace(vertices, indices,
                leftScrewPoint + x * right + yMax * up + zMax * forward,
                rightScrewPoint + x * left + yMax * up + zMax * forward,
                leftScrewPoint + x * right + yMin * up + zMax * forward,
                rightScrewPoint + x * left + yMin * up + zMax * forward, ref tris
            );
            
            AddFace(vertices, indices,
                rightScrewPoint + x * left + yMax * up + zMin * forward,
                leftScrewPoint + x * right + yMax * up + zMin * forward,
                rightScrewPoint + x * left + yMin * up + zMin * forward,
                leftScrewPoint + x * right + yMin * up + zMin * forward, ref tris
            );
            
            AddFace(vertices, indices,
                leftScrewPoint + x * right + yMin * up,
                rightScrewPoint + x * left + yMin * up,
                leftScrewPoint + x * right + yMin * up + zMin * forward,
                rightScrewPoint + x * left + yMin * up + zMin * forward, ref tris
            );

            AddFace(vertices, indices,
                leftScrewPoint + x * right + yMin * up + zMax * forward,
                rightScrewPoint + x * left + yMin * up + zMax * forward,
                leftScrewPoint + x * right + yMin * up,
                rightScrewPoint + x * left + yMin * up, ref tris
            );

            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(), indices.ToArray());
        }
    }
}