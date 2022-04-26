using System.Collections.Generic;
using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public struct Material
    {
        public float3 Emissive;

        public Texture2D DiffuseMap;
        public Texture2D BumpMap;
        public Sampler TextureSampler;

        public float3 Diffuse;
        public float3 Specular;
        public float SpecularPower;
        public float RefractionIndex;

        // 4 float values with Diffuseness, Glossyness, Mirrorness, Fresnelness
        public float WeightDiffuse
        {
            get { return 1 - OneMinusWeightDiffuse; }
            set { OneMinusWeightDiffuse = 1 - value; }
        }

        float
            OneMinusWeightDiffuse; // This is intended for default values of the struct to work as 1, 0, 0, 0 weight initial settings

        public float WeightGlossy;
        public float WeightMirror;
        public float WeightFresnel;

        public float WeightNormalization
        {
            get { return max(0.0001f, WeightDiffuse + WeightGlossy + WeightMirror + WeightFresnel); }
        }

        public float3 EvalBRDF(PositionNormalCoordinate surfel, float3 wout, float3 win)
        {
            var diffuse = Diffuse * (DiffuseMap == null
                ? float3(1, 1, 1)
                : DiffuseMap.Sample(TextureSampler, surfel.Coordinates).xyz) / pi;
            var H = normalize(win + wout);
            var specular = Specular * pow(max(0, dot(H, surfel.Normal)), SpecularPower) * (SpecularPower + 2) /
                           two_pi;
            return diffuse * WeightDiffuse / WeightNormalization + specular * WeightGlossy / WeightNormalization;
        }

        // Compute fresnel reflection component given the cosine of input direction and refraction index ratio.
        // Refraction can be obtained subtracting to one.
        // Uses the Schlick's approximation
        float ComputeFresnel(float NdotL, float ratio)
        {
            var f = pow((1 - ratio) / (1 + ratio), 2);
            return f + (1.0f - f) * pow(1.0f - NdotL, 5);
        }

        public IEnumerable<Impulse> GetBRDFImpulses(PositionNormalCoordinate surfel, float3 wout)
        {
            if (!any(Specular))
                yield break; // No specular => Ratio == 0

            var NdotL = dot(surfel.Normal, wout);
            // Check if ray is entering the medium or leaving
            var entering = NdotL > 0;

            // Invert all data if leaving
            NdotL = entering ? NdotL : -NdotL;
            surfel.Normal = entering ? surfel.Normal : -surfel.Normal;
            var ratio =
                entering
                    ? 1.0f / RefractionIndex
                    : RefractionIndex / 1.0f; // 1.0f air refraction index approx

            // Reflection vector
            var R = reflect(wout, surfel.Normal);

            // Refraction vector
            var T = refract(wout, surfel.Normal, ratio);

            // Reflection quantity, (1 - F) will be the refracted quantity.
            var F = ComputeFresnel(NdotL, ratio);

            if (!any(T))
                F = 1; // total internal reflection (produced with critical angles)

            if (WeightMirror + WeightFresnel * F > 0) // something is reflected
                yield return new Impulse
                {
                    Direction = R,
                    Ratio = Specular * (WeightMirror + WeightFresnel * F) / WeightNormalization / NdotL
                };

            if (WeightFresnel * (1 - F) > 0) // something to refract
                yield return new Impulse
                {
                    Direction = T,
                    Ratio = Specular * WeightFresnel * (1 - F) / WeightNormalization / -dot(surfel.Normal, T)
                };
        }

        /// <summary>
        /// Scatter a ray using the BRDF and Impulses
        /// </summary>
        public ScatteredRay Scatter(PositionNormalCoordinate surfel, float3 w)
        {
            var selection = random();
            float impulseProb = 0;

            foreach (var impulse in GetBRDFImpulses(surfel, w))
            {
                var pdf = (impulse.Ratio.x + impulse.Ratio.y + impulse.Ratio.z) / 3;
                if (selection < pdf) // this impulse is choosen
                    return new ScatteredRay
                    {
                        Ratio = impulse.Ratio,
                        Direction = impulse.Direction,
                        PDF = pdf
                    };
                selection -= pdf;
                impulseProb += pdf;
            }

            var wout = randomHSDirection(surfel.Normal);
            /// BRDF uniform sampling
            return new ScatteredRay
            {
                Direction = wout,
                Ratio = EvalBRDF(surfel, wout, w),
                PDF = (1 - impulseProb) / (2 * pi)
            };
        }

    }
}