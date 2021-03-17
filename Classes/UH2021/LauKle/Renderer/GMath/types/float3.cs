namespace GMath 
{
	public struct float3
	{
		public float x;
		public float y;
		public float z;

		public float2 xy => new float2(x, y);

		public static float3 zero => new float3(0, 0, 0);
		
		public static float3 one => new float3(1, 1, 1);
		
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

		public float3(float v) : this(v, v, v) { }

		public static explicit operator float1(float3 v)
		{
			return new float1(v.x);
		}

		public static explicit operator float2(float3 v)
		{
			return new float2(v.x, v.y);
		}

		public static implicit operator float3(float v)
		{
			return new float3(v);
		}

		public static explicit operator int3(float3 v)
		{
			return new int3((int) v.x, (int) v.y, (int) v.z);
		}

		public static float3 operator -(float3 a)
		{
			return new float3(-a.x, -a.y, -a.z);
		}

		public static float3 operator +(float3 a)
		{
			return new float3(+a.x, +a.y, +a.z);
		}

		public static int3 operator !(float3 a)
		{
			return new int3(a.x == 0 ? 1 : 0, a.y == 0 ? 1 : 0, a.z == 0 ? 1 : 0);
		}

		public static float3 operator +(float3 a, float3 b)
		{
			return new float3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static float3 operator *(float3 a, float3 b)
		{
			return new float3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static float3 operator -(float3 a, float3 b)
		{
			return new float3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static float3 operator /(float3 a, float3 b)
		{
			return new float3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static float3 operator %(float3 a, float3 b)
		{
			return new float3(a.x % b.x, a.y % b.y, a.z % b.z);
		}

		public static int3 operator ==(float3 a, float3 b)
		{
			return new int3(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0, a.z == b.z ? 1 : 0);
		}

		public static int3 operator !=(float3 a, float3 b)
		{
			return new int3(a.x != b.x ? 1 : 0, a.y != b.y ? 1 : 0, a.z != b.z ? 1 : 0);
		}

		public static int3 operator <(float3 a, float3 b)
		{
			return new int3(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0, a.z < b.z ? 1 : 0);
		}

		public static int3 operator <=(float3 a, float3 b)
		{
			return new int3(a.x <= b.x ? 1 : 0, a.y <= b.y ? 1 : 0, a.z <= b.z ? 1 : 0);
		}

		public static int3 operator >=(float3 a, float3 b)
		{
			return new int3(a.x >= b.x ? 1 : 0, a.y >= b.y ? 1 : 0, a.z >= b.z ? 1 : 0);
		}

		public static int3 operator >(float3 a, float3 b)
		{
			return new int3(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0, a.z > b.z ? 1 : 0);
		}

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}
	}
}
