Shader "Custom/SpriteWithMask" {

    Properties {
        _MainTex ("Base", 2D) = "white" {}
        _MaskTex ("Mask", 2D) = "white" {}
    }

    SubShader {

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Cull Off
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Offset -1, -1
        Fog { Mode Off }

        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;

            struct appdata_t {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
                half4 color     : COLOR;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                half4 color : COLOR;
            };

            float4 _MainTex_ST;
            float4 _MaskTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv1 = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.texcoord, _MaskTex);
                o.color = v.color;
                return o;
            }

            half4 frag (v2f i) : COLOR
            {
                half4 base = tex2D(_MainTex, i.uv1);
                half4 mask = tex2D(_MaskTex, i.uv2);
                base.a = mask.r;
                return base * i.color;
            }

            ENDCG
        }
    } 
    FallBack Off
}

