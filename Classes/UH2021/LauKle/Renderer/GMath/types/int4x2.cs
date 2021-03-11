namespace GMath {
public struct int4x2{
	public int _m00;
	public int _m01;
	public int _m10;
	public int _m11;
	public int _m20;
	public int _m21;
	public int _m30;
	public int _m31;
	public int2 this[int row] {
		get{
			if(row == 0) return new int2 (_m00, _m01);
			if(row == 1) return new int2 (_m10, _m11);
			if(row == 2) return new int2 (_m20, _m21);
			if(row == 3) return new int2 (_m30, _m31);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int4x2(int _m00,int _m01,int _m10,int _m11,int _m20,int _m21,int _m30,int _m31){
		this._m00=_m00;
		this._m01=_m01;
		this._m10=_m10;
		this._m11=_m11;
		this._m20=_m20;
		this._m21=_m21;
		this._m30=_m30;
		this._m31=_m31;
	}
	public int4x2(int v):this(v,v,v,v,v,v,v,v){}
	public static explicit operator int1x1(int4x2 m) { return new int1x1(m._m00); }
	public static explicit operator int1x2(int4x2 m) { return new int1x2(m._m00, m._m01); }
	public static explicit operator int2x1(int4x2 m) { return new int2x1(m._m00, m._m10); }
	public static explicit operator int2x2(int4x2 m) { return new int2x2(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator int3x1(int4x2 m) { return new int3x1(m._m00, m._m10, m._m20); }
	public static explicit operator int3x2(int4x2 m) { return new int3x2(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21); }
	public static explicit operator int4x1(int4x2 m) { return new int4x1(m._m00, m._m10, m._m20, m._m30); }
	public static implicit operator int4x2(int v) { return new int4x2(v); }
	public static implicit operator float4x2(int4x2 v) { return new float4x2((float)v._m00,(float)v._m01,(float)v._m10,(float)v._m11,(float)v._m20,(float)v._m21,(float)v._m30,(float)v._m31); }
	public static int4x2 operator -(int4x2 a) { return new int4x2(-a._m00,-a._m01,-a._m10,-a._m11,-a._m20,-a._m21,-a._m30,-a._m31); }
	public static int4x2 operator +(int4x2 a) { return new int4x2(+a._m00,+a._m01,+a._m10,+a._m11,+a._m20,+a._m21,+a._m30,+a._m31); }
	public static int4x2 operator ~(int4x2 a) { return new int4x2(~a._m00,~a._m01,~a._m10,~a._m11,~a._m20,~a._m21,~a._m30,~a._m31); }
	public static int4x2 operator !(int4x2 a) { return new int4x2(a._m00==0?1:0,a._m01==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m20==0?1:0,a._m21==0?1:0,a._m30==0?1:0,a._m31==0?1:0); }
	public static int4x2 operator +(int4x2 a, int4x2 b) { return new int4x2(a._m00 + b._m00,a._m01 + b._m01,a._m10 + b._m10,a._m11 + b._m11,a._m20 + b._m20,a._m21 + b._m21,a._m30 + b._m30,a._m31 + b._m31); }
	public static int4x2 operator *(int4x2 a, int4x2 b) { return new int4x2(a._m00 * b._m00,a._m01 * b._m01,a._m10 * b._m10,a._m11 * b._m11,a._m20 * b._m20,a._m21 * b._m21,a._m30 * b._m30,a._m31 * b._m31); }
	public static int4x2 operator -(int4x2 a, int4x2 b) { return new int4x2(a._m00 - b._m00,a._m01 - b._m01,a._m10 - b._m10,a._m11 - b._m11,a._m20 - b._m20,a._m21 - b._m21,a._m30 - b._m30,a._m31 - b._m31); }
	public static int4x2 operator /(int4x2 a, int4x2 b) { return new int4x2(a._m00 / b._m00,a._m01 / b._m01,a._m10 / b._m10,a._m11 / b._m11,a._m20 / b._m20,a._m21 / b._m21,a._m30 / b._m30,a._m31 / b._m31); }
	public static int4x2 operator %(int4x2 a, int4x2 b) { return new int4x2(a._m00 % b._m00,a._m01 % b._m01,a._m10 % b._m10,a._m11 % b._m11,a._m20 % b._m20,a._m21 % b._m21,a._m30 % b._m30,a._m31 % b._m31); }
	public static int4x2 operator &(int4x2 a, int4x2 b) { return new int4x2(a._m00 & b._m00,a._m01 & b._m01,a._m10 & b._m10,a._m11 & b._m11,a._m20 & b._m20,a._m21 & b._m21,a._m30 & b._m30,a._m31 & b._m31); }
	public static int4x2 operator |(int4x2 a, int4x2 b) { return new int4x2(a._m00 | b._m00,a._m01 | b._m01,a._m10 | b._m10,a._m11 | b._m11,a._m20 | b._m20,a._m21 | b._m21,a._m30 | b._m30,a._m31 | b._m31); }
	public static int4x2 operator ^(int4x2 a, int4x2 b) { return new int4x2(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m10 ^ b._m10,a._m11 ^ b._m11,a._m20 ^ b._m20,a._m21 ^ b._m21,a._m30 ^ b._m30,a._m31 ^ b._m31); }
	public static int4x2 operator ==(int4x2 a, int4x2 b) { return new int4x2((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m10 == b._m10)?1:0, (a._m11 == b._m11)?1:0, (a._m20 == b._m20)?1:0, (a._m21 == b._m21)?1:0, (a._m30 == b._m30)?1:0, (a._m31 == b._m31)?1:0); }
	public static int4x2 operator !=(int4x2 a, int4x2 b) { return new int4x2((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m10 != b._m10)?1:0, (a._m11 != b._m11)?1:0, (a._m20 != b._m20)?1:0, (a._m21 != b._m21)?1:0, (a._m30 != b._m30)?1:0, (a._m31 != b._m31)?1:0); }
	public static int4x2 operator <(int4x2 a, int4x2 b) { return new int4x2((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m10 < b._m10)?1:0, (a._m11 < b._m11)?1:0, (a._m20 < b._m20)?1:0, (a._m21 < b._m21)?1:0, (a._m30 < b._m30)?1:0, (a._m31 < b._m31)?1:0); }
	public static int4x2 operator <=(int4x2 a, int4x2 b) { return new int4x2((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m10 <= b._m10)?1:0, (a._m11 <= b._m11)?1:0, (a._m20 <= b._m20)?1:0, (a._m21 <= b._m21)?1:0, (a._m30 <= b._m30)?1:0, (a._m31 <= b._m31)?1:0); }
	public static int4x2 operator >=(int4x2 a, int4x2 b) { return new int4x2((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m10 >= b._m10)?1:0, (a._m11 >= b._m11)?1:0, (a._m20 >= b._m20)?1:0, (a._m21 >= b._m21)?1:0, (a._m30 >= b._m30)?1:0, (a._m31 >= b._m31)?1:0); }
	public static int4x2 operator >(int4x2 a, int4x2 b) { return new int4x2((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m10 > b._m10)?1:0, (a._m11 > b._m11)?1:0, (a._m20 > b._m20)?1:0, (a._m21 > b._m21)?1:0, (a._m30 > b._m30)?1:0, (a._m31 > b._m31)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}), ({2}, {3}), ({4}, {5}), ({6}, {7}))", this._m00, this._m01, this._m10, this._m11, this._m20, this._m21, this._m30, this._m31); }
}
}
