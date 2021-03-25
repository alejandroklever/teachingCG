using System;

namespace GMath
{
	public struct float2
	{
		public float x;
		public float y;

		/// <summary>
		/// Shortcut for (0, 0) 
		/// </summary>
		public static float2 zero => new float2(0, 0);

		/// <summary>
		/// Shortcut for (1, 1) 
		/// </summary>
		public static float2 one => new float2(1, 1);

		/// <summary>
		/// Shortcut for (1, 0) 
		/// </summary>
		public static float2 right => new float2(1, 0);

		/// <summary>
		/// Shortcut for (-1, 0) 
		/// </summary>
		public static float2 left => new float2(-1, 0);

		/// <summary>
		/// Shortcut for (0, 1) 
		/// </summary>
		public static float2 up => new float2(0, 1);

		/// <summary>
		/// Shortcut for (0, -1) 
		/// </summary>
		public static float2 down => new float2(0, -1);

		public float this[int idx]
		{
			get
			{
				return idx switch
				{
					0 => x,
					1 => y,
					_ => 0
				};
			}
			set
			{
				if (idx == 0) x = value;
				if (idx == 1) y = value;
			}
		}

		public float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public float2(float v) : this(v, v)
		{
		}

		public static explicit operator float1(float2 v)
		{
			return new float1(v.x);
		}

		public static implicit operator float2(float v)
		{
			return new float2(v);
		}

		public static explicit operator int2(float2 v)
		{
			return new int2((int) v.x, (int) v.y);
		}

		public static float2 operator -(float2 a)
		{
			return new float2(-a.x, -a.y);
		}

		public static float2 operator +(float2 a)
		{
			return new float2(+a.x, +a.y);
		}

		public static int2 operator !(float2 a)
		{
			return new int2(a.x == 0 ? 1 : 0, a.y == 0 ? 1 : 0);
		}

		public static float2 operator +(float2 a, float2 b)
		{
			return new float2(a.x + b.x, a.y + b.y);
		}

		public static float2 operator *(float2 a, float2 b)
		{
			return new float2(a.x * b.x, a.y * b.y);
		}

		public static float2 operator -(float2 a, float2 b)
		{
			return new float2(a.x - b.x, a.y - b.y);
		}

		public static float2 operator /(float2 a, float2 b)
		{
			return new float2(a.x / b.x, a.y / b.y);
		}

		public static float2 operator %(float2 a, float2 b)
		{
			return new float2(a.x % b.x, a.y % b.y);
		}

		public static int2 operator ==(float2 a, float2 b)
		{
			return new int2(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0);
		}

		public static int2 operator !=(float2 a, float2 b)
		{
			return new int2(a.x != b.x ? 1 : 0, a.y != b.y ? 1 : 0);
		}

		public static int2 operator <(float2 a, float2 b)
		{
			return new int2(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0);
		}

		public static int2 operator <=(float2 a, float2 b)
		{
			return new int2(a.x <= b.x ? 1 : 0, a.y <= b.y ? 1 : 0);
		}

		public static int2 operator >=(float2 a, float2 b)
		{
			return new int2(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0);
		}

		public static int2 operator >(float2 a, float2 b)
		{
			return new int2(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0);
		}

		public override string ToString()
		{
			return $"({x}, {y})";
		}

		private bool Equals(float2 other)
		{
			return x.Equals(other.x) && y.Equals(other.y);
		}

		public override bool Equals(object obj)
		{
			return obj is float2 other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y);
		}
	}
}
