// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct bool3 : System.IEquatable<bool3>
    {

        // equal 
        [MethodImpl(0x100)]
        public static bool3 operator == (bool3 lhs, bool3 rhs) { return new bool3 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator == (bool3 lhs, bool rhs) { return new bool3 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator == (bool lhs, bool3 rhs) { return new bool3 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool3 operator != (bool3 lhs, bool3 rhs) { return new bool3 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator != (bool3 lhs, bool rhs) { return new bool3 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator != (bool lhs, bool3 rhs) { return new bool3 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(bool3 rhs)  { return x == rhs.x && y == rhs.y && z == rhs.z; }

        // [int index] 
        unsafe public bool1 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (bool1* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (bool1* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static bool3 operator & (bool3 lhs, bool3 rhs) { return new bool3 (lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator & (bool3 lhs, bool rhs) { return new bool3 (lhs.x & rhs, lhs.y & rhs, lhs.z & rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator & (bool lhs, bool3 rhs) { return new bool3 (lhs & rhs.x, lhs & rhs.y, lhs & rhs.z); }

        // operator |
        [MethodImpl(0x100)]
        public static bool3 operator | (bool3 lhs, bool3 rhs) { return new bool3 (lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator | (bool3 lhs, bool rhs) { return new bool3 (lhs.x | rhs, lhs.y | rhs, lhs.z | rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator | (bool lhs, bool3 rhs) { return new bool3 (lhs | rhs.x, lhs | rhs.y, lhs | rhs.z); }

        // operator ^
        [MethodImpl(0x100)]
        public static bool3 operator ^ (bool3 lhs, bool3 rhs) { return new bool3 (lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z); }
        [MethodImpl(0x100)]
        public static bool3 operator ^ (bool3 lhs, bool rhs) { return new bool3 (lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs); }
        [MethodImpl(0x100)]
        public static bool3 operator ^ (bool lhs, bool3 rhs) { return new bool3 (lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z); }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xxzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xyzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 xzzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(x, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yxzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yyzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 yzzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(y, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zxzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zyzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzxx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzxy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzxz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzyx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzyy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzyz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzzx
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzzy
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool4 zzzz
        {
            [MethodImpl(0x100)]
            get { return new bool4(z, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xxx
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xxy
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xxz
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xyx
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xyy
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xyz
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xzx
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xzy
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 xzz
        {
            [MethodImpl(0x100)]
            get { return new bool3(x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yxx
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yxy
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yxz
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yyx
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yyy
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yyz
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yzx
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yzy
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 yzz
        {
            [MethodImpl(0x100)]
            get { return new bool3(y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zxx
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zxy
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zxz
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zyx
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zyy
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zyz
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zzx
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zzy
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool3 zzz
        {
            [MethodImpl(0x100)]
            get { return new bool3(z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 xx
        {
            [MethodImpl(0x100)]
            get { return new bool2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 xy
        {
            [MethodImpl(0x100)]
            get { return new bool2(x, y); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 xz
        {
            [MethodImpl(0x100)]
            get { return new bool2(x, z); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 yx
        {
            [MethodImpl(0x100)]
            get { return new bool2(y, x); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 yy
        {
            [MethodImpl(0x100)]
            get { return new bool2(y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 yz
        {
            [MethodImpl(0x100)]
            get { return new bool2(y, z); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 zx
        {
            [MethodImpl(0x100)]
            get { return new bool2(z, x); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 zy
        {
            [MethodImpl(0x100)]
            get { return new bool2(z, y); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool2 zz
        {
            [MethodImpl(0x100)]
            get { return new bool2(z, z); }
        }


    }
}
