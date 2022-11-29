Shader "Unlit/Squash"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Velocidad ("Velocidad", float) = 25.0
        _Intensidad ("Intensidad", Range(0,1)) = 0.2
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
            float _Velocidad;
            float _Intensidad;

            inter vert (input v)
            {
                inter o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                float fy = lerp(1.0, 1.0 + _Intensidad, (sin(_Time * _Velocidad) + 1.0) / 2.0);                
                o.uv.y = o.uv.y * fy;

                float fx = lerp(1.0 + _Intensidad, 1.0, (sin(_Time * _Velocidad) + 1.0) / 2.0);                
                o.uv.x = ((o.uv.x - 0.5) * fx) + 0.5;                

                return o;
            }

            fixed4 frag (inter i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
