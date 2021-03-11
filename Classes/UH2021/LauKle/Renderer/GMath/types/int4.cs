namespace GMath {
public struct int4{
	public int x;
	public int y;
	public int z;
	public int w;
	public int2 xy { get { return new int2(this.x, this.y); } }
	public int3 xyz { get { return new int3(this.x, this.y, this.z); } }
	public int this[int idx] {
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
	public int4(int x,int y,int z,int w){
		this.x=x;
		this.y=y;
		this.z=z;
		this.w=w;
	}
	public int4(int v):this(v,v,v,v){}
	public static explicit operator int1(int4 v) { return new int1(v.x); }
	public static explicit operator int2(int4 v) { return new int2(v.x, v.y); }
	public static explicit operator int3(int4 v) { return new int3(v.x, v.y, v.z); }
	public static implicit operator int4(int v) { return new int4(v); }
	public static implicit operator float4(int4 v) { return new float4((float)v.x,(float)v.y,(float)v.z,(float)v.w); }
	public static int4 operator -(int4 a) { return new int4(-a.x,-a.y,-a.z,-a.w); }
	public static int4 operator +(int4 a) { return new int4(+a.x,+a.y,+a.z,+a.w); }
	public static int4 operator ~(int4 a) { return new int4(~a.x,~a.y,~a.z,~a.w); }
	public static int4 operator !(int4 a) { return new int4(a.x==0?1:0,a.y==0?1:0,a.z==0?1:0,a.w==0?1:0); }
	public static int4 operator +(int4 a, int4 b) { return new int4(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w); }
	public static int4 operator *(int4 a, int4 b) { return new int4(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w); }
	public static int4 operator -(int4 a, int4 b) { return new int4(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w); }
	public static int4 operator /(int4 a, int4 b) { return new int4(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w); }
	public static int4 operator %(int4 a, int4 b) { return new int4(a.x % b.x,a.y % b.y,a.z % b.z,a.w % b.w); }
	public static int4 operator &(int4 a, int4 b) { return new int4(a.x & b.x,a.y & b.y,a.z & b.z,a.w & b.w); }
	public static int4 operator |(int4 a, int4 b) { return new int4(a.x | b.x,a.y | b.y,a.z | b.z,a.w | b.w); }
	public static int4 operator ^(int4 a, int4 b) { return new int4(a.x ^ b.x,a.y ^ b.y,a.z ^ b.z,a.w ^ b.w); }
	public static int4 operator ==(int4 a, int4 b) { return new int4((a.x == b.x)?1:0, (a.y == b.y)?1:0, (a.z == b.z)?1:0, (a.w == b.w)?1:0); }
	public static int4 operator !=(int4 a, int4 b) { return new int4((a.x != b.x)?1:0, (a.y != b.y)?1:0, (a.z != b.z)?1:0, (a.w != b.w)?1:0); }
	public static int4 operator <(int4 a, int4 b) { return new int4((a.x < b.x)?1:0, (a.y < b.y)?1:0, (a.z < b.z)?1:0, (a.w < b.w)?1:0); }
	public static int4 operator <=(int4 a, int4 b) { return new int4((a.x <= b.x)?1:0, (a.y <= b.y)?1:0, (a.z <= b.z)?1:0, (a.w <= b.w)?1:0); }
	public static int4 operator >=(int4 a, int4 b) { return new int4((a.x >= b.x)?1:0, (a.y >= b.y)?1:0, (a.z >= b.z)?1:0, (a.w >= b.w)?1:0); }
	public static int4 operator >(int4 a, int4 b) { return new int4((a.x > b.x)?1:0, (a.y > b.y)?1:0, (a.z > b.z)?1:0, (a.w > b.w)?1:0); }
	public override string ToString() { return string.Format("({0}, {1}, {2}, {3})", this.x, this.y, this.z, this.w); }
}
}
