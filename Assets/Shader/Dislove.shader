// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2018/Dislove"
{
	Properties
	{
		_MainTex("MainTex",2D) = "white"{}
		_NoiseTex("NoiseTex",2D) = "white"{}
		_Mask("Mask",2D) = "white"{}
		_ColorA("ColorA",Color) = (1,1,1,1)
		_ColorB("ColorB",Color) = (1,1,1,1)
		_FactorA("FactorA",Range(0,1)) = 0
		_FactorB("FactorB",Range(0,1)) = 0
		_ClipFactor("ClipFactor",Range(0,1)) = 0.5
	}
		SubShader{
			//Tags { "RenderType" = "Opaque" }
			//LOD 200
			Pass
			{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			sampler2D _Mask;
			float4 _ColorA;
			float4 _ColorB;
			float _FactorA;
			float _FactorB;
			float _ClipFactor;

			float4 _MainTex_ST;
			float4 _NoiseTex_ST;

			struct a2v
			{
				float4 vertex:POSITION;
				float2 uv : TEXCOORD0;
				float3 normal:NORMAL;
			};

			struct v2f
			{
				float4 pos:SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 proj:TEXCOORD1;
				float3 normal:TEXCOORD2;
				float4 worldPos:TEXCOORD3;
			};

			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.proj = ComputeGrabScreenPos(o.pos);
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				return o;
			}

			float4 frag(v2f i) :SV_Target
			{
				//float4 texCol = tex2D(_MainTex,i.worldPos.rg);
				float4 col = tex2D(_MainTex,i.uv);

				float4 maskCol = (tex2Dproj(_Mask,i.proj)+1)/2;

				float gradient = 0;

				//texCol = tex2D(_MainTex, i.worldPos.rg);

				gradient = tex2D(_NoiseTex, i.worldPos).r;

				clip(gradient - _ClipFactor);

				//if (abs(i.normal.z)>0)
				//{

				//}
				//if (abs( i.normal.x )>0 )
				//{
				//	gradient = tex2D(_Mask, i.proj.gb).r;
				//	gradient *= tex2Dproj(_Mask, i.proj).r;
				//	clip(gradient - _ClipFactor);
				//}
				//if (abs( i.normal.y )>0 )
				//{
				//	gradient = tex2D(_NoiseTex, i.worldPos.rb).r;
				//	gradient *= tex2Dproj(_Mask, i.proj).r;
				//	clip(gradient - _ClipFactor);
				//}

				//float tempFactor = _ClipFactor / gradient;

				//if (tempFactor>_FactorA)
				//{
				//	if (tempFactor>_FactorB)
				//	{
				//		return _ColorB;
				//	}
				//	return _ColorA;
				//}

				return col*maskCol;
			}
				ENDCG
			}
	}
	FallBack "Diffuse"
}
