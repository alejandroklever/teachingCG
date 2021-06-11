namespace GMath {
public struct float1x1{
	public float _m00;
	public float1 this[int row] {
		get{
			if(row == 0) return new float1 (_m00);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float1x1(float _m00){
		this._m00=_m00;
	}
	public static implicit operator float1(float1x1 m) { return new(m._m00); }
	public static implicit operator float1x1(float1 v) { return new(v.x); }
	public static implicit operator float1x1(float v) { return new(v); }
	public static explicit operator int1x1(float1x1 v) { return new((int)v._m00); }
	public static float1x1 operator -(float1x1 a) { return new(-a._m00); }
	public static float1x1 operator +(float1x1 a) { return new(+a._m00); }
	public static int1x1 operator !(float1x1 a) { return new(a._m00==0?1:0); }
	public static float1x1 operator +(float1x1 a, float1x1 b) { return new(a._m00 + b._m00); }
	public static float1x1 operator *(float1x1 a, float1x1 b) { return new(a._m00 * b._m00); }
	public static float1x1 operator -(float1x1 a, float1x1 b) { return new(a._m00 - b._m00); }
	public static float1x1 operator /(float1x1 a, float1x1 b) { return new(a._m00 / b._m00); }
	public static float1x1 operator %(float1x1 a, float1x1 b) { return new(a._m00 % b._m00); }
	public static int1x1 operator ==(float1x1 a, float1x1 b) { return new((a._m00 == b._m00)?1:0); }
	public static int1x1 operator !=(float1x1 a, float1x1 b) { return new((a._m00 != b._m00)?1:0); }
	public static int1x1 operator <(float1x1 a, float1x1 b) { return new((a._m00 < b._m00)?1:0); }
	public static int1x1 operator <=(float1x1 a, float1x1 b) { return new((a._m00 <= b._m00)?1:0); }
	public static int1x1 operator >=(float1x1 a, float1x1 b) { return new((a._m00 >= b._m00)?1:0); }
	public static int1x1 operator >(float1x1 a, float1x1 b) { return new((a._m00 > b._m00)?1:0); }
	public override string ToString() { return $"(({this._m00}))"; }
}
}
