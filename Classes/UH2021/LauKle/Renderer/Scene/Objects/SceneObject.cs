using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class SceneObject<V> where V : struct, INormalVertex<V>
    {
        public List<Transform> Childs { get; set; }
        public Transform Transform { get; set; }
        public float4x4 TransformMatrix => Transform.Matrix;
        public Mesh<V> Mesh { get; set; }
        public IRaycastGeometry<V> RaycastGeometry => Mesh.AsRaycast();
        public float3 Position => Transform.Position;

        public SceneObject(Transform transform)
        {
            Transform = transform;
            Childs = new List<Transform>();
        }

        public virtual float3 Center()
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            
            var minY = float.MaxValue;
            var maxY = float.MinValue;
            
            var minZ = float.MaxValue;
            var maxZ = float.MinValue;

            foreach (var vertex in Mesh.Vertices)
            {
                if (vertex.Position.x > maxX) maxX = vertex.Position.x;
                if (vertex.Position.y > maxY) maxY = vertex.Position.y;
                if (vertex.Position.z > maxZ) maxZ = vertex.Position.z;
                if (vertex.Position.x < minX) minX = vertex.Position.x;
                if (vertex.Position.y < minY) minY = vertex.Position.y;
                if (vertex.Position.z < minZ) minZ = vertex.Position.z;
            }

            return (float3(minX, minY, minZ) + float3(maxX, maxY, maxZ)) / 2;
        }

        public void UpdateTranslation()
        {
            var center = Center();
            Transform.TranslationVector = Transform.Position - center;
        }
        
        public void UpdateTranslation(float3 source)
        {
            Transform.TranslationVector = Transform.Position - source;
        }

        public virtual void ComputeNormals()
        {
            Mesh.ComputeNormals();
        }
    }
}