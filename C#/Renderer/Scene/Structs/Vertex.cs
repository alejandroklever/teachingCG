using GMath;
using Rendering;

namespace Renderer.Scene
{
    public struct Vertex : IVertex<Vertex>
    {
        public float3 Position { get; set; }

        public Vertex Add(Vertex other)
        {
            return new()
            {
                Position = Position + other.Position,
            };
        }

        public Vertex Mul(float s)
        {
            return new()
            {
                Position = Position * s,
            };
        }
    }
}