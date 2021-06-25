Shader "Unlit/Shader1"
{
    Properties
    {
        _Value ("Value", Float) = 1.0
        _ColorA ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (1,1,1,1)
        _Scale ("UV Scale", Float) = 1
        _Offset ("UV Offset", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            float _Value;
            float4 _ColorA;
            float4 _ColorB;
            float _Scale;
            float _Offset;

            struct MeshData //Per vertex mesh data
            {
                float4 vertex : POSITION; // vertext position 
                float3 normals: NORMAL; // normal data from vertex
                float2 uv0 : TEXCOORD0; // UV coordinates                
            };

            struct Interpolaters // the datastructure send to the fragment shader
            {
                float4 vertex : SV_POSITION; //clip space position.
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
                //float2 uv : TEXCOORD0; // any data passed to the fragment shader.        
            };

            
            Interpolaters vert (MeshData v)
            {
                Interpolaters o;
                o.vertex = UnityObjectToClipPos(v.vertex); //converts local space to clip space.
                o.normal = UnityObjectToWorldNormal(v.normals); // passing data trough.
                o.uv = (v.uv0 + _Offset) * _Scale;
                return o;
            }

            float4 frag (Interpolaters i) : SV_Target
            {
                // lerp
                // Blend between two colors based on x coordinate;
                //float4 outputColor = lerp(_ColorA, _ColorB, i.uv.x);
                //return float4(1,1,0,0);
                return float4(i.uv.x, i.uv.y, 0, 0); // output normal + 1 as last compontent.
            }
            ENDCG
        }
    }
}
