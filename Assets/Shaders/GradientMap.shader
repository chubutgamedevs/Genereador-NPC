Shader "Unlit/GradientMap"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        _Value ("Value", Range(0.0, 1.0)) = 1.0     
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
                float4 color : COLOR;
            };

            struct inter
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color: COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Value;

            inter vert (input v)
            {
                inter o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (inter i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv); 
                float p = dot(float3(0.2126,0.7152,0.0722), col.rgb); // Luma formula

                float q = dot(float3(0.2126,0.7152,0.0722), i.color.rgb) + 0.5;

                float3 a = lerp(float3(0.0,0.0,0.0), i.color.rgb, p);
                float3 b = lerp(i.color.rgb, float3(1.0,1.0,1.0), p);

                return fixed4(lerp(float3(0.0,0.0,0.0),lerp(a,b,p),step(i.uv.y,_Value)), col.a);
            }

            ENDCG
        }
    }
}
