using System.IO;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class Materials
    {
        public static Material Default => White;

        public static Material White => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,
            Diffuse = float3(1, 1, 1)
        };

        public static Material GlossyBlack => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,

            WeightGlossy = .3f,
            Diffuse = .0f * float3.one
        };

        public static Material Black => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,

            Diffuse = .1f * float3.one
        };

        public static Material FretsColor => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,

            Diffuse = float3(0.9803921568627451f, 0.9803921568627451f, 0.8235294117647058f)
        };

        public static Material GuitarRedMaterial => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,

            WeightGlossy = .2f,
            Diffuse = float3(0.67f, 0.133f, 0.133f)
        };

        public static Material SandyMaterial => new()
        {
            Specular = float3(1, 1, 1) * 0.1f,
            SpecularPower = 60,

            WeightGlossy = .5f,
            Diffuse = float3(0.9568627450980393f, 0.6431372549019608f, 0.3764705882352941f)
        };

        public static Material Glass => new()
        {
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            WeightDiffuse = 0,
            WeightFresnel = 1.0f, // Glass sphere
            RefractionIndex = 1.6f
        };

        public static Material Mirror => new()
        {
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            WeightDiffuse = 0,
            WeightMirror = 1.0f, // Mirror sphere
        };

        public static Material Metallic => new()
        {
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            WeightDiffuse = .75f,
            WeightGlossy = .75f,
            WeightMirror = 1,
            // Diffuse = float3.one// float3(.9f, .9f, .9f)
        };

        public static Material DarkLeatherTexture => new()
        {
            DiffuseMap = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "dark_leather.jpg")),
            Diffuse = float3(1, 1, 1),

            TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
        };

        public static Material BrownLeatherTexture => new()
        {
            DiffuseMap = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "brown_leather.jpg")),
            Diffuse = float3(1, 1, 1),

            TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
        };

        public static Material WallTexture => new()
        {
            DiffuseMap = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "wall.jpg")),
            Diffuse = float3(1, 1, 1),

            TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
        };

        public static Material AmpMeshTexture => new()
        {
            DiffuseMap = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "amp_mesh.jpg")),
            Diffuse = float3(1, 1, 1),

            TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
        };

        public static Material WoodTexture => new()
        {
            DiffuseMap = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "wood.jpeg")),
            Diffuse = float3(1, 1, 1),
            TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
        };
    }
}