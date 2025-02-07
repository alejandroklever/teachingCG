namespace GMath {
public struct int3x2{
	public int _m00;
	public int _m01;
	public int _m10;
	public int _m11;
	public int _m20;
	public int _m21;
	public int2 this[int row] {
		get{
			if(row == 0) return new int2 (_m00, _m01);
			if(row == 1) return new int2 (_m10, _m11);
			if(row == 2) return new int2 (_m20, _m21);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int3x2(int _m00,int _m01,int _m10,int _m11,int _m20,int _m21){
		this._m00=_m00;
		this._m01=_m01;
		this._m10=_m10;
		this._m11=_m11;
		this._m20=_m20;
		this._m21=_m21;
	}
	public int3x2(int v):this(v,v,v,v,v,v){}
	public static explicit operator int1x1(int3x2 m) { return new(m._m00); }
	public static explicit operator int1x2(int3x2 m) { return new(m._m00, m._m01); }
	public static explicit operator int2x1(int3x2 m) { return new(m._m00, m._m10); }
	public static explicit operator int2x2(int3x2 m) { return new(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator int3x1(int3x2 m) { return new(m._m00, m._m10, m._m20); }
	public static implicit operator int3x2(int v) { return new(v); }
	public static implicit operator float3x2(int3x2 v) { return new((float)v._m00,(float)v._m01,(float)v._m10,(float)v._m11,(float)v._m20,(float)v._m21); }
	public static int3x2 operator -(int3x2 a) { return new(-a._m00,-a._m01,-a._m10,-a._m11,-a._m20,-a._m21); }
	public static int3x2 operator +(int3x2 a) { return new(+a._m00,+a._m01,+a._m10,+a._m11,+a._m20,+a._m21); }
	public static int3x2 operator ~(int3x2 a) { return new(~a._m00,~a._m01,~a._m10,~a._m11,~a._m20,~a._m21); }
	public static int3x2 operator !(int3x2 a) { return new(a._m00==0?1:0,a._m01==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m20==0?1:0,a._m21==0?1:0); }
	public static int3x2 operator +(int3x2 a, int3x2 b) { return new(a._m00 + b._m00,a._m01 + b._m01,a._m10 + b._m10,a._m11 + b._m11,a._m20 + b._m20,a._m21 + b._m21); }
	public static int3x2 operator *(int3x2 a, int3x2 b) { return new(a._m00 * b._m00,a._m01 * b._m01,a._m10 * b._m10,a._m11 * b._m11,a._m20 * b._m20,a._m21 * b._m21); }
	public static int3x2 operator -(int3x2 a, int3x2 b) { return new(a._m00 - b._m00,a._m01 - b._m01,a._m10 - b._m10,a._m11 - b._m11,a._m20 - b._m20,a._m21 - b._m21); }
	public static int3x2 operator /(int3x2 a, int3x2 b) { return new(a._m00 / b._m00,a._m01 / b._m01,a._m10 / b._m10,a._m11 / b._m11,a._m20 / b._m20,a._m21 / b._m21); }
	public static int3x2 operator %(int3x2 a, int3x2 b) { return new(a._m00 % b._m00,a._m01 % b._m01,a._m10 % b._m10,a._m11 % b._m11,a._m20 % b._m20,a._m21 % b._m21); }
	public static int3x2 operator &(int3x2 a, int3x2 b) { return new(a._m00 & b._m00,a._m01 & b._m01,a._m10 & b._m10,a._m11 & b._m11,a._m20 & b._m20,a._m21 & b._m21); }
	public static int3x2 operator |(int3x2 a, int3x2 b) { return new(a._m00 | b._m00,a._m01 | b._m01,a._m10 | b._m10,a._m11 | b._m11,a._m20 | b._m20,a._m21 | b._m21); }
	public static int3x2 operator ^(int3x2 a, int3x2 b) { return new(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m10 ^ b._m10,a._m11 ^ b._m11,a._m20 ^ b._m20,a._m21 ^ b._m21); }
	public static int3x2 operator ==(int3x2 a, int3x2 b) { return new(a._m00 == b._m00?1:0, a._m01 == b._m01?1:0, a._m10 == b._m10?1:0, a._m11 == b._m11?1:0, a._m20 == b._m20?1:0, a._m21 == b._m21?1:0); }
	public static int3x2 operator !=(int3x2 a, int3x2 b) { return new(a._m00 != b._m00?1:0, a._m01 != b._m01?1:0, a._m10 != b._m10?1:0, a._m11 != b._m11?1:0, a._m20 != b._m20?1:0, a._m21 != b._m21?1:0); }
	public static int3x2 operator <(int3x2 a, int3x2 b) { return new(a._m00 < b._m00?1:0, a._m01 < b._m01?1:0, a._m10 < b._m10?1:0, a._m11 < b._m11?1:0, a._m20 < b._m20?1:0, a._m21 < b._m21?1:0); }
	public static int3x2 operator <=(int3x2 a, int3x2 b) { return new(a._m00 <= b._m00?1:0, a._m01 <= b._m01?1:0, a._m10 <= b._m10?1:0, a._m11 <= b._m11?1:0, a._m20 <= b._m20?1:0, a._m21 <= b._m21?1:0); }
	public static int3x2 operator >=(int3x2 a, int3x2 b) { return new(a._m00 >= b._m00?1:0, a._m01 >= b._m01?1:0, a._m10 >= b._m10?1:0, a._m11 >= b._m11?1:0, a._m20 >= b._m20?1:0, a._m21 >= b._m21?1:0); }
	public static int3x2 operator >(int3x2 a, int3x2 b) { return new(a._m00 > b._m00?1:0, a._m01 > b._m01?1:0, a._m10 > b._m10?1:0, a._m11 > b._m11?1:0, a._m20 > b._m20?1:0, a._m21 > b._m21?1:0); }
	public override string ToString() { return
		$"(({_m00}, {_m01}), ({_m10}, {_m11}), ({_m20}, {_m21}))"; }
}
}
