namespace GMath {
public struct float2{
	public float x;
	public float y;
	public float this[int idx] {
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
	public float2(float x,float y){
		this.x=x;
		this.y=y;
	}
	public float2(float v):this(v,v){}
	public static explicit operator float1(float2 v) { return new float1(v.x); }
	public static implicit operator float2(float v) { return new float2(v); }
	public static explicit operator int2(float2 v) { return new int2((int)v.x,(int)v.y); }
	public static float2 operator -(float2 a) { return new float2(-a.x,-a.y); }
	public static float2 operator +(float2 a) { return new float2(+a.x,+a.y); }
	public static int2 operator !(float2 a) { return new int2(a.x==0?1:0,a.y==0?1:0); }
	public static float2 operator +(float2 a, float2 b) { return new float2(a.x + b.x,a.y + b.y); }
	public static float2 operator *(float2 a, float2 b) { return new float2(a.x * b.x,a.y * b.y); }
	public static float2 operator -(float2 a, float2 b) { return new float2(a.x - b.x,a.y - b.y); }
	public static float2 operator /(float2 a, float2 b) { return new float2(a.x / b.x,a.y / b.y); }
	public static float2 operator %(float2 a, float2 b) { return new float2(a.x % b.x,a.y % b.y); }
	public static int2 operator ==(float2 a, float2 b) { return new int2((a.x == b.x)?1:0, (a.y == b.y)?1:0); }
	public static int2 operator !=(float2 a, float2 b) { return new int2((a.x != b.x)?1:0, (a.y != b.y)?1:0); }
	public static int2 operator <(float2 a, float2 b) { return new int2((a.x < b.x)?1:0, (a.y < b.y)?1:0); }
	public static int2 operator <=(float2 a, float2 b) { return new int2((a.x <= b.x)?1:0, (a.y <= b.y)?1:0); }
	public static int2 operator >=(float2 a, float2 b) { return new int2((a.x >= b.x)?1:0, (a.y >= b.y)?1:0); }
	public static int2 operator >(float2 a, float2 b) { return new int2((a.x > b.x)?1:0, (a.y > b.y)?1:0); }
	public override string ToString() { return string.Format("({0}, {1})", this.x, this.y); }
}
}
