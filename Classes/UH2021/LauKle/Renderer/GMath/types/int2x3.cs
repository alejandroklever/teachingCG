namespace GMath {
public struct int2x3{
	public int _m00;
	public int _m01;
	public int _m02;
	public int _m10;
	public int _m11;
	public int _m12;
	public int3 this[int row] {
		get{
			if(row == 0) return new int3 (_m00, _m01, _m02);
			if(row == 1) return new int3 (_m10, _m11, _m12);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int2x3(int _m00,int _m01,int _m02,int _m10,int _m11,int _m12){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
		this._m10=_m10;
		this._m11=_m11;
		this._m12=_m12;
	}
	public int2x3(int v):this(v,v,v,v,v,v){}
	public static explicit operator int1x1(int2x3 m) { return new int1x1(m._m00); }
	public static explicit operator int1x2(int2x3 m) { return new int1x2(m._m00, m._m01); }
	public static explicit operator int1x3(int2x3 m) { return new int1x3(m._m00, m._m01, m._m02); }
	public static explicit operator int2x1(int2x3 m) { return new int2x1(m._m00, m._m10); }
	public static explicit operator int2x2(int2x3 m) { return new int2x2(m._m00, m._m01, m._m10, m._m11); }
	public static implicit operator int2x3(int v) { return new int2x3(v); }
	public static implicit operator float2x3(int2x3 v) { return new float2x3((float)v._m00,(float)v._m01,(float)v._m02,(float)v._m10,(float)v._m11,(float)v._m12); }
	public static int2x3 operator -(int2x3 a) { return new int2x3(-a._m00,-a._m01,-a._m02,-a._m10,-a._m11,-a._m12); }
	public static int2x3 operator +(int2x3 a) { return new int2x3(+a._m00,+a._m01,+a._m02,+a._m10,+a._m11,+a._m12); }
	public static int2x3 operator ~(int2x3 a) { return new int2x3(~a._m00,~a._m01,~a._m02,~a._m10,~a._m11,~a._m12); }
	public static int2x3 operator !(int2x3 a) { return new int2x3(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m12==0?1:0); }
	public static int2x3 operator +(int2x3 a, int2x3 b) { return new int2x3(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m10 + b._m10,a._m11 + b._m11,a._m12 + b._m12); }
	public static int2x3 operator *(int2x3 a, int2x3 b) { return new int2x3(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m10 * b._m10,a._m11 * b._m11,a._m12 * b._m12); }
	public static int2x3 operator -(int2x3 a, int2x3 b) { return new int2x3(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m10 - b._m10,a._m11 - b._m11,a._m12 - b._m12); }
	public static int2x3 operator /(int2x3 a, int2x3 b) { return new int2x3(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m10 / b._m10,a._m11 / b._m11,a._m12 / b._m12); }
	public static int2x3 operator %(int2x3 a, int2x3 b) { return new int2x3(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m10 % b._m10,a._m11 % b._m11,a._m12 % b._m12); }
	public static int2x3 operator &(int2x3 a, int2x3 b) { return new int2x3(a._m00 & b._m00,a._m01 & b._m01,a._m02 & b._m02,a._m10 & b._m10,a._m11 & b._m11,a._m12 & b._m12); }
	public static int2x3 operator |(int2x3 a, int2x3 b) { return new int2x3(a._m00 | b._m00,a._m01 | b._m01,a._m02 | b._m02,a._m10 | b._m10,a._m11 | b._m11,a._m12 | b._m12); }
	public static int2x3 operator ^(int2x3 a, int2x3 b) { return new int2x3(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m02 ^ b._m02,a._m10 ^ b._m10,a._m11 ^ b._m11,a._m12 ^ b._m12); }
	public static int2x3 operator ==(int2x3 a, int2x3 b) { return new int2x3((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0, (a._m10 == b._m10)?1:0, (a._m11 == b._m11)?1:0, (a._m12 == b._m12)?1:0); }
	public static int2x3 operator !=(int2x3 a, int2x3 b) { return new int2x3((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0, (a._m10 != b._m10)?1:0, (a._m11 != b._m11)?1:0, (a._m12 != b._m12)?1:0); }
	public static int2x3 operator <(int2x3 a, int2x3 b) { return new int2x3((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0, (a._m10 < b._m10)?1:0, (a._m11 < b._m11)?1:0, (a._m12 < b._m12)?1:0); }
	public static int2x3 operator <=(int2x3 a, int2x3 b) { return new int2x3((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0, (a._m10 <= b._m10)?1:0, (a._m11 <= b._m11)?1:0, (a._m12 <= b._m12)?1:0); }
	public static int2x3 operator >=(int2x3 a, int2x3 b) { return new int2x3((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0, (a._m10 >= b._m10)?1:0, (a._m11 >= b._m11)?1:0, (a._m12 >= b._m12)?1:0); }
	public static int2x3 operator >(int2x3 a, int2x3 b) { return new int2x3((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0, (a._m10 > b._m10)?1:0, (a._m11 > b._m11)?1:0, (a._m12 > b._m12)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}), ({3}, {4}, {5}))", this._m00, this._m01, this._m02, this._m10, this._m11, this._m12); }
}
}
