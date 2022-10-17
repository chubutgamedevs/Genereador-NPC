// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/RayosX"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Value ("Value", Range(0,1)) = 0.5
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
            
            
            struct inter {
                float2 uv: TEXCOORD0;
                float4 pos: SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Value;

            inter vert (float4 vertex: POSITION, float2 uv: TEXCOORD0)
            {      
                inter o;
                o.uv = uv;
                o.pos = UnityObjectToClipPos(vertex);
                return o;
            }           
        
            float4 frag (inter v): SV_TARGET
            {
                float4 tex = tex2D(_MainTex, v.uv);
                return lerp(tex, float4(0,0,0,tex.a), _Value);
                //return float4(1,1,1,1);
            }

            ENDCG
        }
    }
}
