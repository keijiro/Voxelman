// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct float4 : System.IEquatable<float4>
    {

        // mul
        [MethodImpl(0x100)]
        public static float4 operator * (float4 lhs, float4 rhs) { return new float4 (lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w); }
        [MethodImpl(0x100)]
        public static float4 operator * (float4 lhs, float rhs) { return new float4 (lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
        [MethodImpl(0x100)]
        public static float4 operator * (float lhs, float4 rhs) { return new float4 (lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }

        // add
        [MethodImpl(0x100)]
        public static float4 operator + (float4 lhs, float4 rhs) { return new float4 (lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }
        [MethodImpl(0x100)]
        public static float4 operator + (float4 lhs, float rhs) { return new float4 (lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs); }
        [MethodImpl(0x100)]
        public static float4 operator + (float lhs, float4 rhs) { return new float4 (lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w); }

        // sub
        [MethodImpl(0x100)]
        public static float4 operator - (float4 lhs, float4 rhs) { return new float4 (lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }
        [MethodImpl(0x100)]
        public static float4 operator - (float4 lhs, float rhs) { return new float4 (lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs); }
        [MethodImpl(0x100)]
        public static float4 operator - (float lhs, float4 rhs) { return new float4 (lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w); }

        // div
        [MethodImpl(0x100)]
        public static float4 operator / (float4 lhs, float4 rhs) { return new float4 (lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w); }
        [MethodImpl(0x100)]
        public static float4 operator / (float4 lhs, float rhs) { return new float4 (lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }
        [MethodImpl(0x100)]
        public static float4 operator / (float lhs, float4 rhs) { return new float4 (lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool4 operator < (float4 lhs, float4 rhs) { return new bool4 (lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator < (float4 lhs, float rhs) { return new bool4 (lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator < (float lhs, float4 rhs) { return new bool4 (lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (float4 lhs, float4 rhs) { return new bool4 (lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (float4 lhs, float rhs) { return new bool4 (lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator <= (float lhs, float4 rhs) { return new bool4 (lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w); }

        // greater 
        [MethodImpl(0x100)]
        public static bool4 operator > (float4 lhs, float4 rhs) { return new bool4 (lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator > (float4 lhs, float rhs) { return new bool4 (lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator > (float lhs, float4 rhs) { return new bool4 (lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (float4 lhs, float4 rhs) { return new bool4 (lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (float4 lhs, float rhs) { return new bool4 (lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator >= (float lhs, float4 rhs) { return new bool4 (lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w); }

        // neg 
        [MethodImpl(0x100)]
        public static float4 operator - (float4 val) { return new float4 (-val.x, -val.y, -val.z, -val.w); }
        // plus 
        [MethodImpl(0x100)]
        public static float4 operator + (float4 val) { return new float4 (+val.x, +val.y, +val.z, +val.w); }
        // equal 
        [MethodImpl(0x100)]
        public static bool4 operator == (float4 lhs, float4 rhs) { return new bool4 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator == (float4 lhs, float rhs) { return new bool4 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator == (float lhs, float4 rhs) { return new bool4 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool4 operator != (float4 lhs, float4 rhs) { return new bool4 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w); }
        [MethodImpl(0x100)]
        public static bool4 operator != (float4 lhs, float rhs) { return new bool4 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs); }
        [MethodImpl(0x100)]
        public static bool4 operator != (float lhs, float4 rhs) { return new bool4 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(float4 rhs)  { return x == rhs.x && y == rhs.y && z == rhs.z && w == rhs.w; }

        // [int index] 
        unsafe public float this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (float* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (float* array = &x) { array[index] = value; }
            }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxxx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxxy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxxz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxxw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxyx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxyy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxyz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxyw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxzx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxzy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxzz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxzw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxwx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxwy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxwz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xxww
        {
            [MethodImpl(0x100)]
            get { return new float4(x, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyxx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyxy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyxz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyxw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyyx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyyy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyyz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyyw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyzx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyzy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyzz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyzw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xywx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xywy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xywz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xyww
        {
            [MethodImpl(0x100)]
            get { return new float4(x, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzxx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzxy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzxz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzxw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzyx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzyy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzyz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzyw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzzx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzzy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzzz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzzw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzwx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzwy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzwz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xzww
        {
            [MethodImpl(0x100)]
            get { return new float4(x, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwxx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwxy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwxz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwxw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwyx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwyy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwyz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwyw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwzx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwzy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwzz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwzw
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwwx
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwwy
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwwz
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 xwww
        {
            [MethodImpl(0x100)]
            get { return new float4(x, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxxx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxxy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxxz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxxw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxyx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxyy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxyz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxyw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxzx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxzy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxzz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxzw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxwx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxwy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxwz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yxww
        {
            [MethodImpl(0x100)]
            get { return new float4(y, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyxx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyxy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyxz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyxw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyyx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyyy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyyz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyyw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyzx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyzy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyzz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyzw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yywx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yywy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yywz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yyww
        {
            [MethodImpl(0x100)]
            get { return new float4(y, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzxx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzxy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzxz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzxw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzyx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzyy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzyz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzyw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzzx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzzy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzzz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzzw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzwx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzwy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzwz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 yzww
        {
            [MethodImpl(0x100)]
            get { return new float4(y, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywxx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywxy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywxz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywxw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywyx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywyy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywyz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywyw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywzx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywzy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywzz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywzw
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywwx
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywwy
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywwz
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 ywww
        {
            [MethodImpl(0x100)]
            get { return new float4(y, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxxx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxxy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxxz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxxw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxyx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxyy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxyz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxyw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxzx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxzy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxzz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxzw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxwx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxwy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxwz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zxww
        {
            [MethodImpl(0x100)]
            get { return new float4(z, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyxx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyxy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyxz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyxw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; w = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyyx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyyy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyyz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyyw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyzx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyzy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyzz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyzw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zywx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zywy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zywz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zyww
        {
            [MethodImpl(0x100)]
            get { return new float4(z, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzxx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzxy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzxz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzxw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzyx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzyy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzyz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzyw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzzx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzzy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzzz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzzw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzwx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzwy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzwz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zzww
        {
            [MethodImpl(0x100)]
            get { return new float4(z, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwxx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwxy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwxz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwxw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwyx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwyy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwyz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwyw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwzx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwzy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwzz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwzw
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwwx
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwwy
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwwz
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 zwww
        {
            [MethodImpl(0x100)]
            get { return new float4(z, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxxx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxxy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxxz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxxw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxyx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxyy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxyz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxyw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxzx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxzy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxzz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxzw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxwx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxwy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxwz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wxww
        {
            [MethodImpl(0x100)]
            get { return new float4(w, x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyxx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyxy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyxz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; z = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyxw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyyx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyyy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyyz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyyw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyzx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyzy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyzz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyzw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wywx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wywy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wywz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wyww
        {
            [MethodImpl(0x100)]
            get { return new float4(w, y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzxx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzxy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; y = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzxz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzxw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzyx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; x = value.w; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzyy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzyz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzyw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzzx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzzy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzzz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzzw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzwx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzwy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzwz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wzww
        {
            [MethodImpl(0x100)]
            get { return new float4(w, z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwxx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwxy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwxz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwxw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwyx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwyy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwyz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwyw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwzx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwzy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwzz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwzw
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwwx
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwwy
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwwz
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float4 wwww
        {
            [MethodImpl(0x100)]
            get { return new float4(w, w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xxx
        {
            [MethodImpl(0x100)]
            get { return new float3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xxy
        {
            [MethodImpl(0x100)]
            get { return new float3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xxz
        {
            [MethodImpl(0x100)]
            get { return new float3(x, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xxw
        {
            [MethodImpl(0x100)]
            get { return new float3(x, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xyx
        {
            [MethodImpl(0x100)]
            get { return new float3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xyy
        {
            [MethodImpl(0x100)]
            get { return new float3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xyz
        {
            [MethodImpl(0x100)]
            get { return new float3(x, y, z); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xyw
        {
            [MethodImpl(0x100)]
            get { return new float3(x, y, w); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xzx
        {
            [MethodImpl(0x100)]
            get { return new float3(x, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xzy
        {
            [MethodImpl(0x100)]
            get { return new float3(x, z, y); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xzz
        {
            [MethodImpl(0x100)]
            get { return new float3(x, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xzw
        {
            [MethodImpl(0x100)]
            get { return new float3(x, z, w); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xwx
        {
            [MethodImpl(0x100)]
            get { return new float3(x, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xwy
        {
            [MethodImpl(0x100)]
            get { return new float3(x, w, y); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xwz
        {
            [MethodImpl(0x100)]
            get { return new float3(x, w, z); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 xww
        {
            [MethodImpl(0x100)]
            get { return new float3(x, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yxx
        {
            [MethodImpl(0x100)]
            get { return new float3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yxy
        {
            [MethodImpl(0x100)]
            get { return new float3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yxz
        {
            [MethodImpl(0x100)]
            get { return new float3(y, x, z); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yxw
        {
            [MethodImpl(0x100)]
            get { return new float3(y, x, w); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yyx
        {
            [MethodImpl(0x100)]
            get { return new float3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yyy
        {
            [MethodImpl(0x100)]
            get { return new float3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yyz
        {
            [MethodImpl(0x100)]
            get { return new float3(y, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yyw
        {
            [MethodImpl(0x100)]
            get { return new float3(y, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yzx
        {
            [MethodImpl(0x100)]
            get { return new float3(y, z, x); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yzy
        {
            [MethodImpl(0x100)]
            get { return new float3(y, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yzz
        {
            [MethodImpl(0x100)]
            get { return new float3(y, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yzw
        {
            [MethodImpl(0x100)]
            get { return new float3(y, z, w); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 ywx
        {
            [MethodImpl(0x100)]
            get { return new float3(y, w, x); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 ywy
        {
            [MethodImpl(0x100)]
            get { return new float3(y, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 ywz
        {
            [MethodImpl(0x100)]
            get { return new float3(y, w, z); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 yww
        {
            [MethodImpl(0x100)]
            get { return new float3(y, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zxx
        {
            [MethodImpl(0x100)]
            get { return new float3(z, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zxy
        {
            [MethodImpl(0x100)]
            get { return new float3(z, x, y); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zxz
        {
            [MethodImpl(0x100)]
            get { return new float3(z, x, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zxw
        {
            [MethodImpl(0x100)]
            get { return new float3(z, x, w); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zyx
        {
            [MethodImpl(0x100)]
            get { return new float3(z, y, x); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zyy
        {
            [MethodImpl(0x100)]
            get { return new float3(z, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zyz
        {
            [MethodImpl(0x100)]
            get { return new float3(z, y, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zyw
        {
            [MethodImpl(0x100)]
            get { return new float3(z, y, w); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; w = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zzx
        {
            [MethodImpl(0x100)]
            get { return new float3(z, z, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zzy
        {
            [MethodImpl(0x100)]
            get { return new float3(z, z, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zzz
        {
            [MethodImpl(0x100)]
            get { return new float3(z, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zzw
        {
            [MethodImpl(0x100)]
            get { return new float3(z, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zwx
        {
            [MethodImpl(0x100)]
            get { return new float3(z, w, x); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zwy
        {
            [MethodImpl(0x100)]
            get { return new float3(z, w, y); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zwz
        {
            [MethodImpl(0x100)]
            get { return new float3(z, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 zww
        {
            [MethodImpl(0x100)]
            get { return new float3(z, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wxx
        {
            [MethodImpl(0x100)]
            get { return new float3(w, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wxy
        {
            [MethodImpl(0x100)]
            get { return new float3(w, x, y); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wxz
        {
            [MethodImpl(0x100)]
            get { return new float3(w, x, z); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wxw
        {
            [MethodImpl(0x100)]
            get { return new float3(w, x, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wyx
        {
            [MethodImpl(0x100)]
            get { return new float3(w, y, x); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wyy
        {
            [MethodImpl(0x100)]
            get { return new float3(w, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wyz
        {
            [MethodImpl(0x100)]
            get { return new float3(w, y, z); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; z = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wyw
        {
            [MethodImpl(0x100)]
            get { return new float3(w, y, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wzx
        {
            [MethodImpl(0x100)]
            get { return new float3(w, z, x); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; x = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wzy
        {
            [MethodImpl(0x100)]
            get { return new float3(w, z, y); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; y = value.z; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wzz
        {
            [MethodImpl(0x100)]
            get { return new float3(w, z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wzw
        {
            [MethodImpl(0x100)]
            get { return new float3(w, z, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wwx
        {
            [MethodImpl(0x100)]
            get { return new float3(w, w, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wwy
        {
            [MethodImpl(0x100)]
            get { return new float3(w, w, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 wwz
        {
            [MethodImpl(0x100)]
            get { return new float3(w, w, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3 www
        {
            [MethodImpl(0x100)]
            get { return new float3(w, w, w); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xx
        {
            [MethodImpl(0x100)]
            get { return new float2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xy
        {
            [MethodImpl(0x100)]
            get { return new float2(x, y); }
            [MethodImpl(0x100)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xz
        {
            [MethodImpl(0x100)]
            get { return new float2(x, z); }
            [MethodImpl(0x100)]
            set { x = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xw
        {
            [MethodImpl(0x100)]
            get { return new float2(x, w); }
            [MethodImpl(0x100)]
            set { x = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yx
        {
            [MethodImpl(0x100)]
            get { return new float2(y, x); }
            [MethodImpl(0x100)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yy
        {
            [MethodImpl(0x100)]
            get { return new float2(y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yz
        {
            [MethodImpl(0x100)]
            get { return new float2(y, z); }
            [MethodImpl(0x100)]
            set { y = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yw
        {
            [MethodImpl(0x100)]
            get { return new float2(y, w); }
            [MethodImpl(0x100)]
            set { y = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zx
        {
            [MethodImpl(0x100)]
            get { return new float2(z, x); }
            [MethodImpl(0x100)]
            set { z = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zy
        {
            [MethodImpl(0x100)]
            get { return new float2(z, y); }
            [MethodImpl(0x100)]
            set { z = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zz
        {
            [MethodImpl(0x100)]
            get { return new float2(z, z); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zw
        {
            [MethodImpl(0x100)]
            get { return new float2(z, w); }
            [MethodImpl(0x100)]
            set { z = value.x; w = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 wx
        {
            [MethodImpl(0x100)]
            get { return new float2(w, x); }
            [MethodImpl(0x100)]
            set { w = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 wy
        {
            [MethodImpl(0x100)]
            get { return new float2(w, y); }
            [MethodImpl(0x100)]
            set { w = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 wz
        {
            [MethodImpl(0x100)]
            get { return new float2(w, z); }
            [MethodImpl(0x100)]
            set { w = value.x; z = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 ww
        {
            [MethodImpl(0x100)]
            get { return new float2(w, w); }
        }


    }
}
