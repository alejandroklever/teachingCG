namespace GMath {
public struct int1{
	public int x;
	public int this[int idx] {
		get{
			if(idx == 0) return this.x;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) this.x = value;
		}
	}
	public int1(int x){
		this.x=x;
	}
	public static implicit operator int1(int v) { return new int1(v); }
	public static implicit operator float1(int1 v) { return new float1((float)v.x); }
	public static int1 operator -(int1 a) { return new int1(-a.x); }
	public static int1 operator +(int1 a) { return new int1(+a.x); }
	public static int1 operator ~(int1 a) { return new int1(~a.x); }
	public static int1 operator !(int1 a) { return new int1(a.x==0?1:0); }
	public static int1 operator +(int1 a, int1 b) { return new int1(a.x + b.x); }
	public static int1 operator *(int1 a, int1 b) { return new int1(a.x * b.x); }
	public static int1 operator -(int1 a, int1 b) { return new int1(a.x - b.x); }
	public static int1 operator /(int1 a, int1 b) { return new int1(a.x / b.x); }
	public static int1 operator %(int1 a, int1 b) { return new int1(a.x % b.x); }
	public static int1 operator &(int1 a, int1 b) { return new int1(a.x & b.x); }
	public static int1 operator |(int1 a, int1 b) { return new int1(a.x | b.x); }
	public static int1 operator ^(int1 a, int1 b) { return new int1(a.x ^ b.x); }
	public static int1 operator ==(int1 a, int1 b) { return new int1((a.x == b.x)?1:0); }
	public static int1 operator !=(int1 a, int1 b) { return new int1((a.x != b.x)?1:0); }
	public static int1 operator <(int1 a, int1 b) { return new int1((a.x < b.x)?1:0); }
	public static int1 operator <=(int1 a, int1 b) { return new int1((a.x <= b.x)?1:0); }
	public static int1 operator >=(int1 a, int1 b) { return new int1((a.x >= b.x)?1:0); }
	public static int1 operator >(int1 a, int1 b) { return new int1((a.x > b.x)?1:0); }
	public override string ToString() { return string.Format("({0})", this.x); }
}
}
