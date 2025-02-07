using System;

namespace GMath
{
	public struct float3
	{
		public float x;
		public float y;
		public float z;

		public float2 xy => new(x, y);

		/// <summary>
		/// Shortcut for (0, 0, 0) 
		/// </summary>
		public static float3 zero => new(0, 0, 0);

		/// <summary>
		/// Shortcut for (1, 1, 1) 
		/// </summary>
		public static float3 one => new(1, 1, 1);

		/// <summary>
		/// Shortcut for (1, 0, 0) 
		/// </summary>
		public static float3 right => new(1, 0, 0);

		/// <summary>
		/// Shortcut for (-1, 0, 0) 
		/// </summary>
		public static float3 left => new(-1, 0, 0);

		/// <summary>
		/// Shortcut for (0, 1, 0) 
		/// </summary>
		public static float3 up => new(0, 1, 0);

		/// <summary>
		/// Shortcut for (0, -1, 0) 
		/// </summary>
		public static float3 down => new(0, -1, 0);

		/// <summary>
		/// Shortcut for (0, 0, 1) 
		/// </summary>
		public static float3 forward => new(0, 0, 1);

		/// <summary>
		/// Shortcut for (0, 0, -1) 
		/// </summary>
		public static float3 back => new(0, 0, -1);

		public float this[int idx]
		{
			get
			{
				return idx switch
				{
					0 => x,
					1 => y,
					2 => z,
					_ => 0
				};
			}
			set
			{
				switch (idx)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					case 2:
						z = value;
						break;
				}
			}
		}

		public float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float3(float v) : this(v, v, v)
		{
		}
		
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

		public static explicit operator float1(float3 v)
		{
			return new(v.x);
		}

		public static explicit operator float2(float3 v)
		{
			return new(v.x, v.y);
		}

		public static implicit operator float3(float v)
		{
			return new(v);
		}

		public static explicit operator int3(float3 v)
		{
			return new((int) v.x, (int) v.y, (int) v.z);
		}

		public static float3 operator -(float3 a)
		{
			return new(-a.x, -a.y, -a.z);
		}

		public static float3 operator +(float3 a)
		{
			return new(+a.x, +a.y, +a.z);
		}

		public static int3 operator !(float3 a)
		{
			return new(a.x == 0 ? 1 : 0, a.y == 0 ? 1 : 0, a.z == 0 ? 1 : 0);
		}

		public static float3 operator +(float3 a, float3 b)
		{
			return new(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static float3 operator *(float3 a, float3 b)
		{
			return new(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static float3 operator -(float3 a, float3 b)
		{
			return new(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static float3 operator /(float3 a, float3 b)
		{
			return new(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static float3 operator %(float3 a, float3 b)
		{
			return new(a.x % b.x, a.y % b.y, a.z % b.z);
		}

		public static int3 operator ==(float3 a, float3 b)
		{
			return new(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0, a.z == b.z ? 1 : 0);
		}

		public static int3 operator !=(float3 a, float3 b)
		{
			return new(a.x != b.x ? 1 : 0, a.y != b.y ? 1 : 0, a.z != b.z ? 1 : 0);
		}

		public static int3 operator <(float3 a, float3 b)
		{
			return new(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0, a.z < b.z ? 1 : 0);
		}

		public static int3 operator <=(float3 a, float3 b)
		{
			return new(a.x <= b.x ? 1 : 0, a.y <= b.y ? 1 : 0, a.z <= b.z ? 1 : 0);
		}

		public static int3 operator >=(float3 a, float3 b)
		{
			return new(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0, a.z >= b.z ? 1 : 0);
		}

		public static int3 operator >(float3 a, float3 b)
		{
			return new(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0, a.z > b.z ? 1 : 0);
		}

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}

		private bool Equals(float3 other)
		{
			return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
		}

		public override bool Equals(object obj)
		{
			return obj is float3 other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y, z);
		}
	}
}
