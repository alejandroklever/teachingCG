namespace GMath {
public struct int1x1{
	public int _m00;
	public int1 this[int row] {
		get{
			if(row == 0) return new int1 (_m00);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int1x1(int _m00){
		this._m00=_m00;
	}
	public static implicit operator int1(int1x1 m) { return new int1(m._m00); }
	public static implicit operator int1x1(int1 v) { return new int1x1(v.x); }
	public static implicit operator int1x1(int v) { return new int1x1(v); }
	public static implicit operator float1x1(int1x1 v) { return new float1x1((float)v._m00); }
	public static int1x1 operator -(int1x1 a) { return new int1x1(-a._m00); }
	public static int1x1 operator +(int1x1 a) { return new int1x1(+a._m00); }
	public static int1x1 operator ~(int1x1 a) { return new int1x1(~a._m00); }
	public static int1x1 operator !(int1x1 a) { return new int1x1(a._m00==0?1:0); }
	public static int1x1 operator +(int1x1 a, int1x1 b) { return new int1x1(a._m00 + b._m00); }
	public static int1x1 operator *(int1x1 a, int1x1 b) { return new int1x1(a._m00 * b._m00); }
	public static int1x1 operator -(int1x1 a, int1x1 b) { return new int1x1(a._m00 - b._m00); }
	public static int1x1 operator /(int1x1 a, int1x1 b) { return new int1x1(a._m00 / b._m00); }
	public static int1x1 operator %(int1x1 a, int1x1 b) { return new int1x1(a._m00 % b._m00); }
	public static int1x1 operator &(int1x1 a, int1x1 b) { return new int1x1(a._m00 & b._m00); }
	public static int1x1 operator |(int1x1 a, int1x1 b) { return new int1x1(a._m00 | b._m00); }
	public static int1x1 operator ^(int1x1 a, int1x1 b) { return new int1x1(a._m00 ^ b._m00); }
	public static int1x1 operator ==(int1x1 a, int1x1 b) { return new int1x1((a._m00 == b._m00)?1:0); }
	public static int1x1 operator !=(int1x1 a, int1x1 b) { return new int1x1((a._m00 != b._m00)?1:0); }
	public static int1x1 operator <(int1x1 a, int1x1 b) { return new int1x1((a._m00 < b._m00)?1:0); }
	public static int1x1 operator <=(int1x1 a, int1x1 b) { return new int1x1((a._m00 <= b._m00)?1:0); }
	public static int1x1 operator >=(int1x1 a, int1x1 b) { return new int1x1((a._m00 >= b._m00)?1:0); }
	public static int1x1 operator >(int1x1 a, int1x1 b) { return new int1x1((a._m00 > b._m00)?1:0); }
	public override string ToString() { return string.Format("(({0}))", this._m00); }
}
}
