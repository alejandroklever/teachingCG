using GMath;
using Rendering;

namespace Renderer.Scene
{
    public class Transform
    {
        public float3 Position = float3.zero;
        public float3 Rotation = float3.zero;
        public float3 Scale = float3.one;
        public float3 TranslationVector = float3.zero;
        public Transform Parent;
 
        public float4x4 Matrix => GetMatrix();

        private float4x4 GetMatrix()
        {
            var parent = new Transform();
            if (Parent != null)
                parent = Parent;
            
            return Transforms.DesiredTransform(Transforms.Identity, 
                parent.TranslationVector + TranslationVector,
                parent.Scale * Scale,
                parent.Rotation + Rotation);
        }
    }
}