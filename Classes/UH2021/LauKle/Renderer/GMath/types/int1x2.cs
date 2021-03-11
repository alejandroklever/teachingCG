namespace GMath {
public struct int1x2{
	public int _m00;
	public int _m01;
	public int2 this[int row] {
		get{
			if(row == 0) return new int2 (_m00, _m01);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int1x2(int _m00,int _m01){
		this._m00=_m00;
		this._m01=_m01;
	}
	public int1x2(int v):this(v,v){}
	public static implicit operator int2(int1x2 m) { return new int2(m._m00, m._m01); }
	public static implicit operator int1x2(int2 v) { return new int1x2(v.x, v.y); }
	public static explicit operator int1x1(int1x2 m) { return new int1x1(m._m00); }
	public static implicit operator int1x2(int v) { return new int1x2(v); }
	public static implicit operator float1x2(int1x2 v) { return new float1x2((float)v._m00,(float)v._m01); }
	public static int1x2 operator -(int1x2 a) { return new int1x2(-a._m00,-a._m01); }
	public static int1x2 operator +(int1x2 a) { return new int1x2(+a._m00,+a._m01); }
	public static int1x2 operator ~(int1x2 a) { return new int1x2(~a._m00,~a._m01); }
	public static int1x2 operator !(int1x2 a) { return new int1x2(a._m00==0?1:0,a._m01==0?1:0); }
	public static int1x2 operator +(int1x2 a, int1x2 b) { return new int1x2(a._m00 + b._m00,a._m01 + b._m01); }
	public static int1x2 operator *(int1x2 a, int1x2 b) { return new int1x2(a._m00 * b._m00,a._m01 * b._m01); }
	public static int1x2 operator -(int1x2 a, int1x2 b) { return new int1x2(a._m00 - b._m00,a._m01 - b._m01); }
	public static int1x2 operator /(int1x2 a, int1x2 b) { return new int1x2(a._m00 / b._m00,a._m01 / b._m01); }
	public static int1x2 operator %(int1x2 a, int1x2 b) { return new int1x2(a._m00 % b._m00,a._m01 % b._m01); }
	public static int1x2 operator &(int1x2 a, int1x2 b) { return new int1x2(a._m00 & b._m00,a._m01 & b._m01); }
	public static int1x2 operator |(int1x2 a, int1x2 b) { return new int1x2(a._m00 | b._m00,a._m01 | b._m01); }
	public static int1x2 operator ^(int1x2 a, int1x2 b) { return new int1x2(a._m00 ^ b._m00,a._m01 ^ b._m01); }
	public static int1x2 operator ==(int1x2 a, int1x2 b) { return new int1x2((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0); }
	public static int1x2 operator !=(int1x2 a, int1x2 b) { return new int1x2((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0); }
	public static int1x2 operator <(int1x2 a, int1x2 b) { return new int1x2((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0); }
	public static int1x2 operator <=(int1x2 a, int1x2 b) { return new int1x2((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0); }
	public static int1x2 operator >=(int1x2 a, int1x2 b) { return new int1x2((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0); }
	public static int1x2 operator >(int1x2 a, int1x2 b) { return new int1x2((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}))", this._m00, this._m01); }
}
}
