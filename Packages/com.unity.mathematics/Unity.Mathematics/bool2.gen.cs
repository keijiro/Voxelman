// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct bool2 : System.IEquatable<bool2>
    {

        // equal 
        [MethodImpl(0x100)]
        public static bool2 operator == (bool2 lhs, bool2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator == (bool2 lhs, bool rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator == (bool lhs, bool2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool2 operator != (bool2 lhs, bool2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator != (bool2 lhs, bool rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator != (bool lhs, bool2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(bool2 rhs)  { return x == rhs.x && y == rhs.y; }

        // [int index] 
        unsafe public bool1 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (bool1* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (bool1* array = &x) { array[index] = value; }
            }
        }

        // operator &
        [MethodImpl(0x100)]
        public static bool2 operator & (bool2 lhs, bool2 rhs) { return new bool2 (lhs.x & rhs.x, lhs.y & rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator & (bool2 lhs, bool rhs) { return new bool2 (lhs.x & rhs, lhs.y & rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator & (bool lhs, bool2 rhs) { return new bool2 (lhs & rhs.x, lhs & rhs.y); }

        // operator |
        [MethodImpl(0x100)]
        public static bool2 operator | (bool2 lhs, bool2 rhs) { return new bool2 (lhs.x | rhs.x, lhs.y | rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator | (bool2 lhs, bool rhs) { return new bool2 (lhs.x | rhs, lhs.y | rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator | (bool lhs, bool2 rhs) { return new bool2 (lhs | rhs.x, lhs | rhs.y); }

        // operator ^
        [MethodImpl(0x100)]
        public static bool2 operator ^ (bool2 lhs, bool2 rhs) { return new bool2 (lhs.x ^ rhs.x, lhs.y ^ rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator ^ (bool2 lhs, bool rhs) { return new bool2 (lhs.x ^ rhs, lhs.y ^ rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator ^ (bool lhs, bool2 rhs) { return new bool2 (lhs ^ rhs.x, lhs ^ rhs.y); }
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


    }
}
