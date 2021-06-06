using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Renderer.Scene;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class Microphone<V>: SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Microphone(Transform transform) : base(transform)
        {
            var centerCube = new Cube<V>(new Transform(), 4, 4, 6, 1);
            var rightCube = new Cube<V>(new Transform {Position = float3(2.5f, 2, 0)}, 1, 1, centerCube.zSize + 2);
            var leftCube = new Cube<V>(new Transform {Position = float3(-2.5f, 2, 0)}, 1, 1, centerCube.zSize + 2);
            var frontCube = new Cube<V>(new Transform {Position = float3(0, 2, -3.5f)}, 4, 1, 1);
            var backCube = new Cube<V>(new Transform {Position = float3(0, 2, 3.5f)}, 4, 1, 1);
            
            var centerCubePos = centerCube.Center();
            centerCube.UpdateTranslation(centerCubePos);
            centerCube.Mesh = centerCube.Mesh.Transform(centerCube.Transform.Matrix);
            
            rightCube.Mesh = rightCube.Mesh.DeleteTriangles(new[]
            {
                (float3(0, 0, 1), float3(0, 1, 0), float3(0, 0, 0)),
                (float3(0, 0, 1), float3(0, 1, 1), float3(0, 1, 0)),
                (float3(0, 0, rightCube.zSize), float3(0, 1, rightCube.zSize), float3(0, 1, rightCube.zSize - 1)),
                (float3(0, 0, rightCube.zSize), float3(0, 1, rightCube.zSize - 1), float3(0, 0, rightCube.zSize - 1)),
            
            });
            var rightCubePos = rightCube.Center();
            rightCube.UpdateTranslation(rightCubePos);
            rightCube.Mesh = rightCube.Mesh.Transform(rightCube.Transform.Matrix);
            
            leftCube.Mesh = leftCube.Mesh.DeleteTriangles(new[]
            {
                (float3(1, 0, 0), float3(1, 1, 0), float3(1, 1, 1)),
                (float3(1, 0, 0), float3(1, 1, 1), float3(1, 0, 1)),
                (float3(1, 0, leftCube.zSize - 1), float3(1, 1, leftCube.zSize - 1), float3(1, 1, leftCube.zSize)),
                (float3(1, 0, leftCube.zSize - 1), float3(1, 1, leftCube.zSize), float3(1, 0, leftCube.zSize))
            
            });
            var leftCubePos = leftCube.Center();
            leftCube.UpdateTranslation(leftCubePos);
            leftCube.Mesh = leftCube.Mesh.Transform(leftCube.TransformMatrix);

            frontCube.Mesh = frontCube.Mesh.DeleteTriangles(new[]
            {
                (float3(0, 0, 1), float3(0, 1, 0), float3(0, 0, 0)),
                (float3(0, 0, 1), float3(0, 1, 1), float3(0, 1, 0)),
                (float3(frontCube.xSize, 0, 0), float3(frontCube.xSize, 1, 0), float3(frontCube.xSize, 1, 1)),
                (float3(frontCube.xSize, 0, 0), float3(frontCube.xSize, 1, 1), float3(frontCube.xSize, 0, 1)),
            });
            var frontCubePos = frontCube.Center();
            frontCube.UpdateTranslation(frontCubePos);
            frontCube.Mesh = frontCube.Mesh.Transform(frontCube.TransformMatrix);
            
            backCube.Mesh = backCube.Mesh.DeleteTriangles(new[]
            {
                (float3(0, 0, 1), float3(0, 1, 0), float3(0, 0, 0)),
                (float3(0, 0, 1), float3(0, 1, 1), float3(0, 1, 0)),
                (float3(backCube.xSize, 0, 0), float3(backCube.xSize, 1, 0), float3(backCube.xSize, 1, 1)),
                (float3(backCube.xSize, 0, 0), float3(backCube.xSize, 1, 1), float3(backCube.xSize, 0, 1)),
            });
            var backCubePos = backCube.Center();
            backCube.UpdateTranslation(backCubePos);
            backCube.Mesh = backCube.Mesh.Transform(backCube.TransformMatrix);

            Mesh = MeshTools.Join(centerCube.Mesh, frontCube.Mesh, rightCube.Mesh, backCube.Mesh, leftCube.Mesh).Weld();

            UpdateTranslation();
        }
    }
}