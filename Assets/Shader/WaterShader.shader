Shader "Unlit/WaterShader"
{
    Properties
    {
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
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

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;
            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

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
                o.uv = v.uv0;
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
