using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class Scenes
    {
        public static void CreateMeshScene(Scene<PositionNormalCoordinate, Material> scene, float3 LightPosition,
            float3 LightIntensity)
        {
            const bool renderBottom = false;
            var planeTexture = Texture2D.LoadFromFile("wood.jpeg");

            var sphereModel = Raycasting.UnitarySphere.AttributesMap(a => new PositionNormalCoordinate
                {Position = a, Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), Normal = normalize(a)});

            // Light source
            scene.Add(sphereModel, new Material
                {
                    Emissive = 200 * float3.one / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(2.4f, .2f, 2.4f), Transforms.Translate(LightPosition)));
            
            scene.Add(sphereModel, new Material
                {
                    Emissive = LightIntensity / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(1f, 1f, 1f), Transforms.Translate(5, 3, -9)));

            #region Planes

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormalCoordinate
                    {Position = a, Coordinates = float2(a.x * 0.2f, a.z * 0.2f), Normal = float3(0, 1, 0)}),
                new Material
                {
                    DiffuseMap = planeTexture, Diffuse = float3(1, 1, 1),
                    TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
                },
                Transforms.Identity);

            scene.Add(
                Raycasting.PlaneYZ.AttributesMap(a => new PositionNormalCoordinate
                {
                    Position = a, Coordinates = float2(a.x * 0.2f, a.z * 0.2f), Normal = float3(0, 1, 0)
                }),
                new Material
                {
                    DiffuseMap = planeTexture, Diffuse = float3(1, 1, 1),
                    TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
                },
                Transforms.Identity);

            scene.Add(
                Raycasting.PlaneXY.AttributesMap(a => new PositionNormalCoordinate
                {
                    Position = a, Coordinates = float2(a.x * 0.2f, a.z * 0.2f),
                    Normal = float3(0, 1, 0)
                }),
                new Material
                {
                    DiffuseMap = planeTexture, Diffuse = float3(1, 1, 1),
                    TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
                },
                Transforms.Identity);

            #endregion

            var guitarBody = new GuitarBody<PositionNormalCoordinate>(
                new Transform
                {
                    Position = float3(5, 3, -5),
                    Rotation = float3(0, -pi_over_4, 0)
                }, 10, 1, renderBottom);
            guitarBody.ApplyTransform();
            guitarBody.ComputeNormals();
            guitarBody.Log("Guitar Body");
            guitarBody.AddMeshesToScene(scene);
            
            var fretboard = new Fretboard<PositionNormalCoordinate>(
                new Transform
                {
                    Parent = guitarBody.Transform,
                    Position = 1.2f * float3.up,
                    Rotation = float3(-pi / 64, 0, 0)
                }, .3f, 5f, .5f, 22, 3, true);
            fretboard.ApplyTransform();
            fretboard.ComputeNormals();
            fretboard.Log("Fretboard");
            fretboard.AddMeshesToScene(scene);

            var head = new Head<PositionNormalCoordinate>(new Transform
            {
                Parent = fretboard.Transform,
                Position = 5f * float3.up,
                Rotation = float3(-pi / 64, 0, 0)
            }, 5, .3f, 1.5f, .5f, true, true);
            head.ApplyTransform();
            head.ComputeNormals();
            head.Log("Head");
            head.AddMeshesToScene(scene);
        }

        public static void CreateRaycastScene(Scene<PositionNormalCoordinate, Material> scene, float3 LightPosition,
            float3 LightIntensity)
        {
            Texture2D planeTexture = Texture2D.LoadFromFile("wood.jpeg");

            var sphereModel = Raycasting.UnitarySphere.AttributesMap(a => new PositionNormalCoordinate
                {Position = a, Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), Normal = normalize(a)});

            // Adding elements of the scene
            scene.Add(sphereModel, new Material
                {
                    Specular = float3(1, 1, 1),
                    SpecularPower = 260,

                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.6f
                },
                Transforms.Translate(0, 1, -1.5f));

            scene.Add(sphereModel, new Material
                {
                    Specular = float3(1, 1, 1),
                    SpecularPower = 260,

                    WeightDiffuse = 0,
                    WeightMirror = 1.0f, // Mirror sphere
                },
                Transforms.Translate(1.5f, 1, 0));

            scene.Add(sphereModel, new Material
                {
                    Specular = float3(1, 1, 1) * 0.1f,
                    SpecularPower = 60,
                    Diffuse = float3(1, 1, 1)
                },
                Transforms.Translate(-1.5f, 1, 0));

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormalCoordinate
                    {Position = a, Coordinates = float2(a.x * 0.2f, a.z * 0.2f), Normal = float3(0, 1, 0)}),
                new Material
                {
                    DiffuseMap = planeTexture, Diffuse = float3(1, 1, 1),
                    TextureSampler = new Sampler {Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear}
                },
                Transforms.Identity);

            // Light source
            scene.Add(sphereModel, new Material
                {
                    Emissive = LightIntensity / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(2.4f, 0.4f, 2.4f), Transforms.Translate(LightPosition)));
        }
    }
}