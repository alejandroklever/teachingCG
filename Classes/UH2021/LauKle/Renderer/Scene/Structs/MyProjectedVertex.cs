using GMath;
using Rendering;

namespace Renderer.Scene.Structs
{
    public struct MyProjectedVertex : IProjectedVertex<MyProjectedVertex>
    {
        public float4 Homogeneous { get; set; }

        public MyProjectedVertex Add(MyProjectedVertex other)
        {
            return new MyProjectedVertex
            {
                Homogeneous = Homogeneous + other.Homogeneous
            };
        }

        public MyProjectedVertex Mul(float s)
        {
            return new MyProjectedVertex
            {
                Homogeneous = Homogeneous * s
            };
        }
    }
}