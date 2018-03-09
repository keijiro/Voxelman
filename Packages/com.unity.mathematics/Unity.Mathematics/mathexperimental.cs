namespace Unity.Mathematics.Experimental
{
    public static partial class math_experimental
    {
        //@TODO hack
        static float4 epsilon4f { get { return new float4(0.00001f); } }
        static float3 epsilon3f { get { return new float3(0.00001f); } }
        static float2 epsilon2f { get { return new float2(0.00001f); } }

        public const float epsilon = 0.00001f;

        //@TODO: Test len > epsilon4f... it is weird. compare to hlsl beahviour...
        // discuss if we want this function at all???
        static public float4 normalizeSafe(float4 v, float4 defaultValue = new float4())
        {
            float len = math.dot(v, v);
            return math.select(defaultValue, v * math.rsqrt(len), len > epsilon4f);
        }

        //@TODO: Merge back into one function with default values... Causes compiler error right now...
        static public float3 normalizeSafe(float3 v, float3 defaultValue)
        {
            float len = math.dot(v, v);
            return math.select(defaultValue, v * math.rsqrt(len), len > epsilon3f);
        }
        static public float3 normalizeSafe(float3 v)
        {
            float len = math.dot(v, v);
            return math.select(new float3(), v * math.rsqrt(len), len > epsilon3f);
        }

        static public float2 normalizeSafe(float2 v, float2 defaultValue = new float2())
        {
            float len = math.dot(v, v);
            return math.select(defaultValue, v * math.rsqrt(len), len > epsilon2f);
        }
    }
}
