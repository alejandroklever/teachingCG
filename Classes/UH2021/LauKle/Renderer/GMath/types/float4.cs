namespace GMath {
public struct float4{
	public float x;
	public float y;
	public float z;
	public float w;
	public float2 xy { get { return new float2(this.x, this.y); } }
	public float3 xyz { get { return new float3(this.x, this.y, this.z); } }
	public float this[int idx] {
		get{
			if(idx == 0) return this.x;
			if(idx == 1) return this.y;
			if(idx == 2) return this.z;
			if(idx == 3) return this.w;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) this.x = value;
			if(idx == 1) this.y = value;
			if(idx == 2) this.z = value;
			if(idx == 3) this.w = value;
		}
	}
	public float4(float x,float y,float z,float w){
		this.x=x;
		this.y=y;
		this.z=z;
		this.w=w;
	}

	public float4(float3 xyz, float w)
	{
		this.x = xyz.x;
		this.y = xyz.y;
		this.z = xyz.z;
		this.w = w;
	}
		public float4(float v):this(v,v,v,v){}
	public static explicit operator float1(float4 v) { return new float1(v.x); }
	public static explicit operator float2(float4 v) { return new float2(v.x, v.y); }
	public static explicit operator float3(float4 v) { return new float3(v.x, v.y, v.z); }
	public static implicit operator float4(float v) { return new float4(v); }
	public static explicit operator int4(float4 v) { return new int4((int)v.x,(int)v.y,(int)v.z,(int)v.w); }
	public static float4 operator -(float4 a) { return new float4(-a.x,-a.y,-a.z,-a.w); }
	public static float4 operator +(float4 a) { return new float4(+a.x,+a.y,+a.z,+a.w); }
	public static int4 operator !(float4 a) { return new int4(a.x==0?1:0,a.y==0?1:0,a.z==0?1:0,a.w==0?1:0); }
	public static float4 operator +(float4 a, float4 b) { return new float4(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w); }
	public static float4 operator *(float4 a, float4 b) { return new float4(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w); }
	public static float4 operator -(float4 a, float4 b) { return new float4(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w); }
	public static float4 operator /(float4 a, float4 b) { return new float4(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w); }
	public static float4 operator %(float4 a, float4 b) { return new float4(a.x % b.x,a.y % b.y,a.z % b.z,a.w % b.w); }
	public static int4 operator ==(float4 a, float4 b) { return new int4((a.x == b.x)?1:0, (a.y == b.y)?1:0, (a.z == b.z)?1:0, (a.w == b.w)?1:0); }
	public static int4 operator !=(float4 a, float4 b) { return new int4((a.x != b.x)?1:0, (a.y != b.y)?1:0, (a.z != b.z)?1:0, (a.w != b.w)?1:0); }
	public static int4 operator <(float4 a, float4 b) { return new int4((a.x < b.x)?1:0, (a.y < b.y)?1:0, (a.z < b.z)?1:0, (a.w < b.w)?1:0); }
	public static int4 operator <=(float4 a, float4 b) { return new int4((a.x <= b.x)?1:0, (a.y <= b.y)?1:0, (a.z <= b.z)?1:0, (a.w <= b.w)?1:0); }
	public static int4 operator >=(float4 a, float4 b) { return new int4((a.x >= b.x)?1:0, (a.y >= b.y)?1:0, (a.z >= b.z)?1:0, (a.w >= b.w)?1:0); }
	public static int4 operator >(float4 a, float4 b) { return new int4((a.x > b.x)?1:0, (a.y > b.y)?1:0, (a.z > b.z)?1:0, (a.w > b.w)?1:0); }
	public override string ToString() { return string.Format("({0}, {1}, {2}, {3})", this.x, this.y, this.z, this.w); }
}
}
