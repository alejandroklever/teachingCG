namespace GMath {
public struct float1x4{
	public float _m00;
	public float _m01;
	public float _m02;
	public float _m03;
	public float4 this[int row] {
		get{
			if(row == 0) return new float4 (_m00, _m01, _m02, _m03);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float1x4(float _m00,float _m01,float _m02,float _m03){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
		this._m03=_m03;
	}
	public float1x4(float v):this(v,v,v,v){}
	public static implicit operator float4(float1x4 m) { return new float4(m._m00, m._m01, m._m02, m._m03); }
	public static implicit operator float1x4(float4 v) { return new float1x4(v.x, v.y, v.z, v.w); }
	public static explicit operator float1x1(float1x4 m) { return new float1x1(m._m00); }
	public static explicit operator float1x2(float1x4 m) { return new float1x2(m._m00, m._m01); }
	public static explicit operator float1x3(float1x4 m) { return new float1x3(m._m00, m._m01, m._m02); }
	public static implicit operator float1x4(float v) { return new float1x4(v); }
	public static explicit operator int1x4(float1x4 v) { return new int1x4((int)v._m00,(int)v._m01,(int)v._m02,(int)v._m03); }
	public static float1x4 operator -(float1x4 a) { return new float1x4(-a._m00,-a._m01,-a._m02,-a._m03); }
	public static float1x4 operator +(float1x4 a) { return new float1x4(+a._m00,+a._m01,+a._m02,+a._m03); }
	public static int1x4 operator !(float1x4 a) { return new int1x4(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m03==0?1:0); }
	public static float1x4 operator +(float1x4 a, float1x4 b) { return new float1x4(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m03 + b._m03); }
	public static float1x4 operator *(float1x4 a, float1x4 b) { return new float1x4(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m03 * b._m03); }
	public static float1x4 operator -(float1x4 a, float1x4 b) { return new float1x4(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m03 - b._m03); }
	public static float1x4 operator /(float1x4 a, float1x4 b) { return new float1x4(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m03 / b._m03); }
	public static float1x4 operator %(float1x4 a, float1x4 b) { return new float1x4(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m03 % b._m03); }
	public static int1x4 operator ==(float1x4 a, float1x4 b) { return new int1x4((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0, (a._m03 == b._m03)?1:0); }
	public static int1x4 operator !=(float1x4 a, float1x4 b) { return new int1x4((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0, (a._m03 != b._m03)?1:0); }
	public static int1x4 operator <(float1x4 a, float1x4 b) { return new int1x4((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0, (a._m03 < b._m03)?1:0); }
	public static int1x4 operator <=(float1x4 a, float1x4 b) { return new int1x4((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0, (a._m03 <= b._m03)?1:0); }
	public static int1x4 operator >=(float1x4 a, float1x4 b) { return new int1x4((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0, (a._m03 >= b._m03)?1:0); }
	public static int1x4 operator >(float1x4 a, float1x4 b) { return new int1x4((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0, (a._m03 > b._m03)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}, {3}))", this._m00, this._m01, this._m02, this._m03); }
}
}
