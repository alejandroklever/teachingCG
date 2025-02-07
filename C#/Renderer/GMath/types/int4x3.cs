namespace GMath
{
	public struct int4x3
	{
		public int _m00;
		public int _m01;
		public int _m02;
		public int _m10;
		public int _m11;
		public int _m12;
		public int _m20;
		public int _m21;
		public int _m22;
		public int _m30;
		public int _m31;
		public int _m32;

		public int3 this[int row]
		{
			get
			{
				if (row == 0) return new int3(_m00, _m01, _m02);
				if (row == 1) return new int3(_m10, _m11, _m12);
				if (row == 2) return new int3(_m20, _m21, _m22);
				if (row == 3) return new int3(_m30, _m31, _m32);
				return 0; // Silent return ... valid for HLSL
			}
		}

		public int4x3(int _m00, int _m01, int _m02, int _m10, int _m11, int _m12, int _m20, int _m21, int _m22,
			int _m30, int _m31, int _m32)
		{
			this._m00 = _m00;
			this._m01 = _m01;
			this._m02 = _m02;
			this._m10 = _m10;
			this._m11 = _m11;
			this._m12 = _m12;
			this._m20 = _m20;
			this._m21 = _m21;
			this._m22 = _m22;
			this._m30 = _m30;
			this._m31 = _m31;
			this._m32 = _m32;
		}

		public int4x3(int v) : this(v, v, v, v, v, v, v, v, v, v, v, v)
		{
		}

		public static explicit operator int1x1(int4x3 m)
		{
			return new(m._m00);
		}

		public static explicit operator int1x2(int4x3 m)
		{
			return new(m._m00, m._m01);
		}

		public static explicit operator int1x3(int4x3 m)
		{
			return new(m._m00, m._m01, m._m02);
		}

		public static explicit operator int2x1(int4x3 m)
		{
			return new(m._m00, m._m10);
		}

		public static explicit operator int2x2(int4x3 m)
		{
			return new(m._m00, m._m01, m._m10, m._m11);
		}

		public static explicit operator int2x3(int4x3 m)
		{
			return new(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12);
		}

		public static explicit operator int3x1(int4x3 m)
		{
			return new(m._m00, m._m10, m._m20);
		}

		public static explicit operator int3x2(int4x3 m)
		{
			return new(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21);
		}

		public static explicit operator int3x3(int4x3 m)
		{
			return new(m._m00, m._m01, m._m02, m._m10, m._m11, m._m12, m._m20, m._m21, m._m22);
		}

		public static explicit operator int4x1(int4x3 m)
		{
			return new(m._m00, m._m10, m._m20, m._m30);
		}

		public static explicit operator int4x2(int4x3 m)
		{
			return new(m._m00, m._m01, m._m10, m._m11, m._m20, m._m21, m._m30, m._m31);
		}

		public static implicit operator int4x3(int v)
		{
			return new(v);
		}

		public static implicit operator float4x3(int4x3 v)
		{
			return new((float) v._m00, (float) v._m01, (float) v._m02, (float) v._m10, (float) v._m11, (float) v._m12,
				(float) v._m20, (float) v._m21, (float) v._m22, (float) v._m30, (float) v._m31, (float) v._m32);
		}

		public static int4x3 operator -(int4x3 a)
		{
			return new(-a._m00, -a._m01, -a._m02, -a._m10, -a._m11, -a._m12, -a._m20, -a._m21, -a._m22, -a._m30, -a._m31
				, -a._m32);
		}

		public static int4x3 operator +(int4x3 a)
		{
			return new(+a._m00, +a._m01, +a._m02, +a._m10, +a._m11, +a._m12, +a._m20, +a._m21, +a._m22, +a._m30, +a._m31
				, +a._m32);
		}

		public static int4x3 operator ~(int4x3 a)
		{
			return new(~a._m00, ~a._m01, ~a._m02, ~a._m10, ~a._m11, ~a._m12, ~a._m20, ~a._m21, ~a._m22, ~a._m30, ~a._m31
				, ~a._m32);
		}

		public static int4x3 operator !(int4x3 a)
		{
			return new(a._m00 == 0 ? 1 : 0, a._m01 == 0 ? 1 : 0, a._m02 == 0 ? 1 : 0, a._m10 == 0 ? 1 : 0,
				a._m11 == 0 ? 1 : 0, a._m12 == 0 ? 1 : 0, a._m20 == 0 ? 1 : 0, a._m21 == 0 ? 1 : 0, a._m22 == 0 ? 1 : 0,
				a._m30 == 0 ? 1 : 0, a._m31 == 0 ? 1 : 0, a._m32 == 0 ? 1 : 0);
		}

		public static int4x3 operator +(int4x3 a, int4x3 b)
		{
			return new(a._m00 + b._m00, a._m01 + b._m01, a._m02 + b._m02, a._m10 + b._m10, a._m11 + b._m11,
				a._m12 + b._m12, a._m20 + b._m20, a._m21 + b._m21, a._m22 + b._m22, a._m30 + b._m30, a._m31 + b._m31,
				a._m32 + b._m32);
		}

		public static int4x3 operator *(int4x3 a, int4x3 b)
		{
			return new(a._m00 * b._m00, a._m01 * b._m01, a._m02 * b._m02, a._m10 * b._m10, a._m11 * b._m11,
				a._m12 * b._m12, a._m20 * b._m20, a._m21 * b._m21, a._m22 * b._m22, a._m30 * b._m30, a._m31 * b._m31,
				a._m32 * b._m32);
		}

		public static int4x3 operator -(int4x3 a, int4x3 b)
		{
			return new(a._m00 - b._m00, a._m01 - b._m01, a._m02 - b._m02, a._m10 - b._m10, a._m11 - b._m11,
				a._m12 - b._m12, a._m20 - b._m20, a._m21 - b._m21, a._m22 - b._m22, a._m30 - b._m30, a._m31 - b._m31,
				a._m32 - b._m32);
		}

		public static int4x3 operator /(int4x3 a, int4x3 b)
		{
			return new(a._m00 / b._m00, a._m01 / b._m01, a._m02 / b._m02, a._m10 / b._m10, a._m11 / b._m11,
				a._m12 / b._m12, a._m20 / b._m20, a._m21 / b._m21, a._m22 / b._m22, a._m30 / b._m30, a._m31 / b._m31,
				a._m32 / b._m32);
		}

		public static int4x3 operator %(int4x3 a, int4x3 b)
		{
			return new(a._m00 % b._m00, a._m01 % b._m01, a._m02 % b._m02, a._m10 % b._m10, a._m11 % b._m11,
				a._m12 % b._m12, a._m20 % b._m20, a._m21 % b._m21, a._m22 % b._m22, a._m30 % b._m30, a._m31 % b._m31,
				a._m32 % b._m32);
		}

		public static int4x3 operator &(int4x3 a, int4x3 b)
		{
			return new(a._m00 & b._m00, a._m01 & b._m01, a._m02 & b._m02, a._m10 & b._m10, a._m11 & b._m11,
				a._m12 & b._m12, a._m20 & b._m20, a._m21 & b._m21, a._m22 & b._m22, a._m30 & b._m30, a._m31 & b._m31,
				a._m32 & b._m32);
		}

		public static int4x3 operator |(int4x3 a, int4x3 b)
		{
			return new(a._m00 | b._m00, a._m01 | b._m01, a._m02 | b._m02, a._m10 | b._m10, a._m11 | b._m11,
				a._m12 | b._m12, a._m20 | b._m20, a._m21 | b._m21, a._m22 | b._m22, a._m30 | b._m30, a._m31 | b._m31,
				a._m32 | b._m32);
		}

		public static int4x3 operator ^(int4x3 a, int4x3 b)
		{
			return new(a._m00 ^ b._m00, a._m01 ^ b._m01, a._m02 ^ b._m02, a._m10 ^ b._m10, a._m11 ^ b._m11,
				a._m12 ^ b._m12, a._m20 ^ b._m20, a._m21 ^ b._m21, a._m22 ^ b._m22, a._m30 ^ b._m30, a._m31 ^ b._m31,
				a._m32 ^ b._m32);
		}

		public static int4x3 operator ==(int4x3 a, int4x3 b)
		{
			return new(a._m00 == b._m00 ? 1 : 0, a._m01 == b._m01 ? 1 : 0, a._m02 == b._m02 ? 1 : 0,
				a._m10 == b._m10 ? 1 : 0, a._m11 == b._m11 ? 1 : 0, a._m12 == b._m12 ? 1 : 0,
				a._m20 == b._m20 ? 1 : 0, a._m21 == b._m21 ? 1 : 0, a._m22 == b._m22 ? 1 : 0,
				a._m30 == b._m30 ? 1 : 0, a._m31 == b._m31 ? 1 : 0, a._m32 == b._m32 ? 1 : 0);
		}

		public static int4x3 operator !=(int4x3 a, int4x3 b)
		{
			return new(a._m00 != b._m00 ? 1 : 0, a._m01 != b._m01 ? 1 : 0, a._m02 != b._m02 ? 1 : 0,
				a._m10 != b._m10 ? 1 : 0, a._m11 != b._m11 ? 1 : 0, a._m12 != b._m12 ? 1 : 0,
				a._m20 != b._m20 ? 1 : 0, a._m21 != b._m21 ? 1 : 0, a._m22 != b._m22 ? 1 : 0,
				a._m30 != b._m30 ? 1 : 0, a._m31 != b._m31 ? 1 : 0, a._m32 != b._m32 ? 1 : 0);
		}

		public static int4x3 operator <(int4x3 a, int4x3 b)
		{
			return new(a._m00 < b._m00 ? 1 : 0, a._m01 < b._m01 ? 1 : 0, a._m02 < b._m02 ? 1 : 0,
				a._m10 < b._m10 ? 1 : 0, a._m11 < b._m11 ? 1 : 0, a._m12 < b._m12 ? 1 : 0,
				a._m20 < b._m20 ? 1 : 0, a._m21 < b._m21 ? 1 : 0, a._m22 < b._m22 ? 1 : 0,
				a._m30 < b._m30 ? 1 : 0, a._m31 < b._m31 ? 1 : 0, a._m32 < b._m32 ? 1 : 0);
		}

		public static int4x3 operator <=(int4x3 a, int4x3 b)
		{
			return new(a._m00 <= b._m00 ? 1 : 0, a._m01 <= b._m01 ? 1 : 0, a._m02 <= b._m02 ? 1 : 0,
				a._m10 <= b._m10 ? 1 : 0, a._m11 <= b._m11 ? 1 : 0, a._m12 <= b._m12 ? 1 : 0,
				a._m20 <= b._m20 ? 1 : 0, a._m21 <= b._m21 ? 1 : 0, a._m22 <= b._m22 ? 1 : 0,
				a._m30 <= b._m30 ? 1 : 0, a._m31 <= b._m31 ? 1 : 0, a._m32 <= b._m32 ? 1 : 0);
		}

		public static int4x3 operator >=(int4x3 a, int4x3 b)
		{
			return new(a._m00 >= b._m00 ? 1 : 0, a._m01 >= b._m01 ? 1 : 0, a._m02 >= b._m02 ? 1 : 0,
				a._m10 >= b._m10 ? 1 : 0, a._m11 >= b._m11 ? 1 : 0, a._m12 >= b._m12 ? 1 : 0,
				a._m20 >= b._m20 ? 1 : 0, a._m21 >= b._m21 ? 1 : 0, a._m22 >= b._m22 ? 1 : 0,
				a._m30 >= b._m30 ? 1 : 0, a._m31 >= b._m31 ? 1 : 0, a._m32 >= b._m32 ? 1 : 0);
		}

		public static int4x3 operator >(int4x3 a, int4x3 b)
		{
			return new(a._m00 > b._m00 ? 1 : 0, a._m01 > b._m01 ? 1 : 0, a._m02 > b._m02 ? 1 : 0,
				a._m10 > b._m10 ? 1 : 0, a._m11 > b._m11 ? 1 : 0, a._m12 > b._m12 ? 1 : 0,
				a._m20 > b._m20 ? 1 : 0, a._m21 > b._m21 ? 1 : 0, a._m22 > b._m22 ? 1 : 0,
				a._m30 > b._m30 ? 1 : 0, a._m31 > b._m31 ? 1 : 0, a._m32 > b._m32 ? 1 : 0);
		}

		public override string ToString()
		{
			return
				$"(({_m00}, {_m01}, {_m02}), ({_m10}, {_m11}, {_m12}), ({_m20}, {_m21}, {_m22}), ({_m30}, {_m31}, {_m32}))";
		}
	}
}
