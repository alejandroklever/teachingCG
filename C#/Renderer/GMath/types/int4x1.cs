namespace GMath {
public struct int4x1{
	public int _m00;
	public int _m10;
	public int _m20;
	public int _m30;
	public int1 this[int row] {
		get{
			if(row == 0) return new int1 (_m00);
			if(row == 1) return new int1 (_m10);
			if(row == 2) return new int1 (_m20);
			if(row == 3) return new int1 (_m30);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int4x1(int _m00,int _m10,int _m20,int _m30){
		this._m00=_m00;
		this._m10=_m10;
		this._m20=_m20;
		this._m30=_m30;
	}
	public int4x1(int v):this(v,v,v,v){}
	public static explicit operator int1x1(int4x1 m) { return new(m._m00); }
	public static explicit operator int2x1(int4x1 m) { return new(m._m00, m._m10); }
	public static explicit operator int3x1(int4x1 m) { return new(m._m00, m._m10, m._m20); }
	public static implicit operator int4x1(int v) { return new(v); }
	public static implicit operator float4x1(int4x1 v) { return new((float)v._m00,(float)v._m10,(float)v._m20,(float)v._m30); }
	public static int4x1 operator -(int4x1 a) { return new(-a._m00,-a._m10,-a._m20,-a._m30); }
	public static int4x1 operator +(int4x1 a) { return new(+a._m00,+a._m10,+a._m20,+a._m30); }
	public static int4x1 operator ~(int4x1 a) { return new(~a._m00,~a._m10,~a._m20,~a._m30); }
	public static int4x1 operator !(int4x1 a) { return new(a._m00==0?1:0,a._m10==0?1:0,a._m20==0?1:0,a._m30==0?1:0); }
	public static int4x1 operator +(int4x1 a, int4x1 b) { return new(a._m00 + b._m00,a._m10 + b._m10,a._m20 + b._m20,a._m30 + b._m30); }
	public static int4x1 operator *(int4x1 a, int4x1 b) { return new(a._m00 * b._m00,a._m10 * b._m10,a._m20 * b._m20,a._m30 * b._m30); }
	public static int4x1 operator -(int4x1 a, int4x1 b) { return new(a._m00 - b._m00,a._m10 - b._m10,a._m20 - b._m20,a._m30 - b._m30); }
	public static int4x1 operator /(int4x1 a, int4x1 b) { return new(a._m00 / b._m00,a._m10 / b._m10,a._m20 / b._m20,a._m30 / b._m30); }
	public static int4x1 operator %(int4x1 a, int4x1 b) { return new(a._m00 % b._m00,a._m10 % b._m10,a._m20 % b._m20,a._m30 % b._m30); }
	public static int4x1 operator &(int4x1 a, int4x1 b) { return new(a._m00 & b._m00,a._m10 & b._m10,a._m20 & b._m20,a._m30 & b._m30); }
	public static int4x1 operator |(int4x1 a, int4x1 b) { return new(a._m00 | b._m00,a._m10 | b._m10,a._m20 | b._m20,a._m30 | b._m30); }
	public static int4x1 operator ^(int4x1 a, int4x1 b) { return new(a._m00 ^ b._m00,a._m10 ^ b._m10,a._m20 ^ b._m20,a._m30 ^ b._m30); }
	public static int4x1 operator ==(int4x1 a, int4x1 b) { return new(a._m00 == b._m00?1:0, a._m10 == b._m10?1:0, a._m20 == b._m20?1:0, a._m30 == b._m30?1:0); }
	public static int4x1 operator !=(int4x1 a, int4x1 b) { return new(a._m00 != b._m00?1:0, a._m10 != b._m10?1:0, a._m20 != b._m20?1:0, a._m30 != b._m30?1:0); }
	public static int4x1 operator <(int4x1 a, int4x1 b) { return new(a._m00 < b._m00?1:0, a._m10 < b._m10?1:0, a._m20 < b._m20?1:0, a._m30 < b._m30?1:0); }
	public static int4x1 operator <=(int4x1 a, int4x1 b) { return new(a._m00 <= b._m00?1:0, a._m10 <= b._m10?1:0, a._m20 <= b._m20?1:0, a._m30 <= b._m30?1:0); }
	public static int4x1 operator >=(int4x1 a, int4x1 b) { return new(a._m00 >= b._m00?1:0, a._m10 >= b._m10?1:0, a._m20 >= b._m20?1:0, a._m30 >= b._m30?1:0); }
	public static int4x1 operator >(int4x1 a, int4x1 b) { return new(a._m00 > b._m00?1:0, a._m10 > b._m10?1:0, a._m20 > b._m20?1:0, a._m30 > b._m30?1:0); }
	public override string ToString() { return $"(({_m00}), ({_m10}), ({_m20}), ({_m30}))"; }
}
}
