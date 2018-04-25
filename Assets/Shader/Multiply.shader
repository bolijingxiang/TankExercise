// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Multiply" {
	Properties 
	{
		_MainTex("MainTex",2D)="white"{}
		_BlendTex("BlendTex",2D)="white"{}
		_opacity("opacity",Range(0,1))=0.5
	}

	SubShader {
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _BlendTex;
			float _opacity;

			struct a2v
			{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
			};

			struct v2f
			{
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			v2f vert(a2v v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.uv=v.uv;
				return o;
			}

			float4 frag(v2f i):SV_Target
			{
				fixed4 m=tex2D(_MainTex,i.uv);
				fixed4 n=tex2D(_BlendTex,i.uv);
				
				fixed4 blendMultiply=m*n;

				float4 texCol=lerp(m,blendMultiply,_opacity);
				return texCol;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
