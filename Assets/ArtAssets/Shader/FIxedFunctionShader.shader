Shader "Unlit/FIxedFunctionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1, 1, 1, 0)
    }
    SubShader
    {
        Pass
        {
            Lighting On
            Material {
                Diffuse [_Color]
                Ambient [_Color]
            }
            SetTexture [_MainTex] {
                combine previous * texture
            }
        }
    }
}
