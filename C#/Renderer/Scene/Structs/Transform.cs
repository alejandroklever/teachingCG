using GMath;
using Rendering;

namespace Renderer.Scene
{
    public class Transform
    {
        public float3 Position = float3.zero;
        public float3 Rotation = float3.zero;
        public float3 Scale = float3.one;
        public float3 Translation = float3.zero;
        public Transform Parent;
 
        public float4x4 Matrix => GetMatrix();

        private float4x4 GetMatrix()
        {

            var transform = Parent?.Matrix ?? Transforms.Identity;
            
            return Transforms.DesiredTransform(transform, 
                translation: Translation,
                scale: Scale,
                eulerRotation: Rotation);
        }

        public static Transform Default => new();
    }
}