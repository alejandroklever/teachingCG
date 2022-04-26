using GMath;
using Rendering;

namespace Renderer.Scene
{
    public struct ProjectedVertex : IProjectedVertex<ProjectedVertex>
    {
        public float4 Homogeneous { get; set; }

        public ProjectedVertex Add(ProjectedVertex other)
        {
            return new()
            {
                Homogeneous = Homogeneous + other.Homogeneous
            };
        }

        public ProjectedVertex Mul(float s)
        {
            return new()
            {
                Homogeneous = Homogeneous * s
            };
        }
    }
}