using System.Collections.Generic;
using GMath;
using Rendering;

namespace Renderer.Scene.Objects
{
    public class SceneObject<V> where V : struct, INormalVertex<V>
    {
        public List<Transform> Childs { get; protected set; }
        public Transform Transform { get; protected set; }
        public float4x4 TransformMatrix => Transform.Matrix;
        public Mesh<V> Mesh { get; protected set; }
        public IRaycastGeometry<V> RaycastGeometry => Mesh.AsRaycast();
        
        public SceneObject(Transform transform)
        {
            Transform = transform;
            Childs = new List<Transform>();
        }

    }

    public class Transform
    {
        public float3 Position = float3.zero;
        public float3 Rotation = float3.zero;
        public float3 Scale = float3.one;
        public Transform Parent;
        public float4x4 Matrix => GetMatrix();

        private float4x4 GetMatrix()
        {
            return Transforms.GetDesiredTransform(Transforms.Identity, Position, Scale, Rotation);
        }
    }
}