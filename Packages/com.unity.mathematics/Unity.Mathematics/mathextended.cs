using System.Runtime.CompilerServices;

namespace Unity.Mathematics
{
    public static partial class math
    {
        public const float epsilon_normal = 1e-30f;


        [MethodImpl((MethodImplOptions)0x100)]
        public static int bool_to_int(bool value) { return value ? 1 : 0; }

        [MethodImpl((MethodImplOptions)0x100)]
        public static int bool_to_mask(bool value) { return value ? -1 : 0; }

        public static int count_bits(int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }

        /// <summary>
        /// Returns the smallest power of two that is greater than or equal to the given integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int ceil_pow2(int i)
        {
            i -= 1;
            i |= i >> 1;
            i |= i >> 2;
            i |= i >> 4;
            i |= i >> 8;
            i |= i >> 16;
            return i + 1;
        }

        // Packs components with an enabled mask (LSB) to the left
        // The value of components after the last packed component are undefined.
        // Returns the number of enabled mask bits. (0 ... 4)
        public static unsafe int compress(int* output, int index, int4 val, bool4 mask)
        {
            if (mask.x)
                output[index++] = val.x;
            if (mask.y)
                output[index++] = val.y;
            if (mask.z)
                output[index++] = val.z;
            if (mask.w)
                output[index++] = val.w;

            return index;
        }

        // radians (convert from degrees to radians)
        [MethodImpl((MethodImplOptions)0x100)]
        public static float radians(float degrees) { return degrees * 0.0174532925f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float2 radians(float2 degrees) { return degrees * 0.0174532925f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float3 radians(float3 degrees) { return degrees * 0.0174532925f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float4 radians(float4 degrees) { return degrees * 0.0174532925f; }

        // radians (convert from radians to degrees)
        [MethodImpl((MethodImplOptions)0x100)]
        public static float degrees(float radians) { return radians * 57.295779513f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float2 degrees(float2 radians) { return radians * 57.295779513f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float3 degrees(float3 radians) { return radians * 57.295779513f; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float4 degrees(float4 radians) { return radians * 57.295779513f; }


        // cmin - returns the smallest component of the vector
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmin(float a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmin(float2 a) { return min(a.x, a.y); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmin(float3 a) { return min(min(a.x, a.y), a.z); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmin(float4 a) { return min(min(min(a.x, a.y), a.z), a.w); }

        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmin(int a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmin(int2 a) { return min(a.x, a.y); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmin(int3 a) { return min(min(a.x, a.y), a.z); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmin(int4 a) { return min(min(min(a.x, a.y), a.z), a.w); }

        // cmax - returns the largest component of the vector
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmax(float a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmax(float2 a) { return max(a.x, a.y); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmax(float3 a) { return max(max(a.x, a.y), a.z); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float cmax(float4 a) { return max(max(max(a.x, a.y), a.z), a.w); }

        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmax(int a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmax(int2 a) { return max(a.x, a.y); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmax(int3 a) { return max(max(a.x, a.y), a.z); }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int cmax(int4 a) { return max(max(max(a.x, a.y), a.z), a.w); }

        // csum - sums all components of the vector
        [MethodImpl((MethodImplOptions)0x100)]
        public static float csum(float a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float csum(float2 a) { return a.x + a.y; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float csum(float3 a) { return a.x + a.y + a.z; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float csum(float4 a) { return a.x + a.y + a.z + a.w; }

        [MethodImpl((MethodImplOptions)0x100)]
        public static int csum(int a) { return a; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int csum(int2 a) { return a.x + a.y; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int csum(int3 a) { return a.x + a.y + a.z; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static int csum(int4 a) { return a.x + a.y + a.z + a.w; }

        // A numeric optimization fence.
        // prevents the compiler from optimizing operators.
        // Some algorithms are written in specific ways to get more precision.
        // For example: https://en.wikipedia.org/wiki/Kahan_summation_algorithm
        // this gives the programmer a tool to prevent specific optimization.
        // example:
        // var c = math.nfence(a + b) * c;
        [MethodImpl((MethodImplOptions)0x100)]
        public static float nfence(float value) { return value; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float2 nfence(float2 value) { return value; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float3 nfence(float3 value) { return value; }
        [MethodImpl((MethodImplOptions)0x100)]
        public static float4 nfence(float4 value) { return value; }

#if false

        //@TODO: Complete all versions of this also, this implementation doesn't actaully do  msb(y) ? -x : x...

        //  changesign: change sign
        //  return value: msb(y) ? -x : x
        public static float4 chgsign(float4 val, float sign) { return new float4(chgsign(val.x, sign), chgsign(val.y, sign), chgsign(val.z, sign), chgsign(val.w, sign)); }
        public static float4 chgsign(float4 val, float4 sign) { return new float4(chgsign(val.x, sign.x), chgsign(val.y, sign.y), chgsign(val.z, sign.z), chgsign(val.w, sign.w)); }
        public static float chgsign(float val, float sign) { return sign >= 0.0F ? val : -val; }

        //  sign: change sign
        //  return value: Returns -1 if x is less than zero; 0 if x equals zero; and 1 if x is greater than zero.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool msb(float val) { return (IntFloatUnion.ToInt(val) & 0x80000000) != 0; }

        //  copysign: copys the sign bit from sign to val
        //  return value: msb(sign) ? abs(val) : -abs(val)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 copysign(float4 val, float sign)  { return sign < 0.0F ? -val : val; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 copysign(float4 val, float4 sign) { return new float4(sign.x < 0.0F ? -val.x : val.x, sign.y < 0.0F ? -val.y : val.y, sign.z < 0.0F ? -val.z : val.z, sign.w < 0.0F ? -val.w : val.w); }

#endif
    }
}
