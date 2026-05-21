Shader "Custom/UI_GreenScreen_Mask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Green Threshold", Range(0,1)) = 0.4
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref 1
            Comp Equal
            Pass Keep
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Threshold;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // —Î”²‚«”»’è
                if (col.g > _Threshold && col.g > col.r * 1.2 && col.g > col.b * 1.2)
                {
                    col.a = 0;
                }

                return col;
            }
            ENDCG
        }
    }
}