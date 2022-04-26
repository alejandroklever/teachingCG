using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public Transform Transform { get; set; }
        public float4x4 TransformMatrix => Transform.Matrix;
        public Mesh<V> Mesh { get; set; }

        public readonly List<Mesh<V>> meshes; 
        public readonly List<Material> materials;
        
        public IRaycastGeometry<V> RaycastGeometry => Mesh.AsRaycast();
        public float3 Position => Transform.Position;
        public float3 Rotation => Transform.Rotation;
        public float3 Scale => Transform.Scale;

        public SceneObject(Transform transform)
        {
            Transform = transform;
            meshes = new List<Mesh<V>>();
            materials = new List<Material>();
        }

        public void AddMeshesToScene(Scene<V, Material> scene)
        {
            foreach (var (mesh, material) in meshes.Zip(materials))
            {
                scene.Add(mesh.AsRaycast(), material, Transforms.Identity);
            }
        }

        public void AddMesh(Mesh<V> mesh, Material material)
        {
            meshes.Add(mesh);
            materials.Add(material);
        }

        public float3 Center
        {
            get
            {
                var minX = float.MaxValue;
                var maxX = float.MinValue;
            
                var minY = float.MaxValue;
                var maxY = float.MinValue;
            
                var minZ = float.MaxValue;
                var maxZ = float.MinValue;
                foreach (var mesh in meshes)
                {
                    foreach (var vertex in mesh.Vertices)
                    {
                        if (vertex.Position.x > maxX) maxX = vertex.Position.x;
                        if (vertex.Position.y > maxY) maxY = vertex.Position.y;
                        if (vertex.Position.z > maxZ) maxZ = vertex.Position.z;
                        if (vertex.Position.x < minX) minX = vertex.Position.x;
                        if (vertex.Position.y < minY) minY = vertex.Position.y;
                        if (vertex.Position.z < minZ) minZ = vertex.Position.z;
                    }
                }
                
                return (float3(minX, minY, minZ) + float3(maxX, maxY, maxZ)) / 2;
            }
        }

        public void Log(string id = "")
        {
            return;
            id = string.IsNullOrEmpty(id) ? $"mesh {GetHashCode()}" : id;
            Console.WriteLine($"{id} data:");
            Console.WriteLine($"  - {meshes.Sum(m => m.Vertices.Length)} points");
            Console.WriteLine($"  - {meshes.Sum(m => m.Indices.Length / 3)} polygons");
        }

        public void UpdateTranslation()
        {
            var center = Center;
            Transform.Translation = Transform.Position - center;
        }

        public void UpdateTranslation(float3 source)
        {
            Transform.Translation = Transform.Position - source;
        }

        public void ComputeNormals()
        {
            foreach (var mesh in meshes)
            {
                mesh.ComputeNormals();
            }
        }

        public void ApplyTransform()
        {
            for (var i = 0; i < meshes.Count; i++)
            {
                meshes[i] = meshes[i].Transform(TransformMatrix);
            }
        }

        public void ApplyTransform(float4x4 t)
        {
            for (var i = 0; i < meshes.Count; i++)
            {
                meshes[i] = meshes[i].Transform(t);
            }
        }

        /// <summary>
        /// Shortcut for (0, 0, 0) 
        /// </summary>
        public static float3 zero => float3.zero;

        /// <summary>
        /// Shortcut for (1, 1, 1) 
        /// </summary>
        public static float3 one => float3.one;

        /// <summary>
        /// Shortcut for (1, 0, 0) 
        /// </summary>
        public static float3 right => float3.right;

        /// <summary>
        /// Shortcut for (-1, 0, 0) 
        /// </summary>
        public static float3 left => float3.left;

        /// <summary>
        /// Shortcut for (0, 1, 0) 
        /// </summary>
        public static float3 up => float3.up;

        /// <summary>
        /// Shortcut for (0, -1, 0) 
        /// </summary>
        public static float3 down => float3.down;

        /// <summary>
        /// Shortcut for (0, 0, 1) 
        /// </summary>
        public static float3 forward => float3.forward;

        /// <summary>
        /// Shortcut for (0, 0, -1) 
        /// </summary>
        public static float3 back => float3.back;
    }
}
