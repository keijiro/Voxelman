// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct int2 : System.IEquatable<int2>
    {

        // mul
        [MethodImpl(0x100)]
        public static int2 operator * (int2 lhs, int2 rhs) { return new int2 (lhs.x * rhs.x, lhs.y * rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator * (int2 lhs, int rhs) { return new int2 (lhs.x * rhs, lhs.y * rhs); }
        [MethodImpl(0x100)]
        public static int2 operator * (int lhs, int2 rhs) { return new int2 (lhs * rhs.x, lhs * rhs.y); }

        // add
        [MethodImpl(0x100)]
        public static int2 operator + (int2 lhs, int2 rhs) { return new int2 (lhs.x + rhs.x, lhs.y + rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator + (int2 lhs, int rhs) { return new int2 (lhs.x + rhs, lhs.y + rhs); }
        [MethodImpl(0x100)]
        public static int2 operator + (int lhs, int2 rhs) { return new int2 (lhs + rhs.x, lhs + rhs.y); }

        // sub
        [MethodImpl(0x100)]
        public static int2 operator - (int2 lhs, int2 rhs) { return new int2 (lhs.x - rhs.x, lhs.y - rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator - (int2 lhs, int rhs) { return new int2 (lhs.x - rhs, lhs.y - rhs); }
        [MethodImpl(0x100)]
        public static int2 operator - (int lhs, int2 rhs) { return new int2 (lhs - rhs.x, lhs - rhs.y); }

        // div
        [MethodImpl(0x100)]
        public static int2 operator / (int2 lhs, int2 rhs) { return new int2 (lhs.x / rhs.x, lhs.y / rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator / (int2 lhs, int rhs) { return new int2 (lhs.x / rhs, lhs.y / rhs); }
        [MethodImpl(0x100)]
        public static int2 operator / (int lhs, int2 rhs) { return new int2 (lhs / rhs.x, lhs / rhs.y); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool2 operator < (int2 lhs, int2 rhs) { return new bool2 (lhs.x < rhs.x, lhs.y < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator < (int2 lhs, int rhs) { return new bool2 (lhs.x < rhs, lhs.y < rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator < (int lhs, int2 rhs) { return new bool2 (lhs < rhs.x, lhs < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (int2 lhs, int2 rhs) { return new bool2 (lhs.x <= rhs.x, lhs.y <= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (int2 lhs, int rhs) { return new bool2 (lhs.x <= rhs, lhs.y <= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (int lhs, int2 rhs) { return new bool2 (lhs <= rhs.x, lhs <= rhs.y); }

        // greater 
        [MethodImpl(0x100)]
        public static bool2 operator > (int2 lhs, int2 rhs) { return new bool2 (lhs.x > rhs.x, lhs.y > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator > (int2 lhs, int rhs) { return new bool2 (lhs.x > rhs, lhs.y > rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator > (int lhs, int2 rhs) { return new bool2 (lhs > rhs.x, lhs > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (int2 lhs, int2 rhs) { return new bool2 (lhs.x >= rhs.x, lhs.y >= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (int2 lhs, int rhs) { return new bool2 (lhs.x >= rhs, lhs.y >= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (int lhs, int2 rhs) { return new bool2 (lhs >= rhs.x, lhs >= rhs.y); }

        // neg 
        [MethodImpl(0x100)]
        public static int2 operator - (int2 val) { return new int2 (-val.x, -val.y); }
        // plus 
        [MethodImpl(0x100)]
        public static int2 operator + (int2 val) { return new int2 (+val.x, +val.y); }
        // left shift
        [MethodImpl(0x100)]
        public static int2 operator << (int2 lhs, int rhs) { return new int2 (lhs.x << rhs, lhs.y << rhs); }

        // right shift
        [MethodImpl(0x100)]
        public static int2 operator >> (int2 lhs, int rhs) { return new int2 (lhs.x >> rhs, lhs.y >> rhs); }

        // equal 
        [MethodImpl(0x100)]
        public static bool2 operator == (int2 lhs, int2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator == (int2 lhs, int rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator == (int lhs, int2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool2 operator != (int2 lhs, int2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator != (int2 lhs, int rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator != (int lhs, int2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(int2 rhs)  { return x == rhs.x && y == rhs.y; }

        // [int index] 
        unsafe public int this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (int* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (int* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static int2 operator & (int2 lhs, int2 rhs) { return new int2 (lhs.x & rhs.x, lhs.y & rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator & (int2 lhs, int rhs) { return new int2 (lhs.x & rhs, lhs.y & rhs); }
        [MethodImpl(0x100)]
        public static int2 operator & (int lhs, int2 rhs) { return new int2 (lhs & rhs.x, lhs & rhs.y); }

        // operator |
        [MethodImpl(0x100)]
        public static int2 operator | (int2 lhs, int2 rhs) { return new int2 (lhs.x | rhs.x, lhs.y | rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator | (int2 lhs, int rhs) { return new int2 (lhs.x | rhs, lhs.y | rhs); }
        [MethodImpl(0x100)]
        public static int2 operator | (int lhs, int2 rhs) { return new int2 (lhs | rhs.x, lhs | rhs.y); }

        // operator ^
        [MethodImpl(0x100)]
        public static int2 operator ^ (int2 lhs, int2 rhs) { return new int2 (lhs.x ^ rhs.x, lhs.y ^ rhs.y); }
        [MethodImpl(0x100)]
        public static int2 operator ^ (int2 lhs, int rhs) { return new int2 (lhs.x ^ rhs, lhs.y ^ rhs); }
        [MethodImpl(0x100)]
        public static int2 operator ^ (int lhs, int2 rhs) { return new int2 (lhs ^ rhs.x, lhs ^ rhs.y); }

        // operator ~ 
        [MethodImpl(0x100)]
        public static int2 operator ~ (int2 val) { return new int2 (~val.x, ~val.y); }        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxxx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxxy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxyx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxyy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyxx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyxy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyyx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyyy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxxx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxxy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxyx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxyy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyxx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyxy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyyx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyyy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xxx
        {
            [MethodImpl(0x100)]
            get { return new int3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xxy
        {
            [MethodImpl(0x100)]
            get { return new int3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xyx
        {
            [MethodImpl(0x100)]
            get { return new int3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xyy
        {
            [MethodImpl(0x100)]
            get { return new int3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yxx
        {
            [MethodImpl(0x100)]
            get { return new int3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yxy
        {
            [MethodImpl(0x100)]
            get { return new int3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yyx
        {
            [MethodImpl(0x100)]
            get { return new int3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yyy
        {
            [MethodImpl(0x100)]
            get { return new int3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 xx
        {
            [MethodImpl(0x100)]
            get { return new int2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 xy
        {
            [MethodImpl(0x100)]
            get { return new int2(x, y); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 yx
        {
            [MethodImpl(0x100)]
            get { return new int2(y, x); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 yy
        {
            [MethodImpl(0x100)]
            get { return new int2(y, y); }
        }


    }
}
