using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Rendering;
using static GMath.Gfx;
using static Renderer.Scene.MeshTools;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Bridge<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private readonly float3 leftScrewPoint = 3 * float3.left;
        private readonly float3 rightScrewPoint = 3 * float3.right;
        
        private const float baseHeight = .5f;
        
        private const int topRoundness = 6;
        
        private const int lateralRoundness = 10;
        private const float lateralRadio = 1f;
        
        private const int screwRoundness = 8;
        private const float screwHeight = .75f;
        private const float screwRadio = .75f;

        private readonly bool renderBottom;

        public Bridge(Transform transform, bool renderBottom = true) : base(transform)
        {
            this.renderBottom = renderBottom;

            var leftMesh = MyManifold<V>.Cylinder(lateralRoundness, 1, leftScrewPoint, lateralRadio, baseHeight,
                (pi / 2, 3 * pi / 2), renderBottom);
            if (!renderBottom)
                leftMesh = leftMesh.Concat(
                    MyManifold<V>.Disc(lateralRoundness, 1, leftScrewPoint + baseHeight * up, lateralRadio,
                        (3 * pi / 2, two_pi + pi / 2)));

            var rightMesh = MyManifold<V>.Cylinder(lateralRoundness, 1, rightScrewPoint, lateralRadio, baseHeight,
                (3 * pi / 2, two_pi + pi / 2), renderBottom);
            if (!renderBottom)
                rightMesh = rightMesh.Concat(
                    MyManifold<V>.Disc(lateralRoundness, 1, rightScrewPoint + baseHeight * up, lateralRadio,
                        (3 * pi / 2, two_pi + pi / 2)));

            var lateralMesh = LateralMesh();
            var topMesh = TopMesh();
            var rightScrew = RightScrew();
            var leftScrew = LeftScrew();

            var body = Join(leftMesh, rightMesh, lateralMesh, topMesh).Weld();

            AddMesh(leftScrew, Materials.Metallic);
            AddMesh(rightScrew, Materials.Metallic);
            AddMesh(body, Materials.Metallic);

            // Mesh = Join(leftMesh, rightMesh, lateralMesh, topMesh, leftScrew, rightScrew).Weld();

            UpdateTranslation(zero);
        }

        private Mesh<V> RightScrew()
        {
            var lp = rightScrewPoint + .05f * left + screwHeight * up;
            var rp = rightScrewPoint + .05f * right + screwHeight * up;

            const float height = .15f;
            const float radio = 1.05f * screwRadio;
            var l = MyManifold<V>.Cylinder(3, 1, lp, radio, height, (pi / 2, 3 * pi / 2));

            var tris = 0;
            var v = new List<float3>();
            var i = new List<int>();
            var p0 = lp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2)) + height * up;
            var p1 = lp  + height * up;
            var p2 = lp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2));
            var p3 = lp;
            AddFace(v, i, p0, p1, p2, p3, ref tris);
            
            p0 = lp  + height * up;
            p1 = lp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2)) + height * up;
            p2 = lp;
            p3 = lp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2));
            AddFace(v, i, p0, p1, p2, p3, ref tris);
            
            var m1 = new Mesh<V>(v.Select(p => new V {Position = p}).ToArray(), i.ToArray());
            
            var r = MyManifold<V>.Cylinder(3, 1, rp, radio, height, (3 * pi / 2, two_pi + pi / 2));

            tris = 0;
            v = new List<float3>();
            i = new List<int>();
            p0 = rp + float3(radio * cos(pi / 2), 0, radio * sin( pi / 2)) + height * up;
            p1 = rp  + height * up;
            p2 = rp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2));
            p3 = rp;
            AddFace(v, i, p0, p1, p2, p3, ref tris);
            
            p0 = rp  + height * up;
            p1 = rp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2)) + height * up;
            p2 = rp;
            p3 = rp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2));
            AddFace(v, i, p0, p1, p2, p3, ref tris);
            
            var m2 = new Mesh<V>(v.Select(p => new V {Position = p}).ToArray(), i.ToArray());

            var body = MyManifold<V>.Cylinder(screwRoundness, 1, rightScrewPoint, screwRadio, screwHeight,
                createDiscs: renderBottom);

            if (!renderBottom)
                body = body.Concat(
                    MyManifold<V>.Disc(screwRoundness, 1, rightScrewPoint + screwHeight * up, screwRadio));
            
            return Join(body, l, r, m1, m2);
        }

        private Mesh<V> LeftScrew()
        {
            var lp = leftScrewPoint + .05f * left + screwHeight * up;
            var rp = leftScrewPoint + .05f * right + screwHeight * up;

            const float height = .15f;
            const float radio = 1.05f * screwRadio;
            var l = MyManifold<V>.Cylinder(3, 1, lp, radio, height, (pi / 2, 3 * pi / 2));

            var tris = 0;
            var v = new List<float3>();
            var i = new List<int>();
            var p0 = lp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2)) + height * up;
            var p1 = lp + height * up;
            var p2 = lp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2));
            var p3 = lp;
            AddFace(v, i, p0, p1, p2, p3, ref tris);

            p0 = lp + height * up;
            p1 = lp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2)) + height * up;
            p2 = lp;
            p3 = lp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2));
            AddFace(v, i, p0, p1, p2, p3, ref tris);

            var m1 = new Mesh<V>(v.Select(p => new V {Position = p}).ToArray(), i.ToArray());

            var r = MyManifold<V>.Cylinder(3, 1, rp, radio, height, (3 * pi / 2, two_pi + pi / 2));

            tris = 0;
            v = new List<float3>();
            i = new List<int>();
            p0 = rp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2)) + height * up;
            p1 = rp + height * up;
            p2 = rp + float3(radio * cos(pi / 2), 0, radio * sin(pi / 2));
            p3 = rp;
            AddFace(v, i, p0, p1, p2, p3, ref tris);

            p0 = rp + height * up;
            p1 = rp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2)) + height * up;
            p2 = rp;
            p3 = rp + float3(radio * cos(3 * pi / 2), 0, radio * sin(3 * pi / 2));
            AddFace(v, i, p0, p1, p2, p3, ref tris);

            var m2 = new Mesh<V>(v.Select(p => new V {Position = p}).ToArray(), i.ToArray());

            var body = MyManifold<V>.Cylinder(screwRoundness, 1, leftScrewPoint, screwRadio, screwHeight,
                createDiscs: renderBottom);

            if (!renderBottom)
                body = body.Concat(MyManifold<V>.Disc(screwRoundness, 1, leftScrewPoint + screwHeight * up,
                    screwRadio));

            return Join(body, l, r, m1, m2);
        }

        private Mesh<V> TopMesh()
        {
            var topBezier = new BezierCurve(
                back + baseHeight * up,
                back + (baseHeight + .75f) * up,
                forward + (baseHeight + .75f) * up,
                forward + baseHeight * up
            );

            return MyManifold<V>.Lofted(topRoundness, 1,
                    u => topBezier.GetPoint(u) + rightScrewPoint + left,
                    v => topBezier.GetPoint(v) + leftScrewPoint + right)
                .Concat(MyManifold<V>.Centred(topRoundness, 1, rightScrewPoint + left,
                    t => topBezier.GetPoint(t) + rightScrewPoint + left))
                .Concat(MyManifold<V>.Centred(topRoundness, 1, leftScrewPoint + right,
                    t => topBezier.GetPoint(1 - t) + leftScrewPoint + right));
        }

        private Mesh<V> LateralMesh()
        {
            var tris = 0;
            var vertices = new List<float3>();
            var indices = new List<int>();

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + back + baseHeight * up,
                    leftScrewPoint + back + baseHeight * up + right,
                    rightScrewPoint + back + baseHeight * up + left,
                    rightScrewPoint + back + baseHeight * up
                }, new[]
                {
                    leftScrewPoint + back,
                    leftScrewPoint + back + right,
                    rightScrewPoint + back + left,
                    rightScrewPoint + back
                }, ref tris);

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + baseHeight * up,
                    leftScrewPoint + baseHeight * up + right,
                }, new[]
                {
                    leftScrewPoint + back + baseHeight * up,
                    leftScrewPoint + back + baseHeight * up + right,
                }, ref tris);

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    rightScrewPoint + baseHeight * up + left,
                    rightScrewPoint + baseHeight * up
                }, new[]
                {
                    rightScrewPoint + back + baseHeight * up + left,
                    rightScrewPoint + back + baseHeight * up
                }, ref tris);

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + forward + baseHeight * up,
                    leftScrewPoint + forward + baseHeight * up + right
                }, new[]
                {
                    leftScrewPoint + baseHeight * up,
                    leftScrewPoint + baseHeight * up + right,
                }, ref tris);

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    rightScrewPoint + forward + baseHeight * up + left,
                    rightScrewPoint + forward + baseHeight * up

                }, new[]
                {
                    rightScrewPoint + baseHeight * up + left,
                    rightScrewPoint + baseHeight * up
                }, ref tris);

            AddFacesBetweenLines(vertices, indices,
                new[]
                {
                    leftScrewPoint + forward + baseHeight * up,
                    leftScrewPoint + forward + baseHeight * up + right,
                    rightScrewPoint + forward + baseHeight * up + left,
                    rightScrewPoint + forward + baseHeight * up
                }, new[]
                {
                    leftScrewPoint + forward,
                    leftScrewPoint + forward + right,
                    rightScrewPoint + forward + left,
                    rightScrewPoint + forward
                }, ref tris, true);
            
            if(renderBottom)
            {
                AddFacesBetweenLines(vertices, indices,
                    new[]
                    {
                        leftScrewPoint + back,
                        leftScrewPoint + back + right,
                        rightScrewPoint + back + left,
                        rightScrewPoint + back
                    }, new[]
                    {
                        leftScrewPoint,
                        leftScrewPoint + right,
                        rightScrewPoint + left,
                        rightScrewPoint
                    }, ref tris);

                AddFacesBetweenLines(vertices, indices,
                    new[]
                    {
                        leftScrewPoint,
                        leftScrewPoint + right,
                        rightScrewPoint + left,
                        rightScrewPoint
                    }, new[]
                    {
                        leftScrewPoint + forward,
                        leftScrewPoint + forward + right,
                        rightScrewPoint + forward + left,
                        rightScrewPoint + forward

                    }, ref tris);
            }
            return new Mesh<V>(vertices.Select(p => new V {Position = p}).ToArray(), indices.ToArray());
        }
    }
}