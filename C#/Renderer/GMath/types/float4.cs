namespace GMath {
public struct float4{
	public float x;
	public float y;
	public float z;
	public float w;
	public float2 xy { get { return new(x, y); } }
	public float3 xyz { get { return new(x, y, z); } }
	public float this[int idx] {
		get{
			if(idx == 0) return x;
			if(idx == 1) return y;
			if(idx == 2) return z;
			if(idx == 3) return w;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) x = value;
			if(idx == 1) y = value;
			if(idx == 2) z = value;
			if(idx == 3) w = value;
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
		x = xyz.x;
		y = xyz.y;
		z = xyz.z;
		this.w = w;
	}
		public float4(float v):this(v,v,v,v){}
	public static explicit operator float1(float4 v) { return new(v.x); }
	public static explicit operator float2(float4 v) { return new(v.x, v.y); }
	public static explicit operator float3(float4 v) { return new(v.x, v.y, v.z); }
	public static implicit operator float4(float v) { return new(v); }
	public static explicit operator int4(float4 v) { return new((int)v.x,(int)v.y,(int)v.z,(int)v.w); }
	public static float4 operator -(float4 a) { return new(-a.x,-a.y,-a.z,-a.w); }
	public static float4 operator +(float4 a) { return new(+a.x,+a.y,+a.z,+a.w); }
	public static int4 operator !(float4 a) { return new(a.x==0?1:0,a.y==0?1:0,a.z==0?1:0,a.w==0?1:0); }
	public static float4 operator +(float4 a, float4 b) { return new(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w); }
	public static float4 operator *(float4 a, float4 b) { return new(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w); }
	public static float4 operator -(float4 a, float4 b) { return new(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w); }
	public static float4 operator /(float4 a, float4 b) { return new(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w); }
	public static float4 operator %(float4 a, float4 b) { return new(a.x % b.x,a.y % b.y,a.z % b.z,a.w % b.w); }
	public static int4 operator ==(float4 a, float4 b) { return new(a.x == b.x?1:0, a.y == b.y?1:0, a.z == b.z?1:0, a.w == b.w?1:0); }
	public static int4 operator !=(float4 a, float4 b) { return new(a.x != b.x?1:0, a.y != b.y?1:0, a.z != b.z?1:0, a.w != b.w?1:0); }
	public static int4 operator <(float4 a, float4 b) { return new(a.x < b.x?1:0, a.y < b.y?1:0, a.z < b.z?1:0, a.w < b.w?1:0); }
	public static int4 operator <=(float4 a, float4 b) { return new(a.x <= b.x?1:0, a.y <= b.y?1:0, a.z <= b.z?1:0, a.w <= b.w?1:0); }
	public static int4 operator >=(float4 a, float4 b) { return new(a.x >= b.x?1:0, a.y >= b.y?1:0, a.z >= b.z?1:0, a.w >= b.w?1:0); }
	public static int4 operator >(float4 a, float4 b) { return new(a.x > b.x?1:0, a.y > b.y?1:0, a.z > b.z?1:0, a.w > b.w?1:0); }
	public override string ToString() { return $"({x}, {y}, {z}, {w})"; }
}
}
