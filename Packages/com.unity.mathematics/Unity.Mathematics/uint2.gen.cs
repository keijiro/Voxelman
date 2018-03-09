// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct uint2 : System.IEquatable<uint2>
    {

        // mul
        [MethodImpl(0x100)]
        public static uint2 operator * (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x * rhs.x, lhs.y * rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator * (uint2 lhs, uint rhs) { return new uint2 (lhs.x * rhs, lhs.y * rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator * (uint lhs, uint2 rhs) { return new uint2 (lhs * rhs.x, lhs * rhs.y); }

        // add
        [MethodImpl(0x100)]
        public static uint2 operator + (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x + rhs.x, lhs.y + rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator + (uint2 lhs, uint rhs) { return new uint2 (lhs.x + rhs, lhs.y + rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator + (uint lhs, uint2 rhs) { return new uint2 (lhs + rhs.x, lhs + rhs.y); }

        // sub
        [MethodImpl(0x100)]
        public static uint2 operator - (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x - rhs.x, lhs.y - rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator - (uint2 lhs, uint rhs) { return new uint2 (lhs.x - rhs, lhs.y - rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator - (uint lhs, uint2 rhs) { return new uint2 (lhs - rhs.x, lhs - rhs.y); }

        // div
        [MethodImpl(0x100)]
        public static uint2 operator / (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x / rhs.x, lhs.y / rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator / (uint2 lhs, uint rhs) { return new uint2 (lhs.x / rhs, lhs.y / rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator / (uint lhs, uint2 rhs) { return new uint2 (lhs / rhs.x, lhs / rhs.y); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool2 operator < (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x < rhs.x, lhs.y < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator < (uint2 lhs, uint rhs) { return new bool2 (lhs.x < rhs, lhs.y < rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator < (uint lhs, uint2 rhs) { return new bool2 (lhs < rhs.x, lhs < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x <= rhs.x, lhs.y <= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (uint2 lhs, uint rhs) { return new bool2 (lhs.x <= rhs, lhs.y <= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (uint lhs, uint2 rhs) { return new bool2 (lhs <= rhs.x, lhs <= rhs.y); }

        // greater 
        [MethodImpl(0x100)]
        public static bool2 operator > (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x > rhs.x, lhs.y > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator > (uint2 lhs, uint rhs) { return new bool2 (lhs.x > rhs, lhs.y > rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator > (uint lhs, uint2 rhs) { return new bool2 (lhs > rhs.x, lhs > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x >= rhs.x, lhs.y >= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (uint2 lhs, uint rhs) { return new bool2 (lhs.x >= rhs, lhs.y >= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (uint lhs, uint2 rhs) { return new bool2 (lhs >= rhs.x, lhs >= rhs.y); }

        // left shift
        [MethodImpl(0x100)]
        public static uint2 operator << (uint2 lhs, int rhs) { return new uint2 (lhs.x << rhs, lhs.y << rhs); }

        // right shift
        [MethodImpl(0x100)]
        public static uint2 operator >> (uint2 lhs, int rhs) { return new uint2 (lhs.x >> rhs, lhs.y >> rhs); }

        // equal 
        [MethodImpl(0x100)]
        public static bool2 operator == (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator == (uint2 lhs, uint rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator == (uint lhs, uint2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool2 operator != (uint2 lhs, uint2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator != (uint2 lhs, uint rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator != (uint lhs, uint2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(uint2 rhs)  { return x == rhs.x && y == rhs.y; }

        // [int index] 
        unsafe public uint this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (uint* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (uint* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static uint2 operator & (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x & rhs.x, lhs.y & rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator & (uint2 lhs, uint rhs) { return new uint2 (lhs.x & rhs, lhs.y & rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator & (uint lhs, uint2 rhs) { return new uint2 (lhs & rhs.x, lhs & rhs.y); }

        // operator |
        [MethodImpl(0x100)]
        public static uint2 operator | (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x | rhs.x, lhs.y | rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator | (uint2 lhs, uint rhs) { return new uint2 (lhs.x | rhs, lhs.y | rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator | (uint lhs, uint2 rhs) { return new uint2 (lhs | rhs.x, lhs | rhs.y); }

        // operator ^
        [MethodImpl(0x100)]
        public static uint2 operator ^ (uint2 lhs, uint2 rhs) { return new uint2 (lhs.x ^ rhs.x, lhs.y ^ rhs.y); }
        [MethodImpl(0x100)]
        public static uint2 operator ^ (uint2 lhs, uint rhs) { return new uint2 (lhs.x ^ rhs, lhs.y ^ rhs); }
        [MethodImpl(0x100)]
        public static uint2 operator ^ (uint lhs, uint2 rhs) { return new uint2 (lhs ^ rhs.x, lhs ^ rhs.y); }

        // operator ~ 
        [MethodImpl(0x100)]
        public static uint2 operator ~ (uint2 val) { return new uint2 (~val.x, ~val.y); }        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xxx
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xxy
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xyx
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xyy
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yxx
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yxy
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yyx
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yyy
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 xx
        {
            [MethodImpl(0x100)]
            get { return new uint2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 xy
        {
            [MethodImpl(0x100)]
            get { return new uint2(x, y); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 yx
        {
            [MethodImpl(0x100)]
            get { return new uint2(y, x); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 yy
        {
            [MethodImpl(0x100)]
            get { return new uint2(y, y); }
        }


    }
}
