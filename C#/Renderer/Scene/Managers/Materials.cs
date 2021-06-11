using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class Materials
    {
        public static Material Default => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,
            Diffuse = .25f * float3(1, 1, 1)
        };
        
        public static Material Black => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,
            Diffuse = .0f * float3.one
        };
        
        public static Material GuitarRedMaterial => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,
            Diffuse = float3(1, 0, 0)
        };

        public static Material Glass => new()
        {
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            WeightDiffuse = 0,
            WeightFresnel = 1.0f, // Glass sphere
            RefractionIndex = 1.6f
        };
    }
}