Shader "Unlit/GradientMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (0,0,0)
        _ColorB ("Color B", Color) = (1,1,1)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct input
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct inter
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ColorA;
            float4 _ColorB;

            inter vert (input v)
            {
                inter o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (inter i) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, i.uv);                
                float p = (col.r + col.g + col.b) / 3.0;
                return fixed4(lerp(_ColorA, _ColorB, p).rgb, col.a);
            }

            ENDCG
        }
    }
}
