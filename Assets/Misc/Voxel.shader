Shader "Voxel"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Standard addshadow
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        // Hue value -> RGB color
        half3 Hue2RGB(half h)
        {
            h = frac(h) * 6 - 2;
            half3 rgb = saturate(half3(abs(h - 1) - 1, 2 - abs(h), 2 - abs(h - 2)));
            return rgb;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half d = dot(IN.worldPos, half3(0, 0.3, 1)) + _Time.y / 2;
            half3 c = Hue2RGB(d);
            c = lerp(c, 1, 0.3);
            o.Albedo = GammaToLinearSpace(c);
            o.Metallic = 0;
            o.Smoothness = 0;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
