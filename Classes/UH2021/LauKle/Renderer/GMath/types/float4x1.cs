namespace GMath {
public struct float4x1{
	public float _m00;
	public float _m10;
	public float _m20;
	public float _m30;
	public float1 this[int row] {
		get{
			if(row == 0) return new float1 (_m00);
			if(row == 1) return new float1 (_m10);
			if(row == 2) return new float1 (_m20);
			if(row == 3) return new float1 (_m30);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float4x1(float _m00,float _m10,float _m20,float _m30){
		this._m00=_m00;
		this._m10=_m10;
		this._m20=_m20;
		this._m30=_m30;
	}
	public float4x1(float v):this(v,v,v,v){}
	public static explicit operator float1x1(float4x1 m) { return new float1x1(m._m00); }
	public static explicit operator float2x1(float4x1 m) { return new float2x1(m._m00, m._m10); }
	public static explicit operator float3x1(float4x1 m) { return new float3x1(m._m00, m._m10, m._m20); }
	public static implicit operator float4x1(float v) { return new float4x1(v); }
	public static explicit operator int4x1(float4x1 v) { return new int4x1((int)v._m00,(int)v._m10,(int)v._m20,(int)v._m30); }
	public static float4x1 operator -(float4x1 a) { return new float4x1(-a._m00,-a._m10,-a._m20,-a._m30); }
	public static float4x1 operator +(float4x1 a) { return new float4x1(+a._m00,+a._m10,+a._m20,+a._m30); }
	public static int4x1 operator !(float4x1 a) { return new int4x1(a._m00==0?1:0,a._m10==0?1:0,a._m20==0?1:0,a._m30==0?1:0); }
	public static float4x1 operator +(float4x1 a, float4x1 b) { return new float4x1(a._m00 + b._m00,a._m10 + b._m10,a._m20 + b._m20,a._m30 + b._m30); }
	public static float4x1 operator *(float4x1 a, float4x1 b) { return new float4x1(a._m00 * b._m00,a._m10 * b._m10,a._m20 * b._m20,a._m30 * b._m30); }
	public static float4x1 operator -(float4x1 a, float4x1 b) { return new float4x1(a._m00 - b._m00,a._m10 - b._m10,a._m20 - b._m20,a._m30 - b._m30); }
	public static float4x1 operator /(float4x1 a, float4x1 b) { return new float4x1(a._m00 / b._m00,a._m10 / b._m10,a._m20 / b._m20,a._m30 / b._m30); }
	public static float4x1 operator %(float4x1 a, float4x1 b) { return new float4x1(a._m00 % b._m00,a._m10 % b._m10,a._m20 % b._m20,a._m30 % b._m30); }
	public static int4x1 operator ==(float4x1 a, float4x1 b) { return new int4x1((a._m00 == b._m00)?1:0, (a._m10 == b._m10)?1:0, (a._m20 == b._m20)?1:0, (a._m30 == b._m30)?1:0); }
	public static int4x1 operator !=(float4x1 a, float4x1 b) { return new int4x1((a._m00 != b._m00)?1:0, (a._m10 != b._m10)?1:0, (a._m20 != b._m20)?1:0, (a._m30 != b._m30)?1:0); }
	public static int4x1 operator <(float4x1 a, float4x1 b) { return new int4x1((a._m00 < b._m00)?1:0, (a._m10 < b._m10)?1:0, (a._m20 < b._m20)?1:0, (a._m30 < b._m30)?1:0); }
	public static int4x1 operator <=(float4x1 a, float4x1 b) { return new int4x1((a._m00 <= b._m00)?1:0, (a._m10 <= b._m10)?1:0, (a._m20 <= b._m20)?1:0, (a._m30 <= b._m30)?1:0); }
	public static int4x1 operator >=(float4x1 a, float4x1 b) { return new int4x1((a._m00 >= b._m00)?1:0, (a._m10 >= b._m10)?1:0, (a._m20 >= b._m20)?1:0, (a._m30 >= b._m30)?1:0); }
	public static int4x1 operator >(float4x1 a, float4x1 b) { return new int4x1((a._m00 > b._m00)?1:0, (a._m10 > b._m10)?1:0, (a._m20 > b._m20)?1:0, (a._m30 > b._m30)?1:0); }
	public override string ToString() { return string.Format("(({0}), ({1}), ({2}), ({3}))", this._m00, this._m10, this._m20, this._m30); }
}
}
