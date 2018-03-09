// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct uint4 : System.IEquatable<uint4>
    {

        // mul
        [MethodImpl(0x100)]
        public static uint4 operator * (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator * (uint4 lhs, uint rhs) { return new uint4 (lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator * (uint lhs, uint4 rhs) { return new uint4 (lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }

        // add
        [MethodImpl(0x100)]
        public static uint4 operator + (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator + (uint4 lhs, uint rhs) { return new uint4 (lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator + (uint lhs, uint4 rhs) { return new uint4 (lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w); }

        // sub
        [MethodImpl(0x100)]
        public static uint4 operator - (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator - (uint4 lhs, uint rhs) { return new uint4 (lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator - (uint lhs, uint4 rhs) { return new uint4 (lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w); }

        // div
        [MethodImpl(0x100)]
        public static uint4 operator / (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator / (uint4 lhs, uint rhs) { return new uint4 (lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator / (uint lhs, uint4 rhs) { return new uint4 (lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool4 operator < (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator < (uint4 lhs, uint rhs) { return new bool4 (lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator < (uint lhs, uint4 rhs) { return new bool4 (lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (uint4 lhs, uint rhs) { return new bool4 (lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (uint lhs, uint4 rhs) { return new bool4 (lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w); }

        // greater 
        [MethodImpl(0x100)]
        public static bool4 operator > (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator > (uint4 lhs, uint rhs) { return new bool4 (lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator > (uint lhs, uint4 rhs) { return new bool4 (lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (uint4 lhs, uint rhs) { return new bool4 (lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (uint lhs, uint4 rhs) { return new bool4 (lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w); }

        // left shift
        [MethodImpl(0x100)]
        public static uint4 operator << (uint4 lhs, int rhs) { return new uint4 (lhs.x << rhs, lhs.y << rhs, lhs.z << rhs, lhs.w << rhs); }

        // right shift
        [MethodImpl(0x100)]
        public static uint4 operator >> (uint4 lhs, int rhs) { return new uint4 (lhs.x >> rhs, lhs.y >> rhs, lhs.z >> rhs, lhs.w >> rhs); }

        // equal 
        [MethodImpl(0x100)]
        public static bool4 operator == (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator == (uint4 lhs, uint rhs) { return new bool4 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator == (uint lhs, uint4 rhs) { return new bool4 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool4 operator != (uint4 lhs, uint4 rhs) { return new bool4 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator != (uint4 lhs, uint rhs) { return new bool4 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator != (uint lhs, uint4 rhs) { return new bool4 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(uint4 rhs)  { return x == rhs.x && y == rhs.y && z == rhs.z && w == rhs.w; }

        // [int index] 
        unsafe public uint this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (uint* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (uint* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static uint4 operator & (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator & (uint4 lhs, uint rhs) { return new uint4 (lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator & (uint lhs, uint4 rhs) { return new uint4 (lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w); }

        // operator |
        [MethodImpl(0x100)]
        public static uint4 operator | (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator | (uint4 lhs, uint rhs) { return new uint4 (lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator | (uint lhs, uint4 rhs) { return new uint4 (lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w); }

        // operator ^
        [MethodImpl(0x100)]
        public static uint4 operator ^ (uint4 lhs, uint4 rhs) { return new uint4 (lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w); }
        [MethodImpl(0x100)]
        public static uint4 operator ^ (uint4 lhs, uint rhs) { return new uint4 (lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs); }
        [MethodImpl(0x100)]
        public static uint4 operator ^ (uint lhs, uint4 rhs) { return new uint4 (lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w); }

        // operator ~ 
        [MethodImpl(0x100)]
        public static uint4 operator ~ (uint4 val) { return new uint4 (~val.x, ~val.y, ~val.z, ~val.w); }        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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
        public uint4 xxxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, x, w); }
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
        public uint4 xxyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xxww
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, x, w, w); }
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
        public uint4 xyxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, x, w); }
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
        public uint4 xyyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xywx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xywy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xywz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xyww
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xzww
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 xwww
        {
            [MethodImpl(0x100)]
            get { return new uint4(x, w, w, w); }
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
        public uint4 yxxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, x, w); }
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
        public uint4 yxyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yxww
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, x, w, w); }
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
        public uint4 yyxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, x, w); }
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
        public uint4 yyyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yywx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yywy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yywz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yyww
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 yzww
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 ywww
        {
            [MethodImpl(0x100)]
            get { return new uint4(y, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zxww
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zywx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zywy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zywz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zyww
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zzww
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 zwww
        {
            [MethodImpl(0x100)]
            get { return new uint4(z, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wxww
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wywx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wywy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wywz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wyww
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wzww
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwxx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwxy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwxz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwxw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwyx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwyy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwyz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwyw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwzx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwzy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwzz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwzw
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwwx
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwwy
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwwz
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint4 wwww
        {
            [MethodImpl(0x100)]
            get { return new uint4(w, w, w, w); }
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
        public uint3 xxz
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xxw
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, x, w); }
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
        public uint3 xyz
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xyw
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xzx
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xzy
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xzz
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xzw
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xwx
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xwy
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xwz
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 xww
        {
            [MethodImpl(0x100)]
            get { return new uint3(x, w, w); }
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
        public uint3 yxz
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yxw
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; }
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
        public uint3 yyz
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yyw
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yzx
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yzy
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yzz
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yzw
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 ywx
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 ywy
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 ywz
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 yww
        {
            [MethodImpl(0x100)]
            get { return new uint3(y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zxx
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zxy
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zxz
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zxw
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zyx
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zyy
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zyz
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zyw
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zzx
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zzy
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zzz
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zzw
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zwx
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zwy
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zwz
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 zww
        {
            [MethodImpl(0x100)]
            get { return new uint3(z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wxx
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wxy
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wxz
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wxw
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wyx
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wyy
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wyz
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wyw
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wzx
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wzy
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wzz
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wzw
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wwx
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wwy
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 wwz
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint3 www
        {
            [MethodImpl(0x100)]
            get { return new uint3(w, w, w); }
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
        public uint2 xz
        {
            [MethodImpl(0x100)]
            get { return new uint2(x, z); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 xw
        {
            [MethodImpl(0x100)]
            get { return new uint2(x, w); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; }
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


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 yz
        {
            [MethodImpl(0x100)]
            get { return new uint2(y, z); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 yw
        {
            [MethodImpl(0x100)]
            get { return new uint2(y, w); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 zx
        {
            [MethodImpl(0x100)]
            get { return new uint2(z, x); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 zy
        {
            [MethodImpl(0x100)]
            get { return new uint2(z, y); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 zz
        {
            [MethodImpl(0x100)]
            get { return new uint2(z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 zw
        {
            [MethodImpl(0x100)]
            get { return new uint2(z, w); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 wx
        {
            [MethodImpl(0x100)]
            get { return new uint2(w, x); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 wy
        {
            [MethodImpl(0x100)]
            get { return new uint2(w, y); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 wz
        {
            [MethodImpl(0x100)]
            get { return new uint2(w, z); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public uint2 ww
        {
            [MethodImpl(0x100)]
            get { return new uint2(w, w); }
        }


    }
}
