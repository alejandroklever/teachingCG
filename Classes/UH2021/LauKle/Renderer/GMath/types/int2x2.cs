namespace GMath {
public struct int2x2{
	public int _m00;
	public int _m01;
	public int _m10;
	public int _m11;
	public int2 this[int row] {
		get{
			if(row == 0) return new int2 (_m00, _m01);
			if(row == 1) return new int2 (_m10, _m11);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int2x2(int _m00,int _m01,int _m10,int _m11){
		this._m00=_m00;
		this._m01=_m01;
		this._m10=_m10;
		this._m11=_m11;
	}
	public int2x2(int v):this(v,v,v,v){}
	public static explicit operator int1x1(int2x2 m) { return new int1x1(m._m00); }
	public static explicit operator int1x2(int2x2 m) { return new int1x2(m._m00, m._m01); }
	public static explicit operator int2x1(int2x2 m) { return new int2x1(m._m00, m._m10); }
	public static implicit operator int2x2(int v) { return new int2x2(v); }
	public static implicit operator float2x2(int2x2 v) { return new float2x2((float)v._m00,(float)v._m01,(float)v._m10,(float)v._m11); }
	public static int2x2 operator -(int2x2 a) { return new int2x2(-a._m00,-a._m01,-a._m10,-a._m11); }
	public static int2x2 operator +(int2x2 a) { return new int2x2(+a._m00,+a._m01,+a._m10,+a._m11); }
	public static int2x2 operator ~(int2x2 a) { return new int2x2(~a._m00,~a._m01,~a._m10,~a._m11); }
	public static int2x2 operator !(int2x2 a) { return new int2x2(a._m00==0?1:0,a._m01==0?1:0,a._m10==0?1:0,a._m11==0?1:0); }
	public static int2x2 operator +(int2x2 a, int2x2 b) { return new int2x2(a._m00 + b._m00,a._m01 + b._m01,a._m10 + b._m10,a._m11 + b._m11); }
	public static int2x2 operator *(int2x2 a, int2x2 b) { return new int2x2(a._m00 * b._m00,a._m01 * b._m01,a._m10 * b._m10,a._m11 * b._m11); }
	public static int2x2 operator -(int2x2 a, int2x2 b) { return new int2x2(a._m00 - b._m00,a._m01 - b._m01,a._m10 - b._m10,a._m11 - b._m11); }
	public static int2x2 operator /(int2x2 a, int2x2 b) { return new int2x2(a._m00 / b._m00,a._m01 / b._m01,a._m10 / b._m10,a._m11 / b._m11); }
	public static int2x2 operator %(int2x2 a, int2x2 b) { return new int2x2(a._m00 % b._m00,a._m01 % b._m01,a._m10 % b._m10,a._m11 % b._m11); }
	public static int2x2 operator &(int2x2 a, int2x2 b) { return new int2x2(a._m00 & b._m00,a._m01 & b._m01,a._m10 & b._m10,a._m11 & b._m11); }
	public static int2x2 operator |(int2x2 a, int2x2 b) { return new int2x2(a._m00 | b._m00,a._m01 | b._m01,a._m10 | b._m10,a._m11 | b._m11); }
	public static int2x2 operator ^(int2x2 a, int2x2 b) { return new int2x2(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m10 ^ b._m10,a._m11 ^ b._m11); }
	public static int2x2 operator ==(int2x2 a, int2x2 b) { return new int2x2((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m10 == b._m10)?1:0, (a._m11 == b._m11)?1:0); }
	public static int2x2 operator !=(int2x2 a, int2x2 b) { return new int2x2((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m10 != b._m10)?1:0, (a._m11 != b._m11)?1:0); }
	public static int2x2 operator <(int2x2 a, int2x2 b) { return new int2x2((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m10 < b._m10)?1:0, (a._m11 < b._m11)?1:0); }
	public static int2x2 operator <=(int2x2 a, int2x2 b) { return new int2x2((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m10 <= b._m10)?1:0, (a._m11 <= b._m11)?1:0); }
	public static int2x2 operator >=(int2x2 a, int2x2 b) { return new int2x2((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m10 >= b._m10)?1:0, (a._m11 >= b._m11)?1:0); }
	public static int2x2 operator >(int2x2 a, int2x2 b) { return new int2x2((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m10 > b._m10)?1:0, (a._m11 > b._m11)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}), ({2}, {3}))", this._m00, this._m01, this._m10, this._m11); }
}
}
