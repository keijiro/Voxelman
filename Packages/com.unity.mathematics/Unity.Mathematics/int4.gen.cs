// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct int4 : System.IEquatable<int4>
    {

        // mul
        [MethodImpl(0x100)]
        public static int4 operator * (int4 lhs, int4 rhs) { return new int4 (lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator * (int4 lhs, int rhs) { return new int4 (lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
        [MethodImpl(0x100)]
        public static int4 operator * (int lhs, int4 rhs) { return new int4 (lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }

        // add
        [MethodImpl(0x100)]
        public static int4 operator + (int4 lhs, int4 rhs) { return new int4 (lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator + (int4 lhs, int rhs) { return new int4 (lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs); }
        [MethodImpl(0x100)]
        public static int4 operator + (int lhs, int4 rhs) { return new int4 (lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w); }

        // sub
        [MethodImpl(0x100)]
        public static int4 operator - (int4 lhs, int4 rhs) { return new int4 (lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator - (int4 lhs, int rhs) { return new int4 (lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs); }
        [MethodImpl(0x100)]
        public static int4 operator - (int lhs, int4 rhs) { return new int4 (lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w); }

        // div
        [MethodImpl(0x100)]
        public static int4 operator / (int4 lhs, int4 rhs) { return new int4 (lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator / (int4 lhs, int rhs) { return new int4 (lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }
        [MethodImpl(0x100)]
        public static int4 operator / (int lhs, int4 rhs) { return new int4 (lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool4 operator < (int4 lhs, int4 rhs) { return new bool4 (lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator < (int4 lhs, int rhs) { return new bool4 (lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator < (int lhs, int4 rhs) { return new bool4 (lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (int4 lhs, int4 rhs) { return new bool4 (lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (int4 lhs, int rhs) { return new bool4 (lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (int lhs, int4 rhs) { return new bool4 (lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w); }

        // greater 
        [MethodImpl(0x100)]
        public static bool4 operator > (int4 lhs, int4 rhs) { return new bool4 (lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator > (int4 lhs, int rhs) { return new bool4 (lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator > (int lhs, int4 rhs) { return new bool4 (lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (int4 lhs, int4 rhs) { return new bool4 (lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (int4 lhs, int rhs) { return new bool4 (lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (int lhs, int4 rhs) { return new bool4 (lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w); }

        // neg 
        [MethodImpl(0x100)]
        public static int4 operator - (int4 val) { return new int4 (-val.x, -val.y, -val.z, -val.w); }
        // plus 
        [MethodImpl(0x100)]
        public static int4 operator + (int4 val) { return new int4 (+val.x, +val.y, +val.z, +val.w); }
        // left shift
        [MethodImpl(0x100)]
        public static int4 operator << (int4 lhs, int rhs) { return new int4 (lhs.x << rhs, lhs.y << rhs, lhs.z << rhs, lhs.w << rhs); }

        // right shift
        [MethodImpl(0x100)]
        public static int4 operator >> (int4 lhs, int rhs) { return new int4 (lhs.x >> rhs, lhs.y >> rhs, lhs.z >> rhs, lhs.w >> rhs); }

        // equal 
        [MethodImpl(0x100)]
        public static bool4 operator == (int4 lhs, int4 rhs) { return new bool4 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator == (int4 lhs, int rhs) { return new bool4 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator == (int lhs, int4 rhs) { return new bool4 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool4 operator != (int4 lhs, int4 rhs) { return new bool4 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator != (int4 lhs, int rhs) { return new bool4 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator != (int lhs, int4 rhs) { return new bool4 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(int4 rhs)  { return x == rhs.x && y == rhs.y && z == rhs.z && w == rhs.w; }

        // [int index] 
        unsafe public int this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (int* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (int* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static int4 operator & (int4 lhs, int4 rhs) { return new int4 (lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator & (int4 lhs, int rhs) { return new int4 (lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs); }
        [MethodImpl(0x100)]
        public static int4 operator & (int lhs, int4 rhs) { return new int4 (lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w); }

        // operator |
        [MethodImpl(0x100)]
        public static int4 operator | (int4 lhs, int4 rhs) { return new int4 (lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator | (int4 lhs, int rhs) { return new int4 (lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs); }
        [MethodImpl(0x100)]
        public static int4 operator | (int lhs, int4 rhs) { return new int4 (lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w); }

        // operator ^
        [MethodImpl(0x100)]
        public static int4 operator ^ (int4 lhs, int4 rhs) { return new int4 (lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w); }
        [MethodImpl(0x100)]
        public static int4 operator ^ (int4 lhs, int rhs) { return new int4 (lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs); }
        [MethodImpl(0x100)]
        public static int4 operator ^ (int lhs, int4 rhs) { return new int4 (lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w); }

        // operator ~ 
        [MethodImpl(0x100)]
        public static int4 operator ~ (int4 val) { return new int4 (~val.x, ~val.y, ~val.z, ~val.w); }        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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
        public int4 xxxw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, x, w); }
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
        public int4 xxyw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, y, w); }
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
        public int4 xxzw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxwx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxwy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxwz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xxww
        {
            [MethodImpl(0x100)]
            get { return new int4(x, x, w, w); }
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
        public int4 xyxw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, x, w); }
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
        public int4 xyyw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, y, w); }
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
        public int4 xyzw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xywx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xywy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xywz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xyww
        {
            [MethodImpl(0x100)]
            get { return new int4(x, y, w, w); }
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
        public int4 xzxw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, x, w); }
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
        public int4 xzyw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; w = value.w; }
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
        public int4 xzzw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzwx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzwy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzwz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xzww
        {
            [MethodImpl(0x100)]
            get { return new int4(x, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwxx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwxy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwxz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwxw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwyx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwyy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwyz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwyw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwzx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwzy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwzz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwzw
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwwx
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwwy
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwwz
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 xwww
        {
            [MethodImpl(0x100)]
            get { return new int4(x, w, w, w); }
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
        public int4 yxxw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, x, w); }
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
        public int4 yxyw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, y, w); }
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
        public int4 yxzw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxwx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxwy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxwz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yxww
        {
            [MethodImpl(0x100)]
            get { return new int4(y, x, w, w); }
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
        public int4 yyxw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, x, w); }
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
        public int4 yyyw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, y, w); }
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
        public int4 yyzw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yywx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yywy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yywz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yyww
        {
            [MethodImpl(0x100)]
            get { return new int4(y, y, w, w); }
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
        public int4 yzxw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; w = value.w; }
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
        public int4 yzyw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, y, w); }
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
        public int4 yzzw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzwx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzwy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzwz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 yzww
        {
            [MethodImpl(0x100)]
            get { return new int4(y, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywxx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywxy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywxz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywxw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywyx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywyy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywyz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywyw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywzx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywzy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywzz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywzw
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywwx
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywwy
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywwz
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 ywww
        {
            [MethodImpl(0x100)]
            get { return new int4(y, w, w, w); }
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
        public int4 zxxw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, x, w); }
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
        public int4 zxyw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; w = value.w; }
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
        public int4 zxzw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxwx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxwy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxwz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zxww
        {
            [MethodImpl(0x100)]
            get { return new int4(z, x, w, w); }
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
        public int4 zyxw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; w = value.w; }
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
        public int4 zyyw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, y, w); }
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
        public int4 zyzw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zywx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zywy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zywz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zyww
        {
            [MethodImpl(0x100)]
            get { return new int4(z, y, w, w); }
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
        public int4 zzxw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, x, w); }
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
        public int4 zzyw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, y, w); }
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
        public int4 zzzw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzwx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzwy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzwz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zzww
        {
            [MethodImpl(0x100)]
            get { return new int4(z, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwxx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwxy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwxz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwxw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwyx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwyy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwyz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwyw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwzx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwzy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwzz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwzw
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwwx
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwwy
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwwz
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 zwww
        {
            [MethodImpl(0x100)]
            get { return new int4(z, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxxx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxxy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxxz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxxw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxyx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxyy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxyz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxyw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxzx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxzy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxzz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxzw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxwx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxwy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxwz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wxww
        {
            [MethodImpl(0x100)]
            get { return new int4(w, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyxx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyxy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyxz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyxw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyyx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyyy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyyz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyyw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyzx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyzy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyzz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyzw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wywx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wywy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wywz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wyww
        {
            [MethodImpl(0x100)]
            get { return new int4(w, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzxx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzxy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzxz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzxw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzyx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzyy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzyz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzyw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzzx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzzy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzzz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzzw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzwx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzwy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzwz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wzww
        {
            [MethodImpl(0x100)]
            get { return new int4(w, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwxx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwxy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwxz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwxw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwyx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwyy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwyz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwyw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwzx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwzy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwzz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwzw
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwwx
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwwy
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwwz
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int4 wwww
        {
            [MethodImpl(0x100)]
            get { return new int4(w, w, w, w); }
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
        public int3 xxw
        {
            [MethodImpl(0x100)]
            get { return new int3(x, x, w); }
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
        public int3 xyw
        {
            [MethodImpl(0x100)]
            get { return new int3(x, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; }
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
        public int3 xzw
        {
            [MethodImpl(0x100)]
            get { return new int3(x, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xwx
        {
            [MethodImpl(0x100)]
            get { return new int3(x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xwy
        {
            [MethodImpl(0x100)]
            get { return new int3(x, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xwz
        {
            [MethodImpl(0x100)]
            get { return new int3(x, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 xww
        {
            [MethodImpl(0x100)]
            get { return new int3(x, w, w); }
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
        public int3 yxw
        {
            [MethodImpl(0x100)]
            get { return new int3(y, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; }
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
        public int3 yyw
        {
            [MethodImpl(0x100)]
            get { return new int3(y, y, w); }
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
        public int3 yzw
        {
            [MethodImpl(0x100)]
            get { return new int3(y, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 ywx
        {
            [MethodImpl(0x100)]
            get { return new int3(y, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 ywy
        {
            [MethodImpl(0x100)]
            get { return new int3(y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 ywz
        {
            [MethodImpl(0x100)]
            get { return new int3(y, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 yww
        {
            [MethodImpl(0x100)]
            get { return new int3(y, w, w); }
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
        public int3 zxw
        {
            [MethodImpl(0x100)]
            get { return new int3(z, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; }
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
        public int3 zyw
        {
            [MethodImpl(0x100)]
            get { return new int3(z, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; }
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
        public int3 zzw
        {
            [MethodImpl(0x100)]
            get { return new int3(z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zwx
        {
            [MethodImpl(0x100)]
            get { return new int3(z, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zwy
        {
            [MethodImpl(0x100)]
            get { return new int3(z, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zwz
        {
            [MethodImpl(0x100)]
            get { return new int3(z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 zww
        {
            [MethodImpl(0x100)]
            get { return new int3(z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wxx
        {
            [MethodImpl(0x100)]
            get { return new int3(w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wxy
        {
            [MethodImpl(0x100)]
            get { return new int3(w, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wxz
        {
            [MethodImpl(0x100)]
            get { return new int3(w, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wxw
        {
            [MethodImpl(0x100)]
            get { return new int3(w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wyx
        {
            [MethodImpl(0x100)]
            get { return new int3(w, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wyy
        {
            [MethodImpl(0x100)]
            get { return new int3(w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wyz
        {
            [MethodImpl(0x100)]
            get { return new int3(w, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wyw
        {
            [MethodImpl(0x100)]
            get { return new int3(w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wzx
        {
            [MethodImpl(0x100)]
            get { return new int3(w, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wzy
        {
            [MethodImpl(0x100)]
            get { return new int3(w, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wzz
        {
            [MethodImpl(0x100)]
            get { return new int3(w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wzw
        {
            [MethodImpl(0x100)]
            get { return new int3(w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wwx
        {
            [MethodImpl(0x100)]
            get { return new int3(w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wwy
        {
            [MethodImpl(0x100)]
            get { return new int3(w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 wwz
        {
            [MethodImpl(0x100)]
            get { return new int3(w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int3 www
        {
            [MethodImpl(0x100)]
            get { return new int3(w, w, w); }
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
        public int2 xw
        {
            [MethodImpl(0x100)]
            get { return new int2(x, w); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; }
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
        public int2 yw
        {
            [MethodImpl(0x100)]
            get { return new int2(y, w); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; }
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


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 zw
        {
            [MethodImpl(0x100)]
            get { return new int2(z, w); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 wx
        {
            [MethodImpl(0x100)]
            get { return new int2(w, x); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 wy
        {
            [MethodImpl(0x100)]
            get { return new int2(w, y); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 wz
        {
            [MethodImpl(0x100)]
            get { return new int2(w, z); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int2 ww
        {
            [MethodImpl(0x100)]
            get { return new int2(w, w); }
        }


    }
}
