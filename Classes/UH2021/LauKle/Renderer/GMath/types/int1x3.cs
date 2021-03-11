namespace GMath {
public struct int1x3{
	public int _m00;
	public int _m01;
	public int _m02;
	public int3 this[int row] {
		get{
			if(row == 0) return new int3 (_m00, _m01, _m02);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public int1x3(int _m00,int _m01,int _m02){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
	}
	public int1x3(int v):this(v,v,v){}
	public static implicit operator int3(int1x3 m) { return new int3(m._m00, m._m01, m._m02); }
	public static implicit operator int1x3(int3 v) { return new int1x3(v.x, v.y, v.z); }
	public static explicit operator int1x1(int1x3 m) { return new int1x1(m._m00); }
	public static explicit operator int1x2(int1x3 m) { return new int1x2(m._m00, m._m01); }
	public static implicit operator int1x3(int v) { return new int1x3(v); }
	public static implicit operator float1x3(int1x3 v) { return new float1x3((float)v._m00,(float)v._m01,(float)v._m02); }
	public static int1x3 operator -(int1x3 a) { return new int1x3(-a._m00,-a._m01,-a._m02); }
	public static int1x3 operator +(int1x3 a) { return new int1x3(+a._m00,+a._m01,+a._m02); }
	public static int1x3 operator ~(int1x3 a) { return new int1x3(~a._m00,~a._m01,~a._m02); }
	public static int1x3 operator !(int1x3 a) { return new int1x3(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0); }
	public static int1x3 operator +(int1x3 a, int1x3 b) { return new int1x3(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02); }
	public static int1x3 operator *(int1x3 a, int1x3 b) { return new int1x3(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02); }
	public static int1x3 operator -(int1x3 a, int1x3 b) { return new int1x3(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02); }
	public static int1x3 operator /(int1x3 a, int1x3 b) { return new int1x3(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02); }
	public static int1x3 operator %(int1x3 a, int1x3 b) { return new int1x3(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02); }
	public static int1x3 operator &(int1x3 a, int1x3 b) { return new int1x3(a._m00 & b._m00,a._m01 & b._m01,a._m02 & b._m02); }
	public static int1x3 operator |(int1x3 a, int1x3 b) { return new int1x3(a._m00 | b._m00,a._m01 | b._m01,a._m02 | b._m02); }
	public static int1x3 operator ^(int1x3 a, int1x3 b) { return new int1x3(a._m00 ^ b._m00,a._m01 ^ b._m01,a._m02 ^ b._m02); }
	public static int1x3 operator ==(int1x3 a, int1x3 b) { return new int1x3((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0); }
	public static int1x3 operator !=(int1x3 a, int1x3 b) { return new int1x3((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0); }
	public static int1x3 operator <(int1x3 a, int1x3 b) { return new int1x3((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0); }
	public static int1x3 operator <=(int1x3 a, int1x3 b) { return new int1x3((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0); }
	public static int1x3 operator >=(int1x3 a, int1x3 b) { return new int1x3((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0); }
	public static int1x3 operator >(int1x3 a, int1x3 b) { return new int1x3((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}))", this._m00, this._m01, this._m02); }
}
}
