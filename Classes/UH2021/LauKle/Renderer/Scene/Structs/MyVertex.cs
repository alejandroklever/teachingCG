using GMath;
using Rendering;

namespace Renderer.Scene.Structs
{
    public struct MyVertex : IVertex<MyVertex>
    {
        public float3 Position { get; set; }

        public MyVertex Add(MyVertex other)
        {
            return new MyVertex
            {
                Position = Position + other.Position,
            };
        }

        public MyVertex Mul(float s)
        {
            return new MyVertex
            {
                Position = Position * s,
            };
        }
    }
}