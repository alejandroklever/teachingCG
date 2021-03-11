namespace GMath {
public struct float3x4{
	public float _m00;
	public float _m01;
	public float _m02;
	public float _m03;
	public float _m10;
	public float _m11;
	public float _m12;
	public float _m13;
	public float _m20;
	public float _m21;
	public float _m22;
	public float _m23;
	public float4 this[int row] {
		get{
			if(row == 0) return new float4 (_m00, _m01, _m02, _m03);
			if(row == 1) return new float4 (_m10, _m11, _m12, _m13);
			if(row == 2) return new float4 (_m20, _m21, _m22, _m23);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float3x4(float _m00,float _m01,float _m02,float _m03,float _m10,float _m11,float _m12,float _m13,float _m20,float _m21,float _m22,float _m23){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
		this._m03=_m03;
		this._m10=_m10;
		this._m11=_m11;
		this._m12=_m12;
		this._m13=_m13;
		this._m20=_m20;
		this._m21=_m21;
		this._m22=_m22;
		this._m23=_m23;
	}
	public float3x4(float v):this(v,v,v,v,v,v,v,v,v,v,v,v){}
	public static explicit operator float1x1(float3x4 m) { return new float1x1(m._m00); }
	public static explicit operator float1x2(float3x4 m) { return new float1x2(m._m00, m._m01); }
	public static explicit operator float1x3(float3x4 m) { return new float1x3(m._m00, m._m01, m._m02); }
	public static explicit operator float1x4(float3x4 m) { return new float1x4(m._m00, m._m01, m._m02, m._m03); }
	public static explicit operator float2x1(float3x4 m) { return new float2x1(m._m00, m._m10); }
	public static explicit operator float2x2(float3x4 m) { return new float2x2(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator float2x3(float3x4 m) { return new float2x3(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12); }
	public static explicit operator float2x4(float3x4 m) { return new float2x4(m._m00, m._m01, m._m02, m._m03, m._m10, m._m11, m._m12, m._m13); }
	public static explicit operator float3x1(float3x4 m) { return new float3x1(m._m00, m._m10, m._m20); }
	public static explicit operator float3x2(float3x4 m) { return new float3x2(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21); }
	public static explicit operator float3x3(float3x4 m) { return new float3x3(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12, m._m20, m._m21, m._m22); }
	public static implicit operator float3x4(float v) { return new float3x4(v); }
	public static explicit operator int3x4(float3x4 v) { return new int3x4((int)v._m00,(int)v._m01,(int)v._m02,(int)v._m03,(int)v._m10,(int)v._m11,(int)v._m12,(int)v._m13,(int)v._m20,(int)v._m21,(int)v._m22,(int)v._m23); }
	public static float3x4 operator -(float3x4 a) { return new float3x4(-a._m00,-a._m01,-a._m02,-a._m03,-a._m10,-a._m11,-a._m12,-a._m13,-a._m20,-a._m21,-a._m22,-a._m23); }
	public static float3x4 operator +(float3x4 a) { return new float3x4(+a._m00,+a._m01,+a._m02,+a._m03,+a._m10,+a._m11,+a._m12,+a._m13,+a._m20,+a._m21,+a._m22,+a._m23); }
	public static int3x4 operator !(float3x4 a) { return new int3x4(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m03==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m12==0?1:0,a._m13==0?1:0,a._m20==0?1:0,a._m21==0?1:0,a._m22==0?1:0,a._m23==0?1:0); }
	public static float3x4 operator +(float3x4 a, float3x4 b) { return new float3x4(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m03 + b._m03,a._m10 + b._m10,a._m11 + b._m11,a._m12 + b._m12,a._m13 + b._m13,a._m20 + b._m20,a._m21 + b._m21,a._m22 + b._m22,a._m23 + b._m23); }
	public static float3x4 operator *(float3x4 a, float3x4 b) { return new float3x4(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m03 * b._m03,a._m10 * b._m10,a._m11 * b._m11,a._m12 * b._m12,a._m13 * b._m13,a._m20 * b._m20,a._m21 * b._m21,a._m22 * b._m22,a._m23 * b._m23); }
	public static float3x4 operator -(float3x4 a, float3x4 b) { return new float3x4(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m03 - b._m03,a._m10 - b._m10,a._m11 - b._m11,a._m12 - b._m12,a._m13 - b._m13,a._m20 - b._m20,a._m21 - b._m21,a._m22 - b._m22,a._m23 - b._m23); }
	public static float3x4 operator /(float3x4 a, float3x4 b) { return new float3x4(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m03 / b._m03,a._m10 / b._m10,a._m11 / b._m11,a._m12 / b._m12,a._m13 / b._m13,a._m20 / b._m20,a._m21 / b._m21,a._m22 / b._m22,a._m23 / b._m23); }
	public static float3x4 operator %(float3x4 a, float3x4 b) { return new float3x4(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m03 % b._m03,a._m10 % b._m10,a._m11 % b._m11,a._m12 % b._m12,a._m13 % b._m13,a._m20 % b._m20,a._m21 % b._m21,a._m22 % b._m22,a._m23 % b._m23); }
	public static int3x4 operator ==(float3x4 a, float3x4 b) { return new int3x4((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0, (a._m03 == b._m03)?1:0, (a._m10 == b._m10)?1:0, (a._m11 == b._m11)?1:0, (a._m12 == b._m12)?1:0, (a._m13 == b._m13)?1:0, (a._m20 == b._m20)?1:0, (a._m21 == b._m21)?1:0, (a._m22 == b._m22)?1:0, (a._m23 == b._m23)?1:0); }
	public static int3x4 operator !=(float3x4 a, float3x4 b) { return new int3x4((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0, (a._m03 != b._m03)?1:0, (a._m10 != b._m10)?1:0, (a._m11 != b._m11)?1:0, (a._m12 != b._m12)?1:0, (a._m13 != b._m13)?1:0, (a._m20 != b._m20)?1:0, (a._m21 != b._m21)?1:0, (a._m22 != b._m22)?1:0, (a._m23 != b._m23)?1:0); }
	public static int3x4 operator <(float3x4 a, float3x4 b) { return new int3x4((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0, (a._m03 < b._m03)?1:0, (a._m10 < b._m10)?1:0, (a._m11 < b._m11)?1:0, (a._m12 < b._m12)?1:0, (a._m13 < b._m13)?1:0, (a._m20 < b._m20)?1:0, (a._m21 < b._m21)?1:0, (a._m22 < b._m22)?1:0, (a._m23 < b._m23)?1:0); }
	public static int3x4 operator <=(float3x4 a, float3x4 b) { return new int3x4((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0, (a._m03 <= b._m03)?1:0, (a._m10 <= b._m10)?1:0, (a._m11 <= b._m11)?1:0, (a._m12 <= b._m12)?1:0, (a._m13 <= b._m13)?1:0, (a._m20 <= b._m20)?1:0, (a._m21 <= b._m21)?1:0, (a._m22 <= b._m22)?1:0, (a._m23 <= b._m23)?1:0); }
	public static int3x4 operator >=(float3x4 a, float3x4 b) { return new int3x4((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0, (a._m03 >= b._m03)?1:0, (a._m10 >= b._m10)?1:0, (a._m11 >= b._m11)?1:0, (a._m12 >= b._m12)?1:0, (a._m13 >= b._m13)?1:0, (a._m20 >= b._m20)?1:0, (a._m21 >= b._m21)?1:0, (a._m22 >= b._m22)?1:0, (a._m23 >= b._m23)?1:0); }
	public static int3x4 operator >(float3x4 a, float3x4 b) { return new int3x4((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0, (a._m03 > b._m03)?1:0, (a._m10 > b._m10)?1:0, (a._m11 > b._m11)?1:0, (a._m12 > b._m12)?1:0, (a._m13 > b._m13)?1:0, (a._m20 > b._m20)?1:0, (a._m21 > b._m21)?1:0, (a._m22 > b._m22)?1:0, (a._m23 > b._m23)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}, {3}), ({4}, {5}, {6}, {7}), ({8}, {9}, {10}, {11}))", this._m00, this._m01, this._m02, this._m03, this._m10, this._m11, this._m12, this._m13, this._m20, this._m21, this._m22, this._m23); }
}
}
