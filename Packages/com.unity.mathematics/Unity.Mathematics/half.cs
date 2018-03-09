namespace Unity.Mathematics
{
    public struct half
    {
        private short value;

        public static float MaxValue { get { return 65536.0F; } }
        public static float MinValue { get { return -65536.0F; } }

        public static implicit operator half(float v)
        {
            unchecked
            {
                v = math.clamp(v, -65536.0f, 65536.0f) * 1.925930e-34f;
                int i = IntFloatUnion.ToInt(v);
                uint ui = (uint)i;
                int h = ((i >> 16) & (int)0xffff8000) | ((int)(ui >> 13));
                half value;
                value.value = (short)h;
                return value;
            }
        }

        public static implicit operator float(half d)
        {
            int iv = d.value;
            int i = (iv & 0x47fff) << 13;
            return IntFloatUnion.ToFloat(i) * 5.192297e+33f;
        }
    }

    public struct half2
    {
        public half x;
        public half y;

        public half2(half x, half y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator half2(float2 d) { return new half2(d.x, d.y); }
        public static implicit operator float2(half2 d) { return new float2(d.x, d.y); }
    }

    public struct half3
    {
        public half x;
        public half y;
        public half z;

        public half3(half x, half y, half z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator half3(float3 d) { return new half3(d.x, d.y, d.z); }
        public static implicit operator float3(half3 d) { return new float3(d.x, d.y, d.z); }
    }

    public struct half4
    {
        public half x;
        public half y;
        public half z;
        public half w;

        public half4(half x, half y, half z, half w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static implicit operator half4(float4 d) { return new half4(d.x, d.y, d.z, d.w); }
        public static implicit operator float4(half4 d) { return new float4(d.x, d.y, d.z, d.w); }

    }
}
