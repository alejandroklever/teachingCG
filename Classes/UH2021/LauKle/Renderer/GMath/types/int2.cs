namespace GMath {
public struct int2{
	public int x;
	public int y;
	public int this[int idx] {
		get{
			if(idx == 0) return this.x;
			if(idx == 1) return this.y;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) this.x = value;
			if(idx == 1) this.y = value;
		}
	}
	public int2(int x,int y){
		this.x=x;
		this.y=y;
	}
	public int2(int v):this(v,v){}
	public static explicit operator int1(int2 v) { return new int1(v.x); }
	public static implicit operator int2(int v) { return new int2(v); }
	public static implicit operator float2(int2 v) { return new float2((float)v.x,(float)v.y); }
	public static int2 operator -(int2 a) { return new int2(-a.x,-a.y); }
	public static int2 operator +(int2 a) { return new int2(+a.x,+a.y); }
	public static int2 operator ~(int2 a) { return new int2(~a.x,~a.y); }
	public static int2 operator !(int2 a) { return new int2(a.x==0?1:0,a.y==0?1:0); }
	public static int2 operator +(int2 a, int2 b) { return new int2(a.x + b.x,a.y + b.y); }
	public static int2 operator *(int2 a, int2 b) { return new int2(a.x * b.x,a.y * b.y); }
	public static int2 operator -(int2 a, int2 b) { return new int2(a.x - b.x,a.y - b.y); }
	public static int2 operator /(int2 a, int2 b) { return new int2(a.x / b.x,a.y / b.y); }
	public static int2 operator %(int2 a, int2 b) { return new int2(a.x % b.x,a.y % b.y); }
	public static int2 operator &(int2 a, int2 b) { return new int2(a.x & b.x,a.y & b.y); }
	public static int2 operator |(int2 a, int2 b) { return new int2(a.x | b.x,a.y | b.y); }
	public static int2 operator ^(int2 a, int2 b) { return new int2(a.x ^ b.x,a.y ^ b.y); }
	public static int2 operator ==(int2 a, int2 b) { return new int2((a.x == b.x)?1:0, (a.y == b.y)?1:0); }
	public static int2 operator !=(int2 a, int2 b) { return new int2((a.x != b.x)?1:0, (a.y != b.y)?1:0); }
	public static int2 operator <(int2 a, int2 b) { return new int2((a.x < b.x)?1:0, (a.y < b.y)?1:0); }
	public static int2 operator <=(int2 a, int2 b) { return new int2((a.x <= b.x)?1:0, (a.y <= b.y)?1:0); }
	public static int2 operator >=(int2 a, int2 b) { return new int2((a.x >= b.x)?1:0, (a.y >= b.y)?1:0); }
	public static int2 operator >(int2 a, int2 b) { return new int2((a.x > b.x)?1:0, (a.y > b.y)?1:0); }
	public override string ToString() { return string.Format("({0}, {1})", this.x, this.y); }
}
}
