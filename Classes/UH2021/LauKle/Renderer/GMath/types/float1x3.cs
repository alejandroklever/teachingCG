namespace GMath {
public struct float1x3{
	public float _m00;
	public float _m01;
	public float _m02;
	public float3 this[int row] {
		get{
			if(row == 0) return new float3 (_m00, _m01, _m02);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float1x3(float _m00,float _m01,float _m02){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
	}
	public float1x3(float v):this(v,v,v){}
	public static implicit operator float3(float1x3 m) { return new float3(m._m00, m._m01, m._m02); }
	public static implicit operator float1x3(float3 v) { return new float1x3(v.x, v.y, v.z); }
	public static explicit operator float1x1(float1x3 m) { return new float1x1(m._m00); }
	public static explicit operator float1x2(float1x3 m) { return new float1x2(m._m00, m._m01); }
	public static implicit operator float1x3(float v) { return new float1x3(v); }
	public static explicit operator int1x3(float1x3 v) { return new int1x3((int)v._m00,(int)v._m01,(int)v._m02); }
	public static float1x3 operator -(float1x3 a) { return new float1x3(-a._m00,-a._m01,-a._m02); }
	public static float1x3 operator +(float1x3 a) { return new float1x3(+a._m00,+a._m01,+a._m02); }
	public static int1x3 operator !(float1x3 a) { return new int1x3(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0); }
	public static float1x3 operator +(float1x3 a, float1x3 b) { return new float1x3(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02); }
	public static float1x3 operator *(float1x3 a, float1x3 b) { return new float1x3(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02); }
	public static float1x3 operator -(float1x3 a, float1x3 b) { return new float1x3(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02); }
	public static float1x3 operator /(float1x3 a, float1x3 b) { return new float1x3(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02); }
	public static float1x3 operator %(float1x3 a, float1x3 b) { return new float1x3(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02); }
	public static int1x3 operator ==(float1x3 a, float1x3 b) { return new int1x3((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0); }
	public static int1x3 operator !=(float1x3 a, float1x3 b) { return new int1x3((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0); }
	public static int1x3 operator <(float1x3 a, float1x3 b) { return new int1x3((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0); }
	public static int1x3 operator <=(float1x3 a, float1x3 b) { return new int1x3((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0); }
	public static int1x3 operator >=(float1x3 a, float1x3 b) { return new int1x3((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0); }
	public static int1x3 operator >(float1x3 a, float1x3 b) { return new int1x3((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}))", this._m00, this._m01, this._m02); }
}
}
