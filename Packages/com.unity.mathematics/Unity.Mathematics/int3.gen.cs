// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct int3 : System.IEquatable<int3>
    {

        // mul
        [MethodImpl(0x100)]
        public static int3 operator * (int3 lhs, int3 rhs) { return new int3 (lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator * (int3 lhs, int rhs) { return new int3 (lhs.x * rhs, lhs.y * rhs, lhs.z * rhs); }
        [MethodImpl(0x100)]
        public static int3 operator * (int lhs, int3 rhs) { return new int3 (lhs * rhs.x, lhs * rhs.y, lhs * rhs.z); }

        // add
        [MethodImpl(0x100)]
        public static int3 operator + (int3 lhs, int3 rhs) { return new int3 (lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator + (int3 lhs, int rhs) { return new int3 (lhs.x + rhs, lhs.y + rhs, lhs.z + rhs); }
        [MethodImpl(0x100)]
        public static int3 operator + (int lhs, int3 rhs) { return new int3 (lhs + rhs.x, lhs + rhs.y, lhs + rhs.z); }

        // sub
        [MethodImpl(0x100)]
        public static int3 operator - (int3 lhs, int3 rhs) { return new int3 (lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator - (int3 lhs, int rhs) { return new int3 (lhs.x - rhs, lhs.y - rhs, lhs.z - rhs); }
        [MethodImpl(0x100)]
        public static int3 operator - (int lhs, int3 rhs) { return new int3 (lhs - rhs.x, lhs - rhs.y, lhs - rhs.z); }

        // div
        [MethodImpl(0x100)]
        public static int3 operator / (int3 lhs, int3 rhs) { return new int3 (lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator / (int3 lhs, int rhs) { return new int3 (lhs.x / rhs, lhs.y / rhs, lhs.z / rhs); }
        [MethodImpl(0x100)]
        public static int3 operator / (int lhs, int3 rhs) { return new int3 (lhs / rhs.x, lhs / rhs.y, lhs / rhs.z); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool3 operator < (int3 lhs, int3 rhs) { return new bool3 (lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator < (int3 lhs, int rhs) { return new bool3 (lhs.x < rhs, lhs.y < rhs, lhs.z < rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator < (int lhs, int3 rhs) { return new bool3 (lhs < rhs.x, lhs < rhs.y, lhs < rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator <= (int3 lhs, int3 rhs) { return new bool3 (lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator <= (int3 lhs, int rhs) { return new bool3 (lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator <= (int lhs, int3 rhs) { return new bool3 (lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z); }

        // greater 
        [MethodImpl(0x100)]
        public static bool3 operator > (int3 lhs, int3 rhs) { return new bool3 (lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator > (int3 lhs, int rhs) { return new bool3 (lhs.x > rhs, lhs.y > rhs, lhs.z > rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator > (int lhs, int3 rhs) { return new bool3 (lhs > rhs.x, lhs > rhs.y, lhs > rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator >= (int3 lhs, int3 rhs) { return new bool3 (lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator >= (int3 lhs, int rhs) { return new bool3 (lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator >= (int lhs, int3 rhs) { return new bool3 (lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z); }

        // neg 
        [MethodImpl(0x100)]
        public static int3 operator - (int3 val) { return new int3 (-val.x, -val.y, -val.z); }
        // plus 
        [MethodImpl(0x100)]
        public static int3 operator + (int3 val) { return new int3 (+val.x, +val.y, +val.z); }
        // left shift
        [MethodImpl(0x100)]
        public static int3 operator << (int3 lhs, int rhs) { return new int3 (lhs.x << rhs, lhs.y << rhs, lhs.z << rhs); }

        // right shift
        [MethodImpl(0x100)]
        public static int3 operator >> (int3 lhs, int rhs) { return new int3 (lhs.x >> rhs, lhs.y >> rhs, lhs.z >> rhs); }

        // equal 
        [MethodImpl(0x100)]
        public static bool3 operator == (int3 lhs, int3 rhs) { return new bool3 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator == (int3 lhs, int rhs) { return new bool3 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator == (int lhs, int3 rhs) { return new bool3 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool3 operator != (int3 lhs, int3 rhs) { return new bool3 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator != (int3 lhs, int rhs) { return new bool3 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator != (int lhs, int3 rhs) { return new bool3 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(int3 rhs)  { return x == rhs.x && y == rhs.y && z == rhs.z; }

        // [int index] 
        unsafe public int this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (int* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (int* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static int3 operator & (int3 lhs, int3 rhs) { return new int3 (lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator & (int3 lhs, int rhs) { return new int3 (lhs.x & rhs, lhs.y & rhs, lhs.z & rhs); }
        [MethodImpl(0x100)]
        public static int3 operator & (int lhs, int3 rhs) { return new int3 (lhs & rhs.x, lhs & rhs.y, lhs & rhs.z); }

        // operator |
        [MethodImpl(0x100)]
        public static int3 operator | (int3 lhs, int3 rhs) { return new int3 (lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator | (int3 lhs, int rhs) { return new int3 (lhs.x | rhs, lhs.y | rhs, lhs.z | rhs); }
        [MethodImpl(0x100)]
        public static int3 operator | (int lhs, int3 rhs) { return new int3 (lhs | rhs.x, lhs | rhs.y, lhs | rhs.z); }

        // operator ^
        [MethodImpl(0x100)]
        public static int3 operator ^ (int3 lhs, int3 rhs) { return new int3 (lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z); }
        [MethodImpl(0x100)]
        public static int3 operator ^ (int3 lhs, int rhs) { return new int3 (lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs); }
        [MethodImpl(0x100)]
        public static int3 operator ^ (int lhs, int3 rhs) { return new int3 (lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z); }

        // operator ~ 
        [MethodImpl(0x100)]
        public static int3 operator ~ (int3 val) { return new int3 (~val.x, ~val.y, ~val.z); }        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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
        public int4 xxxz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, x, z); }
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
        public int4 xxyz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxzx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxzy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxzz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, z, z); }
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
        public int4 xyxz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, x, z); }
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
        public int4 xyyz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyzx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyzy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyzz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzxx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzxy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzxz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzyx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzyy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzyz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzzx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzzy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzzz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, z, z); }
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
        public int4 yxxz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, x, z); }
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
        public int4 yxyz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxzx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxzy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxzz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, z, z); }
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
        public int4 yyxz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, x, z); }
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
        public int4 yyyz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyzx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyzy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyzz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzxx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzxy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzxz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzyx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzyy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzyz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzzx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzzy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzzz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxxx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxxy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxxz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxyx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxyy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxyz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxzx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxzy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxzz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyxx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyxy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyxz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyyx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyyy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyyz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyzx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyzy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyzz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzxx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzxy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzxz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzyx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzyy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzyz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzzx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzzy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzzz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, z, z); }
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
        public int3 xxz
        {
            [MethodImpl(0x100)]
            get { return new int3(x, x, z); }
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
        public int3 xyz
        {
            [MethodImpl(0x100)]
            get { return new int3(x, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xzx
        {
            [MethodImpl(0x100)]
            get { return new int3(x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xzy
        {
            [MethodImpl(0x100)]
            get { return new int3(x, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xzz
        {
            [MethodImpl(0x100)]
            get { return new int3(x, z, z); }
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
        public int3 yxz
        {
            [MethodImpl(0x100)]
            get { return new int3(y, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; }
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
        public int3 yyz
        {
            [MethodImpl(0x100)]
            get { return new int3(y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yzx
        {
            [MethodImpl(0x100)]
            get { return new int3(y, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yzy
        {
            [MethodImpl(0x100)]
            get { return new int3(y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yzz
        {
            [MethodImpl(0x100)]
            get { return new int3(y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zxx
        {
            [MethodImpl(0x100)]
            get { return new int3(z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zxy
        {
            [MethodImpl(0x100)]
            get { return new int3(z, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zxz
        {
            [MethodImpl(0x100)]
            get { return new int3(z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zyx
        {
            [MethodImpl(0x100)]
            get { return new int3(z, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zyy
        {
            [MethodImpl(0x100)]
            get { return new int3(z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zyz
        {
            [MethodImpl(0x100)]
            get { return new int3(z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zzx
        {
            [MethodImpl(0x100)]
            get { return new int3(z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zzy
        {
            [MethodImpl(0x100)]
            get { return new int3(z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zzz
        {
            [MethodImpl(0x100)]
            get { return new int3(z, z, z); }
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
        public int2 xz
        {
            [MethodImpl(0x100)]
            get { return new int2(x, z); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; }
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


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 yz
        {
            [MethodImpl(0x100)]
            get { return new int2(y, z); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 zx
        {
            [MethodImpl(0x100)]
            get { return new int2(z, x); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 zy
        {
            [MethodImpl(0x100)]
            get { return new int2(z, y); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 zz
        {
            [MethodImpl(0x100)]
            get { return new int2(z, z); }
        }


    }
}
