namespace GMath {
public struct float4x4{
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
	public float _m30;
	public float _m31;
	public float _m32;
	public float _m33;
	public float4 this[int row] {
		get{
			if(row == 0) return new float4 (_m00, _m01, _m02, _m03);
			if(row == 1) return new float4 (_m10, _m11, _m12, _m13);
			if(row == 2) return new float4 (_m20, _m21, _m22, _m23);
			if(row == 3) return new float4 (_m30, _m31, _m32, _m33);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float4x4(float _m00,float _m01,float _m02,float _m03,float _m10,float _m11,float _m12,float _m13,float _m20,float _m21,float _m22,float _m23,float _m30,float _m31,float _m32,float _m33){
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
		this._m30=_m30;
		this._m31=_m31;
		this._m32=_m32;
		this._m33=_m33;
	}
	public float4x4(float v):this(v,v,v,v,v,v,v,v,v,v,v,v,v,v,v,v){}
	public static explicit operator float1x1(float4x4 m) { return new(m._m00); }
	public static explicit operator float1x2(float4x4 m) { return new(m._m00, m._m01); }
	public static explicit operator float1x3(float4x4 m) { return new(m._m00, m._m01, m._m02); }
	public static explicit operator float1x4(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m03); }
	public static explicit operator float2x1(float4x4 m) { return new(m._m00, m._m10); }
	public static explicit operator float2x2(float4x4 m) { return new(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator float2x3(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12); }
	public static explicit operator float2x4(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m03, m._m10, m._m11, m._m12, m._m13); }
	public static explicit operator float3x1(float4x4 m) { return new(m._m00, m._m10, m._m20); }
	public static explicit operator float3x2(float4x4 m) { return new(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21); }
	public static explicit operator float3x3(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12, m._m20, m._m21, m._m22); }
	public static explicit operator float3x4(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m03, m._m10, m._m11, m._m12, m._m13, m._m20, m._m21, m._m22, m._m23); }
	public static explicit operator float4x1(float4x4 m) { return new(m._m00, m._m10, m._m20, m._m30); }
	public static explicit operator float4x2(float4x4 m) { return new(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21, m._m30, m._m31); }
	public static explicit operator float4x3(float4x4 m) { return new(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12, m._m20, m._m21, m._m22, m._m30, m._m31, m._m32); }
	public static implicit operator float4x4(float v) { return new(v); }
	public static explicit operator int4x4(float4x4 v) { return new((int)v._m00,(int)v._m01,(int)v._m02,(int)v._m03,(int)v._m10,(int)v._m11,(int)v._m12,(int)v._m13,(int)v._m20,(int)v._m21,(int)v._m22,(int)v._m23,(int)v._m30,(int)v._m31,(int)v._m32,(int)v._m33); }
	public static float4x4 operator -(float4x4 a) { return new(-a._m00,-a._m01,-a._m02,-a._m03,-a._m10,-a._m11,-a._m12,-a._m13,-a._m20,-a._m21,-a._m22,-a._m23,-a._m30,-a._m31,-a._m32,-a._m33); }
	public static float4x4 operator +(float4x4 a) { return new(+a._m00,+a._m01,+a._m02,+a._m03,+a._m10,+a._m11,+a._m12,+a._m13,+a._m20,+a._m21,+a._m22,+a._m23,+a._m30,+a._m31,+a._m32,+a._m33); }
	public static int4x4 operator !(float4x4 a) { return new(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m03==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m12==0?1:0,a._m13==0?1:0,a._m20==0?1:0,a._m21==0?1:0,a._m22==0?1:0,a._m23==0?1:0,a._m30==0?1:0,a._m31==0?1:0,a._m32==0?1:0,a._m33==0?1:0); }
	public static float4x4 operator +(float4x4 a, float4x4 b) { return new(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m03 + b._m03,a._m10 + b._m10,a._m11 + b._m11,a._m12 + b._m12,a._m13 + b._m13,a._m20 + b._m20,a._m21 + b._m21,a._m22 + b._m22,a._m23 + b._m23,a._m30 + b._m30,a._m31 + b._m31,a._m32 + b._m32,a._m33 + b._m33); }
	public static float4x4 operator *(float4x4 a, float4x4 b) { return new(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m03 * b._m03,a._m10 * b._m10,a._m11 * b._m11,a._m12 * b._m12,a._m13 * b._m13,a._m20 * b._m20,a._m21 * b._m21,a._m22 * b._m22,a._m23 * b._m23,a._m30 * b._m30,a._m31 * b._m31,a._m32 * b._m32,a._m33 * b._m33); }
	public static float4x4 operator -(float4x4 a, float4x4 b) { return new(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m03 - b._m03,a._m10 - b._m10,a._m11 - b._m11,a._m12 - b._m12,a._m13 - b._m13,a._m20 - b._m20,a._m21 - b._m21,a._m22 - b._m22,a._m23 - b._m23,a._m30 - b._m30,a._m31 - b._m31,a._m32 - b._m32,a._m33 - b._m33); }
	public static float4x4 operator /(float4x4 a, float4x4 b) { return new(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m03 / b._m03,a._m10 / b._m10,a._m11 / b._m11,a._m12 / b._m12,a._m13 / b._m13,a._m20 / b._m20,a._m21 / b._m21,a._m22 / b._m22,a._m23 / b._m23,a._m30 / b._m30,a._m31 / b._m31,a._m32 / b._m32,a._m33 / b._m33); }
	public static float4x4 operator %(float4x4 a, float4x4 b) { return new(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m03 % b._m03,a._m10 % b._m10,a._m11 % b._m11,a._m12 % b._m12,a._m13 % b._m13,a._m20 % b._m20,a._m21 % b._m21,a._m22 % b._m22,a._m23 % b._m23,a._m30 % b._m30,a._m31 % b._m31,a._m32 % b._m32,a._m33 % b._m33); }
	public static int4x4 operator ==(float4x4 a, float4x4 b) { return new(a._m00 == b._m00?1:0, a._m01 == b._m01?1:0, a._m02 == b._m02?1:0, a._m03 == b._m03?1:0, a._m10 == b._m10?1:0, a._m11 == b._m11?1:0, a._m12 == b._m12?1:0, a._m13 == b._m13?1:0, a._m20 == b._m20?1:0, a._m21 == b._m21?1:0, a._m22 == b._m22?1:0, a._m23 == b._m23?1:0, a._m30 == b._m30?1:0, a._m31 == b._m31?1:0, a._m32 == b._m32?1:0, a._m33 == b._m33?1:0); }
	public static int4x4 operator !=(float4x4 a, float4x4 b) { return new(a._m00 != b._m00?1:0, a._m01 != b._m01?1:0, a._m02 != b._m02?1:0, a._m03 != b._m03?1:0, a._m10 != b._m10?1:0, a._m11 != b._m11?1:0, a._m12 != b._m12?1:0, a._m13 != b._m13?1:0, a._m20 != b._m20?1:0, a._m21 != b._m21?1:0, a._m22 != b._m22?1:0, a._m23 != b._m23?1:0, a._m30 != b._m30?1:0, a._m31 != b._m31?1:0, a._m32 != b._m32?1:0, a._m33 != b._m33?1:0); }
	public static int4x4 operator <(float4x4 a, float4x4 b) { return new(a._m00 < b._m00?1:0, a._m01 < b._m01?1:0, a._m02 < b._m02?1:0, a._m03 < b._m03?1:0, a._m10 < b._m10?1:0, a._m11 < b._m11?1:0, a._m12 < b._m12?1:0, a._m13 < b._m13?1:0, a._m20 < b._m20?1:0, a._m21 < b._m21?1:0, a._m22 < b._m22?1:0, a._m23 < b._m23?1:0, a._m30 < b._m30?1:0, a._m31 < b._m31?1:0, a._m32 < b._m32?1:0, a._m33 < b._m33?1:0); }
	public static int4x4 operator <=(float4x4 a, float4x4 b) { return new(a._m00 <= b._m00?1:0, a._m01 <= b._m01?1:0, a._m02 <= b._m02?1:0, a._m03 <= b._m03?1:0, a._m10 <= b._m10?1:0, a._m11 <= b._m11?1:0, a._m12 <= b._m12?1:0, a._m13 <= b._m13?1:0, a._m20 <= b._m20?1:0, a._m21 <= b._m21?1:0, a._m22 <= b._m22?1:0, a._m23 <= b._m23?1:0, a._m30 <= b._m30?1:0, a._m31 <= b._m31?1:0, a._m32 <= b._m32?1:0, a._m33 <= b._m33?1:0); }
	public static int4x4 operator >=(float4x4 a, float4x4 b) { return new(a._m00 >= b._m00?1:0, a._m01 >= b._m01?1:0, a._m02 >= b._m02?1:0, a._m03 >= b._m03?1:0, a._m10 >= b._m10?1:0, a._m11 >= b._m11?1:0, a._m12 >= b._m12?1:0, a._m13 >= b._m13?1:0, a._m20 >= b._m20?1:0, a._m21 >= b._m21?1:0, a._m22 >= b._m22?1:0, a._m23 >= b._m23?1:0, a._m30 >= b._m30?1:0, a._m31 >= b._m31?1:0, a._m32 >= b._m32?1:0, a._m33 >= b._m33?1:0); }
	public static int4x4 operator >(float4x4 a, float4x4 b) { return new(a._m00 > b._m00?1:0, a._m01 > b._m01?1:0, a._m02 > b._m02?1:0, a._m03 > b._m03?1:0, a._m10 > b._m10?1:0, a._m11 > b._m11?1:0, a._m12 > b._m12?1:0, a._m13 > b._m13?1:0, a._m20 > b._m20?1:0, a._m21 > b._m21?1:0, a._m22 > b._m22?1:0, a._m23 > b._m23?1:0, a._m30 > b._m30?1:0, a._m31 > b._m31?1:0, a._m32 > b._m32?1:0, a._m33 > b._m33?1:0); }
	public override string ToString() { return
		$"(({_m00}, {_m01}, {_m02}, {_m03}), ({_m10}, {_m11}, {_m12}, {_m13}), ({_m20}, {_m21}, {_m22}, {_m23}), ({_m30}, {_m31}, {_m32}, {_m33}))"; }
}
}
