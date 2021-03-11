namespace GMath {
public struct float1x2{
	public float _m00;
	public float _m01;
	public float2 this[int row] {
		get{
			if(row == 0) return new float2 (_m00, _m01);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float1x2(float _m00,float _m01){
		this._m00=_m00;
		this._m01=_m01;
	}
	public float1x2(float v):this(v,v){}
	public static implicit operator float2(float1x2 m) { return new float2(m._m00, m._m01); }
	public static implicit operator float1x2(float2 v) { return new float1x2(v.x, v.y); }
	public static explicit operator float1x1(float1x2 m) { return new float1x1(m._m00); }
	public static implicit operator float1x2(float v) { return new float1x2(v); }
	public static explicit operator int1x2(float1x2 v) { return new int1x2((int)v._m00,(int)v._m01); }
	public static float1x2 operator -(float1x2 a) { return new float1x2(-a._m00,-a._m01); }
	public static float1x2 operator +(float1x2 a) { return new float1x2(+a._m00,+a._m01); }
	public static int1x2 operator !(float1x2 a) { return new int1x2(a._m00==0?1:0,a._m01==0?1:0); }
	public static float1x2 operator +(float1x2 a, float1x2 b) { return new float1x2(a._m00 + b._m00,a._m01 + b._m01); }
	public static float1x2 operator *(float1x2 a, float1x2 b) { return new float1x2(a._m00 * b._m00,a._m01 * b._m01); }
	public static float1x2 operator -(float1x2 a, float1x2 b) { return new float1x2(a._m00 - b._m00,a._m01 - b._m01); }
	public static float1x2 operator /(float1x2 a, float1x2 b) { return new float1x2(a._m00 / b._m00,a._m01 / b._m01); }
	public static float1x2 operator %(float1x2 a, float1x2 b) { return new float1x2(a._m00 % b._m00,a._m01 % b._m01); }
	public static int1x2 operator ==(float1x2 a, float1x2 b) { return new int1x2((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0); }
	public static int1x2 operator !=(float1x2 a, float1x2 b) { return new int1x2((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0); }
	public static int1x2 operator <(float1x2 a, float1x2 b) { return new int1x2((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0); }
	public static int1x2 operator <=(float1x2 a, float1x2 b) { return new int1x2((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0); }
	public static int1x2 operator >=(float1x2 a, float1x2 b) { return new int1x2((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0); }
	public static int1x2 operator >(float1x2 a, float1x2 b) { return new int1x2((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}))", this._m00, this._m01); }
}
}
