namespace GMath {
public struct int3{
	public int x;
	public int y;
	public int z;
	public int2 xy { get { return new(x, y); } }
	public int this[int idx] {
		get{
			if(idx == 0) return x;
			if(idx == 1) return y;
			if(idx == 2) return z;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) x = value;
			if(idx == 1) y = value;
			if(idx == 2) z = value;
		}
	}
		public int3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public int3(bool x, bool y, bool z)
		{
			this.x = x ? 1 : 0;
			this.y = y ? 1 : 0;
			this.z = z ? 1 : 0;
		}
		public int3(int v):this(v,v,v){}
	public static explicit operator int1(int3 v) { return new(v.x); }
	public static explicit operator int2(int3 v) { return new(v.x, v.y); }
	public static implicit operator int3(int v) { return new(v); }
	public static implicit operator float3(int3 v) { return new((float)v.x,(float)v.y,(float)v.z); }
	public static int3 operator -(int3 a) { return new(-a.x,-a.y,-a.z); }
	public static int3 operator +(int3 a) { return new(+a.x,+a.y,+a.z); }
	public static int3 operator ~(int3 a) { return new(~a.x,~a.y,~a.z); }
	public static int3 operator !(int3 a) { return new(a.x==0?1:0,a.y==0?1:0,a.z==0?1:0); }
	public static int3 operator +(int3 a, int3 b) { return new(a.x + b.x,a.y + b.y,a.z + b.z); }
	public static int3 operator *(int3 a, int3 b) { return new(a.x * b.x,a.y * b.y,a.z * b.z); }
	public static int3 operator -(int3 a, int3 b) { return new(a.x - b.x,a.y - b.y,a.z - b.z); }
	public static int3 operator /(int3 a, int3 b) { return new(a.x / b.x,a.y / b.y,a.z / b.z); }
	public static int3 operator %(int3 a, int3 b) { return new(a.x % b.x,a.y % b.y,a.z % b.z); }
	public static int3 operator &(int3 a, int3 b) { return new(a.x & b.x,a.y & b.y,a.z & b.z); }
	public static int3 operator |(int3 a, int3 b) { return new(a.x | b.x,a.y | b.y,a.z | b.z); }
	public static int3 operator ^(int3 a, int3 b) { return new(a.x ^ b.x,a.y ^ b.y,a.z ^ b.z); }
	public static int3 operator ==(int3 a, int3 b) { return new(a.x == b.x?1:0, a.y == b.y?1:0, a.z == b.z?1:0); }
	public static int3 operator !=(int3 a, int3 b) { return new(a.x != b.x?1:0, a.y != b.y?1:0, a.z != b.z?1:0); }
	public static int3 operator <(int3 a, int3 b) { return new(a.x < b.x?1:0, a.y < b.y?1:0, a.z < b.z?1:0); }
	public static int3 operator <=(int3 a, int3 b) { return new(a.x <= b.x?1:0, a.y <= b.y?1:0, a.z <= b.z?1:0); }
	public static int3 operator >=(int3 a, int3 b) { return new(a.x >= b.x?1:0, a.y >= b.y?1:0, a.z >= b.z?1:0); }
	public static int3 operator >(int3 a, int3 b) { return new(a.x > b.x?1:0, a.y > b.y?1:0, a.z > b.z?1:0); }
	public override string ToString() { return $"({x}, {y}, {z})"; }
}
}
