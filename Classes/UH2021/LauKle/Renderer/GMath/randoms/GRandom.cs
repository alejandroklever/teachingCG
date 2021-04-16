using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;

namespace GMath
{
    public partial class GRandom
    {
        Random rnd = new Random();

        public GRandom(int seed)
        {
            rnd = new Random(seed);
        }
        public GRandom()
        {
            rnd = new Random();
        }

        public float random()
        {
            return (float)rnd.NextDouble();
        }

        #region Uniform Distributions

        public float random(float min, float max)
        {
            return random() * (max - min) + min;
        }

        public float2 random2()
        {
            return float2(random(), random());
        }

        public float2 random2(float2 min, float2 max)
        {
            return random2() * (max - min) + min;
        }

        public float3 random3()
        {
            return float3(random(), random(), random());
        }

        public float3 random3(float3 min, float3 max)
        {
            return random3() * (max - min) + min;
        }

        public float4 random4()
        {
            return float4(random(), random(), random(), random());
        }

        public float4 random4(float4 min, float4 max)
        {
            return random4() * (max - min) + min;
        }

        #endregion

        #region Random Directions

        /// <summary>
        /// Gets a random uniform direction
        /// </summary>
        public float3 randomDirection()
        {
            while (true)
            {
                float3 x = random3(-1, 1);
                float lsqr = dot(x, x);
                if (lsqr > 0.01 && lsqr < 1)
                    return normalize(x);// randomHSDirection(x / sqrt(lsqr), s);
            }
        }

        /// <summary>
        /// Gets a random uniform direction using a specific axis.
        /// The distribution is the same but this overload can be used to reduce the accumulated error in a VPT for instance.
        /// </summary>
        public float3 randomDirection(float3 D)
        {
            float r1 = random();
            float r2 = random() * 2 - 1;
            float sqrR2 = r2 * r2;
            float two_pi_by_r1 = two_pi * r1;
            float sqrt_of_one_minus_sqrR2 = sqrt(1.0f - sqrR2);
            float x = cos(two_pi_by_r1) * sqrt_of_one_minus_sqrR2;
            float y = sin(two_pi_by_r1) * sqrt_of_one_minus_sqrR2;
            float z = r2;

            float3 t0, t1;
            createOrthoBasis(D, out t0, out t1);

            return t0 * x + t1 * y + D * z;
        }

        /// <summary>
        /// Random uniform direction in a Hemisphere
        /// </summary>
        public float3 randomHSDirection(float3 N, out float NdotD)
        {
            float r1 = random();
            float r2 = random() * 2 - 1;
            float sqrR2 = r2 * r2;
            float two_pi_by_r1 = two_pi * r1;
            float sqrt_of_one_minus_sqrR2 = sqrt(1.0f - sqrR2);
            float x = cos(two_pi_by_r1) * sqrt_of_one_minus_sqrR2;
            float y = sin(two_pi_by_r1) * sqrt_of_one_minus_sqrR2;
            float z = r2;
            float3 d = float3(x, y, z);
            NdotD = dot(N, d);
            d *= (NdotD < 0 ? -1 : 1);
            NdotD *= NdotD < 0 ? -1 : 1;
            return d;
        }

        /// <summary>
        /// Random uniform direction in a Hemisphere
        /// </summary>
        public float3 randomHSDirection(float3 N)
        {
            float NdotD;
            return randomHSDirection(N, out NdotD);
        }


        #endregion

        #region Gaussian Distributions

        #region Box-Muller
        float2 BM_2()
        {
            float u1 = 1.0f - random(); //uniform(0,1] random doubles
            float u2 = 1.0f - random();
            float r = sqrt(-2.0f * log(max(0.0000000001f, u1)));
            float t = 2.0f * pi * u2;
            return r * float2(cos(t), sin(t));
        }
        float4 BM_4()
        {
            float2 u1 = 1.0f - random2(); //uniform(0,1] random doubles
            float2 u2 = 1.0f - random2();

            float2 r = sqrt(-2.0f * log(max(0.0000000001f, u1)));
            float2 t = 2.0f * pi * u2;
            return float4(r.x * cos(t.x), r.x * sin(t.x), r.y * cos(t.y), r.y * sin(t.y));
        }
        #endregion

        public float gauss()
        {
            return BM_2().x;
        }

        public float3 gauss3()
        {
            return BM_4().xyz;
        }

        public float2 gauss2()
        {
            return BM_2();
        }

        public float4 gauss4()
        {
            return BM_4();
        }

        public float gauss(float mu, float sigma)
        {
            return gauss() * sigma + mu;
        }

        public float2 gauss2(float2 mu, float2 sigma)
        {
            return gauss2() * sigma + mu;
        }

        public float3 gauss3(float3 mu, float3 sigma)
        {
            return gauss3() * sigma + mu;
        }

        public float4 gauss4(float4 mu, float4 sigma)
        {
            return gauss4() * sigma + mu;
        }

        #endregion
    }
}
