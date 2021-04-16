using System;
using System.Collections.Generic;
using System.Text;
using GMath;
using static GMath.Gfx;

namespace Rendering
{
	static class Transforms
	{
		public static float4x4 Identity
        {
            get { return float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1); }
        }

		/// matrices
		/// <summary>
		/// Builds a translate mat using specified offsets.
		/// </summary>
		/// <param name="xslide">x offsets</param>
		/// <param name="yslide">y offsets</param>
		/// <param name="zslide">z offsets</param>
		/// <returns>A mat structure that contains a translated transformation </returns>
		public static float4x4 Translate(float xoffset, float yoffset, float zoffset)
		{
			return float4x4(
				1, 0, 0, 0,
				0, 1, 0, 0,
				0, 0, 1, 0,
				xoffset, yoffset, zoffset, 1
			);
		}

		/// <summary>
		/// Builds a translate mat using specified offsets.
		/// </summary>
		/// <param name="vec">A Vector structure that contains the x-coordinate, y-coordinate, and z-coordinate offsets.</param>
		/// <returns>A mat structure that contains a translated transformation </returns>
		public static float4x4 Translate(float3 vec)
		{
			return Translate(vec.x, vec.y, vec.z);
		}

		/// <summary>
		/// Builds a 4x4 matrix transform from an affine transformation.
		/// </summary>
		public static float4x4 FromAffine(float4x3 affine)
		{
			return float4x4(
				affine._m00, affine._m01, affine._m02, 0,
				affine._m10, affine._m11, affine._m12, 0,
				affine._m20, affine._m21, affine._m22, 0,
				affine._m30, affine._m31, affine._m32, 1
			);
		}

		// Rotations

		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in radian for rotation</param>
		public static float4x4 RotateZ(float alpha)
		{
			float c = cos(alpha);
			float s = sin(alpha);
			return float4x4(
				c, -s, 0, 0,
				s, c, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1
			);
		}
		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in grades for rotation</param>
		public static float4x4 RotateZGrad(float alpha)
		{
			return RotateZ(alpha * pi / 180);
		}
		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in radian for rotation</param>
		public static float4x4 RotateY(float alpha)
		{
			float c = cos(alpha);
			float s = sin(alpha);
			return float4x4(
				c, 0, -s, 0,
				0, 1, 0, 0,
				s, 0, c, 0,
				0, 0, 0, 1
			);
		}
		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in grades for rotation</param>
		public static float4x4 RotateYGrad(float alpha)
		{
			return RotateY(alpha * pi / 180);
		}
		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in radian for rotation</param>
		public static float4x4 RotateX(float alpha)
		{
			float c = cos(alpha);
			float s = sin(alpha);
			return float4x4(
				1, 0, 0, 0,
				0, c, -s, 0,
				0, s, c, 0,
				0, 0, 0, 1
			);
		}
		/// <summary>
		/// Rotation mat around Z axis
		/// </summary>
		/// <param name="alpha">value in grades for rotation</param>
		public static float4x4 RotateXGrad(float alpha)
		{
			return RotateX(alpha * pi / 180);
		}
		public static float4x4 Rotate(float angle, float3 dir)
		{
			float x = dir.x;
			float y = dir.y;
			float z = dir.z;
			float cos = Gfx.cos(angle);
			float sin = Gfx.sin(angle);

			return float4x4(
				x * x * (1 - cos) + cos, y * x * (1 - cos) + z * sin, z * x * (1 - cos) - y * sin, 0,
				x * y * (1 - cos) - z * sin, y * y * (1 - cos) + cos, z * y * (1 - cos) + x * sin, 0,
				x * z * (1 - cos) + y * sin, y * z * (1 - cos) - x * sin, z * z * (1 - cos) + cos, 0,
				0, 0, 0, 1
			);
		}
		public static float4x4 RotateRespectTo(float3 center, float3 direction, float angle)
		{
			return mul(Translate(center), mul(Rotate(angle, direction), Translate(center * -1.0f)));
		}

		public static float4x4 RotateGrad(float angle, float3 dir)
		{
			return Rotate(pi * angle / 180, dir);
		}

		//

		// Scale

		public static float4x4 Scale(float xscale, float yscale, float zscale)
		{
			return float4x4(
				xscale, 0, 0, 0,
				0, yscale, 0, 0,
				0, 0, zscale, 0,
				0, 0, 0, 1);
		}
		public static float4x4 Scale(float3 size)
		{
			return Scale(size.x, size.y, size.z);
		}

		public static float4x4 ScaleRespectTo(float3 center, float3 size)
		{
			return mul(mul(Translate(center), Scale(size)), Translate(center * -1));
		}
		public static float4x4 ScaleRespectTo(float3 center, float sx, float sy, float sz)
		{
			return ScaleRespectTo(center, float3(sx, sy, sz));
		}

		//

		// Viewing

		public static float4x4 LookAtLH(float3 camera, float3 target, float3 upVector)
		{
			float3 zaxis = normalize(target - camera);
			float3 xaxis = normalize(cross(upVector, zaxis));
			float3 yaxis = cross(zaxis, xaxis);

			return float4x4(
				xaxis.x, yaxis.x, zaxis.x, 0,
				xaxis.y, yaxis.y, zaxis.y, 0,
				xaxis.z, yaxis.z, zaxis.z, 0,
				-dot(xaxis, camera), -dot(yaxis, camera), -dot(zaxis, camera), 1);
		}

		public static float4x4 LookAtRH(float3 camera, float3 target, float3 upVector)
		{
			float3 zaxis = normalize(camera - target);
			float3 xaxis = normalize(cross(upVector, zaxis));
			float3 yaxis = cross(zaxis, xaxis);

			return float4x4(
				xaxis.x, yaxis.x, zaxis.x, 0,
				xaxis.y, yaxis.y, zaxis.y, 0,
				xaxis.z, yaxis.z, zaxis.z, 0,
				-dot(xaxis, camera), -dot(yaxis, camera), -dot(zaxis, camera), 1);
		}

		//

		// Projection Methods

		public static float4x4 PerspectiveFovLH(float fieldOfView, float aspectRatio, float znearPlane, float zfarPlane)
		{
			float h = 1.0f / tan(fieldOfView / 2);
			float w = h * aspectRatio;

			return float4x4(
				w, 0, 0, 0,
				0, h, 0, 0,
				0, 0, zfarPlane / (zfarPlane - znearPlane), 1,
				0, 0, -znearPlane * zfarPlane / (zfarPlane - znearPlane), 0);
		}

		public static float4x4 PerspectiveFovRH(float fieldOfView, float aspectRatio, float znearPlane, float zfarPlane)
		{
			float h = 1.0f / tan(fieldOfView / 2);
			float w = h * aspectRatio;

			return float4x4(
				w, 0, 0, 0,
				0, h, 0, 0,
				0, 0, zfarPlane / (znearPlane - zfarPlane), -1,
				0, 0, znearPlane * zfarPlane / (znearPlane - zfarPlane), 0);
		}

		public static float4x4 PerspectiveLH(float width, float height, float znearPlane, float zfarPlane)
		{
			return float4x4(
				2 * znearPlane / width, 0, 0, 0,
				0, 2 * znearPlane / height, 0, 0,
				0, 0, zfarPlane / (zfarPlane - znearPlane), 1,
				0, 0, znearPlane * zfarPlane / (znearPlane - zfarPlane), 0);
		}

		public static float4x4 PerspectiveRH(float width, float height, float znearPlane, float zfarPlane)
		{
			return float4x4(
				2 * znearPlane / width, 0, 0, 0,
				0, 2 * znearPlane / height, 0, 0,
				0, 0, zfarPlane / (znearPlane - zfarPlane), -1,
				0, 0, znearPlane * zfarPlane / (znearPlane - zfarPlane), 0);
		}

		public static float4x4 OrthoLH(float width, float height, float znearPlane, float zfarPlane)
		{
			return float4x4(
				2 / width, 0, 0, 0,
				0, 2 / height, 0, 0,
				0, 0, 1 / (zfarPlane - znearPlane), 0,
				0, 0, znearPlane / (znearPlane - zfarPlane), 1);
		}

		public static float4x4 OrthoRH(float width, float height, float znearPlane, float zfarPlane)
		{
			return float4x4(
				2 / width, 0, 0, 0,
				0, 2 / height, 0, 0,
				0, 0, 1 / (znearPlane - zfarPlane), 0,
				0, 0, znearPlane / (znearPlane - zfarPlane), 1);
		}

	}
}
