namespace GMath {
public struct int2x1{
	public int _m00;
	public int _m10;
	public int1 this[int row] {
		get{
			if(row == 0) return new int1 (_m00);
			if(row == 1) return new int1 (_m10);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int2x1(int _m00,int _m10){
		this._m00=_m00;
		this._m10=_m10;
	}
	public int2x1(int v):this(v,v){}
	public static explicit operator int1x1(int2x1 m) { return new int1x1(m._m00); }
	public static implicit operator int2x1(int v) { return new int2x1(v); }
	public static implicit operator float2x1(int2x1 v) { return new float2x1((float)v._m00,(float)v._m10); }
	public static int2x1 operator -(int2x1 a) { return new int2x1(-a._m00,-a._m10); }
	public static int2x1 operator +(int2x1 a) { return new int2x1(+a._m00,+a._m10); }
	public static int2x1 operator ~(int2x1 a) { return new int2x1(~a._m00,~a._m10); }
	public static int2x1 operator !(int2x1 a) { return new int2x1(a._m00==0?1:0,a._m10==0?1:0); }
	public static int2x1 operator +(int2x1 a, int2x1 b) { return new int2x1(a._m00 + b._m00,a._m10 + b._m10); }
	public static int2x1 operator *(int2x1 a, int2x1 b) { return new int2x1(a._m00 * b._m00,a._m10 * b._m10); }
	public static int2x1 operator -(int2x1 a, int2x1 b) { return new int2x1(a._m00 - b._m00,a._m10 - b._m10); }
	public static int2x1 operator /(int2x1 a, int2x1 b) { return new int2x1(a._m00 / b._m00,a._m10 / b._m10); }
	public static int2x1 operator %(int2x1 a, int2x1 b) { return new int2x1(a._m00 % b._m00,a._m10 % b._m10); }
	public static int2x1 operator &(int2x1 a, int2x1 b) { return new int2x1(a._m00 & b._m00,a._m10 & b._m10); }
	public static int2x1 operator |(int2x1 a, int2x1 b) { return new int2x1(a._m00 | b._m00,a._m10 | b._m10); }
	public static int2x1 operator ^(int2x1 a, int2x1 b) { return new int2x1(a._m00 ^ b._m00,a._m10 ^ b._m10); }
	public static int2x1 operator ==(int2x1 a, int2x1 b) { return new int2x1((a._m00 == b._m00)?1:0, (a._m10 == b._m10)?1:0); }
	public static int2x1 operator !=(int2x1 a, int2x1 b) { return new int2x1((a._m00 != b._m00)?1:0, (a._m10 != b._m10)?1:0); }
	public static int2x1 operator <(int2x1 a, int2x1 b) { return new int2x1((a._m00 < b._m00)?1:0, (a._m10 < b._m10)?1:0); }
	public static int2x1 operator <=(int2x1 a, int2x1 b) { return new int2x1((a._m00 <= b._m00)?1:0, (a._m10 <= b._m10)?1:0); }
	public static int2x1 operator >=(int2x1 a, int2x1 b) { return new int2x1((a._m00 >= b._m00)?1:0, (a._m10 >= b._m10)?1:0); }
	public static int2x1 operator >(int2x1 a, int2x1 b) { return new int2x1((a._m00 > b._m00)?1:0, (a._m10 > b._m10)?1:0); }
	public override string ToString() { return string.Format("(({0}), ({1}))", this._m00, this._m10); }
}
}
