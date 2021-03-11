namespace GMath {
public struct float2x4{
	public float _m00;
	public float _m01;
	public float _m02;
	public float _m03;
	public float _m10;
	public float _m11;
	public float _m12;
	public float _m13;
	public float4 this[int row] {
		get{
			if(row == 0) return new float4 (_m00, _m01, _m02, _m03);
			if(row == 1) return new float4 (_m10, _m11, _m12, _m13);
			return 0; // Silent return ... valid for HLSL
		}
	}
	public float2x4(float _m00,float _m01,float _m02,float _m03,float _m10,float _m11,float _m12,float _m13){
		this._m00=_m00;
		this._m01=_m01;
		this._m02=_m02;
		this._m03=_m03;
		this._m10=_m10;
		this._m11=_m11;
		this._m12=_m12;
		this._m13=_m13;
	}
	public float2x4(float v):this(v,v,v,v,v,v,v,v){}
	public static explicit operator float1x1(float2x4 m) { return new float1x1(m._m00); }
	public static explicit operator float1x2(float2x4 m) { return new float1x2(m._m00, m._m01); }
	public static explicit operator float1x3(float2x4 m) { return new float1x3(m._m00, m._m01, m._m02); }
	public static explicit operator float1x4(float2x4 m) { return new float1x4(m._m00, m._m01, m._m02, m._m03); }
	public static explicit operator float2x1(float2x4 m) { return new float2x1(m._m00, m._m10); }
	public static explicit operator float2x2(float2x4 m) { return new float2x2(m._m00, m._m01, m._m10, m._m11); }
	public static explicit operator float2x3(float2x4 m) { return new float2x3(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12); }
	public static implicit operator float2x4(float v) { return new float2x4(v); }
	public static explicit operator int2x4(float2x4 v) { return new int2x4((int)v._m00,(int)v._m01,(int)v._m02,(int)v._m03,(int)v._m10,(int)v._m11,(int)v._m12,(int)v._m13); }
	public static float2x4 operator -(float2x4 a) { return new float2x4(-a._m00,-a._m01,-a._m02,-a._m03,-a._m10,-a._m11,-a._m12,-a._m13); }
	public static float2x4 operator +(float2x4 a) { return new float2x4(+a._m00,+a._m01,+a._m02,+a._m03,+a._m10,+a._m11,+a._m12,+a._m13); }
	public static int2x4 operator !(float2x4 a) { return new int2x4(a._m00==0?1:0,a._m01==0?1:0,a._m02==0?1:0,a._m03==0?1:0,a._m10==0?1:0,a._m11==0?1:0,a._m12==0?1:0,a._m13==0?1:0); }
	public static float2x4 operator +(float2x4 a, float2x4 b) { return new float2x4(a._m00 + b._m00,a._m01 + b._m01,a._m02 + b._m02,a._m03 + b._m03,a._m10 + b._m10,a._m11 + b._m11,a._m12 + b._m12,a._m13 + b._m13); }
	public static float2x4 operator *(float2x4 a, float2x4 b) { return new float2x4(a._m00 * b._m00,a._m01 * b._m01,a._m02 * b._m02,a._m03 * b._m03,a._m10 * b._m10,a._m11 * b._m11,a._m12 * b._m12,a._m13 * b._m13); }
	public static float2x4 operator -(float2x4 a, float2x4 b) { return new float2x4(a._m00 - b._m00,a._m01 - b._m01,a._m02 - b._m02,a._m03 - b._m03,a._m10 - b._m10,a._m11 - b._m11,a._m12 - b._m12,a._m13 - b._m13); }
	public static float2x4 operator /(float2x4 a, float2x4 b) { return new float2x4(a._m00 / b._m00,a._m01 / b._m01,a._m02 / b._m02,a._m03 / b._m03,a._m10 / b._m10,a._m11 / b._m11,a._m12 / b._m12,a._m13 / b._m13); }
	public static float2x4 operator %(float2x4 a, float2x4 b) { return new float2x4(a._m00 % b._m00,a._m01 % b._m01,a._m02 % b._m02,a._m03 % b._m03,a._m10 % b._m10,a._m11 % b._m11,a._m12 % b._m12,a._m13 % b._m13); }
	public static int2x4 operator ==(float2x4 a, float2x4 b) { return new int2x4((a._m00 == b._m00)?1:0, (a._m01 == b._m01)?1:0, (a._m02 == b._m02)?1:0, (a._m03 == b._m03)?1:0, (a._m10 == b._m10)?1:0, (a._m11 == b._m11)?1:0, (a._m12 == b._m12)?1:0, (a._m13 == b._m13)?1:0); }
	public static int2x4 operator !=(float2x4 a, float2x4 b) { return new int2x4((a._m00 != b._m00)?1:0, (a._m01 != b._m01)?1:0, (a._m02 != b._m02)?1:0, (a._m03 != b._m03)?1:0, (a._m10 != b._m10)?1:0, (a._m11 != b._m11)?1:0, (a._m12 != b._m12)?1:0, (a._m13 != b._m13)?1:0); }
	public static int2x4 operator <(float2x4 a, float2x4 b) { return new int2x4((a._m00 < b._m00)?1:0, (a._m01 < b._m01)?1:0, (a._m02 < b._m02)?1:0, (a._m03 < b._m03)?1:0, (a._m10 < b._m10)?1:0, (a._m11 < b._m11)?1:0, (a._m12 < b._m12)?1:0, (a._m13 < b._m13)?1:0); }
	public static int2x4 operator <=(float2x4 a, float2x4 b) { return new int2x4((a._m00 <= b._m00)?1:0, (a._m01 <= b._m01)?1:0, (a._m02 <= b._m02)?1:0, (a._m03 <= b._m03)?1:0, (a._m10 <= b._m10)?1:0, (a._m11 <= b._m11)?1:0, (a._m12 <= b._m12)?1:0, (a._m13 <= b._m13)?1:0); }
	public static int2x4 operator >=(float2x4 a, float2x4 b) { return new int2x4((a._m00 >= b._m00)?1:0, (a._m01 >= b._m01)?1:0, (a._m02 >= b._m02)?1:0, (a._m03 >= b._m03)?1:0, (a._m10 >= b._m10)?1:0, (a._m11 >= b._m11)?1:0, (a._m12 >= b._m12)?1:0, (a._m13 >= b._m13)?1:0); }
	public static int2x4 operator >(float2x4 a, float2x4 b) { return new int2x4((a._m00 > b._m00)?1:0, (a._m01 > b._m01)?1:0, (a._m02 > b._m02)?1:0, (a._m03 > b._m03)?1:0, (a._m10 > b._m10)?1:0, (a._m11 > b._m11)?1:0, (a._m12 > b._m12)?1:0, (a._m13 > b._m13)?1:0); }
	public override string ToString() { return string.Format("(({0}, {1}, {2}, {3}), ({4}, {5}, {6}, {7}))", this._m00, this._m01, this._m02, this._m03, this._m10, this._m11, this._m12, this._m13); }
}
}
