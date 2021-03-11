namespace GMath {
public struct float2x1{
	public float _m00;
	public float _m10;
	public float1 this[int row] {
		get{
			if(row == 0) return new float1 (_m00);
			if(row == 1) return new float1 (_m10);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float2x1(float _m00,float _m10){
		this._m00=_m00;
		this._m10=_m10;
	}
	public float2x1(float v):this(v,v){}
	public static explicit operator float1x1(float2x1 m) { return new float1x1(m._m00); }
	public static implicit operator float2x1(float v) { return new float2x1(v); }
	public static explicit operator int2x1(float2x1 v) { return new int2x1((int)v._m00,(int)v._m10); }
	public static float2x1 operator -(float2x1 a) { return new float2x1(-a._m00,-a._m10); }
	public static float2x1 operator +(float2x1 a) { return new float2x1(+a._m00,+a._m10); }
	public static int2x1 operator !(float2x1 a) { return new int2x1(a._m00==0?1:0,a._m10==0?1:0); }
	public static float2x1 operator +(float2x1 a, float2x1 b) { return new float2x1(a._m00 + b._m00,a._m10 + b._m10); }
	public static float2x1 operator *(float2x1 a, float2x1 b) { return new float2x1(a._m00 * b._m00,a._m10 * b._m10); }
	public static float2x1 operator -(float2x1 a, float2x1 b) { return new float2x1(a._m00 - b._m00,a._m10 - b._m10); }
	public static float2x1 operator /(float2x1 a, float2x1 b) { return new float2x1(a._m00 / b._m00,a._m10 / b._m10); }
	public static float2x1 operator %(float2x1 a, float2x1 b) { return new float2x1(a._m00 % b._m00,a._m10 % b._m10); }
	public static int2x1 operator ==(float2x1 a, float2x1 b) { return new int2x1((a._m00 == b._m00)?1:0, (a._m10 == b._m10)?1:0); }
	public static int2x1 operator !=(float2x1 a, float2x1 b) { return new int2x1((a._m00 != b._m00)?1:0, (a._m10 != b._m10)?1:0); }
	public static int2x1 operator <(float2x1 a, float2x1 b) { return new int2x1((a._m00 < b._m00)?1:0, (a._m10 < b._m10)?1:0); }
	public static int2x1 operator <=(float2x1 a, float2x1 b) { return new int2x1((a._m00 <= b._m00)?1:0, (a._m10 <= b._m10)?1:0); }
	public static int2x1 operator >=(float2x1 a, float2x1 b) { return new int2x1((a._m00 >= b._m00)?1:0, (a._m10 >= b._m10)?1:0); }
	public static int2x1 operator >(float2x1 a, float2x1 b) { return new int2x1((a._m00 > b._m00)?1:0, (a._m10 > b._m10)?1:0); }
	public override string ToString() { return string.Format("(({0}), ({1}))", this._m00, this._m10); }
}
}
