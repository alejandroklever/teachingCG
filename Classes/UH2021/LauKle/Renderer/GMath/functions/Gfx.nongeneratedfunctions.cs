using System;

namespace GMath
{
    public static partial class Gfx
    {
        #region cross

        static Gfx()
        {
        }

        public static float3 cross(float3 pto1, float3 pto2)
        {
            return new float3(
                pto1.y * pto2.z - pto1.z * pto2.y,
                pto1.z * pto2.x - pto1.x * pto2.z,
                pto1.x * pto2.y - pto1.y * pto2.x);
        }
        #endregion

        #region determinant

        public static float determinant(float1x1 m)
        {
            return m._m00;
        }

        public static float determinant(float2x2 m)
        {
            return m._m00 * m._m11 - m._m01 * m._m10;
        }

        public static float determinant(float3x3 m)
        {
            // 00 01 02
            // 10 11 12
            // 20 21 22
            float Min00 = m._m11 * m._m22 - m._m12 * m._m21;
            float Min01 = m._m10 * m._m22 - m._m12 * m._m20;
            float Min02 = m._m10 * m._m21 - m._m11 * m._m20;

            return Min00 * m._m00 - Min01 * m._m01 + Min02 * m._m02;
        }

        public static float determinant(float4x4 m)
        {
            float Min00 = m._m11 * m._m22 * m._m33 + m._m12 * m._m23 * m._m31 + m._m13 * m._m21 * m._m32 -
                          m._m11 * m._m23 * m._m32 - m._m12 * m._m21 * m._m33 - m._m13 * m._m22 * m._m31;
            float Min01 = m._m10 * m._m22 * m._m33 + m._m12 * m._m23 * m._m30 + m._m13 * m._m20 * m._m32 -
                          m._m10 * m._m23 * m._m32 - m._m12 * m._m20 * m._m33 - m._m13 * m._m22 * m._m30;
            float Min02 = m._m10 * m._m21 * m._m33 + m._m11 * m._m23 * m._m30 + m._m13 * m._m20 * m._m31 -
                          m._m10 * m._m23 * m._m31 - m._m11 * m._m20 * m._m33 - m._m13 * m._m21 * m._m30;
            float Min03 = m._m10 * m._m21 * m._m32 + m._m11 * m._m22 * m._m30 + m._m12 * m._m20 * m._m31 -
                          m._m10 * m._m22 * m._m31 - m._m11 * m._m20 * m._m32 - m._m12 * m._m21 * m._m30;

            return Min00 * m._m00 - Min01 * m._m01 + Min02 * m._m02 - Min03 * m._m03;
        }

        #endregion

        #region faceNormal
        public static float3 faceNormal(float3 normal, float3 direction)
        {
            return dot(normal, direction) > 0 ? normal : -normal;
        }
        #endregion

        #region lit
        public static float4 lit(float NdotL, float NdotH, float power)
        {
            return new float4(1, NdotL < 0 ? 0 : NdotL, NdotL < 0 || NdotH < 0 ? 0 : (float)Math.Pow(NdotH, power), 1);
        }
        #endregion

        #region reflect

        /// <summary>
        /// Performs the reflect function to the specified float3 objects.
        /// Gets the reflection vector. L is direction to Light, N is the surface normal
        /// </summary>
        public static float3 reflect(float3 L, float3 N)
        {
            return 2 * dot(L, N) * N - L;
        }

        #endregion

        #region refract

        /// <summary>
        /// Performs the refract function to the specified float3 objects.
        /// Gets the refraction vector.
        /// L is direction to Light, N is the normal, eta is the refraction index factor.
        /// </summary>
        public static float3 refract(float3 L, float3 N, float eta)
        {
            float3 I = -1 * L;

            float cosAngle = dot(I, N);
            float delta = 1.0f - eta * eta * (1.0f - cosAngle * cosAngle);

            if (delta < 0)
                return new float3(0, 0, 0);

            return normalize(eta * I - N * (eta * cosAngle + (float)Math.Sqrt(delta)));
        }

        #endregion

        #region ortho basis

        public static float copysign(float f, float t)
        {
            return (float)Math.CopySign(f, t);
        }

        /// <summary>
        /// Given a direction, creates two othonormal vectors to it.
        /// From the paper: Building an Orthonormal Basis, Revisited, Tom Duff, et.al.
        /// </summary>
        public static void CreateOrthoBasis(float3 N, out float3 T, out float3 B)
        {
            float sign = copysign(1.0f, N.z);
            float a = -1.0f / (sign + N.z);
            float b = N.x * N.y * a;
            T = float3(1.0f + sign * N.x * N.x * a, sign * b, -sign * N.x);
            B = float3(b, sign + N.y * N.y * a, -N.y);
        }

        #endregion

        #region Randoms

        private static readonly GRandom __random = new GRandom();

        public static float random() => __random.random();

        public static float random(float a, float b) => a + (b - a) * random();

        public static float random(float a) => a * random();

        public static int random(int a) => (int) random(1f * a);

        public static int random(int a, int b) => (int)random((float)a, b);

        public static float2 random2()
        {
            return __random.random2();
        }

        /// <summary>
        /// Return a random point inside an unit cube
        /// </summary>
        public static float3 randomInBox()
        {
            float u = random() * 2 - 1, v = random() * 2 - 1;
            return random(6) switch
            {
                0 => float3(u, v, -1), // negZ
                1 => float3(u, v, 1), // posZ
                2 => float3(u, -1, v), // negY
                3 => float3(u, 1, v), // posY
                4 => float3(-1, u, v), // negX
                5 => float3(1, u, v), // posX
                _ => float3(0, 0, 0) // should never occur... but compiler doesn't know...
            };
        }

        /// <summary>
        /// Return a random point inside a cylinder of height 1f
        /// </summary>
        /// <param name="r">Radio of the cylinder</param>
        /// <param name="minAngle"></param>
        /// <param name="maxAngle"></param>
        /// <returns></returns>
        public static float3 randomInCylinder(float r = 1f, float minAngle = 0, float maxAngle = two_pi)
        {
            var (top, bottom) = (-1f, 1f);
            var theta = random(minAngle , maxAngle);
            var dr = sqrt(random()) * r;
            var dh = random(bottom, top);

            return random(3) switch
            {
                0 => float3(dr * cos(theta), top, dr * sin(theta)), // Top Base
                1 => float3(dr * cos(theta), bottom, dr * sin(theta)), // Bottom Base
                2 => float3(r * cos(theta), dh, r * sin(theta)), // Lateral
                _ => GMath.float3.zero
            };
            
        }

        #endregion
    }
}
