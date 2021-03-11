namespace GMath {
public struct float1{
	public float x;
	public float this[int idx] {
		get{
			if(idx == 0) return this.x;
			return 0; // Silent return ... valid for HLSL
		}
		set{
			if(idx == 0) this.x = value;
		}
	}
	public float1(float x){
		this.x=x;
	}
	public static implicit operator float1(float v) { return new float1(v); }
	public static explicit operator int1(float1 v) { return new int1((int)v.x); }
	public static float1 operator -(float1 a) { return new float1(-a.x); }
	public static float1 operator +(float1 a) { return new float1(+a.x); }
	public static int1 operator !(float1 a) { return new int1(a.x==0?1:0); }
	public static float1 operator +(float1 a, float1 b) { return new float1(a.x + b.x); }
	public static float1 operator *(float1 a, float1 b) { return new float1(a.x * b.x); }
	public static float1 operator -(float1 a, float1 b) { return new float1(a.x - b.x); }
	public static float1 operator /(float1 a, float1 b) { return new float1(a.x / b.x); }
	public static float1 operator %(float1 a, float1 b) { return new float1(a.x % b.x); }
	public static int1 operator ==(float1 a, float1 b) { return new int1((a.x == b.x)?1:0); }
	public static int1 operator !=(float1 a, float1 b) { return new int1((a.x != b.x)?1:0); }
	public static int1 operator <(float1 a, float1 b) { return new int1((a.x < b.x)?1:0); }
	public static int1 operator <=(float1 a, float1 b) { return new int1((a.x <= b.x)?1:0); }
	public static int1 operator >=(float1 a, float1 b) { return new int1((a.x >= b.x)?1:0); }
	public static int1 operator >(float1 a, float1 b) { return new int1((a.x > b.x)?1:0); }
	public override string ToString() { return string.Format("({0})", this.x); }
}
}
