using System;
namespace GMath {
public static partial class Gfx {
	#region abs
	public static float abs(float v) { return (float)Math.Abs(v); }

	public static float1 abs(float1 v) { return new float1((float)Math.Abs(v.x)); }
	public static float2 abs(float2 v) { return new float2((float)Math.Abs(v.x), (float)Math.Abs(v.y)); }
	public static float3 abs(float3 v) { return new float3((float)Math.Abs(v.x), (float)Math.Abs(v.y), (float)Math.Abs(v.z)); }
	public static float4 abs(float4 v) { return new float4((float)Math.Abs(v.x), (float)Math.Abs(v.y), (float)Math.Abs(v.z), (float)Math.Abs(v.w)); }

	public static float1x1 abs(float1x1 v) { return new float1x1((float)Math.Abs(v._m00)); }
	public static float1x2 abs(float1x2 v) { return new float1x2((float)Math.Abs(v._m00), (float)Math.Abs(v._m01)); }
	public static float1x3 abs(float1x3 v) { return new float1x3((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02)); }
	public static float1x4 abs(float1x4 v) { return new float1x4((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m03)); }
	public static float2x1 abs(float2x1 v) { return new float2x1((float)Math.Abs(v._m00), (float)Math.Abs(v._m10)); }
	public static float2x2 abs(float2x2 v) { return new float2x2((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11)); }
	public static float2x3 abs(float2x3 v) { return new float2x3((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12)); }
	public static float2x4 abs(float2x4 v) { return new float2x4((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m03), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12), (float)Math.Abs(v._m13)); }
	public static float3x1 abs(float3x1 v) { return new float3x1((float)Math.Abs(v._m00), (float)Math.Abs(v._m10), (float)Math.Abs(v._m20)); }
	public static float3x2 abs(float3x2 v) { return new float3x2((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21)); }
	public static float3x3 abs(float3x3 v) { return new float3x3((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21), (float)Math.Abs(v._m22)); }
	public static float3x4 abs(float3x4 v) { return new float3x4((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m03), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12), (float)Math.Abs(v._m13), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21), (float)Math.Abs(v._m22), (float)Math.Abs(v._m23)); }
	public static float4x1 abs(float4x1 v) { return new float4x1((float)Math.Abs(v._m00), (float)Math.Abs(v._m10), (float)Math.Abs(v._m20), (float)Math.Abs(v._m30)); }
	public static float4x2 abs(float4x2 v) { return new float4x2((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21), (float)Math.Abs(v._m30), (float)Math.Abs(v._m31)); }
	public static float4x3 abs(float4x3 v) { return new float4x3((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21), (float)Math.Abs(v._m22), (float)Math.Abs(v._m30), (float)Math.Abs(v._m31), (float)Math.Abs(v._m32)); }
	public static float4x4 abs(float4x4 v) { return new float4x4((float)Math.Abs(v._m00), (float)Math.Abs(v._m01), (float)Math.Abs(v._m02), (float)Math.Abs(v._m03), (float)Math.Abs(v._m10), (float)Math.Abs(v._m11), (float)Math.Abs(v._m12), (float)Math.Abs(v._m13), (float)Math.Abs(v._m20), (float)Math.Abs(v._m21), (float)Math.Abs(v._m22), (float)Math.Abs(v._m23), (float)Math.Abs(v._m30), (float)Math.Abs(v._m31), (float)Math.Abs(v._m32), (float)Math.Abs(v._m33)); }

	#endregion


	#region absdot
	public static float absdot(float1 a, float1 b) { return abs(a.x * b.x); }
	public static float absdot(float2 a, float2 b) { return abs(a.x * b.x + a.y * b.y); }
	public static float absdot(float3 a, float3 b) { return abs(a.x * b.x + a.y * b.y + a.z * b.z); }
	public static float absdot(float4 a, float4 b) { return abs(a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w); }
	#endregion


	#region acos
	public static float acos(float v) { return (float)Math.Acos(v); }

	public static float1 acos(float1 v) { return new float1((float)Math.Acos(v.x)); }
	public static float2 acos(float2 v) { return new float2((float)Math.Acos(v.x), (float)Math.Acos(v.y)); }
	public static float3 acos(float3 v) { return new float3((float)Math.Acos(v.x), (float)Math.Acos(v.y), (float)Math.Acos(v.z)); }
	public static float4 acos(float4 v) { return new float4((float)Math.Acos(v.x), (float)Math.Acos(v.y), (float)Math.Acos(v.z), (float)Math.Acos(v.w)); }

	public static float1x1 acos(float1x1 v) { return new float1x1((float)Math.Acos(v._m00)); }
	public static float1x2 acos(float1x2 v) { return new float1x2((float)Math.Acos(v._m00), (float)Math.Acos(v._m01)); }
	public static float1x3 acos(float1x3 v) { return new float1x3((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02)); }
	public static float1x4 acos(float1x4 v) { return new float1x4((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m03)); }
	public static float2x1 acos(float2x1 v) { return new float2x1((float)Math.Acos(v._m00), (float)Math.Acos(v._m10)); }
	public static float2x2 acos(float2x2 v) { return new float2x2((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11)); }
	public static float2x3 acos(float2x3 v) { return new float2x3((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12)); }
	public static float2x4 acos(float2x4 v) { return new float2x4((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m03), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12), (float)Math.Acos(v._m13)); }
	public static float3x1 acos(float3x1 v) { return new float3x1((float)Math.Acos(v._m00), (float)Math.Acos(v._m10), (float)Math.Acos(v._m20)); }
	public static float3x2 acos(float3x2 v) { return new float3x2((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21)); }
	public static float3x3 acos(float3x3 v) { return new float3x3((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21), (float)Math.Acos(v._m22)); }
	public static float3x4 acos(float3x4 v) { return new float3x4((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m03), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12), (float)Math.Acos(v._m13), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21), (float)Math.Acos(v._m22), (float)Math.Acos(v._m23)); }
	public static float4x1 acos(float4x1 v) { return new float4x1((float)Math.Acos(v._m00), (float)Math.Acos(v._m10), (float)Math.Acos(v._m20), (float)Math.Acos(v._m30)); }
	public static float4x2 acos(float4x2 v) { return new float4x2((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21), (float)Math.Acos(v._m30), (float)Math.Acos(v._m31)); }
	public static float4x3 acos(float4x3 v) { return new float4x3((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21), (float)Math.Acos(v._m22), (float)Math.Acos(v._m30), (float)Math.Acos(v._m31), (float)Math.Acos(v._m32)); }
	public static float4x4 acos(float4x4 v) { return new float4x4((float)Math.Acos(v._m00), (float)Math.Acos(v._m01), (float)Math.Acos(v._m02), (float)Math.Acos(v._m03), (float)Math.Acos(v._m10), (float)Math.Acos(v._m11), (float)Math.Acos(v._m12), (float)Math.Acos(v._m13), (float)Math.Acos(v._m20), (float)Math.Acos(v._m21), (float)Math.Acos(v._m22), (float)Math.Acos(v._m23), (float)Math.Acos(v._m30), (float)Math.Acos(v._m31), (float)Math.Acos(v._m32), (float)Math.Acos(v._m33)); }

	#endregion


	#region all
	public static bool all(float1 v) { return (v.x != 0); }
	public static bool all(float2 v) { return (v.x != 0) && (v.y != 0); }
	public static bool all(float3 v) { return (v.x != 0) && (v.y != 0) && (v.z != 0); }
	public static bool all(float4 v) { return (v.x != 0) && (v.y != 0) && (v.z != 0) && (v.w != 0); }
	public static bool all(float1x1 m) { return (m._m00 != 0); }
	public static bool all(float1x2 m) { return (m._m00 != 0) && (m._m01 != 0); }
	public static bool all(float1x3 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0); }
	public static bool all(float1x4 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m03 != 0); }
	public static bool all(float2x1 m) { return (m._m00 != 0) && (m._m10 != 0); }
	public static bool all(float2x2 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m10 != 0) && (m._m11 != 0); }
	public static bool all(float2x3 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0); }
	public static bool all(float2x4 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m03 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0) && (m._m13 != 0); }
	public static bool all(float3x1 m) { return (m._m00 != 0) && (m._m10 != 0) && (m._m20 != 0); }
	public static bool all(float3x2 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m20 != 0) && (m._m21 != 0); }
	public static bool all(float3x3 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0) && (m._m20 != 0) && (m._m21 != 0) && (m._m22 != 0); }
	public static bool all(float3x4 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m03 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0) && (m._m13 != 0) && (m._m20 != 0) && (m._m21 != 0) && (m._m22 != 0) && (m._m23 != 0); }
	public static bool all(float4x1 m) { return (m._m00 != 0) && (m._m10 != 0) && (m._m20 != 0) && (m._m30 != 0); }
	public static bool all(float4x2 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m20 != 0) && (m._m21 != 0) && (m._m30 != 0) && (m._m31 != 0); }
	public static bool all(float4x3 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0) && (m._m20 != 0) && (m._m21 != 0) && (m._m22 != 0) && (m._m30 != 0) && (m._m31 != 0) && (m._m32 != 0); }
	public static bool all(float4x4 m) { return (m._m00 != 0) && (m._m01 != 0) && (m._m02 != 0) && (m._m03 != 0) && (m._m10 != 0) && (m._m11 != 0) && (m._m12 != 0) && (m._m13 != 0) && (m._m20 != 0) && (m._m21 != 0) && (m._m22 != 0) && (m._m23 != 0) && (m._m30 != 0) && (m._m31 != 0) && (m._m32 != 0) && (m._m33 != 0); }
	#endregion


	#region any
	public static bool any(float1 v) { return (v.x != 0); }
	public static bool any(float2 v) { return (v.x != 0) || (v.y != 0); }
	public static bool any(float3 v) { return (v.x != 0) || (v.y != 0) || (v.z != 0); }
	public static bool any(float4 v) { return (v.x != 0) || (v.y != 0) || (v.z != 0) || (v.w != 0); }
	public static bool any(float1x1 m) { return (m._m00 != 0); }
	public static bool any(float1x2 m) { return (m._m00 != 0) || (m._m01 != 0); }
	public static bool any(float1x3 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0); }
	public static bool any(float1x4 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m03 != 0); }
	public static bool any(float2x1 m) { return (m._m00 != 0) || (m._m10 != 0); }
	public static bool any(float2x2 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m10 != 0) || (m._m11 != 0); }
	public static bool any(float2x3 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0); }
	public static bool any(float2x4 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m03 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0) || (m._m13 != 0); }
	public static bool any(float3x1 m) { return (m._m00 != 0) || (m._m10 != 0) || (m._m20 != 0); }
	public static bool any(float3x2 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m20 != 0) || (m._m21 != 0); }
	public static bool any(float3x3 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0) || (m._m20 != 0) || (m._m21 != 0) || (m._m22 != 0); }
	public static bool any(float3x4 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m03 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0) || (m._m13 != 0) || (m._m20 != 0) || (m._m21 != 0) || (m._m22 != 0) || (m._m23 != 0); }
	public static bool any(float4x1 m) { return (m._m00 != 0) || (m._m10 != 0) || (m._m20 != 0) || (m._m30 != 0); }
	public static bool any(float4x2 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m20 != 0) || (m._m21 != 0) || (m._m30 != 0) || (m._m31 != 0); }
	public static bool any(float4x3 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0) || (m._m20 != 0) || (m._m21 != 0) || (m._m22 != 0) || (m._m30 != 0) || (m._m31 != 0) || (m._m32 != 0); }
	public static bool any(float4x4 m) { return (m._m00 != 0) || (m._m01 != 0) || (m._m02 != 0) || (m._m03 != 0) || (m._m10 != 0) || (m._m11 != 0) || (m._m12 != 0) || (m._m13 != 0) || (m._m20 != 0) || (m._m21 != 0) || (m._m22 != 0) || (m._m23 != 0) || (m._m30 != 0) || (m._m31 != 0) || (m._m32 != 0) || (m._m33 != 0); }
	#endregion


	#region asin
	public static float asin(float v) { return (float)Math.Asin(v); }

	public static float1 asin(float1 v) { return new float1((float)Math.Asin(v.x)); }
	public static float2 asin(float2 v) { return new float2((float)Math.Asin(v.x), (float)Math.Asin(v.y)); }
	public static float3 asin(float3 v) { return new float3((float)Math.Asin(v.x), (float)Math.Asin(v.y), (float)Math.Asin(v.z)); }
	public static float4 asin(float4 v) { return new float4((float)Math.Asin(v.x), (float)Math.Asin(v.y), (float)Math.Asin(v.z), (float)Math.Asin(v.w)); }

	public static float1x1 asin(float1x1 v) { return new float1x1((float)Math.Asin(v._m00)); }
	public static float1x2 asin(float1x2 v) { return new float1x2((float)Math.Asin(v._m00), (float)Math.Asin(v._m01)); }
	public static float1x3 asin(float1x3 v) { return new float1x3((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02)); }
	public static float1x4 asin(float1x4 v) { return new float1x4((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m03)); }
	public static float2x1 asin(float2x1 v) { return new float2x1((float)Math.Asin(v._m00), (float)Math.Asin(v._m10)); }
	public static float2x2 asin(float2x2 v) { return new float2x2((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11)); }
	public static float2x3 asin(float2x3 v) { return new float2x3((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12)); }
	public static float2x4 asin(float2x4 v) { return new float2x4((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m03), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12), (float)Math.Asin(v._m13)); }
	public static float3x1 asin(float3x1 v) { return new float3x1((float)Math.Asin(v._m00), (float)Math.Asin(v._m10), (float)Math.Asin(v._m20)); }
	public static float3x2 asin(float3x2 v) { return new float3x2((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21)); }
	public static float3x3 asin(float3x3 v) { return new float3x3((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21), (float)Math.Asin(v._m22)); }
	public static float3x4 asin(float3x4 v) { return new float3x4((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m03), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12), (float)Math.Asin(v._m13), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21), (float)Math.Asin(v._m22), (float)Math.Asin(v._m23)); }
	public static float4x1 asin(float4x1 v) { return new float4x1((float)Math.Asin(v._m00), (float)Math.Asin(v._m10), (float)Math.Asin(v._m20), (float)Math.Asin(v._m30)); }
	public static float4x2 asin(float4x2 v) { return new float4x2((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21), (float)Math.Asin(v._m30), (float)Math.Asin(v._m31)); }
	public static float4x3 asin(float4x3 v) { return new float4x3((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21), (float)Math.Asin(v._m22), (float)Math.Asin(v._m30), (float)Math.Asin(v._m31), (float)Math.Asin(v._m32)); }
	public static float4x4 asin(float4x4 v) { return new float4x4((float)Math.Asin(v._m00), (float)Math.Asin(v._m01), (float)Math.Asin(v._m02), (float)Math.Asin(v._m03), (float)Math.Asin(v._m10), (float)Math.Asin(v._m11), (float)Math.Asin(v._m12), (float)Math.Asin(v._m13), (float)Math.Asin(v._m20), (float)Math.Asin(v._m21), (float)Math.Asin(v._m22), (float)Math.Asin(v._m23), (float)Math.Asin(v._m30), (float)Math.Asin(v._m31), (float)Math.Asin(v._m32), (float)Math.Asin(v._m33)); }

	#endregion


	#region atan
	public static float atan(float v) { return (float)Math.Atan(v); }

	public static float1 atan(float1 v) { return new float1((float)Math.Atan(v.x)); }
	public static float2 atan(float2 v) { return new float2((float)Math.Atan(v.x), (float)Math.Atan(v.y)); }
	public static float3 atan(float3 v) { return new float3((float)Math.Atan(v.x), (float)Math.Atan(v.y), (float)Math.Atan(v.z)); }
	public static float4 atan(float4 v) { return new float4((float)Math.Atan(v.x), (float)Math.Atan(v.y), (float)Math.Atan(v.z), (float)Math.Atan(v.w)); }

	public static float1x1 atan(float1x1 v) { return new float1x1((float)Math.Atan(v._m00)); }
	public static float1x2 atan(float1x2 v) { return new float1x2((float)Math.Atan(v._m00), (float)Math.Atan(v._m01)); }
	public static float1x3 atan(float1x3 v) { return new float1x3((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02)); }
	public static float1x4 atan(float1x4 v) { return new float1x4((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m03)); }
	public static float2x1 atan(float2x1 v) { return new float2x1((float)Math.Atan(v._m00), (float)Math.Atan(v._m10)); }
	public static float2x2 atan(float2x2 v) { return new float2x2((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11)); }
	public static float2x3 atan(float2x3 v) { return new float2x3((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12)); }
	public static float2x4 atan(float2x4 v) { return new float2x4((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m03), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12), (float)Math.Atan(v._m13)); }
	public static float3x1 atan(float3x1 v) { return new float3x1((float)Math.Atan(v._m00), (float)Math.Atan(v._m10), (float)Math.Atan(v._m20)); }
	public static float3x2 atan(float3x2 v) { return new float3x2((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21)); }
	public static float3x3 atan(float3x3 v) { return new float3x3((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21), (float)Math.Atan(v._m22)); }
	public static float3x4 atan(float3x4 v) { return new float3x4((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m03), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12), (float)Math.Atan(v._m13), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21), (float)Math.Atan(v._m22), (float)Math.Atan(v._m23)); }
	public static float4x1 atan(float4x1 v) { return new float4x1((float)Math.Atan(v._m00), (float)Math.Atan(v._m10), (float)Math.Atan(v._m20), (float)Math.Atan(v._m30)); }
	public static float4x2 atan(float4x2 v) { return new float4x2((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21), (float)Math.Atan(v._m30), (float)Math.Atan(v._m31)); }
	public static float4x3 atan(float4x3 v) { return new float4x3((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21), (float)Math.Atan(v._m22), (float)Math.Atan(v._m30), (float)Math.Atan(v._m31), (float)Math.Atan(v._m32)); }
	public static float4x4 atan(float4x4 v) { return new float4x4((float)Math.Atan(v._m00), (float)Math.Atan(v._m01), (float)Math.Atan(v._m02), (float)Math.Atan(v._m03), (float)Math.Atan(v._m10), (float)Math.Atan(v._m11), (float)Math.Atan(v._m12), (float)Math.Atan(v._m13), (float)Math.Atan(v._m20), (float)Math.Atan(v._m21), (float)Math.Atan(v._m22), (float)Math.Atan(v._m23), (float)Math.Atan(v._m30), (float)Math.Atan(v._m31), (float)Math.Atan(v._m32), (float)Math.Atan(v._m33)); }

	#endregion


	#region atan2
	public static float atan2(float a, float b) { return (float)Math.Atan2(a, b); }

	public static float1 atan2(float1 a, float1 b) { return new float1((float)Math.Atan2(a.x, b.x)); }
	public static float2 atan2(float2 a, float2 b) { return new float2((float)Math.Atan2(a.x, b.x), (float)Math.Atan2(a.y, b.y)); }
	public static float3 atan2(float3 a, float3 b) { return new float3((float)Math.Atan2(a.x, b.x), (float)Math.Atan2(a.y, b.y), (float)Math.Atan2(a.z, b.z)); }
	public static float4 atan2(float4 a, float4 b) { return new float4((float)Math.Atan2(a.x, b.x), (float)Math.Atan2(a.y, b.y), (float)Math.Atan2(a.z, b.z), (float)Math.Atan2(a.w, b.w)); }

	public static float1x1 atan2(float1x1 a, float1x1 b) { return new float1x1((float)Math.Atan2(a._m00, b._m00)); }
	public static float1x2 atan2(float1x2 a, float1x2 b) { return new float1x2((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01)); }
	public static float1x3 atan2(float1x3 a, float1x3 b) { return new float1x3((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02)); }
	public static float1x4 atan2(float1x4 a, float1x4 b) { return new float1x4((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m03, b._m03)); }
	public static float2x1 atan2(float2x1 a, float2x1 b) { return new float2x1((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m10, b._m10)); }
	public static float2x2 atan2(float2x2 a, float2x2 b) { return new float2x2((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11)); }
	public static float2x3 atan2(float2x3 a, float2x3 b) { return new float2x3((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12)); }
	public static float2x4 atan2(float2x4 a, float2x4 b) { return new float2x4((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m03, b._m03), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12), (float)Math.Atan2(a._m13, b._m13)); }
	public static float3x1 atan2(float3x1 a, float3x1 b) { return new float3x1((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m20, b._m20)); }
	public static float3x2 atan2(float3x2 a, float3x2 b) { return new float3x2((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21)); }
	public static float3x3 atan2(float3x3 a, float3x3 b) { return new float3x3((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21), (float)Math.Atan2(a._m22, b._m22)); }
	public static float3x4 atan2(float3x4 a, float3x4 b) { return new float3x4((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m03, b._m03), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12), (float)Math.Atan2(a._m13, b._m13), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21), (float)Math.Atan2(a._m22, b._m22), (float)Math.Atan2(a._m23, b._m23)); }
	public static float4x1 atan2(float4x1 a, float4x1 b) { return new float4x1((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m30, b._m30)); }
	public static float4x2 atan2(float4x2 a, float4x2 b) { return new float4x2((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21), (float)Math.Atan2(a._m30, b._m30), (float)Math.Atan2(a._m31, b._m31)); }
	public static float4x3 atan2(float4x3 a, float4x3 b) { return new float4x3((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21), (float)Math.Atan2(a._m22, b._m22), (float)Math.Atan2(a._m30, b._m30), (float)Math.Atan2(a._m31, b._m31), (float)Math.Atan2(a._m32, b._m32)); }
	public static float4x4 atan2(float4x4 a, float4x4 b) { return new float4x4((float)Math.Atan2(a._m00, b._m00), (float)Math.Atan2(a._m01, b._m01), (float)Math.Atan2(a._m02, b._m02), (float)Math.Atan2(a._m03, b._m03), (float)Math.Atan2(a._m10, b._m10), (float)Math.Atan2(a._m11, b._m11), (float)Math.Atan2(a._m12, b._m12), (float)Math.Atan2(a._m13, b._m13), (float)Math.Atan2(a._m20, b._m20), (float)Math.Atan2(a._m21, b._m21), (float)Math.Atan2(a._m22, b._m22), (float)Math.Atan2(a._m23, b._m23), (float)Math.Atan2(a._m30, b._m30), (float)Math.Atan2(a._m31, b._m31), (float)Math.Atan2(a._m32, b._m32), (float)Math.Atan2(a._m33, b._m33)); }

	#endregion


	#region ceil
	public static float ceil(float v) { return (float)Math.Ceiling(v); }

	public static float1 ceil(float1 v) { return new float1((float)Math.Ceiling(v.x)); }
	public static float2 ceil(float2 v) { return new float2((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y)); }
	public static float3 ceil(float3 v) { return new float3((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y), (float)Math.Ceiling(v.z)); }
	public static float4 ceil(float4 v) { return new float4((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y), (float)Math.Ceiling(v.z), (float)Math.Ceiling(v.w)); }

	public static float1x1 ceil(float1x1 v) { return new float1x1((float)Math.Ceiling(v._m00)); }
	public static float1x2 ceil(float1x2 v) { return new float1x2((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01)); }
	public static float1x3 ceil(float1x3 v) { return new float1x3((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02)); }
	public static float1x4 ceil(float1x4 v) { return new float1x4((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m03)); }
	public static float2x1 ceil(float2x1 v) { return new float2x1((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m10)); }
	public static float2x2 ceil(float2x2 v) { return new float2x2((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11)); }
	public static float2x3 ceil(float2x3 v) { return new float2x3((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12)); }
	public static float2x4 ceil(float2x4 v) { return new float2x4((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m03), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12), (float)Math.Ceiling(v._m13)); }
	public static float3x1 ceil(float3x1 v) { return new float3x1((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m20)); }
	public static float3x2 ceil(float3x2 v) { return new float3x2((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21)); }
	public static float3x3 ceil(float3x3 v) { return new float3x3((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21), (float)Math.Ceiling(v._m22)); }
	public static float3x4 ceil(float3x4 v) { return new float3x4((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m03), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12), (float)Math.Ceiling(v._m13), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21), (float)Math.Ceiling(v._m22), (float)Math.Ceiling(v._m23)); }
	public static float4x1 ceil(float4x1 v) { return new float4x1((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m30)); }
	public static float4x2 ceil(float4x2 v) { return new float4x2((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21), (float)Math.Ceiling(v._m30), (float)Math.Ceiling(v._m31)); }
	public static float4x3 ceil(float4x3 v) { return new float4x3((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21), (float)Math.Ceiling(v._m22), (float)Math.Ceiling(v._m30), (float)Math.Ceiling(v._m31), (float)Math.Ceiling(v._m32)); }
	public static float4x4 ceil(float4x4 v) { return new float4x4((float)Math.Ceiling(v._m00), (float)Math.Ceiling(v._m01), (float)Math.Ceiling(v._m02), (float)Math.Ceiling(v._m03), (float)Math.Ceiling(v._m10), (float)Math.Ceiling(v._m11), (float)Math.Ceiling(v._m12), (float)Math.Ceiling(v._m13), (float)Math.Ceiling(v._m20), (float)Math.Ceiling(v._m21), (float)Math.Ceiling(v._m22), (float)Math.Ceiling(v._m23), (float)Math.Ceiling(v._m30), (float)Math.Ceiling(v._m31), (float)Math.Ceiling(v._m32), (float)Math.Ceiling(v._m33)); }

	#endregion


	#region clamp
	public static float clamp(float a, float b, float c) { return max(b, min(c, a)); }
	public static float1 clamp(float1 a, float1 b, float1 c) { return max(b, min(c, a)); }
	public static float2 clamp(float2 a, float2 b, float2 c) { return max(b, min(c, a)); }
	public static float3 clamp(float3 a, float3 b, float3 c) { return max(b, min(c, a)); }
	public static float4 clamp(float4 a, float4 b, float4 c) { return max(b, min(c, a)); }
	public static float1x1 clamp(float1x1 a, float1x1 b, float1x1 c) { return max(b, min(c, a)); }
	public static float1x2 clamp(float1x2 a, float1x2 b, float1x2 c) { return max(b, min(c, a)); }
	public static float1x3 clamp(float1x3 a, float1x3 b, float1x3 c) { return max(b, min(c, a)); }
	public static float1x4 clamp(float1x4 a, float1x4 b, float1x4 c) { return max(b, min(c, a)); }
	public static float2x1 clamp(float2x1 a, float2x1 b, float2x1 c) { return max(b, min(c, a)); }
	public static float2x2 clamp(float2x2 a, float2x2 b, float2x2 c) { return max(b, min(c, a)); }
	public static float2x3 clamp(float2x3 a, float2x3 b, float2x3 c) { return max(b, min(c, a)); }
	public static float2x4 clamp(float2x4 a, float2x4 b, float2x4 c) { return max(b, min(c, a)); }
	public static float3x1 clamp(float3x1 a, float3x1 b, float3x1 c) { return max(b, min(c, a)); }
	public static float3x2 clamp(float3x2 a, float3x2 b, float3x2 c) { return max(b, min(c, a)); }
	public static float3x3 clamp(float3x3 a, float3x3 b, float3x3 c) { return max(b, min(c, a)); }
	public static float3x4 clamp(float3x4 a, float3x4 b, float3x4 c) { return max(b, min(c, a)); }
	public static float4x1 clamp(float4x1 a, float4x1 b, float4x1 c) { return max(b, min(c, a)); }
	public static float4x2 clamp(float4x2 a, float4x2 b, float4x2 c) { return max(b, min(c, a)); }
	public static float4x3 clamp(float4x3 a, float4x3 b, float4x3 c) { return max(b, min(c, a)); }
	public static float4x4 clamp(float4x4 a, float4x4 b, float4x4 c) { return max(b, min(c, a)); }
	#endregion


	#region cos
	public static float cos(float v) { return (float)Math.Cos(v); }

	public static float1 cos(float1 v) { return new float1((float)Math.Cos(v.x)); }
	public static float2 cos(float2 v) { return new float2((float)Math.Cos(v.x), (float)Math.Cos(v.y)); }
	public static float3 cos(float3 v) { return new float3((float)Math.Cos(v.x), (float)Math.Cos(v.y), (float)Math.Cos(v.z)); }
	public static float4 cos(float4 v) { return new float4((float)Math.Cos(v.x), (float)Math.Cos(v.y), (float)Math.Cos(v.z), (float)Math.Cos(v.w)); }

	public static float1x1 cos(float1x1 v) { return new float1x1((float)Math.Cos(v._m00)); }
	public static float1x2 cos(float1x2 v) { return new float1x2((float)Math.Cos(v._m00), (float)Math.Cos(v._m01)); }
	public static float1x3 cos(float1x3 v) { return new float1x3((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02)); }
	public static float1x4 cos(float1x4 v) { return new float1x4((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m03)); }
	public static float2x1 cos(float2x1 v) { return new float2x1((float)Math.Cos(v._m00), (float)Math.Cos(v._m10)); }
	public static float2x2 cos(float2x2 v) { return new float2x2((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11)); }
	public static float2x3 cos(float2x3 v) { return new float2x3((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12)); }
	public static float2x4 cos(float2x4 v) { return new float2x4((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m03), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12), (float)Math.Cos(v._m13)); }
	public static float3x1 cos(float3x1 v) { return new float3x1((float)Math.Cos(v._m00), (float)Math.Cos(v._m10), (float)Math.Cos(v._m20)); }
	public static float3x2 cos(float3x2 v) { return new float3x2((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21)); }
	public static float3x3 cos(float3x3 v) { return new float3x3((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21), (float)Math.Cos(v._m22)); }
	public static float3x4 cos(float3x4 v) { return new float3x4((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m03), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12), (float)Math.Cos(v._m13), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21), (float)Math.Cos(v._m22), (float)Math.Cos(v._m23)); }
	public static float4x1 cos(float4x1 v) { return new float4x1((float)Math.Cos(v._m00), (float)Math.Cos(v._m10), (float)Math.Cos(v._m20), (float)Math.Cos(v._m30)); }
	public static float4x2 cos(float4x2 v) { return new float4x2((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21), (float)Math.Cos(v._m30), (float)Math.Cos(v._m31)); }
	public static float4x3 cos(float4x3 v) { return new float4x3((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21), (float)Math.Cos(v._m22), (float)Math.Cos(v._m30), (float)Math.Cos(v._m31), (float)Math.Cos(v._m32)); }
	public static float4x4 cos(float4x4 v) { return new float4x4((float)Math.Cos(v._m00), (float)Math.Cos(v._m01), (float)Math.Cos(v._m02), (float)Math.Cos(v._m03), (float)Math.Cos(v._m10), (float)Math.Cos(v._m11), (float)Math.Cos(v._m12), (float)Math.Cos(v._m13), (float)Math.Cos(v._m20), (float)Math.Cos(v._m21), (float)Math.Cos(v._m22), (float)Math.Cos(v._m23), (float)Math.Cos(v._m30), (float)Math.Cos(v._m31), (float)Math.Cos(v._m32), (float)Math.Cos(v._m33)); }

	#endregion


	#region cosh
	public static float cosh(float v) { return (float)Math.Cosh(v); }

	public static float1 cosh(float1 v) { return new float1((float)Math.Cosh(v.x)); }
	public static float2 cosh(float2 v) { return new float2((float)Math.Cosh(v.x), (float)Math.Cosh(v.y)); }
	public static float3 cosh(float3 v) { return new float3((float)Math.Cosh(v.x), (float)Math.Cosh(v.y), (float)Math.Cosh(v.z)); }
	public static float4 cosh(float4 v) { return new float4((float)Math.Cosh(v.x), (float)Math.Cosh(v.y), (float)Math.Cosh(v.z), (float)Math.Cosh(v.w)); }

	public static float1x1 cosh(float1x1 v) { return new float1x1((float)Math.Cosh(v._m00)); }
	public static float1x2 cosh(float1x2 v) { return new float1x2((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01)); }
	public static float1x3 cosh(float1x3 v) { return new float1x3((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02)); }
	public static float1x4 cosh(float1x4 v) { return new float1x4((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m03)); }
	public static float2x1 cosh(float2x1 v) { return new float2x1((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m10)); }
	public static float2x2 cosh(float2x2 v) { return new float2x2((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11)); }
	public static float2x3 cosh(float2x3 v) { return new float2x3((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12)); }
	public static float2x4 cosh(float2x4 v) { return new float2x4((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m03), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12), (float)Math.Cosh(v._m13)); }
	public static float3x1 cosh(float3x1 v) { return new float3x1((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m20)); }
	public static float3x2 cosh(float3x2 v) { return new float3x2((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21)); }
	public static float3x3 cosh(float3x3 v) { return new float3x3((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21), (float)Math.Cosh(v._m22)); }
	public static float3x4 cosh(float3x4 v) { return new float3x4((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m03), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12), (float)Math.Cosh(v._m13), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21), (float)Math.Cosh(v._m22), (float)Math.Cosh(v._m23)); }
	public static float4x1 cosh(float4x1 v) { return new float4x1((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m30)); }
	public static float4x2 cosh(float4x2 v) { return new float4x2((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21), (float)Math.Cosh(v._m30), (float)Math.Cosh(v._m31)); }
	public static float4x3 cosh(float4x3 v) { return new float4x3((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21), (float)Math.Cosh(v._m22), (float)Math.Cosh(v._m30), (float)Math.Cosh(v._m31), (float)Math.Cosh(v._m32)); }
	public static float4x4 cosh(float4x4 v) { return new float4x4((float)Math.Cosh(v._m00), (float)Math.Cosh(v._m01), (float)Math.Cosh(v._m02), (float)Math.Cosh(v._m03), (float)Math.Cosh(v._m10), (float)Math.Cosh(v._m11), (float)Math.Cosh(v._m12), (float)Math.Cosh(v._m13), (float)Math.Cosh(v._m20), (float)Math.Cosh(v._m21), (float)Math.Cosh(v._m22), (float)Math.Cosh(v._m23), (float)Math.Cosh(v._m30), (float)Math.Cosh(v._m31), (float)Math.Cosh(v._m32), (float)Math.Cosh(v._m33)); }

	#endregion


	#region dot
	public static float dot(float1 a, float1 b) { return a.x * b.x; }
	public static float dot(float2 a, float2 b) { return a.x * b.x + a.y * b.y; }
	public static float dot(float3 a, float3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
	public static float dot(float4 a, float4 b) { return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w; }
	#endregion


	#region min
	public static float min(float a, float b) { return a<b?a:b; }

	public static float1 min(float1 a, float1 b) { return new float1(a.x<b.x?a.x:b.x); }
	public static float2 min(float2 a, float2 b) { return new float2(a.x<b.x?a.x:b.x, a.y<b.y?a.y:b.y); }
	public static float3 min(float3 a, float3 b) { return new float3(a.x<b.x?a.x:b.x, a.y<b.y?a.y:b.y, a.z<b.z?a.z:b.z); }
	public static float4 min(float4 a, float4 b) { return new float4(a.x<b.x?a.x:b.x, a.y<b.y?a.y:b.y, a.z<b.z?a.z:b.z, a.w<b.w?a.w:b.w); }

	public static float1x1 min(float1x1 a, float1x1 b) { return new float1x1(a._m00<b._m00?a._m00:b._m00); }
	public static float1x2 min(float1x2 a, float1x2 b) { return new float1x2(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01); }
	public static float1x3 min(float1x3 a, float1x3 b) { return new float1x3(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02); }
	public static float1x4 min(float1x4 a, float1x4 b) { return new float1x4(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m03<b._m03?a._m03:b._m03); }
	public static float2x1 min(float2x1 a, float2x1 b) { return new float2x1(a._m00<b._m00?a._m00:b._m00, a._m10<b._m10?a._m10:b._m10); }
	public static float2x2 min(float2x2 a, float2x2 b) { return new float2x2(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11); }
	public static float2x3 min(float2x3 a, float2x3 b) { return new float2x3(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12); }
	public static float2x4 min(float2x4 a, float2x4 b) { return new float2x4(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m03<b._m03?a._m03:b._m03, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12, a._m13<b._m13?a._m13:b._m13); }
	public static float3x1 min(float3x1 a, float3x1 b) { return new float3x1(a._m00<b._m00?a._m00:b._m00, a._m10<b._m10?a._m10:b._m10, a._m20<b._m20?a._m20:b._m20); }
	public static float3x2 min(float3x2 a, float3x2 b) { return new float3x2(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21); }
	public static float3x3 min(float3x3 a, float3x3 b) { return new float3x3(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21, a._m22<b._m22?a._m22:b._m22); }
	public static float3x4 min(float3x4 a, float3x4 b) { return new float3x4(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m03<b._m03?a._m03:b._m03, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12, a._m13<b._m13?a._m13:b._m13, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21, a._m22<b._m22?a._m22:b._m22, a._m23<b._m23?a._m23:b._m23); }
	public static float4x1 min(float4x1 a, float4x1 b) { return new float4x1(a._m00<b._m00?a._m00:b._m00, a._m10<b._m10?a._m10:b._m10, a._m20<b._m20?a._m20:b._m20, a._m30<b._m30?a._m30:b._m30); }
	public static float4x2 min(float4x2 a, float4x2 b) { return new float4x2(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21, a._m30<b._m30?a._m30:b._m30, a._m31<b._m31?a._m31:b._m31); }
	public static float4x3 min(float4x3 a, float4x3 b) { return new float4x3(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21, a._m22<b._m22?a._m22:b._m22, a._m30<b._m30?a._m30:b._m30, a._m31<b._m31?a._m31:b._m31, a._m32<b._m32?a._m32:b._m32); }
	public static float4x4 min(float4x4 a, float4x4 b) { return new float4x4(a._m00<b._m00?a._m00:b._m00, a._m01<b._m01?a._m01:b._m01, a._m02<b._m02?a._m02:b._m02, a._m03<b._m03?a._m03:b._m03, a._m10<b._m10?a._m10:b._m10, a._m11<b._m11?a._m11:b._m11, a._m12<b._m12?a._m12:b._m12, a._m13<b._m13?a._m13:b._m13, a._m20<b._m20?a._m20:b._m20, a._m21<b._m21?a._m21:b._m21, a._m22<b._m22?a._m22:b._m22, a._m23<b._m23?a._m23:b._m23, a._m30<b._m30?a._m30:b._m30, a._m31<b._m31?a._m31:b._m31, a._m32<b._m32?a._m32:b._m32, a._m33<b._m33?a._m33:b._m33); }

	#endregion


	#region max
	public static float max(float a, float b) { return a>b?a:b; }

	public static float1 max(float1 a, float1 b) { return new float1(a.x>b.x?a.x:b.x); }
	public static float2 max(float2 a, float2 b) { return new float2(a.x>b.x?a.x:b.x, a.y>b.y?a.y:b.y); }
	public static float3 max(float3 a, float3 b) { return new float3(a.x>b.x?a.x:b.x, a.y>b.y?a.y:b.y, a.z>b.z?a.z:b.z); }
	public static float4 max(float4 a, float4 b) { return new float4(a.x>b.x?a.x:b.x, a.y>b.y?a.y:b.y, a.z>b.z?a.z:b.z, a.w>b.w?a.w:b.w); }

	public static float1x1 max(float1x1 a, float1x1 b) { return new float1x1(a._m00>b._m00?a._m00:b._m00); }
	public static float1x2 max(float1x2 a, float1x2 b) { return new float1x2(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01); }
	public static float1x3 max(float1x3 a, float1x3 b) { return new float1x3(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02); }
	public static float1x4 max(float1x4 a, float1x4 b) { return new float1x4(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m03>b._m03?a._m03:b._m03); }
	public static float2x1 max(float2x1 a, float2x1 b) { return new float2x1(a._m00>b._m00?a._m00:b._m00, a._m10>b._m10?a._m10:b._m10); }
	public static float2x2 max(float2x2 a, float2x2 b) { return new float2x2(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11); }
	public static float2x3 max(float2x3 a, float2x3 b) { return new float2x3(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12); }
	public static float2x4 max(float2x4 a, float2x4 b) { return new float2x4(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m03>b._m03?a._m03:b._m03, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12, a._m13>b._m13?a._m13:b._m13); }
	public static float3x1 max(float3x1 a, float3x1 b) { return new float3x1(a._m00>b._m00?a._m00:b._m00, a._m10>b._m10?a._m10:b._m10, a._m20>b._m20?a._m20:b._m20); }
	public static float3x2 max(float3x2 a, float3x2 b) { return new float3x2(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21); }
	public static float3x3 max(float3x3 a, float3x3 b) { return new float3x3(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21, a._m22>b._m22?a._m22:b._m22); }
	public static float3x4 max(float3x4 a, float3x4 b) { return new float3x4(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m03>b._m03?a._m03:b._m03, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12, a._m13>b._m13?a._m13:b._m13, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21, a._m22>b._m22?a._m22:b._m22, a._m23>b._m23?a._m23:b._m23); }
	public static float4x1 max(float4x1 a, float4x1 b) { return new float4x1(a._m00>b._m00?a._m00:b._m00, a._m10>b._m10?a._m10:b._m10, a._m20>b._m20?a._m20:b._m20, a._m30>b._m30?a._m30:b._m30); }
	public static float4x2 max(float4x2 a, float4x2 b) { return new float4x2(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21, a._m30>b._m30?a._m30:b._m30, a._m31>b._m31?a._m31:b._m31); }
	public static float4x3 max(float4x3 a, float4x3 b) { return new float4x3(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21, a._m22>b._m22?a._m22:b._m22, a._m30>b._m30?a._m30:b._m30, a._m31>b._m31?a._m31:b._m31, a._m32>b._m32?a._m32:b._m32); }
	public static float4x4 max(float4x4 a, float4x4 b) { return new float4x4(a._m00>b._m00?a._m00:b._m00, a._m01>b._m01?a._m01:b._m01, a._m02>b._m02?a._m02:b._m02, a._m03>b._m03?a._m03:b._m03, a._m10>b._m10?a._m10:b._m10, a._m11>b._m11?a._m11:b._m11, a._m12>b._m12?a._m12:b._m12, a._m13>b._m13?a._m13:b._m13, a._m20>b._m20?a._m20:b._m20, a._m21>b._m21?a._m21:b._m21, a._m22>b._m22?a._m22:b._m22, a._m23>b._m23?a._m23:b._m23, a._m30>b._m30?a._m30:b._m30, a._m31>b._m31?a._m31:b._m31, a._m32>b._m32?a._m32:b._m32, a._m33>b._m33?a._m33:b._m33); }

	#endregion


	#region degrees
	public static float degrees(float v) { return (float)(v*180.0/Math.PI); }

	public static float1 degrees(float1 v) { return new float1((float)(v.x*180.0/Math.PI)); }
	public static float2 degrees(float2 v) { return new float2((float)(v.x*180.0/Math.PI), (float)(v.y*180.0/Math.PI)); }
	public static float3 degrees(float3 v) { return new float3((float)(v.x*180.0/Math.PI), (float)(v.y*180.0/Math.PI), (float)(v.z*180.0/Math.PI)); }
	public static float4 degrees(float4 v) { return new float4((float)(v.x*180.0/Math.PI), (float)(v.y*180.0/Math.PI), (float)(v.z*180.0/Math.PI), (float)(v.w*180.0/Math.PI)); }

	public static float1x1 degrees(float1x1 v) { return new float1x1((float)(v._m00*180.0/Math.PI)); }
	public static float1x2 degrees(float1x2 v) { return new float1x2((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI)); }
	public static float1x3 degrees(float1x3 v) { return new float1x3((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI)); }
	public static float1x4 degrees(float1x4 v) { return new float1x4((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m03*180.0/Math.PI)); }
	public static float2x1 degrees(float2x1 v) { return new float2x1((float)(v._m00*180.0/Math.PI), (float)(v._m10*180.0/Math.PI)); }
	public static float2x2 degrees(float2x2 v) { return new float2x2((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI)); }
	public static float2x3 degrees(float2x3 v) { return new float2x3((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI)); }
	public static float2x4 degrees(float2x4 v) { return new float2x4((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m03*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI), (float)(v._m13*180.0/Math.PI)); }
	public static float3x1 degrees(float3x1 v) { return new float3x1((float)(v._m00*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m20*180.0/Math.PI)); }
	public static float3x2 degrees(float3x2 v) { return new float3x2((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI)); }
	public static float3x3 degrees(float3x3 v) { return new float3x3((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI), (float)(v._m22*180.0/Math.PI)); }
	public static float3x4 degrees(float3x4 v) { return new float3x4((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m03*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI), (float)(v._m13*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI), (float)(v._m22*180.0/Math.PI), (float)(v._m23*180.0/Math.PI)); }
	public static float4x1 degrees(float4x1 v) { return new float4x1((float)(v._m00*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m30*180.0/Math.PI)); }
	public static float4x2 degrees(float4x2 v) { return new float4x2((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI), (float)(v._m30*180.0/Math.PI), (float)(v._m31*180.0/Math.PI)); }
	public static float4x3 degrees(float4x3 v) { return new float4x3((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI), (float)(v._m22*180.0/Math.PI), (float)(v._m30*180.0/Math.PI), (float)(v._m31*180.0/Math.PI), (float)(v._m32*180.0/Math.PI)); }
	public static float4x4 degrees(float4x4 v) { return new float4x4((float)(v._m00*180.0/Math.PI), (float)(v._m01*180.0/Math.PI), (float)(v._m02*180.0/Math.PI), (float)(v._m03*180.0/Math.PI), (float)(v._m10*180.0/Math.PI), (float)(v._m11*180.0/Math.PI), (float)(v._m12*180.0/Math.PI), (float)(v._m13*180.0/Math.PI), (float)(v._m20*180.0/Math.PI), (float)(v._m21*180.0/Math.PI), (float)(v._m22*180.0/Math.PI), (float)(v._m23*180.0/Math.PI), (float)(v._m30*180.0/Math.PI), (float)(v._m31*180.0/Math.PI), (float)(v._m32*180.0/Math.PI), (float)(v._m33*180.0/Math.PI)); }

	#endregion


	#region length
	public static float length(float1 v) { return (float)Math.Sqrt(dot(v, v)); }
	public static float length(float2 v) { return (float)Math.Sqrt(dot(v, v)); }
	public static float length(float3 v) { return (float)Math.Sqrt(dot(v, v)); }
	public static float length(float4 v) { return (float)Math.Sqrt(dot(v, v)); }
	#endregion


	#region sqrlength
	public static float sqrlength(float1 v) { return dot(v, v); }
	public static float sqrlength(float2 v) { return dot(v, v); }
	public static float sqrlength(float3 v) { return dot(v, v); }
	public static float sqrlength(float4 v) { return dot(v, v); }
	#endregion


	#region distance
	public static float distance(float1 a, float1 b) { return length(a - b); }
	public static float distance(float2 a, float2 b) { return length(a - b); }
	public static float distance(float3 a, float3 b) { return length(a - b); }
	public static float distance(float4 a, float4 b) { return length(a - b); }
	#endregion


	#region sqrdistance
	public static float sqrdistance(float1 a, float1 b) { return sqrlength(a - b); }
	public static float sqrdistance(float2 a, float2 b) { return sqrlength(a - b); }
	public static float sqrdistance(float3 a, float3 b) { return sqrlength(a - b); }
	public static float sqrdistance(float4 a, float4 b) { return sqrlength(a - b); }
	#endregion


	#region exp
	public static float exp(float v) { return (float)Math.Exp(v); }

	public static float1 exp(float1 v) { return new float1((float)Math.Exp(v.x)); }
	public static float2 exp(float2 v) { return new float2((float)Math.Exp(v.x), (float)Math.Exp(v.y)); }
	public static float3 exp(float3 v) { return new float3((float)Math.Exp(v.x), (float)Math.Exp(v.y), (float)Math.Exp(v.z)); }
	public static float4 exp(float4 v) { return new float4((float)Math.Exp(v.x), (float)Math.Exp(v.y), (float)Math.Exp(v.z), (float)Math.Exp(v.w)); }

	public static float1x1 exp(float1x1 v) { return new float1x1((float)Math.Exp(v._m00)); }
	public static float1x2 exp(float1x2 v) { return new float1x2((float)Math.Exp(v._m00), (float)Math.Exp(v._m01)); }
	public static float1x3 exp(float1x3 v) { return new float1x3((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02)); }
	public static float1x4 exp(float1x4 v) { return new float1x4((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m03)); }
	public static float2x1 exp(float2x1 v) { return new float2x1((float)Math.Exp(v._m00), (float)Math.Exp(v._m10)); }
	public static float2x2 exp(float2x2 v) { return new float2x2((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11)); }
	public static float2x3 exp(float2x3 v) { return new float2x3((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12)); }
	public static float2x4 exp(float2x4 v) { return new float2x4((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m03), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12), (float)Math.Exp(v._m13)); }
	public static float3x1 exp(float3x1 v) { return new float3x1((float)Math.Exp(v._m00), (float)Math.Exp(v._m10), (float)Math.Exp(v._m20)); }
	public static float3x2 exp(float3x2 v) { return new float3x2((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21)); }
	public static float3x3 exp(float3x3 v) { return new float3x3((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21), (float)Math.Exp(v._m22)); }
	public static float3x4 exp(float3x4 v) { return new float3x4((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m03), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12), (float)Math.Exp(v._m13), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21), (float)Math.Exp(v._m22), (float)Math.Exp(v._m23)); }
	public static float4x1 exp(float4x1 v) { return new float4x1((float)Math.Exp(v._m00), (float)Math.Exp(v._m10), (float)Math.Exp(v._m20), (float)Math.Exp(v._m30)); }
	public static float4x2 exp(float4x2 v) { return new float4x2((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21), (float)Math.Exp(v._m30), (float)Math.Exp(v._m31)); }
	public static float4x3 exp(float4x3 v) { return new float4x3((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21), (float)Math.Exp(v._m22), (float)Math.Exp(v._m30), (float)Math.Exp(v._m31), (float)Math.Exp(v._m32)); }
	public static float4x4 exp(float4x4 v) { return new float4x4((float)Math.Exp(v._m00), (float)Math.Exp(v._m01), (float)Math.Exp(v._m02), (float)Math.Exp(v._m03), (float)Math.Exp(v._m10), (float)Math.Exp(v._m11), (float)Math.Exp(v._m12), (float)Math.Exp(v._m13), (float)Math.Exp(v._m20), (float)Math.Exp(v._m21), (float)Math.Exp(v._m22), (float)Math.Exp(v._m23), (float)Math.Exp(v._m30), (float)Math.Exp(v._m31), (float)Math.Exp(v._m32), (float)Math.Exp(v._m33)); }

	#endregion


	#region exp2
	public static float exp2(float v) { return (float)Math.Pow(2, v); }

	public static float1 exp2(float1 v) { return new float1((float)Math.Pow(2, v.x)); }
	public static float2 exp2(float2 v) { return new float2((float)Math.Pow(2, v.x), (float)Math.Pow(2, v.y)); }
	public static float3 exp2(float3 v) { return new float3((float)Math.Pow(2, v.x), (float)Math.Pow(2, v.y), (float)Math.Pow(2, v.z)); }
	public static float4 exp2(float4 v) { return new float4((float)Math.Pow(2, v.x), (float)Math.Pow(2, v.y), (float)Math.Pow(2, v.z), (float)Math.Pow(2, v.w)); }

	public static float1x1 exp2(float1x1 v) { return new float1x1((float)Math.Pow(2, v._m00)); }
	public static float1x2 exp2(float1x2 v) { return new float1x2((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01)); }
	public static float1x3 exp2(float1x3 v) { return new float1x3((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02)); }
	public static float1x4 exp2(float1x4 v) { return new float1x4((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m03)); }
	public static float2x1 exp2(float2x1 v) { return new float2x1((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m10)); }
	public static float2x2 exp2(float2x2 v) { return new float2x2((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11)); }
	public static float2x3 exp2(float2x3 v) { return new float2x3((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12)); }
	public static float2x4 exp2(float2x4 v) { return new float2x4((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m03), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12), (float)Math.Pow(2, v._m13)); }
	public static float3x1 exp2(float3x1 v) { return new float3x1((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m20)); }
	public static float3x2 exp2(float3x2 v) { return new float3x2((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21)); }
	public static float3x3 exp2(float3x3 v) { return new float3x3((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21), (float)Math.Pow(2, v._m22)); }
	public static float3x4 exp2(float3x4 v) { return new float3x4((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m03), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12), (float)Math.Pow(2, v._m13), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21), (float)Math.Pow(2, v._m22), (float)Math.Pow(2, v._m23)); }
	public static float4x1 exp2(float4x1 v) { return new float4x1((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m30)); }
	public static float4x2 exp2(float4x2 v) { return new float4x2((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21), (float)Math.Pow(2, v._m30), (float)Math.Pow(2, v._m31)); }
	public static float4x3 exp2(float4x3 v) { return new float4x3((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21), (float)Math.Pow(2, v._m22), (float)Math.Pow(2, v._m30), (float)Math.Pow(2, v._m31), (float)Math.Pow(2, v._m32)); }
	public static float4x4 exp2(float4x4 v) { return new float4x4((float)Math.Pow(2, v._m00), (float)Math.Pow(2, v._m01), (float)Math.Pow(2, v._m02), (float)Math.Pow(2, v._m03), (float)Math.Pow(2, v._m10), (float)Math.Pow(2, v._m11), (float)Math.Pow(2, v._m12), (float)Math.Pow(2, v._m13), (float)Math.Pow(2, v._m20), (float)Math.Pow(2, v._m21), (float)Math.Pow(2, v._m22), (float)Math.Pow(2, v._m23), (float)Math.Pow(2, v._m30), (float)Math.Pow(2, v._m31), (float)Math.Pow(2, v._m32), (float)Math.Pow(2, v._m33)); }

	#endregion


	#region floor
	public static float floor(float v) { return (float)Math.Floor(v); }

	public static float1 floor(float1 v) { return new float1((float)Math.Floor(v.x)); }
	public static float2 floor(float2 v) { return new float2((float)Math.Floor(v.x), (float)Math.Floor(v.y)); }
	public static float3 floor(float3 v) { return new float3((float)Math.Floor(v.x), (float)Math.Floor(v.y), (float)Math.Floor(v.z)); }
	public static float4 floor(float4 v) { return new float4((float)Math.Floor(v.x), (float)Math.Floor(v.y), (float)Math.Floor(v.z), (float)Math.Floor(v.w)); }

	public static float1x1 floor(float1x1 v) { return new float1x1((float)Math.Floor(v._m00)); }
	public static float1x2 floor(float1x2 v) { return new float1x2((float)Math.Floor(v._m00), (float)Math.Floor(v._m01)); }
	public static float1x3 floor(float1x3 v) { return new float1x3((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02)); }
	public static float1x4 floor(float1x4 v) { return new float1x4((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m03)); }
	public static float2x1 floor(float2x1 v) { return new float2x1((float)Math.Floor(v._m00), (float)Math.Floor(v._m10)); }
	public static float2x2 floor(float2x2 v) { return new float2x2((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11)); }
	public static float2x3 floor(float2x3 v) { return new float2x3((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12)); }
	public static float2x4 floor(float2x4 v) { return new float2x4((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m03), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12), (float)Math.Floor(v._m13)); }
	public static float3x1 floor(float3x1 v) { return new float3x1((float)Math.Floor(v._m00), (float)Math.Floor(v._m10), (float)Math.Floor(v._m20)); }
	public static float3x2 floor(float3x2 v) { return new float3x2((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21)); }
	public static float3x3 floor(float3x3 v) { return new float3x3((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21), (float)Math.Floor(v._m22)); }
	public static float3x4 floor(float3x4 v) { return new float3x4((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m03), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12), (float)Math.Floor(v._m13), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21), (float)Math.Floor(v._m22), (float)Math.Floor(v._m23)); }
	public static float4x1 floor(float4x1 v) { return new float4x1((float)Math.Floor(v._m00), (float)Math.Floor(v._m10), (float)Math.Floor(v._m20), (float)Math.Floor(v._m30)); }
	public static float4x2 floor(float4x2 v) { return new float4x2((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21), (float)Math.Floor(v._m30), (float)Math.Floor(v._m31)); }
	public static float4x3 floor(float4x3 v) { return new float4x3((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21), (float)Math.Floor(v._m22), (float)Math.Floor(v._m30), (float)Math.Floor(v._m31), (float)Math.Floor(v._m32)); }
	public static float4x4 floor(float4x4 v) { return new float4x4((float)Math.Floor(v._m00), (float)Math.Floor(v._m01), (float)Math.Floor(v._m02), (float)Math.Floor(v._m03), (float)Math.Floor(v._m10), (float)Math.Floor(v._m11), (float)Math.Floor(v._m12), (float)Math.Floor(v._m13), (float)Math.Floor(v._m20), (float)Math.Floor(v._m21), (float)Math.Floor(v._m22), (float)Math.Floor(v._m23), (float)Math.Floor(v._m30), (float)Math.Floor(v._m31), (float)Math.Floor(v._m32), (float)Math.Floor(v._m33)); }

	#endregion


	#region fmod
	public static float fmod(float a, float b) { return a % b; }
	public static float1 fmod(float1 a, float1 b) { return a % b; }
	public static float2 fmod(float2 a, float2 b) { return a % b; }
	public static float3 fmod(float3 a, float3 b) { return a % b; }
	public static float4 fmod(float4 a, float4 b) { return a % b; }
	public static float1x1 fmod(float1x1 a, float1x1 b) { return a % b; }
	public static float1x2 fmod(float1x2 a, float1x2 b) { return a % b; }
	public static float1x3 fmod(float1x3 a, float1x3 b) { return a % b; }
	public static float1x4 fmod(float1x4 a, float1x4 b) { return a % b; }
	public static float2x1 fmod(float2x1 a, float2x1 b) { return a % b; }
	public static float2x2 fmod(float2x2 a, float2x2 b) { return a % b; }
	public static float2x3 fmod(float2x3 a, float2x3 b) { return a % b; }
	public static float2x4 fmod(float2x4 a, float2x4 b) { return a % b; }
	public static float3x1 fmod(float3x1 a, float3x1 b) { return a % b; }
	public static float3x2 fmod(float3x2 a, float3x2 b) { return a % b; }
	public static float3x3 fmod(float3x3 a, float3x3 b) { return a % b; }
	public static float3x4 fmod(float3x4 a, float3x4 b) { return a % b; }
	public static float4x1 fmod(float4x1 a, float4x1 b) { return a % b; }
	public static float4x2 fmod(float4x2 a, float4x2 b) { return a % b; }
	public static float4x3 fmod(float4x3 a, float4x3 b) { return a % b; }
	public static float4x4 fmod(float4x4 a, float4x4 b) { return a % b; }
	#endregion


	#region frac
	public static float frac(float v) { return v % 1; }
	public static float1 frac(float1 v) { return v % 1; }
	public static float2 frac(float2 v) { return v % 1; }
	public static float3 frac(float3 v) { return v % 1; }
	public static float4 frac(float4 v) { return v % 1; }
	public static float1x1 frac(float1x1 v) { return v % 1; }
	public static float1x2 frac(float1x2 v) { return v % 1; }
	public static float1x3 frac(float1x3 v) { return v % 1; }
	public static float1x4 frac(float1x4 v) { return v % 1; }
	public static float2x1 frac(float2x1 v) { return v % 1; }
	public static float2x2 frac(float2x2 v) { return v % 1; }
	public static float2x3 frac(float2x3 v) { return v % 1; }
	public static float2x4 frac(float2x4 v) { return v % 1; }
	public static float3x1 frac(float3x1 v) { return v % 1; }
	public static float3x2 frac(float3x2 v) { return v % 1; }
	public static float3x3 frac(float3x3 v) { return v % 1; }
	public static float3x4 frac(float3x4 v) { return v % 1; }
	public static float4x1 frac(float4x1 v) { return v % 1; }
	public static float4x2 frac(float4x2 v) { return v % 1; }
	public static float4x3 frac(float4x3 v) { return v % 1; }
	public static float4x4 frac(float4x4 v) { return v % 1; }
	#endregion


	#region ldexp
	public static float ldexp(float a, float b) { return a * exp2(b); }
	public static float1 ldexp(float1 a, float1 b) { return a * exp2(b); }
	public static float2 ldexp(float2 a, float2 b) { return a * exp2(b); }
	public static float3 ldexp(float3 a, float3 b) { return a * exp2(b); }
	public static float4 ldexp(float4 a, float4 b) { return a * exp2(b); }
	public static float1x1 ldexp(float1x1 a, float1x1 b) { return a * exp2(b); }
	public static float1x2 ldexp(float1x2 a, float1x2 b) { return a * exp2(b); }
	public static float1x3 ldexp(float1x3 a, float1x3 b) { return a * exp2(b); }
	public static float1x4 ldexp(float1x4 a, float1x4 b) { return a * exp2(b); }
	public static float2x1 ldexp(float2x1 a, float2x1 b) { return a * exp2(b); }
	public static float2x2 ldexp(float2x2 a, float2x2 b) { return a * exp2(b); }
	public static float2x3 ldexp(float2x3 a, float2x3 b) { return a * exp2(b); }
	public static float2x4 ldexp(float2x4 a, float2x4 b) { return a * exp2(b); }
	public static float3x1 ldexp(float3x1 a, float3x1 b) { return a * exp2(b); }
	public static float3x2 ldexp(float3x2 a, float3x2 b) { return a * exp2(b); }
	public static float3x3 ldexp(float3x3 a, float3x3 b) { return a * exp2(b); }
	public static float3x4 ldexp(float3x4 a, float3x4 b) { return a * exp2(b); }
	public static float4x1 ldexp(float4x1 a, float4x1 b) { return a * exp2(b); }
	public static float4x2 ldexp(float4x2 a, float4x2 b) { return a * exp2(b); }
	public static float4x3 ldexp(float4x3 a, float4x3 b) { return a * exp2(b); }
	public static float4x4 ldexp(float4x4 a, float4x4 b) { return a * exp2(b); }
	#endregion


	#region lerp
	public static float lerp(float a, float b, float c) { return a + c*(b - a); }
	public static float1 lerp(float1 a, float1 b, float1 c) { return a + c*(b - a); }
	public static float2 lerp(float2 a, float2 b, float2 c) { return a + c*(b - a); }
	public static float3 lerp(float3 a, float3 b, float3 c) { return a + c*(b - a); }
	public static float4 lerp(float4 a, float4 b, float4 c) { return a + c*(b - a); }
	public static float1x1 lerp(float1x1 a, float1x1 b, float1x1 c) { return a + c*(b - a); }
	public static float1x2 lerp(float1x2 a, float1x2 b, float1x2 c) { return a + c*(b - a); }
	public static float1x3 lerp(float1x3 a, float1x3 b, float1x3 c) { return a + c*(b - a); }
	public static float1x4 lerp(float1x4 a, float1x4 b, float1x4 c) { return a + c*(b - a); }
	public static float2x1 lerp(float2x1 a, float2x1 b, float2x1 c) { return a + c*(b - a); }
	public static float2x2 lerp(float2x2 a, float2x2 b, float2x2 c) { return a + c*(b - a); }
	public static float2x3 lerp(float2x3 a, float2x3 b, float2x3 c) { return a + c*(b - a); }
	public static float2x4 lerp(float2x4 a, float2x4 b, float2x4 c) { return a + c*(b - a); }
	public static float3x1 lerp(float3x1 a, float3x1 b, float3x1 c) { return a + c*(b - a); }
	public static float3x2 lerp(float3x2 a, float3x2 b, float3x2 c) { return a + c*(b - a); }
	public static float3x3 lerp(float3x3 a, float3x3 b, float3x3 c) { return a + c*(b - a); }
	public static float3x4 lerp(float3x4 a, float3x4 b, float3x4 c) { return a + c*(b - a); }
	public static float4x1 lerp(float4x1 a, float4x1 b, float4x1 c) { return a + c*(b - a); }
	public static float4x2 lerp(float4x2 a, float4x2 b, float4x2 c) { return a + c*(b - a); }
	public static float4x3 lerp(float4x3 a, float4x3 b, float4x3 c) { return a + c*(b - a); }
	public static float4x4 lerp(float4x4 a, float4x4 b, float4x4 c) { return a + c*(b - a); }
	#endregion


	#region log
	public static float log(float v) { return (float)Math.Log(v); }

	public static float1 log(float1 v) { return new float1((float)Math.Log(v.x)); }
	public static float2 log(float2 v) { return new float2((float)Math.Log(v.x), (float)Math.Log(v.y)); }
	public static float3 log(float3 v) { return new float3((float)Math.Log(v.x), (float)Math.Log(v.y), (float)Math.Log(v.z)); }
	public static float4 log(float4 v) { return new float4((float)Math.Log(v.x), (float)Math.Log(v.y), (float)Math.Log(v.z), (float)Math.Log(v.w)); }

	public static float1x1 log(float1x1 v) { return new float1x1((float)Math.Log(v._m00)); }
	public static float1x2 log(float1x2 v) { return new float1x2((float)Math.Log(v._m00), (float)Math.Log(v._m01)); }
	public static float1x3 log(float1x3 v) { return new float1x3((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02)); }
	public static float1x4 log(float1x4 v) { return new float1x4((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m03)); }
	public static float2x1 log(float2x1 v) { return new float2x1((float)Math.Log(v._m00), (float)Math.Log(v._m10)); }
	public static float2x2 log(float2x2 v) { return new float2x2((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m10), (float)Math.Log(v._m11)); }
	public static float2x3 log(float2x3 v) { return new float2x3((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12)); }
	public static float2x4 log(float2x4 v) { return new float2x4((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m03), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12), (float)Math.Log(v._m13)); }
	public static float3x1 log(float3x1 v) { return new float3x1((float)Math.Log(v._m00), (float)Math.Log(v._m10), (float)Math.Log(v._m20)); }
	public static float3x2 log(float3x2 v) { return new float3x2((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m20), (float)Math.Log(v._m21)); }
	public static float3x3 log(float3x3 v) { return new float3x3((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12), (float)Math.Log(v._m20), (float)Math.Log(v._m21), (float)Math.Log(v._m22)); }
	public static float3x4 log(float3x4 v) { return new float3x4((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m03), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12), (float)Math.Log(v._m13), (float)Math.Log(v._m20), (float)Math.Log(v._m21), (float)Math.Log(v._m22), (float)Math.Log(v._m23)); }
	public static float4x1 log(float4x1 v) { return new float4x1((float)Math.Log(v._m00), (float)Math.Log(v._m10), (float)Math.Log(v._m20), (float)Math.Log(v._m30)); }
	public static float4x2 log(float4x2 v) { return new float4x2((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m20), (float)Math.Log(v._m21), (float)Math.Log(v._m30), (float)Math.Log(v._m31)); }
	public static float4x3 log(float4x3 v) { return new float4x3((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12), (float)Math.Log(v._m20), (float)Math.Log(v._m21), (float)Math.Log(v._m22), (float)Math.Log(v._m30), (float)Math.Log(v._m31), (float)Math.Log(v._m32)); }
	public static float4x4 log(float4x4 v) { return new float4x4((float)Math.Log(v._m00), (float)Math.Log(v._m01), (float)Math.Log(v._m02), (float)Math.Log(v._m03), (float)Math.Log(v._m10), (float)Math.Log(v._m11), (float)Math.Log(v._m12), (float)Math.Log(v._m13), (float)Math.Log(v._m20), (float)Math.Log(v._m21), (float)Math.Log(v._m22), (float)Math.Log(v._m23), (float)Math.Log(v._m30), (float)Math.Log(v._m31), (float)Math.Log(v._m32), (float)Math.Log(v._m33)); }

	#endregion


	#region log10
	public static float log10(float v) { return (float)Math.Log10(v); }

	public static float1 log10(float1 v) { return new float1((float)Math.Log10(v.x)); }
	public static float2 log10(float2 v) { return new float2((float)Math.Log10(v.x), (float)Math.Log10(v.y)); }
	public static float3 log10(float3 v) { return new float3((float)Math.Log10(v.x), (float)Math.Log10(v.y), (float)Math.Log10(v.z)); }
	public static float4 log10(float4 v) { return new float4((float)Math.Log10(v.x), (float)Math.Log10(v.y), (float)Math.Log10(v.z), (float)Math.Log10(v.w)); }

	public static float1x1 log10(float1x1 v) { return new float1x1((float)Math.Log10(v._m00)); }
	public static float1x2 log10(float1x2 v) { return new float1x2((float)Math.Log10(v._m00), (float)Math.Log10(v._m01)); }
	public static float1x3 log10(float1x3 v) { return new float1x3((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02)); }
	public static float1x4 log10(float1x4 v) { return new float1x4((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m03)); }
	public static float2x1 log10(float2x1 v) { return new float2x1((float)Math.Log10(v._m00), (float)Math.Log10(v._m10)); }
	public static float2x2 log10(float2x2 v) { return new float2x2((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11)); }
	public static float2x3 log10(float2x3 v) { return new float2x3((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12)); }
	public static float2x4 log10(float2x4 v) { return new float2x4((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m03), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12), (float)Math.Log10(v._m13)); }
	public static float3x1 log10(float3x1 v) { return new float3x1((float)Math.Log10(v._m00), (float)Math.Log10(v._m10), (float)Math.Log10(v._m20)); }
	public static float3x2 log10(float3x2 v) { return new float3x2((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21)); }
	public static float3x3 log10(float3x3 v) { return new float3x3((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21), (float)Math.Log10(v._m22)); }
	public static float3x4 log10(float3x4 v) { return new float3x4((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m03), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12), (float)Math.Log10(v._m13), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21), (float)Math.Log10(v._m22), (float)Math.Log10(v._m23)); }
	public static float4x1 log10(float4x1 v) { return new float4x1((float)Math.Log10(v._m00), (float)Math.Log10(v._m10), (float)Math.Log10(v._m20), (float)Math.Log10(v._m30)); }
	public static float4x2 log10(float4x2 v) { return new float4x2((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21), (float)Math.Log10(v._m30), (float)Math.Log10(v._m31)); }
	public static float4x3 log10(float4x3 v) { return new float4x3((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21), (float)Math.Log10(v._m22), (float)Math.Log10(v._m30), (float)Math.Log10(v._m31), (float)Math.Log10(v._m32)); }
	public static float4x4 log10(float4x4 v) { return new float4x4((float)Math.Log10(v._m00), (float)Math.Log10(v._m01), (float)Math.Log10(v._m02), (float)Math.Log10(v._m03), (float)Math.Log10(v._m10), (float)Math.Log10(v._m11), (float)Math.Log10(v._m12), (float)Math.Log10(v._m13), (float)Math.Log10(v._m20), (float)Math.Log10(v._m21), (float)Math.Log10(v._m22), (float)Math.Log10(v._m23), (float)Math.Log10(v._m30), (float)Math.Log10(v._m31), (float)Math.Log10(v._m32), (float)Math.Log10(v._m33)); }

	#endregion


	#region log2
	public static float log2(float v) { return (float)Math.Log(v, 2); }

	public static float1 log2(float1 v) { return new float1((float)Math.Log(v.x, 2)); }
	public static float2 log2(float2 v) { return new float2((float)Math.Log(v.x, 2), (float)Math.Log(v.y, 2)); }
	public static float3 log2(float3 v) { return new float3((float)Math.Log(v.x, 2), (float)Math.Log(v.y, 2), (float)Math.Log(v.z, 2)); }
	public static float4 log2(float4 v) { return new float4((float)Math.Log(v.x, 2), (float)Math.Log(v.y, 2), (float)Math.Log(v.z, 2), (float)Math.Log(v.w, 2)); }

	public static float1x1 log2(float1x1 v) { return new float1x1((float)Math.Log(v._m00, 2)); }
	public static float1x2 log2(float1x2 v) { return new float1x2((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2)); }
	public static float1x3 log2(float1x3 v) { return new float1x3((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2)); }
	public static float1x4 log2(float1x4 v) { return new float1x4((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m03, 2)); }
	public static float2x1 log2(float2x1 v) { return new float2x1((float)Math.Log(v._m00, 2), (float)Math.Log(v._m10, 2)); }
	public static float2x2 log2(float2x2 v) { return new float2x2((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2)); }
	public static float2x3 log2(float2x3 v) { return new float2x3((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2)); }
	public static float2x4 log2(float2x4 v) { return new float2x4((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m03, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2), (float)Math.Log(v._m13, 2)); }
	public static float3x1 log2(float3x1 v) { return new float3x1((float)Math.Log(v._m00, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m20, 2)); }
	public static float3x2 log2(float3x2 v) { return new float3x2((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2)); }
	public static float3x3 log2(float3x3 v) { return new float3x3((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2), (float)Math.Log(v._m22, 2)); }
	public static float3x4 log2(float3x4 v) { return new float3x4((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m03, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2), (float)Math.Log(v._m13, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2), (float)Math.Log(v._m22, 2), (float)Math.Log(v._m23, 2)); }
	public static float4x1 log2(float4x1 v) { return new float4x1((float)Math.Log(v._m00, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m30, 2)); }
	public static float4x2 log2(float4x2 v) { return new float4x2((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2), (float)Math.Log(v._m30, 2), (float)Math.Log(v._m31, 2)); }
	public static float4x3 log2(float4x3 v) { return new float4x3((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2), (float)Math.Log(v._m22, 2), (float)Math.Log(v._m30, 2), (float)Math.Log(v._m31, 2), (float)Math.Log(v._m32, 2)); }
	public static float4x4 log2(float4x4 v) { return new float4x4((float)Math.Log(v._m00, 2), (float)Math.Log(v._m01, 2), (float)Math.Log(v._m02, 2), (float)Math.Log(v._m03, 2), (float)Math.Log(v._m10, 2), (float)Math.Log(v._m11, 2), (float)Math.Log(v._m12, 2), (float)Math.Log(v._m13, 2), (float)Math.Log(v._m20, 2), (float)Math.Log(v._m21, 2), (float)Math.Log(v._m22, 2), (float)Math.Log(v._m23, 2), (float)Math.Log(v._m30, 2), (float)Math.Log(v._m31, 2), (float)Math.Log(v._m32, 2), (float)Math.Log(v._m33, 2)); }

	#endregion


	#region mul
	public static float1x1 mul(float1x1 a, float1x1 b) { return new float1x1(a._m00*b._m00); }
	public static float1x2 mul(float1x1 a, float1x2 b) { return new float1x2(a._m00*b._m00, a._m00*b._m01); }
	public static float1x3 mul(float1x1 a, float1x3 b) { return new float1x3(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02); }
	public static float1x4 mul(float1x1 a, float1x4 b) { return new float1x4(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m00*b._m03); }
	public static float1x1 mul(float1x2 a, float2x1 b) { return new float1x1(a._m00*b._m00+a._m01*b._m10); }
	public static float1x2 mul(float1x2 a, float2x2 b) { return new float1x2(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11); }
	public static float1x3 mul(float1x2 a, float2x3 b) { return new float1x3(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12); }
	public static float1x4 mul(float1x2 a, float2x4 b) { return new float1x4(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m00*b._m03+a._m01*b._m13); }
	public static float1x1 mul(float1x3 a, float3x1 b) { return new float1x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20); }
	public static float1x2 mul(float1x3 a, float3x2 b) { return new float1x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21); }
	public static float1x3 mul(float1x3 a, float3x3 b) { return new float1x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22); }

	public static float3 mul(float3 a, float3x3 b) { return (float3)mul((float1x3)a, b); }

		public static float1x4 mul(float1x3 a, float3x4 b) { return new float1x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23); }
	public static float1x1 mul(float1x4 a, float4x1 b) { return new float1x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30); }
	public static float1x2 mul(float1x4 a, float4x2 b) { return new float1x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31); }
	public static float1x3 mul(float1x4 a, float4x3 b) { return new float1x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32); }
	public static float1x4 mul(float1x4 a, float4x4 b) { return new float1x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23+a._m03*b._m33); }
	public static float4 mul(float4 a, float4x4 b) { return (float4)mul((float1x4)a, b); }
	public static float2x1 mul(float2x1 a, float1x1 b) { return new float2x1(a._m00*b._m00, a._m10*b._m00); }
	public static float2x2 mul(float2x1 a, float1x2 b) { return new float2x2(a._m00*b._m00, a._m00*b._m01, a._m10*b._m00, a._m10*b._m01); }
	public static float2x3 mul(float2x1 a, float1x3 b) { return new float2x3(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02); }
	public static float2x4 mul(float2x1 a, float1x4 b) { return new float2x4(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m00*b._m03, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02, a._m10*b._m03); }
	public static float2x1 mul(float2x2 a, float2x1 b) { return new float2x1(a._m00*b._m00+a._m01*b._m10, a._m10*b._m00+a._m11*b._m10); }
	public static float2x2 mul(float2x2 a, float2x2 b) { return new float2x2(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11); }
	public static float2x3 mul(float2x2 a, float2x3 b) { return new float2x3(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12); }
	public static float2x4 mul(float2x2 a, float2x4 b) { return new float2x4(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m00*b._m03+a._m01*b._m13, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12, a._m10*b._m03+a._m11*b._m13); }
	public static float2x1 mul(float2x3 a, float3x1 b) { return new float2x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20); }
	public static float2x2 mul(float2x3 a, float3x2 b) { return new float2x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21); }
	public static float2x3 mul(float2x3 a, float3x3 b) { return new float2x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22); }
	public static float2x4 mul(float2x3 a, float3x4 b) { return new float2x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23); }
	public static float2x1 mul(float2x4 a, float4x1 b) { return new float2x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30); }
	public static float2x2 mul(float2x4 a, float4x2 b) { return new float2x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31); }
	public static float2x3 mul(float2x4 a, float4x3 b) { return new float2x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32); }
	public static float2x4 mul(float2x4 a, float4x4 b) { return new float2x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23+a._m03*b._m33, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23+a._m13*b._m33); }
	public static float3x1 mul(float3x1 a, float1x1 b) { return new float3x1(a._m00*b._m00, a._m10*b._m00, a._m20*b._m00); }
	public static float3x2 mul(float3x1 a, float1x2 b) { return new float3x2(a._m00*b._m00, a._m00*b._m01, a._m10*b._m00, a._m10*b._m01, a._m20*b._m00, a._m20*b._m01); }
	public static float3x3 mul(float3x1 a, float1x3 b) { return new float3x3(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02, a._m20*b._m00, a._m20*b._m01, a._m20*b._m02); }
	public static float3x4 mul(float3x1 a, float1x4 b) { return new float3x4(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m00*b._m03, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02, a._m10*b._m03, a._m20*b._m00, a._m20*b._m01, a._m20*b._m02, a._m20*b._m03); }
	public static float3x1 mul(float3x2 a, float2x1 b) { return new float3x1(a._m00*b._m00+a._m01*b._m10, a._m10*b._m00+a._m11*b._m10, a._m20*b._m00+a._m21*b._m10); }
	public static float3x2 mul(float3x2 a, float2x2 b) { return new float3x2(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11); }
	public static float3x3 mul(float3x2 a, float2x3 b) { return new float3x3(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11, a._m20*b._m02+a._m21*b._m12); }
	public static float3x4 mul(float3x2 a, float2x4 b) { return new float3x4(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m00*b._m03+a._m01*b._m13, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12, a._m10*b._m03+a._m11*b._m13, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11, a._m20*b._m02+a._m21*b._m12, a._m20*b._m03+a._m21*b._m13); }
	public static float3x1 mul(float3x3 a, float3x1 b) { return new float3x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20); }
	public static float3x2 mul(float3x3 a, float3x2 b) { return new float3x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21); }
	public static float3x3 mul(float3x3 a, float3x3 b) { return new float3x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22); }
	public static float3x4 mul(float3x3 a, float3x4 b) { return new float3x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22, a._m20*b._m03+a._m21*b._m13+a._m22*b._m23); }
	public static float3x1 mul(float3x4 a, float4x1 b) { return new float3x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30); }
	public static float3x2 mul(float3x4 a, float4x2 b) { return new float3x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31); }
	public static float3x3 mul(float3x4 a, float4x3 b) { return new float3x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22+a._m23*b._m32); }
	public static float3x4 mul(float3x4 a, float4x4 b) { return new float3x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23+a._m03*b._m33, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23+a._m13*b._m33, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22+a._m23*b._m32, a._m20*b._m03+a._m21*b._m13+a._m22*b._m23+a._m23*b._m33); }
	public static float4x1 mul(float4x1 a, float1x1 b) { return new float4x1(a._m00*b._m00, a._m10*b._m00, a._m20*b._m00, a._m30*b._m00); }
	public static float4x2 mul(float4x1 a, float1x2 b) { return new float4x2(a._m00*b._m00, a._m00*b._m01, a._m10*b._m00, a._m10*b._m01, a._m20*b._m00, a._m20*b._m01, a._m30*b._m00, a._m30*b._m01); }
	public static float4x3 mul(float4x1 a, float1x3 b) { return new float4x3(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02, a._m20*b._m00, a._m20*b._m01, a._m20*b._m02, a._m30*b._m00, a._m30*b._m01, a._m30*b._m02); }
	public static float4x4 mul(float4x1 a, float1x4 b) { return new float4x4(a._m00*b._m00, a._m00*b._m01, a._m00*b._m02, a._m00*b._m03, a._m10*b._m00, a._m10*b._m01, a._m10*b._m02, a._m10*b._m03, a._m20*b._m00, a._m20*b._m01, a._m20*b._m02, a._m20*b._m03, a._m30*b._m00, a._m30*b._m01, a._m30*b._m02, a._m30*b._m03); }
	public static float4x1 mul(float4x2 a, float2x1 b) { return new float4x1(a._m00*b._m00+a._m01*b._m10, a._m10*b._m00+a._m11*b._m10, a._m20*b._m00+a._m21*b._m10, a._m30*b._m00+a._m31*b._m10); }
	public static float4x2 mul(float4x2 a, float2x2 b) { return new float4x2(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11, a._m30*b._m00+a._m31*b._m10, a._m30*b._m01+a._m31*b._m11); }
	public static float4x3 mul(float4x2 a, float2x3 b) { return new float4x3(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11, a._m20*b._m02+a._m21*b._m12, a._m30*b._m00+a._m31*b._m10, a._m30*b._m01+a._m31*b._m11, a._m30*b._m02+a._m31*b._m12); }
	public static float4x4 mul(float4x2 a, float2x4 b) { return new float4x4(a._m00*b._m00+a._m01*b._m10, a._m00*b._m01+a._m01*b._m11, a._m00*b._m02+a._m01*b._m12, a._m00*b._m03+a._m01*b._m13, a._m10*b._m00+a._m11*b._m10, a._m10*b._m01+a._m11*b._m11, a._m10*b._m02+a._m11*b._m12, a._m10*b._m03+a._m11*b._m13, a._m20*b._m00+a._m21*b._m10, a._m20*b._m01+a._m21*b._m11, a._m20*b._m02+a._m21*b._m12, a._m20*b._m03+a._m21*b._m13, a._m30*b._m00+a._m31*b._m10, a._m30*b._m01+a._m31*b._m11, a._m30*b._m02+a._m31*b._m12, a._m30*b._m03+a._m31*b._m13); }
	public static float4x1 mul(float4x3 a, float3x1 b) { return new float4x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20); }
	public static float4x2 mul(float4x3 a, float3x2 b) { return new float4x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21); }
	public static float4x3 mul(float4x3 a, float3x3 b) { return new float4x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21, a._m30*b._m02+a._m31*b._m12+a._m32*b._m22); }
	public static float4x4 mul(float4x3 a, float3x4 b) { return new float4x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22, a._m20*b._m03+a._m21*b._m13+a._m22*b._m23, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21, a._m30*b._m02+a._m31*b._m12+a._m32*b._m22, a._m30*b._m03+a._m31*b._m13+a._m32*b._m23); }
	public static float4x1 mul(float4x4 a, float4x1 b) { return new float4x1(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20+a._m33*b._m30); }
	public static float4x2 mul(float4x4 a, float4x2 b) { return new float4x2(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20+a._m33*b._m30, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21+a._m33*b._m31); }
	public static float4x3 mul(float4x4 a, float4x3 b) { return new float4x3(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22+a._m23*b._m32, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20+a._m33*b._m30, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21+a._m33*b._m31, a._m30*b._m02+a._m31*b._m12+a._m32*b._m22+a._m33*b._m32); }
	public static float4x4 mul(float4x4 a, float4x4 b) { return new float4x4(a._m00*b._m00+a._m01*b._m10+a._m02*b._m20+a._m03*b._m30, a._m00*b._m01+a._m01*b._m11+a._m02*b._m21+a._m03*b._m31, a._m00*b._m02+a._m01*b._m12+a._m02*b._m22+a._m03*b._m32, a._m00*b._m03+a._m01*b._m13+a._m02*b._m23+a._m03*b._m33, a._m10*b._m00+a._m11*b._m10+a._m12*b._m20+a._m13*b._m30, a._m10*b._m01+a._m11*b._m11+a._m12*b._m21+a._m13*b._m31, a._m10*b._m02+a._m11*b._m12+a._m12*b._m22+a._m13*b._m32, a._m10*b._m03+a._m11*b._m13+a._m12*b._m23+a._m13*b._m33, a._m20*b._m00+a._m21*b._m10+a._m22*b._m20+a._m23*b._m30, a._m20*b._m01+a._m21*b._m11+a._m22*b._m21+a._m23*b._m31, a._m20*b._m02+a._m21*b._m12+a._m22*b._m22+a._m23*b._m32, a._m20*b._m03+a._m21*b._m13+a._m22*b._m23+a._m23*b._m33, a._m30*b._m00+a._m31*b._m10+a._m32*b._m20+a._m33*b._m30, a._m30*b._m01+a._m31*b._m11+a._m32*b._m21+a._m33*b._m31, a._m30*b._m02+a._m31*b._m12+a._m32*b._m22+a._m33*b._m32, a._m30*b._m03+a._m31*b._m13+a._m32*b._m23+a._m33*b._m33); }
	#endregion


	#region normalize
	public static float1 normalize(float1 v) { return any(v)?v/length(v) : 0; }
	public static float2 normalize(float2 v) { return any(v)?v/length(v) : 0; }
	public static float3 normalize(float3 v) { return any(v)?v/length(v) : 0; }
	public static float4 normalize(float4 v) { return any(v)?v/length(v) : 0; }
	#endregion


	#region pow
	public static float pow(float a, float b) { return (float)Math.Pow(a,b); }

	public static float1 pow(float1 a, float1 b) { return new float1((float)Math.Pow(a.x,b.x)); }
	public static float2 pow(float2 a, float2 b) { return new float2((float)Math.Pow(a.x,b.x), (float)Math.Pow(a.y,b.y)); }
	public static float3 pow(float3 a, float3 b) { return new float3((float)Math.Pow(a.x,b.x), (float)Math.Pow(a.y,b.y), (float)Math.Pow(a.z,b.z)); }
	public static float4 pow(float4 a, float4 b) { return new float4((float)Math.Pow(a.x,b.x), (float)Math.Pow(a.y,b.y), (float)Math.Pow(a.z,b.z), (float)Math.Pow(a.w,b.w)); }

	public static float1x1 pow(float1x1 a, float1x1 b) { return new float1x1((float)Math.Pow(a._m00,b._m00)); }
	public static float1x2 pow(float1x2 a, float1x2 b) { return new float1x2((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01)); }
	public static float1x3 pow(float1x3 a, float1x3 b) { return new float1x3((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02)); }
	public static float1x4 pow(float1x4 a, float1x4 b) { return new float1x4((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m03,b._m03)); }
	public static float2x1 pow(float2x1 a, float2x1 b) { return new float2x1((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m10,b._m10)); }
	public static float2x2 pow(float2x2 a, float2x2 b) { return new float2x2((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11)); }
	public static float2x3 pow(float2x3 a, float2x3 b) { return new float2x3((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12)); }
	public static float2x4 pow(float2x4 a, float2x4 b) { return new float2x4((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m03,b._m03), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12), (float)Math.Pow(a._m13,b._m13)); }
	public static float3x1 pow(float3x1 a, float3x1 b) { return new float3x1((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m20,b._m20)); }
	public static float3x2 pow(float3x2 a, float3x2 b) { return new float3x2((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21)); }
	public static float3x3 pow(float3x3 a, float3x3 b) { return new float3x3((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21), (float)Math.Pow(a._m22,b._m22)); }
	public static float3x4 pow(float3x4 a, float3x4 b) { return new float3x4((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m03,b._m03), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12), (float)Math.Pow(a._m13,b._m13), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21), (float)Math.Pow(a._m22,b._m22), (float)Math.Pow(a._m23,b._m23)); }
	public static float4x1 pow(float4x1 a, float4x1 b) { return new float4x1((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m30,b._m30)); }
	public static float4x2 pow(float4x2 a, float4x2 b) { return new float4x2((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21), (float)Math.Pow(a._m30,b._m30), (float)Math.Pow(a._m31,b._m31)); }
	public static float4x3 pow(float4x3 a, float4x3 b) { return new float4x3((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21), (float)Math.Pow(a._m22,b._m22), (float)Math.Pow(a._m30,b._m30), (float)Math.Pow(a._m31,b._m31), (float)Math.Pow(a._m32,b._m32)); }
	public static float4x4 pow(float4x4 a, float4x4 b) { return new float4x4((float)Math.Pow(a._m00,b._m00), (float)Math.Pow(a._m01,b._m01), (float)Math.Pow(a._m02,b._m02), (float)Math.Pow(a._m03,b._m03), (float)Math.Pow(a._m10,b._m10), (float)Math.Pow(a._m11,b._m11), (float)Math.Pow(a._m12,b._m12), (float)Math.Pow(a._m13,b._m13), (float)Math.Pow(a._m20,b._m20), (float)Math.Pow(a._m21,b._m21), (float)Math.Pow(a._m22,b._m22), (float)Math.Pow(a._m23,b._m23), (float)Math.Pow(a._m30,b._m30), (float)Math.Pow(a._m31,b._m31), (float)Math.Pow(a._m32,b._m32), (float)Math.Pow(a._m33,b._m33)); }

	#endregion


	#region radians
	public static float radians(float v) { return (float)(v*Math.PI/180); }

	public static float1 radians(float1 v) { return new float1((float)(v.x*Math.PI/180)); }
	public static float2 radians(float2 v) { return new float2((float)(v.x*Math.PI/180), (float)(v.y*Math.PI/180)); }
	public static float3 radians(float3 v) { return new float3((float)(v.x*Math.PI/180), (float)(v.y*Math.PI/180), (float)(v.z*Math.PI/180)); }
	public static float4 radians(float4 v) { return new float4((float)(v.x*Math.PI/180), (float)(v.y*Math.PI/180), (float)(v.z*Math.PI/180), (float)(v.w*Math.PI/180)); }

	public static float1x1 radians(float1x1 v) { return new float1x1((float)(v._m00*Math.PI/180)); }
	public static float1x2 radians(float1x2 v) { return new float1x2((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180)); }
	public static float1x3 radians(float1x3 v) { return new float1x3((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180)); }
	public static float1x4 radians(float1x4 v) { return new float1x4((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m03*Math.PI/180)); }
	public static float2x1 radians(float2x1 v) { return new float2x1((float)(v._m00*Math.PI/180), (float)(v._m10*Math.PI/180)); }
	public static float2x2 radians(float2x2 v) { return new float2x2((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180)); }
	public static float2x3 radians(float2x3 v) { return new float2x3((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180)); }
	public static float2x4 radians(float2x4 v) { return new float2x4((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m03*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180), (float)(v._m13*Math.PI/180)); }
	public static float3x1 radians(float3x1 v) { return new float3x1((float)(v._m00*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m20*Math.PI/180)); }
	public static float3x2 radians(float3x2 v) { return new float3x2((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180)); }
	public static float3x3 radians(float3x3 v) { return new float3x3((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180), (float)(v._m22*Math.PI/180)); }
	public static float3x4 radians(float3x4 v) { return new float3x4((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m03*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180), (float)(v._m13*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180), (float)(v._m22*Math.PI/180), (float)(v._m23*Math.PI/180)); }
	public static float4x1 radians(float4x1 v) { return new float4x1((float)(v._m00*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m30*Math.PI/180)); }
	public static float4x2 radians(float4x2 v) { return new float4x2((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180), (float)(v._m30*Math.PI/180), (float)(v._m31*Math.PI/180)); }
	public static float4x3 radians(float4x3 v) { return new float4x3((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180), (float)(v._m22*Math.PI/180), (float)(v._m30*Math.PI/180), (float)(v._m31*Math.PI/180), (float)(v._m32*Math.PI/180)); }
	public static float4x4 radians(float4x4 v) { return new float4x4((float)(v._m00*Math.PI/180), (float)(v._m01*Math.PI/180), (float)(v._m02*Math.PI/180), (float)(v._m03*Math.PI/180), (float)(v._m10*Math.PI/180), (float)(v._m11*Math.PI/180), (float)(v._m12*Math.PI/180), (float)(v._m13*Math.PI/180), (float)(v._m20*Math.PI/180), (float)(v._m21*Math.PI/180), (float)(v._m22*Math.PI/180), (float)(v._m23*Math.PI/180), (float)(v._m30*Math.PI/180), (float)(v._m31*Math.PI/180), (float)(v._m32*Math.PI/180), (float)(v._m33*Math.PI/180)); }

	#endregion


	#region round
	public static float round(float v) { return (float)Math.Round(v); }

	public static float1 round(float1 v) { return new float1((float)Math.Round(v.x)); }
	public static float2 round(float2 v) { return new float2((float)Math.Round(v.x), (float)Math.Round(v.y)); }
	public static float3 round(float3 v) { return new float3((float)Math.Round(v.x), (float)Math.Round(v.y), (float)Math.Round(v.z)); }
	public static float4 round(float4 v) { return new float4((float)Math.Round(v.x), (float)Math.Round(v.y), (float)Math.Round(v.z), (float)Math.Round(v.w)); }

	public static float1x1 round(float1x1 v) { return new float1x1((float)Math.Round(v._m00)); }
	public static float1x2 round(float1x2 v) { return new float1x2((float)Math.Round(v._m00), (float)Math.Round(v._m01)); }
	public static float1x3 round(float1x3 v) { return new float1x3((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02)); }
	public static float1x4 round(float1x4 v) { return new float1x4((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m03)); }
	public static float2x1 round(float2x1 v) { return new float2x1((float)Math.Round(v._m00), (float)Math.Round(v._m10)); }
	public static float2x2 round(float2x2 v) { return new float2x2((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m10), (float)Math.Round(v._m11)); }
	public static float2x3 round(float2x3 v) { return new float2x3((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12)); }
	public static float2x4 round(float2x4 v) { return new float2x4((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m03), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12), (float)Math.Round(v._m13)); }
	public static float3x1 round(float3x1 v) { return new float3x1((float)Math.Round(v._m00), (float)Math.Round(v._m10), (float)Math.Round(v._m20)); }
	public static float3x2 round(float3x2 v) { return new float3x2((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m20), (float)Math.Round(v._m21)); }
	public static float3x3 round(float3x3 v) { return new float3x3((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12), (float)Math.Round(v._m20), (float)Math.Round(v._m21), (float)Math.Round(v._m22)); }
	public static float3x4 round(float3x4 v) { return new float3x4((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m03), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12), (float)Math.Round(v._m13), (float)Math.Round(v._m20), (float)Math.Round(v._m21), (float)Math.Round(v._m22), (float)Math.Round(v._m23)); }
	public static float4x1 round(float4x1 v) { return new float4x1((float)Math.Round(v._m00), (float)Math.Round(v._m10), (float)Math.Round(v._m20), (float)Math.Round(v._m30)); }
	public static float4x2 round(float4x2 v) { return new float4x2((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m20), (float)Math.Round(v._m21), (float)Math.Round(v._m30), (float)Math.Round(v._m31)); }
	public static float4x3 round(float4x3 v) { return new float4x3((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12), (float)Math.Round(v._m20), (float)Math.Round(v._m21), (float)Math.Round(v._m22), (float)Math.Round(v._m30), (float)Math.Round(v._m31), (float)Math.Round(v._m32)); }
	public static float4x4 round(float4x4 v) { return new float4x4((float)Math.Round(v._m00), (float)Math.Round(v._m01), (float)Math.Round(v._m02), (float)Math.Round(v._m03), (float)Math.Round(v._m10), (float)Math.Round(v._m11), (float)Math.Round(v._m12), (float)Math.Round(v._m13), (float)Math.Round(v._m20), (float)Math.Round(v._m21), (float)Math.Round(v._m22), (float)Math.Round(v._m23), (float)Math.Round(v._m30), (float)Math.Round(v._m31), (float)Math.Round(v._m32), (float)Math.Round(v._m33)); }

	#endregion


	#region rsqrt
	public static float rsqrt(float v) { return (float)(1.0/Math.Sqrt(v)); }

	public static float1 rsqrt(float1 v) { return new float1((float)(1.0/Math.Sqrt(v.x))); }
	public static float2 rsqrt(float2 v) { return new float2((float)(1.0/Math.Sqrt(v.x)), (float)(1.0/Math.Sqrt(v.y))); }
	public static float3 rsqrt(float3 v) { return new float3((float)(1.0/Math.Sqrt(v.x)), (float)(1.0/Math.Sqrt(v.y)), (float)(1.0/Math.Sqrt(v.z))); }
	public static float4 rsqrt(float4 v) { return new float4((float)(1.0/Math.Sqrt(v.x)), (float)(1.0/Math.Sqrt(v.y)), (float)(1.0/Math.Sqrt(v.z)), (float)(1.0/Math.Sqrt(v.w))); }

	public static float1x1 rsqrt(float1x1 v) { return new float1x1((float)(1.0/Math.Sqrt(v._m00))); }
	public static float1x2 rsqrt(float1x2 v) { return new float1x2((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01))); }
	public static float1x3 rsqrt(float1x3 v) { return new float1x3((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02))); }
	public static float1x4 rsqrt(float1x4 v) { return new float1x4((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m03))); }
	public static float2x1 rsqrt(float2x1 v) { return new float2x1((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m10))); }
	public static float2x2 rsqrt(float2x2 v) { return new float2x2((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11))); }
	public static float2x3 rsqrt(float2x3 v) { return new float2x3((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12))); }
	public static float2x4 rsqrt(float2x4 v) { return new float2x4((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m03)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12)), (float)(1.0/Math.Sqrt(v._m13))); }
	public static float3x1 rsqrt(float3x1 v) { return new float3x1((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m20))); }
	public static float3x2 rsqrt(float3x2 v) { return new float3x2((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21))); }
	public static float3x3 rsqrt(float3x3 v) { return new float3x3((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21)), (float)(1.0/Math.Sqrt(v._m22))); }
	public static float3x4 rsqrt(float3x4 v) { return new float3x4((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m03)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12)), (float)(1.0/Math.Sqrt(v._m13)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21)), (float)(1.0/Math.Sqrt(v._m22)), (float)(1.0/Math.Sqrt(v._m23))); }
	public static float4x1 rsqrt(float4x1 v) { return new float4x1((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m30))); }
	public static float4x2 rsqrt(float4x2 v) { return new float4x2((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21)), (float)(1.0/Math.Sqrt(v._m30)), (float)(1.0/Math.Sqrt(v._m31))); }
	public static float4x3 rsqrt(float4x3 v) { return new float4x3((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21)), (float)(1.0/Math.Sqrt(v._m22)), (float)(1.0/Math.Sqrt(v._m30)), (float)(1.0/Math.Sqrt(v._m31)), (float)(1.0/Math.Sqrt(v._m32))); }
	public static float4x4 rsqrt(float4x4 v) { return new float4x4((float)(1.0/Math.Sqrt(v._m00)), (float)(1.0/Math.Sqrt(v._m01)), (float)(1.0/Math.Sqrt(v._m02)), (float)(1.0/Math.Sqrt(v._m03)), (float)(1.0/Math.Sqrt(v._m10)), (float)(1.0/Math.Sqrt(v._m11)), (float)(1.0/Math.Sqrt(v._m12)), (float)(1.0/Math.Sqrt(v._m13)), (float)(1.0/Math.Sqrt(v._m20)), (float)(1.0/Math.Sqrt(v._m21)), (float)(1.0/Math.Sqrt(v._m22)), (float)(1.0/Math.Sqrt(v._m23)), (float)(1.0/Math.Sqrt(v._m30)), (float)(1.0/Math.Sqrt(v._m31)), (float)(1.0/Math.Sqrt(v._m32)), (float)(1.0/Math.Sqrt(v._m33))); }

	#endregion


	#region saturate
	public static float saturate(float v) { return max(0, min(1, v)); }
	public static float1 saturate(float1 v) { return max(0, min(1, v)); }
	public static float2 saturate(float2 v) { return max(0, min(1, v)); }
	public static float3 saturate(float3 v) { return max(0, min(1, v)); }
	public static float4 saturate(float4 v) { return max(0, min(1, v)); }
	public static float1x1 saturate(float1x1 v) { return max(0, min(1, v)); }
	public static float1x2 saturate(float1x2 v) { return max(0, min(1, v)); }
	public static float1x3 saturate(float1x3 v) { return max(0, min(1, v)); }
	public static float1x4 saturate(float1x4 v) { return max(0, min(1, v)); }
	public static float2x1 saturate(float2x1 v) { return max(0, min(1, v)); }
	public static float2x2 saturate(float2x2 v) { return max(0, min(1, v)); }
	public static float2x3 saturate(float2x3 v) { return max(0, min(1, v)); }
	public static float2x4 saturate(float2x4 v) { return max(0, min(1, v)); }
	public static float3x1 saturate(float3x1 v) { return max(0, min(1, v)); }
	public static float3x2 saturate(float3x2 v) { return max(0, min(1, v)); }
	public static float3x3 saturate(float3x3 v) { return max(0, min(1, v)); }
	public static float3x4 saturate(float3x4 v) { return max(0, min(1, v)); }
	public static float4x1 saturate(float4x1 v) { return max(0, min(1, v)); }
	public static float4x2 saturate(float4x2 v) { return max(0, min(1, v)); }
	public static float4x3 saturate(float4x3 v) { return max(0, min(1, v)); }
	public static float4x4 saturate(float4x4 v) { return max(0, min(1, v)); }
	#endregion


	#region sign
	public static int sign(int v) { return (int)Math.Sign(v); }

	public static int1 sign(int1 v) { return new int1((int)Math.Sign(v.x)); }
	public static int2 sign(int2 v) { return new int2((int)Math.Sign(v.x), (int)Math.Sign(v.y)); }
	public static int3 sign(int3 v) { return new int3((int)Math.Sign(v.x), (int)Math.Sign(v.y), (int)Math.Sign(v.z)); }
	public static int4 sign(int4 v) { return new int4((int)Math.Sign(v.x), (int)Math.Sign(v.y), (int)Math.Sign(v.z), (int)Math.Sign(v.w)); }

	public static int1x1 sign(int1x1 v) { return new int1x1((int)Math.Sign(v._m00)); }
	public static int1x2 sign(int1x2 v) { return new int1x2((int)Math.Sign(v._m00), (int)Math.Sign(v._m01)); }
	public static int1x3 sign(int1x3 v) { return new int1x3((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02)); }
	public static int1x4 sign(int1x4 v) { return new int1x4((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m03)); }
	public static int2x1 sign(int2x1 v) { return new int2x1((int)Math.Sign(v._m00), (int)Math.Sign(v._m10)); }
	public static int2x2 sign(int2x2 v) { return new int2x2((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11)); }
	public static int2x3 sign(int2x3 v) { return new int2x3((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12)); }
	public static int2x4 sign(int2x4 v) { return new int2x4((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m03), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12), (int)Math.Sign(v._m13)); }
	public static int3x1 sign(int3x1 v) { return new int3x1((int)Math.Sign(v._m00), (int)Math.Sign(v._m10), (int)Math.Sign(v._m20)); }
	public static int3x2 sign(int3x2 v) { return new int3x2((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21)); }
	public static int3x3 sign(int3x3 v) { return new int3x3((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21), (int)Math.Sign(v._m22)); }
	public static int3x4 sign(int3x4 v) { return new int3x4((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m03), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12), (int)Math.Sign(v._m13), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21), (int)Math.Sign(v._m22), (int)Math.Sign(v._m23)); }
	public static int4x1 sign(int4x1 v) { return new int4x1((int)Math.Sign(v._m00), (int)Math.Sign(v._m10), (int)Math.Sign(v._m20), (int)Math.Sign(v._m30)); }
	public static int4x2 sign(int4x2 v) { return new int4x2((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21), (int)Math.Sign(v._m30), (int)Math.Sign(v._m31)); }
	public static int4x3 sign(int4x3 v) { return new int4x3((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21), (int)Math.Sign(v._m22), (int)Math.Sign(v._m30), (int)Math.Sign(v._m31), (int)Math.Sign(v._m32)); }
	public static int4x4 sign(int4x4 v) { return new int4x4((int)Math.Sign(v._m00), (int)Math.Sign(v._m01), (int)Math.Sign(v._m02), (int)Math.Sign(v._m03), (int)Math.Sign(v._m10), (int)Math.Sign(v._m11), (int)Math.Sign(v._m12), (int)Math.Sign(v._m13), (int)Math.Sign(v._m20), (int)Math.Sign(v._m21), (int)Math.Sign(v._m22), (int)Math.Sign(v._m23), (int)Math.Sign(v._m30), (int)Math.Sign(v._m31), (int)Math.Sign(v._m32), (int)Math.Sign(v._m33)); }

	#endregion


	#region sin
	public static float sin(float v) { return (float)Math.Sin(v); }

	public static float1 sin(float1 v) { return new float1((float)Math.Sin(v.x)); }
	public static float2 sin(float2 v) { return new float2((float)Math.Sin(v.x), (float)Math.Sin(v.y)); }
	public static float3 sin(float3 v) { return new float3((float)Math.Sin(v.x), (float)Math.Sin(v.y), (float)Math.Sin(v.z)); }
	public static float4 sin(float4 v) { return new float4((float)Math.Sin(v.x), (float)Math.Sin(v.y), (float)Math.Sin(v.z), (float)Math.Sin(v.w)); }

	public static float1x1 sin(float1x1 v) { return new float1x1((float)Math.Sin(v._m00)); }
	public static float1x2 sin(float1x2 v) { return new float1x2((float)Math.Sin(v._m00), (float)Math.Sin(v._m01)); }
	public static float1x3 sin(float1x3 v) { return new float1x3((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02)); }
	public static float1x4 sin(float1x4 v) { return new float1x4((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m03)); }
	public static float2x1 sin(float2x1 v) { return new float2x1((float)Math.Sin(v._m00), (float)Math.Sin(v._m10)); }
	public static float2x2 sin(float2x2 v) { return new float2x2((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11)); }
	public static float2x3 sin(float2x3 v) { return new float2x3((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12)); }
	public static float2x4 sin(float2x4 v) { return new float2x4((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m03), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12), (float)Math.Sin(v._m13)); }
	public static float3x1 sin(float3x1 v) { return new float3x1((float)Math.Sin(v._m00), (float)Math.Sin(v._m10), (float)Math.Sin(v._m20)); }
	public static float3x2 sin(float3x2 v) { return new float3x2((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21)); }
	public static float3x3 sin(float3x3 v) { return new float3x3((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21), (float)Math.Sin(v._m22)); }
	public static float3x4 sin(float3x4 v) { return new float3x4((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m03), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12), (float)Math.Sin(v._m13), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21), (float)Math.Sin(v._m22), (float)Math.Sin(v._m23)); }
	public static float4x1 sin(float4x1 v) { return new float4x1((float)Math.Sin(v._m00), (float)Math.Sin(v._m10), (float)Math.Sin(v._m20), (float)Math.Sin(v._m30)); }
	public static float4x2 sin(float4x2 v) { return new float4x2((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21), (float)Math.Sin(v._m30), (float)Math.Sin(v._m31)); }
	public static float4x3 sin(float4x3 v) { return new float4x3((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21), (float)Math.Sin(v._m22), (float)Math.Sin(v._m30), (float)Math.Sin(v._m31), (float)Math.Sin(v._m32)); }
	public static float4x4 sin(float4x4 v) { return new float4x4((float)Math.Sin(v._m00), (float)Math.Sin(v._m01), (float)Math.Sin(v._m02), (float)Math.Sin(v._m03), (float)Math.Sin(v._m10), (float)Math.Sin(v._m11), (float)Math.Sin(v._m12), (float)Math.Sin(v._m13), (float)Math.Sin(v._m20), (float)Math.Sin(v._m21), (float)Math.Sin(v._m22), (float)Math.Sin(v._m23), (float)Math.Sin(v._m30), (float)Math.Sin(v._m31), (float)Math.Sin(v._m32), (float)Math.Sin(v._m33)); }

	#endregion


	#region sinh
	public static float sinh(float v) { return (float)Math.Sinh(v); }

	public static float1 sinh(float1 v) { return new float1((float)Math.Sinh(v.x)); }
	public static float2 sinh(float2 v) { return new float2((float)Math.Sinh(v.x), (float)Math.Sinh(v.y)); }
	public static float3 sinh(float3 v) { return new float3((float)Math.Sinh(v.x), (float)Math.Sinh(v.y), (float)Math.Sinh(v.z)); }
	public static float4 sinh(float4 v) { return new float4((float)Math.Sinh(v.x), (float)Math.Sinh(v.y), (float)Math.Sinh(v.z), (float)Math.Sinh(v.w)); }

	public static float1x1 sinh(float1x1 v) { return new float1x1((float)Math.Sinh(v._m00)); }
	public static float1x2 sinh(float1x2 v) { return new float1x2((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01)); }
	public static float1x3 sinh(float1x3 v) { return new float1x3((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02)); }
	public static float1x4 sinh(float1x4 v) { return new float1x4((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m03)); }
	public static float2x1 sinh(float2x1 v) { return new float2x1((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m10)); }
	public static float2x2 sinh(float2x2 v) { return new float2x2((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11)); }
	public static float2x3 sinh(float2x3 v) { return new float2x3((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12)); }
	public static float2x4 sinh(float2x4 v) { return new float2x4((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m03), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12), (float)Math.Sinh(v._m13)); }
	public static float3x1 sinh(float3x1 v) { return new float3x1((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m20)); }
	public static float3x2 sinh(float3x2 v) { return new float3x2((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21)); }
	public static float3x3 sinh(float3x3 v) { return new float3x3((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21), (float)Math.Sinh(v._m22)); }
	public static float3x4 sinh(float3x4 v) { return new float3x4((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m03), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12), (float)Math.Sinh(v._m13), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21), (float)Math.Sinh(v._m22), (float)Math.Sinh(v._m23)); }
	public static float4x1 sinh(float4x1 v) { return new float4x1((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m30)); }
	public static float4x2 sinh(float4x2 v) { return new float4x2((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21), (float)Math.Sinh(v._m30), (float)Math.Sinh(v._m31)); }
	public static float4x3 sinh(float4x3 v) { return new float4x3((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21), (float)Math.Sinh(v._m22), (float)Math.Sinh(v._m30), (float)Math.Sinh(v._m31), (float)Math.Sinh(v._m32)); }
	public static float4x4 sinh(float4x4 v) { return new float4x4((float)Math.Sinh(v._m00), (float)Math.Sinh(v._m01), (float)Math.Sinh(v._m02), (float)Math.Sinh(v._m03), (float)Math.Sinh(v._m10), (float)Math.Sinh(v._m11), (float)Math.Sinh(v._m12), (float)Math.Sinh(v._m13), (float)Math.Sinh(v._m20), (float)Math.Sinh(v._m21), (float)Math.Sinh(v._m22), (float)Math.Sinh(v._m23), (float)Math.Sinh(v._m30), (float)Math.Sinh(v._m31), (float)Math.Sinh(v._m32), (float)Math.Sinh(v._m33)); }

	#endregion


	#region smoothstep
	public static float smoothstep(float a, float b, float c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float1 smoothstep(float1 a, float1 b, float1 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float2 smoothstep(float2 a, float2 b, float2 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float3 smoothstep(float3 a, float3 b, float3 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float4 smoothstep(float4 a, float4 b, float4 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float1x1 smoothstep(float1x1 a, float1x1 b, float1x1 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float1x2 smoothstep(float1x2 a, float1x2 b, float1x2 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float1x3 smoothstep(float1x3 a, float1x3 b, float1x3 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float1x4 smoothstep(float1x4 a, float1x4 b, float1x4 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float2x1 smoothstep(float2x1 a, float2x1 b, float2x1 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float2x2 smoothstep(float2x2 a, float2x2 b, float2x2 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float2x3 smoothstep(float2x3 a, float2x3 b, float2x3 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float2x4 smoothstep(float2x4 a, float2x4 b, float2x4 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float3x1 smoothstep(float3x1 a, float3x1 b, float3x1 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float3x2 smoothstep(float3x2 a, float3x2 b, float3x2 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float3x3 smoothstep(float3x3 a, float3x3 b, float3x3 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float3x4 smoothstep(float3x4 a, float3x4 b, float3x4 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float4x1 smoothstep(float4x1 a, float4x1 b, float4x1 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float4x2 smoothstep(float4x2 a, float4x2 b, float4x2 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float4x3 smoothstep(float4x3 a, float4x3 b, float4x3 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	public static float4x4 smoothstep(float4x4 a, float4x4 b, float4x4 c) { var t = saturate((c - a)/(b - a)); return t*t*(3 - 2 * t);  }
	#endregion


	#region sqrt
	public static float sqrt(float v) { return (float)Math.Sqrt(v); }

	public static float1 sqrt(float1 v) { return new float1((float)Math.Sqrt(v.x)); }
	public static float2 sqrt(float2 v) { return new float2((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y)); }
	public static float3 sqrt(float3 v) { return new float3((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y), (float)Math.Sqrt(v.z)); }
	public static float4 sqrt(float4 v) { return new float4((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y), (float)Math.Sqrt(v.z), (float)Math.Sqrt(v.w)); }

	public static float1x1 sqrt(float1x1 v) { return new float1x1((float)Math.Sqrt(v._m00)); }
	public static float1x2 sqrt(float1x2 v) { return new float1x2((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01)); }
	public static float1x3 sqrt(float1x3 v) { return new float1x3((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02)); }
	public static float1x4 sqrt(float1x4 v) { return new float1x4((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m03)); }
	public static float2x1 sqrt(float2x1 v) { return new float2x1((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m10)); }
	public static float2x2 sqrt(float2x2 v) { return new float2x2((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11)); }
	public static float2x3 sqrt(float2x3 v) { return new float2x3((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12)); }
	public static float2x4 sqrt(float2x4 v) { return new float2x4((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m03), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12), (float)Math.Sqrt(v._m13)); }
	public static float3x1 sqrt(float3x1 v) { return new float3x1((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m20)); }
	public static float3x2 sqrt(float3x2 v) { return new float3x2((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21)); }
	public static float3x3 sqrt(float3x3 v) { return new float3x3((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21), (float)Math.Sqrt(v._m22)); }
	public static float3x4 sqrt(float3x4 v) { return new float3x4((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m03), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12), (float)Math.Sqrt(v._m13), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21), (float)Math.Sqrt(v._m22), (float)Math.Sqrt(v._m23)); }
	public static float4x1 sqrt(float4x1 v) { return new float4x1((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m30)); }
	public static float4x2 sqrt(float4x2 v) { return new float4x2((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21), (float)Math.Sqrt(v._m30), (float)Math.Sqrt(v._m31)); }
	public static float4x3 sqrt(float4x3 v) { return new float4x3((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21), (float)Math.Sqrt(v._m22), (float)Math.Sqrt(v._m30), (float)Math.Sqrt(v._m31), (float)Math.Sqrt(v._m32)); }
	public static float4x4 sqrt(float4x4 v) { return new float4x4((float)Math.Sqrt(v._m00), (float)Math.Sqrt(v._m01), (float)Math.Sqrt(v._m02), (float)Math.Sqrt(v._m03), (float)Math.Sqrt(v._m10), (float)Math.Sqrt(v._m11), (float)Math.Sqrt(v._m12), (float)Math.Sqrt(v._m13), (float)Math.Sqrt(v._m20), (float)Math.Sqrt(v._m21), (float)Math.Sqrt(v._m22), (float)Math.Sqrt(v._m23), (float)Math.Sqrt(v._m30), (float)Math.Sqrt(v._m31), (float)Math.Sqrt(v._m32), (float)Math.Sqrt(v._m33)); }

	#endregion


	#region step
	public static float step(float a, float b) { return a >= b ? 1 : 0; }

	public static float1 step(float1 a, float1 b) { return new float1(a.x >= b.x ? 1 : 0); }
	public static float2 step(float2 a, float2 b) { return new float2(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0); }
	public static float3 step(float3 a, float3 b) { return new float3(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0, a.z >= b.z ? 1 : 0); }
	public static float4 step(float4 a, float4 b) { return new float4(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0, a.z >= b.z ? 1 : 0, a.w >= b.w ? 1 : 0); }

	public static float1x1 step(float1x1 a, float1x1 b) { return new float1x1(a._m00 >= b._m00 ? 1 : 0); }
	public static float1x2 step(float1x2 a, float1x2 b) { return new float1x2(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0); }
	public static float1x3 step(float1x3 a, float1x3 b) { return new float1x3(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0); }
	public static float1x4 step(float1x4 a, float1x4 b) { return new float1x4(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m03 >= b._m03 ? 1 : 0); }
	public static float2x1 step(float2x1 a, float2x1 b) { return new float2x1(a._m00 >= b._m00 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0); }
	public static float2x2 step(float2x2 a, float2x2 b) { return new float2x2(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0); }
	public static float2x3 step(float2x3 a, float2x3 b) { return new float2x3(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0); }
	public static float2x4 step(float2x4 a, float2x4 b) { return new float2x4(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m03 >= b._m03 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0, a._m13 >= b._m13 ? 1 : 0); }
	public static float3x1 step(float3x1 a, float3x1 b) { return new float3x1(a._m00 >= b._m00 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0); }
	public static float3x2 step(float3x2 a, float3x2 b) { return new float3x2(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0); }
	public static float3x3 step(float3x3 a, float3x3 b) { return new float3x3(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m22 >= b._m22 ? 1 : 0); }
	public static float3x4 step(float3x4 a, float3x4 b) { return new float3x4(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m03 >= b._m03 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0, a._m13 >= b._m13 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m22 >= b._m22 ? 1 : 0, a._m23 >= b._m23 ? 1 : 0); }
	public static float4x1 step(float4x1 a, float4x1 b) { return new float4x1(a._m00 >= b._m00 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m30 >= b._m30 ? 1 : 0); }
	public static float4x2 step(float4x2 a, float4x2 b) { return new float4x2(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m30 >= b._m30 ? 1 : 0, a._m31 >= b._m31 ? 1 : 0); }
	public static float4x3 step(float4x3 a, float4x3 b) { return new float4x3(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m22 >= b._m22 ? 1 : 0, a._m30 >= b._m30 ? 1 : 0, a._m31 >= b._m31 ? 1 : 0, a._m32 >= b._m32 ? 1 : 0); }
	public static float4x4 step(float4x4 a, float4x4 b) { return new float4x4(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0, a._m03 >= b._m03 ? 1 : 0, a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0, a._m13 >= b._m13 ? 1 : 0, a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m22 >= b._m22 ? 1 : 0, a._m23 >= b._m23 ? 1 : 0, a._m30 >= b._m30 ? 1 : 0, a._m31 >= b._m31 ? 1 : 0, a._m32 >= b._m32 ? 1 : 0, a._m33 >= b._m33 ? 1 : 0); }

	#endregion


	#region tan
	public static float tan(float v) { return (float)Math.Tan(v); }

	public static float1 tan(float1 v) { return new float1((float)Math.Tan(v.x)); }
	public static float2 tan(float2 v) { return new float2((float)Math.Tan(v.x), (float)Math.Tan(v.y)); }
	public static float3 tan(float3 v) { return new float3((float)Math.Tan(v.x), (float)Math.Tan(v.y), (float)Math.Tan(v.z)); }
	public static float4 tan(float4 v) { return new float4((float)Math.Tan(v.x), (float)Math.Tan(v.y), (float)Math.Tan(v.z), (float)Math.Tan(v.w)); }

	public static float1x1 tan(float1x1 v) { return new float1x1((float)Math.Tan(v._m00)); }
	public static float1x2 tan(float1x2 v) { return new float1x2((float)Math.Tan(v._m00), (float)Math.Tan(v._m01)); }
	public static float1x3 tan(float1x3 v) { return new float1x3((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02)); }
	public static float1x4 tan(float1x4 v) { return new float1x4((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m03)); }
	public static float2x1 tan(float2x1 v) { return new float2x1((float)Math.Tan(v._m00), (float)Math.Tan(v._m10)); }
	public static float2x2 tan(float2x2 v) { return new float2x2((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11)); }
	public static float2x3 tan(float2x3 v) { return new float2x3((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12)); }
	public static float2x4 tan(float2x4 v) { return new float2x4((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m03), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12), (float)Math.Tan(v._m13)); }
	public static float3x1 tan(float3x1 v) { return new float3x1((float)Math.Tan(v._m00), (float)Math.Tan(v._m10), (float)Math.Tan(v._m20)); }
	public static float3x2 tan(float3x2 v) { return new float3x2((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21)); }
	public static float3x3 tan(float3x3 v) { return new float3x3((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21), (float)Math.Tan(v._m22)); }
	public static float3x4 tan(float3x4 v) { return new float3x4((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m03), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12), (float)Math.Tan(v._m13), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21), (float)Math.Tan(v._m22), (float)Math.Tan(v._m23)); }
	public static float4x1 tan(float4x1 v) { return new float4x1((float)Math.Tan(v._m00), (float)Math.Tan(v._m10), (float)Math.Tan(v._m20), (float)Math.Tan(v._m30)); }
	public static float4x2 tan(float4x2 v) { return new float4x2((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21), (float)Math.Tan(v._m30), (float)Math.Tan(v._m31)); }
	public static float4x3 tan(float4x3 v) { return new float4x3((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21), (float)Math.Tan(v._m22), (float)Math.Tan(v._m30), (float)Math.Tan(v._m31), (float)Math.Tan(v._m32)); }
	public static float4x4 tan(float4x4 v) { return new float4x4((float)Math.Tan(v._m00), (float)Math.Tan(v._m01), (float)Math.Tan(v._m02), (float)Math.Tan(v._m03), (float)Math.Tan(v._m10), (float)Math.Tan(v._m11), (float)Math.Tan(v._m12), (float)Math.Tan(v._m13), (float)Math.Tan(v._m20), (float)Math.Tan(v._m21), (float)Math.Tan(v._m22), (float)Math.Tan(v._m23), (float)Math.Tan(v._m30), (float)Math.Tan(v._m31), (float)Math.Tan(v._m32), (float)Math.Tan(v._m33)); }

	#endregion


	#region tanh
	public static float tanh(float v) { return (float)Math.Tanh(v); }

	public static float1 tanh(float1 v) { return new float1((float)Math.Tanh(v.x)); }
	public static float2 tanh(float2 v) { return new float2((float)Math.Tanh(v.x), (float)Math.Tanh(v.y)); }
	public static float3 tanh(float3 v) { return new float3((float)Math.Tanh(v.x), (float)Math.Tanh(v.y), (float)Math.Tanh(v.z)); }
	public static float4 tanh(float4 v) { return new float4((float)Math.Tanh(v.x), (float)Math.Tanh(v.y), (float)Math.Tanh(v.z), (float)Math.Tanh(v.w)); }

	public static float1x1 tanh(float1x1 v) { return new float1x1((float)Math.Tanh(v._m00)); }
	public static float1x2 tanh(float1x2 v) { return new float1x2((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01)); }
	public static float1x3 tanh(float1x3 v) { return new float1x3((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02)); }
	public static float1x4 tanh(float1x4 v) { return new float1x4((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m03)); }
	public static float2x1 tanh(float2x1 v) { return new float2x1((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m10)); }
	public static float2x2 tanh(float2x2 v) { return new float2x2((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11)); }
	public static float2x3 tanh(float2x3 v) { return new float2x3((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12)); }
	public static float2x4 tanh(float2x4 v) { return new float2x4((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m03), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12), (float)Math.Tanh(v._m13)); }
	public static float3x1 tanh(float3x1 v) { return new float3x1((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m20)); }
	public static float3x2 tanh(float3x2 v) { return new float3x2((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21)); }
	public static float3x3 tanh(float3x3 v) { return new float3x3((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21), (float)Math.Tanh(v._m22)); }
	public static float3x4 tanh(float3x4 v) { return new float3x4((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m03), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12), (float)Math.Tanh(v._m13), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21), (float)Math.Tanh(v._m22), (float)Math.Tanh(v._m23)); }
	public static float4x1 tanh(float4x1 v) { return new float4x1((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m30)); }
	public static float4x2 tanh(float4x2 v) { return new float4x2((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21), (float)Math.Tanh(v._m30), (float)Math.Tanh(v._m31)); }
	public static float4x3 tanh(float4x3 v) { return new float4x3((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21), (float)Math.Tanh(v._m22), (float)Math.Tanh(v._m30), (float)Math.Tanh(v._m31), (float)Math.Tanh(v._m32)); }
	public static float4x4 tanh(float4x4 v) { return new float4x4((float)Math.Tanh(v._m00), (float)Math.Tanh(v._m01), (float)Math.Tanh(v._m02), (float)Math.Tanh(v._m03), (float)Math.Tanh(v._m10), (float)Math.Tanh(v._m11), (float)Math.Tanh(v._m12), (float)Math.Tanh(v._m13), (float)Math.Tanh(v._m20), (float)Math.Tanh(v._m21), (float)Math.Tanh(v._m22), (float)Math.Tanh(v._m23), (float)Math.Tanh(v._m30), (float)Math.Tanh(v._m31), (float)Math.Tanh(v._m32), (float)Math.Tanh(v._m33)); }

	#endregion


	#region transpose
	public static float1x1 transpose(float1x1 a) { return new float1x1(a._m00); }
	public static float1x2 transpose(float2x1 a) { return new float1x2(a._m00, a._m10); }
	public static float1x3 transpose(float3x1 a) { return new float1x3(a._m00, a._m10, a._m20); }
	public static float1x4 transpose(float4x1 a) { return new float1x4(a._m00, a._m10, a._m20, a._m30); }
	public static float2x1 transpose(float1x2 a) { return new float2x1(a._m00, a._m01); }
	public static float2x2 transpose(float2x2 a) { return new float2x2(a._m00, a._m10, a._m01, a._m11); }
	public static float2x3 transpose(float3x2 a) { return new float2x3(a._m00, a._m10, a._m20, a._m01, a._m11, a._m21); }
	public static float2x4 transpose(float4x2 a) { return new float2x4(a._m00, a._m10, a._m20, a._m30, a._m01, a._m11, a._m21, a._m31); }
	public static float3x1 transpose(float1x3 a) { return new float3x1(a._m00, a._m01, a._m02); }
	public static float3x2 transpose(float2x3 a) { return new float3x2(a._m00, a._m10, a._m01, a._m11, a._m02, a._m12); }
	public static float3x3 transpose(float3x3 a) { return new float3x3(a._m00, a._m10, a._m20, a._m01, a._m11, a._m21, a._m02, a._m12, a._m22); }
	public static float3x4 transpose(float4x3 a) { return new float3x4(a._m00, a._m10, a._m20, a._m30, a._m01, a._m11, a._m21, a._m31, a._m02, a._m12, a._m22, a._m32); }
	public static float4x1 transpose(float1x4 a) { return new float4x1(a._m00, a._m01, a._m02, a._m03); }
	public static float4x2 transpose(float2x4 a) { return new float4x2(a._m00, a._m10, a._m01, a._m11, a._m02, a._m12, a._m03, a._m13); }
	public static float4x3 transpose(float3x4 a) { return new float4x3(a._m00, a._m10, a._m20, a._m01, a._m11, a._m21, a._m02, a._m12, a._m22, a._m03, a._m13, a._m23); }
	public static float4x4 transpose(float4x4 a) { return new float4x4(a._m00, a._m10, a._m20, a._m30, a._m01, a._m11, a._m21, a._m31, a._m02, a._m12, a._m22, a._m32, a._m03, a._m13, a._m23, a._m33); }
	#endregion


	#region Constructors
	public static float1 float1(float x) { return new float1(x); }

	public static float2 float2(float x, float y) { return new float2(x, y); }

	public static float3 float3(float x, float y, float z) { return new float3(x, y, z); }

	public static float4 float4(float x, float y, float z, float w) { return new float4(x, y, z, w); }
	public static float4 float4(float3 xyz, float w) { return new float4(xyz, w); }

	public static float1x1 float1x1(float _m00) { return new float1x1(_m00); }

	public static float1x1 float1x1(float1 r0) { return new float1x1(r0.x); }

	public static float1x2 float1x2(float _m00, float _m01) { return new float1x2(_m00, _m01); }

	public static float1x2 float1x2(float2 r0) { return new float1x2(r0.x, r0.y); }

	public static float1x3 float1x3(float _m00, float _m01, float _m02) { return new float1x3(_m00, _m01, _m02); }

	public static float1x3 float1x3(float3 r0) { return new float1x3(r0.x, r0.y, r0.z); }

	public static float1x4 float1x4(float _m00, float _m01, float _m02, float _m03) { return new float1x4(_m00, _m01, _m02, _m03); }

	public static float1x4 float1x4(float4 r0) { return new float1x4(r0.x, r0.y, r0.z, r0.w); }

	public static float2x1 float2x1(float _m00, float _m10) { return new float2x1(_m00, _m10); }

	public static float2x1 float2x1(float1 r0, float1 r1) { return new float2x1(r0.x, r1.x); }

	public static float2x2 float2x2(float _m00, float _m01, float _m10, float _m11) { return new float2x2(_m00, _m01, _m10, _m11); }

	public static float2x2 float2x2(float2 r0, float2 r1) { return new float2x2(r0.x, r0.y, r1.x, r1.y); }

	public static float2x3 float2x3(float _m00, float _m01, float _m02, float _m10, float _m11, float _m12) { return new float2x3(_m00, _m01, _m02, _m10, _m11, _m12); }

	public static float2x3 float2x3(float3 r0, float3 r1) { return new float2x3(r0.x, r0.y, r0.z, r1.x, r1.y, r1.z); }

	public static float2x4 float2x4(float _m00, float _m01, float _m02, float _m03, float _m10, float _m11, float _m12, float _m13) { return new float2x4(_m00, _m01, _m02, _m03, _m10, _m11, _m12, _m13); }

	public static float2x4 float2x4(float4 r0, float4 r1) { return new float2x4(r0.x, r0.y, r0.z, r0.w, r1.x, r1.y, r1.z, r1.w); }

	public static float3x1 float3x1(float _m00, float _m10, float _m20) { return new float3x1(_m00, _m10, _m20); }

	public static float3x1 float3x1(float1 r0, float1 r1, float1 r2) { return new float3x1(r0.x, r1.x, r2.x); }

	public static float3x2 float3x2(float _m00, float _m01, float _m10, float _m11, float _m20, float _m21) { return new float3x2(_m00, _m01, _m10, _m11, _m20, _m21); }

	public static float3x2 float3x2(float2 r0, float2 r1, float2 r2) { return new float3x2(r0.x, r0.y, r1.x, r1.y, r2.x, r2.y); }

	public static float3x3 float3x3(float _m00, float _m01, float _m02, float _m10, float _m11, float _m12, float _m20, float _m21, float _m22) { return new float3x3(_m00, _m01, _m02, _m10, _m11, _m12, _m20, _m21, _m22); }

	public static float3x3 float3x3(float3 r0, float3 r1, float3 r2) { return new float3x3(r0.x, r0.y, r0.z, r1.x, r1.y, r1.z, r2.x, r2.y, r2.z); }

	public static float3x4 float3x4(float _m00, float _m01, float _m02, float _m03, float _m10, float _m11, float _m12, float _m13, float _m20, float _m21, float _m22, float _m23) { return new float3x4(_m00, _m01, _m02, _m03, _m10, _m11, _m12, _m13, _m20, _m21, _m22, _m23); }

	public static float3x4 float3x4(float4 r0, float4 r1, float4 r2) { return new float3x4(r0.x, r0.y, r0.z, r0.w, r1.x, r1.y, r1.z, r1.w, r2.x, r2.y, r2.z, r2.w); }

	public static float4x1 float4x1(float _m00, float _m10, float _m20, float _m30) { return new float4x1(_m00, _m10, _m20, _m30); }

	public static float4x1 float4x1(float1 r0, float1 r1, float1 r2, float1 r3) { return new float4x1(r0.x, r1.x, r2.x, r3.x); }

	public static float4x2 float4x2(float _m00, float _m01, float _m10, float _m11, float _m20, float _m21, float _m30, float _m31) { return new float4x2(_m00, _m01, _m10, _m11, _m20, _m21, _m30, _m31); }

	public static float4x2 float4x2(float2 r0, float2 r1, float2 r2, float2 r3) { return new float4x2(r0.x, r0.y, r1.x, r1.y, r2.x, r2.y, r3.x, r3.y); }

	public static float4x3 float4x3(float _m00, float _m01, float _m02, float _m10, float _m11, float _m12, float _m20, float _m21, float _m22, float _m30, float _m31, float _m32) { return new float4x3(_m00, _m01, _m02, _m10, _m11, _m12, _m20, _m21, _m22, _m30, _m31, _m32); }

	public static float4x3 float4x3(float3 r0, float3 r1, float3 r2, float3 r3) { return new float4x3(r0.x, r0.y, r0.z, r1.x, r1.y, r1.z, r2.x, r2.y, r2.z, r3.x, r3.y, r3.z); }

	public static float4x4 float4x4(float _m00, float _m01, float _m02, float _m03, float _m10, float _m11, float _m12, float _m13, float _m20, float _m21, float _m22, float _m23, float _m30, float _m31, float _m32, float _m33) { return new float4x4(_m00, _m01, _m02, _m03, _m10, _m11, _m12, _m13, _m20, _m21, _m22, _m23, _m30, _m31, _m32, _m33); }

	public static float4x4 float4x4(float4 r0, float4 r1, float4 r2, float4 r3) { return new float4x4(r0.x, r0.y, r0.z, r0.w, r1.x, r1.y, r1.z, r1.w, r2.x, r2.y, r2.z, r2.w, r3.x, r3.y, r3.z, r3.w); }

	#endregion


}
}
