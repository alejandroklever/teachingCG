namespace GMath {
public struct float4x2{
	public float _m00;
	public float _m01;
	public float _m10;
	public float _m11;
	public float _m20;
	public float _m21;
	public float _m30;
	public float _m31;
	public float2 this[int row] {
		get{
			if(row == 0) return new float2 (_m00, _m01);
			if(row == 1) return new float2 (_m10, _m11);
			if(row == 2) return new float2 (_m20, _m21);
			if(row == 3) return new float2 (_m30, _m31);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float4x2(float _m00,float _m01,float _m10,float _m11,float _m20,float _m21,float _m30,float _m31){
		this._m00=_m00;
		this._m01=_m01;
		this._m10=_m10;
		this._m11=_m11;
		this._m20=_m20;
		this._m21=_m21;
		this._m30=_m30;
		this._m31=_m31;
	}
	public float4x2(float v):this(v,v,v,v,v,v,v,v){}
	public static explicit operator float1x1(float4x2 m) { return new(m._m00); }
	public static explicit operator float1x2(float4x2 m) { return new(m._m00, m._m01); }
	public static explicit operator float2x1(float4x2 m) { return new(m._m00, m._m10); }
	public static explicit operator float2x2(float4x2 m) { return new(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator float3x1(float4x2 m) { return new(m._m00, m._m10, m._m20); }
	public static explicit operator float3x2(float4x2 m) { return new(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21); }
	public static explicit operator float4x1(float4x2 m) { return new(m._m00, m._m10, m._m20, m._m30); }
	public static implicit operator float4x2(float v) { return new(v); }
	public static explicit operator int4x2(float4x2 v) { return new((int)v._m00,(int)v._m01,(int)v._m10,(int)v._m11,(int)v._m20,(int)v._m21,(int)v._m30,(int)v._m31); }
	public static float4x2 operator -(float4x2 a) { return new(-a._m00,-a._m01,-a._m10,-a._m11,-a._m20,-a._m21,-a._m30,-a._m31); }
	public static float4x2 operator +(float4x2 a) { return new(+a._m00,+a._m01,+a._m10,+a._m11,+a._m20,+a._m21,+a._m30,+a._m31); }
	public static int4x2 operator !(float4x2 a) { return new(a._m00==0?1:0,a._m01==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m20==0?1:0,a._m21==0?1:0,a._m30==0?1:0,a._m31==0?1:0); }
	public static float4x2 operator +(float4x2 a, float4x2 b) { return new(a._m00 + b._m00,a._m01 + b._m01,a._m10 + b._m10,a._m11 + b._m11,a._m20 + b._m20,a._m21 + b._m21,a._m30 + b._m30,a._m31 + b._m31); }
	public static float4x2 operator *(float4x2 a, float4x2 b) { return new(a._m00 * b._m00,a._m01 * b._m01,a._m10 * b._m10,a._m11 * b._m11,a._m20 * b._m20,a._m21 * b._m21,a._m30 * b._m30,a._m31 * b._m31); }
	public static float4x2 operator -(float4x2 a, float4x2 b) { return new(a._m00 - b._m00,a._m01 - b._m01,a._m10 - b._m10,a._m11 - b._m11,a._m20 - b._m20,a._m21 - b._m21,a._m30 - b._m30,a._m31 - b._m31); }
	public static float4x2 operator /(float4x2 a, float4x2 b) { return new(a._m00 / b._m00,a._m01 / b._m01,a._m10 / b._m10,a._m11 / b._m11,a._m20 / b._m20,a._m21 / b._m21,a._m30 / b._m30,a._m31 / b._m31); }
	public static float4x2 operator %(float4x2 a, float4x2 b) { return new(a._m00 % b._m00,a._m01 % b._m01,a._m10 % b._m10,a._m11 % b._m11,a._m20 % b._m20,a._m21 % b._m21,a._m30 % b._m30,a._m31 % b._m31); }
	public static int4x2 operator ==(float4x2 a, float4x2 b) { return new(a._m00 == b._m00?1:0, a._m01 == b._m01?1:0, a._m10 == b._m10?1:0, a._m11 == b._m11?1:0, a._m20 == b._m20?1:0, a._m21 == b._m21?1:0, a._m30 == b._m30?1:0, a._m31 == b._m31?1:0); }
	public static int4x2 operator !=(float4x2 a, float4x2 b) { return new(a._m00 != b._m00?1:0, a._m01 != b._m01?1:0, a._m10 != b._m10?1:0, a._m11 != b._m11?1:0, a._m20 != b._m20?1:0, a._m21 != b._m21?1:0, a._m30 != b._m30?1:0, a._m31 != b._m31?1:0); }
	public static int4x2 operator <(float4x2 a, float4x2 b) { return new(a._m00 < b._m00?1:0, a._m01 < b._m01?1:0, a._m10 < b._m10?1:0, a._m11 < b._m11?1:0, a._m20 < b._m20?1:0, a._m21 < b._m21?1:0, a._m30 < b._m30?1:0, a._m31 < b._m31?1:0); }
	public static int4x2 operator <=(float4x2 a, float4x2 b) { return new(a._m00 <= b._m00?1:0, a._m01 <= b._m01?1:0, a._m10 <= b._m10?1:0, a._m11 <= b._m11?1:0, a._m20 <= b._m20?1:0, a._m21 <= b._m21?1:0, a._m30 <= b._m30?1:0, a._m31 <= b._m31?1:0); }
	public static int4x2 operator >=(float4x2 a, float4x2 b) { return new(a._m00 >= b._m00?1:0, a._m01 >= b._m01?1:0, a._m10 >= b._m10?1:0, a._m11 >= b._m11?1:0, a._m20 >= b._m20?1:0, a._m21 >= b._m21?1:0, a._m30 >= b._m30?1:0, a._m31 >= b._m31?1:0); }
	public static int4x2 operator >(float4x2 a, float4x2 b) { return new(a._m00 > b._m00?1:0, a._m01 > b._m01?1:0, a._m10 > b._m10?1:0, a._m11 > b._m11?1:0, a._m20 > b._m20?1:0, a._m21 > b._m21?1:0, a._m30 > b._m30?1:0, a._m31 > b._m31?1:0); }
	public override string ToString() { return
		$"(({_m00}, {_m01}), ({_m10}, {_m11}), ({_m20}, {_m21}), ({_m30}, {_m31}))"; }
}
}
