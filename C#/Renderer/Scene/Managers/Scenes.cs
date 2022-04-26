using Rendering;
using System.IO;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public static class Scenes
    {
        static float3 LightPosition = float3(0, 5, 3);
        static float3 LightIntensity = float3(1, 1, 1) * 100;

        public static void CreateMeshScene(Scene<PositionNormalCoordinate, Material> scene, float3[] LightsPositions,
            float3[] LightsIntensities)
        {
            const bool renderBottom = false;

            var sphereModel = Raycasting.UnitarySphere.AttributesMap(a => new PositionNormalCoordinate
                {Position = a, Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), Normal = normalize(a)});

            # region Lights

            for (var i = 0; i < LightsPositions.Length; i++)
            {
                scene.Add(sphereModel, new Material
                    {
                        Emissive = LightsIntensities[i] / (4 * pi), // power per unit area
                        WeightDiffuse = 0,
                        WeightFresnel = 1.0f, // Glass sphere
                        RefractionIndex = 1.0f
                    },
                    mul(Transforms.Scale(1, 1f, 1), Transforms.Translate(LightsPositions[i])));
            }

            #endregion

            #region Planes

            scene.Add(
                Raycasting.PlaneXZ.AttributesMap(a => new PositionNormalCoordinate
                    {Position = a, Coordinates = float2(a.x * 0.2f, a.z * 0.2f), Normal = float3(0, 1, 0)}),
                Materials.WoodTexture,
                Transforms.Identity);

            scene.Add(
                Raycasting.PlaneYZ.AttributesMap(a => new PositionNormalCoordinate
                {
                    Position = a, Coordinates = float2(a.z * 0.2f, a.y * 0.2f), Normal = float3(1, 0, 0)
                }),
                Materials.WallTexture,
                Transforms.Identity);

            scene.Add(
                Raycasting.PlaneYZ.AttributesMap(a => new PositionNormalCoordinate
                {
                    Position = a, Coordinates = float2(a.z * 0.2f, a.y * 0.2f), Normal = float3(-1, 0, 0)
                }),
                Materials.WallTexture,
                Transforms.Translate(20 * float3.right)
                // Transforms.Identity
                    );
            
            scene.Add(
                Raycasting.PlaneXY.AttributesMap(a => new PositionNormalCoordinate
                {
                    Position = a, Coordinates = float2(a.x * 0.2f, a.y * 0.2f),
                    Normal = float3(0, 0, -1)
                }),
                Materials.WallTexture,
                Transforms.Identity);

            #endregion

            #region Guitar

            var guitarGeneralTransform = mul(Transforms.Translate(2.4f * float3.down),
                Transforms.Rotate(-pi / 20, float3(-1, 0, -1)));

            var guitarBody = new GuitarBody<PositionNormalCoordinate>(
                new Transform
                {
                    Position = float3(6, 4, -10),
                    Rotation = float3(0, -pi / 3, 0)
                }, 12, 1, 3, renderBottom);
            guitarBody.ApplyTransform();
            guitarBody.ApplyTransform(guitarGeneralTransform);
            guitarBody.ComputeNormals();
            guitarBody.Log("Guitar Body");
            guitarBody.AddMeshesToScene(scene);

            var fretboard = new Fretboard<PositionNormalCoordinate>(
                new Transform
                {
                    Parent = guitarBody.Transform,
                    Position = 1.2f * float3.up + .05f * float3.forward,
                    Rotation = float3(-pi / 64, 0, 0)
                }, .4f, 6f, .5f, 22, 3, true);
            fretboard.ApplyTransform();
            fretboard.ApplyTransform(guitarGeneralTransform);
            fretboard.ComputeNormals();
            fretboard.Log("Fretboard");
            fretboard.AddMeshesToScene(scene);

            var head = new Head<PositionNormalCoordinate>(new Transform
            {
                Parent = fretboard.Transform,
                Position = 6f * float3.up,
                Rotation = float3(-pi / 64, 0, 0)
            }, 5, .4f, 1.5f, .5f, true, true);
            head.ApplyTransform();
            head.ApplyTransform(guitarGeneralTransform);
            head.ComputeNormals();
            head.Log("Head");
            head.AddMeshesToScene(scene);

            #endregion

            #region Amplifier

            var amplifier = new Amplifier<PositionNormalCoordinate>(
                new Transform
                {
                    Position = float3(10, 0, -10),
                    Rotation = float3(0, -pi / 3, 0),
                });
            amplifier.ApplyTransform();
            amplifier.ApplyTransform(Transforms.Scale(.5f));
            amplifier.ComputeNormals();
            amplifier.Log("Amplifier");
            amplifier.AddMeshesToScene(scene);

            #endregion
        }

        public static void CreateRaycastScene(Scene<PositionNormalCoordinate, Material> scene, float3[] LightsPositions,
            float3[] LightsIntensities)
        {
            Texture2D planeTexture = Texture2D.LoadFromFile(Path.Combine("Assets", "Textures", "wood.jpeg"));
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

            scene.Add(sphereModel, new Material
                {
                    Emissive = LightsIntensities[0] / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(2.4f, 0.4f, 2.4f), Transforms.Translate(LightsPositions[0])));
            
            scene.Add(sphereModel, new Material
                {
                    Emissive = LightsIntensities[0] / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(1f, 1f, 1f), Transforms.Translate(float3(5, 1, 0))));
        }
    }
}