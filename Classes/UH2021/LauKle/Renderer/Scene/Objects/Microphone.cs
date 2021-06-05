using System;
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

            var rightCubePos = rightCube.Center();
            rightCube.UpdateTranslation(rightCubePos);
            rightCube.Mesh = rightCube.Mesh.Transform(rightCube.Transform.Matrix);
            
            var leftCubePos = leftCube.Center();
            leftCube.UpdateTranslation(leftCubePos);
            leftCube.Mesh = leftCube.Mesh.Transform(leftCube.Transform.Matrix);
            
            
            var frontCubePos = frontCube.Center();
            frontCube.UpdateTranslation(frontCubePos);
            frontCube.Mesh = frontCube.Mesh.Transform(frontCube.Transform.Matrix);
            
            var backCubePos = backCube.Center();
            backCube.UpdateTranslation(backCubePos);
            backCube.Mesh = backCube.Mesh.Transform(backCube.Transform.Matrix);

            Mesh = MeshTools.Join(centerCube.Mesh, rightCube.Mesh, frontCube.Mesh, leftCube.Mesh, backCube.Mesh).Weld();
            Mesh.ComputeNormals();
            UpdateTranslation();
        }
    }
}