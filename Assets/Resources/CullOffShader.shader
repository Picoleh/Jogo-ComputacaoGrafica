Shader "Unlit/CullOffShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Lighting Off
        Cull Front     // <--- Desenha o lado de dentro
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            SetTexture [_MainTex]
            {
                constantColor [_Color]
                combine texture * constant
            }
        }
    }
}
