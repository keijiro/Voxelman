// GENERATED CODE
using System.Runtime.CompilerServices;
#pragma warning disable 0660, 0661
namespace Unity.Mathematics
{
    public partial struct float2 : System.IEquatable<float2>
    {

        // mul
        [MethodImpl(0x100)]
        public static float2 operator * (float2 lhs, float2 rhs) { return new float2 (lhs.x * rhs.x, lhs.y * rhs.y); }
        [MethodImpl(0x100)]
        public static float2 operator * (float2 lhs, float rhs) { return new float2 (lhs.x * rhs, lhs.y * rhs); }
        [MethodImpl(0x100)]
        public static float2 operator * (float lhs, float2 rhs) { return new float2 (lhs * rhs.x, lhs * rhs.y); }

        // add
        [MethodImpl(0x100)]
        public static float2 operator + (float2 lhs, float2 rhs) { return new float2 (lhs.x + rhs.x, lhs.y + rhs.y); }
        [MethodImpl(0x100)]
        public static float2 operator + (float2 lhs, float rhs) { return new float2 (lhs.x + rhs, lhs.y + rhs); }
        [MethodImpl(0x100)]
        public static float2 operator + (float lhs, float2 rhs) { return new float2 (lhs + rhs.x, lhs + rhs.y); }

        // sub
        [MethodImpl(0x100)]
        public static float2 operator - (float2 lhs, float2 rhs) { return new float2 (lhs.x - rhs.x, lhs.y - rhs.y); }
        [MethodImpl(0x100)]
        public static float2 operator - (float2 lhs, float rhs) { return new float2 (lhs.x - rhs, lhs.y - rhs); }
        [MethodImpl(0x100)]
        public static float2 operator - (float lhs, float2 rhs) { return new float2 (lhs - rhs.x, lhs - rhs.y); }

        // div
        [MethodImpl(0x100)]
        public static float2 operator / (float2 lhs, float2 rhs) { return new float2 (lhs.x / rhs.x, lhs.y / rhs.y); }
        [MethodImpl(0x100)]
        public static float2 operator / (float2 lhs, float rhs) { return new float2 (lhs.x / rhs, lhs.y / rhs); }
        [MethodImpl(0x100)]
        public static float2 operator / (float lhs, float2 rhs) { return new float2 (lhs / rhs.x, lhs / rhs.y); }

        // smaller 
        [MethodImpl(0x100)]
        public static bool2 operator < (float2 lhs, float2 rhs) { return new bool2 (lhs.x < rhs.x, lhs.y < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator < (float2 lhs, float rhs) { return new bool2 (lhs.x < rhs, lhs.y < rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator < (float lhs, float2 rhs) { return new bool2 (lhs < rhs.x, lhs < rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (float2 lhs, float2 rhs) { return new bool2 (lhs.x <= rhs.x, lhs.y <= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (float2 lhs, float rhs) { return new bool2 (lhs.x <= rhs, lhs.y <= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator <= (float lhs, float2 rhs) { return new bool2 (lhs <= rhs.x, lhs <= rhs.y); }

        // greater 
        [MethodImpl(0x100)]
        public static bool2 operator > (float2 lhs, float2 rhs) { return new bool2 (lhs.x > rhs.x, lhs.y > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator > (float2 lhs, float rhs) { return new bool2 (lhs.x > rhs, lhs.y > rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator > (float lhs, float2 rhs) { return new bool2 (lhs > rhs.x, lhs > rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (float2 lhs, float2 rhs) { return new bool2 (lhs.x >= rhs.x, lhs.y >= rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (float2 lhs, float rhs) { return new bool2 (lhs.x >= rhs, lhs.y >= rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator >= (float lhs, float2 rhs) { return new bool2 (lhs >= rhs.x, lhs >= rhs.y); }

        // neg 
        [MethodImpl(0x100)]
        public static float2 operator - (float2 val) { return new float2 (-val.x, -val.y); }
        // plus 
        [MethodImpl(0x100)]
        public static float2 operator + (float2 val) { return new float2 (+val.x, +val.y); }
        // equal 
        [MethodImpl(0x100)]
        public static bool2 operator == (float2 lhs, float2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator == (float2 lhs, float rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator == (float lhs, float2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        // not equal 
        [MethodImpl(0x100)]
        public static bool2 operator != (float2 lhs, float2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }
        [MethodImpl(0x100)]
        public static bool2 operator != (float2 lhs, float rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }
        [MethodImpl(0x100)]
        public static bool2 operator != (float lhs, float2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        // Equals 
        [MethodImpl(0x100)]
        public bool Equals(float2 rhs)  { return x == rhs.x && y == rhs.y; }

        // [int index] 
        unsafe public float this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (float* array = &x) { return array[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
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


    }
}
