Shader "Custom/DissolveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _DissolveColor ("Dissolve Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        half _DissolveAmount;
        fixed4 _DissolveColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            clip(c.a - _DissolveAmount);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Emission = lerp(fixed3(0,0,0), _DissolveColor.rgb, step(_DissolveAmount, c.a));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
