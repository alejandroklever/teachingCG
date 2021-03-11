namespace GMath {
public struct int1x4{
	public int _m00;
	public int _m01;
	public int _m02;
	public int _m03;
	public int4 this[int row] {
		get{
			if(row == 0) return new int4 (_m00, _m01, _m02, _m03);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int1x4(int _m00,int _m01,int _m02,int _m03){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
		this._m03=_m03;
	}
	public int1x4(int v):this(v,v,v,v){}
	public static implicit operator int4(int1x4 m) { return new int4(m._m00, m._m01, m._m02, m._m03); }
	public static implicit operator int1x4(int4 v) { return new int1x4(v.x, v.y, v.z, v.w); }
	public static explicit operator int1x1(int1x4 m) { return new int1x1(m._m00); }
	public static explicit operator int1x2(int1x4 m) { return new int1x2(m._m00, m._m01); }
	public static explicit operator int1x3(int1x4 m) { return new int1x3(m._m00, m._m01, m._m02); }
	public static implicit operator int1x4(int v) { return new int1x4(v); }
	public static implicit operator float1x4(int1x4 v) { return new float1x4((float)v._m00,(float)v._m01,(float)v._m02,(float)v._m03); }
	public static int1x4 operator -(int1x4 a) { return new int1x4(-a._m00,-a._m01,-a._m02,-a._m03); }
	public static int1x4 operator +(int1x4 a) { return new int1x4(+a._m00,+a._m01,+a._m02,+a._m03); }
	public static int1x4 operator ~(int1x4 a) { return new int1x4(~a._m00,~a._m01,~a._m02,~a._m03); }
	public static int1x4 operator !(int1x4 a) { return new int1x4(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m03==0?1:0); }
	public static int1x4 operator +(int1x4 a, int1x4 b) { return new int1x4(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m03 + b._m03); }
	public static int1x4 operator *(int1x4 a, int1x4 b) { return new int1x4(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m03 * b._m03); }
	public static int1x4 operator -(int1x4 a, int1x4 b) { return new int1x4(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m03 - b._m03); }
	public static int1x4 operator /(int1x4 a, int1x4 b) { return new int1x4(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m03 / b._m03); }
	public static int1x4 operator %(int1x4 a, int1x4 b) { return new int1x4(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m03 % b._m03); }
	public static int1x4 operator &(int1x4 a, int1x4 b) { return new int1x4(a._m00 & b._m00,a._m01 & b._m01,a._m02 & b._m02,a._m03 & b._m03); }
	public static int1x4 operator |(int1x4 a, int1x4 b) { return new int1x4(a._m00 | b._m00,a._m01 | b._m01,a._m02 | b._m02,a._m03 | b._m03); }
	public static int1x4 operator ^(int1x4 a, int1x4 b) { return new int1x4(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m02 ^ b._m02,a._m03 ^ b._m03); }
	public static int1x4 operator ==(int1x4 a, int1x4 b) { return new int1x4((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0, (a._m03 == b._m03)?1:0); }
	public static int1x4 operator !=(int1x4 a, int1x4 b) { return new int1x4((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0, (a._m03 != b._m03)?1:0); }
	public static int1x4 operator <(int1x4 a, int1x4 b) { return new int1x4((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0, (a._m03 < b._m03)?1:0); }
	public static int1x4 operator <=(int1x4 a, int1x4 b) { return new int1x4((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0, (a._m03 <= b._m03)?1:0); }
	public static int1x4 operator >=(int1x4 a, int1x4 b) { return new int1x4((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0, (a._m03 >= b._m03)?1:0); }
	public static int1x4 operator >(int1x4 a, int1x4 b) { return new int1x4((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0, (a._m03 > b._m03)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}, {3}))", this._m00, this._m01, this._m02, this._m03); }
}
}
