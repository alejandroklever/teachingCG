namespace GMath
{
	public struct float1
	{


		public float x;

		public float this[int idx]
		{
			get
			{
				if (idx == 0) return x;
				return 0; // Silent return ... valid for HLSL
			}
			set
			{
				if (idx == 0) x = value;
			}
		}

		public float1(float x)
		{
			this.x = x;
		}

		public static implicit operator float1(float v)
		{
			return new(v);
		}

		public static explicit operator int1(float1 v)
		{
			return new((int) v.x);
		}

		public static float1 operator -(float1 a)
		{
			return new(-a.x);
		}

		public static float1 operator +(float1 a)
		{
			return new(+a.x);
		}

		public static int1 operator !(float1 a)
		{
			return new(a.x == 0 ? 1 : 0);
		}

		public static float1 operator +(float1 a, float1 b)
		{
			return new(a.x + b.x);
		}

		public static float1 operator *(float1 a, float1 b)
		{
			return new(a.x * b.x);
		}

		public static float1 operator -(float1 a, float1 b)
		{
			return new(a.x - b.x);
		}

		public static float1 operator /(float1 a, float1 b)
		{
			return new(a.x / b.x);
		}

		public static float1 operator %(float1 a, float1 b)
		{
			return new(a.x % b.x);
		}

		public static int1 operator ==(float1 a, float1 b)
		{
			return new(a.x == b.x ? 1 : 0);
		}

		public static int1 operator !=(float1 a, float1 b)
		{
			return new(a.x != b.x ? 1 : 0);
		}

		public static int1 operator <(float1 a, float1 b)
		{
			return new(a.x < b.x ? 1 : 0);
		}

		public static int1 operator <=(float1 a, float1 b)
		{
			return new(a.x <= b.x ? 1 : 0);
		}

		public static int1 operator >=(float1 a, float1 b)
		{
			return new(a.x >= b.x ? 1 : 0);
		}

		public static int1 operator >(float1 a, float1 b)
		{
			return new(a.x > b.x ? 1 : 0);
		}

		public override string ToString()
		{
			return $"({x})";
		}

		private bool Equals(float1 other)
		{
			return x.Equals(other.x);
		}

		public override bool Equals(object obj)
		{
			return obj is float1 other && Equals(other);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode();
		}
	}
}
