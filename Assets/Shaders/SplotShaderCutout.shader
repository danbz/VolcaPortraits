//UNITY_SHADER_NO_UPGRADE

Shader "Custom/Splot Shader Cutout" 
{
	Properties 
	{
		_Tint ("Tint", COLOR) = (1, 1, 1, 0.5)
		_SpriteTex ("Sprite (RGB)", 2D) = "white" {}
		_Size ("Size", Range(0, 3)) = 0.5
		_MainTex ("Main (RGB)", 2D) = "white" {}
		_AlphaCutoff("Alpha Cutoff", Range(0,1)) = 1
	}

	SubShader 
	{
		Pass
		{
			Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "IgnoreProjector"="True" }

			Blend SrcAlpha OneMinusSrcAlpha
			LOD 200
			Cull Off
			ZTest On ZWrite On

			CGPROGRAM
				#pragma target 5.0
				#pragma vertex VS_Main
				#pragma fragment FS_Main
				#pragma geometry GS_Main
				#include "UnityCG.cginc" 

				// **************************************************************
				// Data structures												*
				// **************************************************************
			     struct appdata
				{
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct GS_INPUT
				{
					float4	pos		: POSITION;
					float3 normal	: NORMAL;
					float4 color : COLOR;
				};

				struct FS_INPUT
				{
					float4	pos		: POSITION;
					float2  tex0	: TEXCOORD0;
					float4 color : COLOR;
				};


				// **************************************************************
				// Vars															*
				// **************************************************************

				float4 _Tint;

				float _Size;
				float4x4 _VP;
				sampler2D  _MainTex;
				SamplerState sampler_MainTex;
				float _AlphaCutoff;

				Texture2D _SpriteTex;
				SamplerState sampler_SpriteTex;

				// **************************************************************
				// Shader Programs												*
				// **************************************************************

				// Vertex Shader ------------------------------------------------
				GS_INPUT VS_Main(appdata v)
				{
					GS_INPUT output = (GS_INPUT)0;

					output.pos =  mul(unity_ObjectToWorld, v.vertex );
					output.normal = v.normal;
					
					//comment this line out to get color from vertices
					output.color = tex2Dlod(_MainTex, float4(v.uv, 0, 0)) * _Tint;

					//comment me out to get color from vertices
					//output.color = v.color * _Tint;

					return output;
				}

				// Geometry Shader -----------------------------------------------------
				[maxvertexcount(4)]
				void GS_Main(point GS_INPUT p[1], inout TriangleStream<FS_INPUT> triStream)
				{
					float3 up = float3(0, 1, 0);
					float3 look = _WorldSpaceCameraPos - p[0].pos;
					//look.y = 0;
					look = normalize(look);
					float3 right = cross(up, look);
					
					float halfS = 0.5f * _Size;
							
					

					float4 v[4];
					v[0] = float4(p[0].pos + halfS * right - halfS * up, 1.0f);
					v[1] = float4(p[0].pos + halfS * right + halfS * up, 1.0f);
					v[2] = float4(p[0].pos - halfS * right - halfS * up, 1.0f);
					v[3] = float4(p[0].pos - halfS * right + halfS * up, 1.0f);

					float4x4 vp = mul(UNITY_MATRIX_MVP, unity_WorldToObject);
					FS_INPUT pIn;

					pIn.color = p[0].color;

					pIn.pos = mul(vp, v[0]);
					pIn.tex0 = float2(1.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[1]);
					pIn.tex0 = float2(1.0f, 1.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[2]);
					pIn.tex0 = float2(0.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos =  mul(vp, v[3]);
					pIn.tex0 = float2(0.0f, 1.0f);
					triStream.Append(pIn);
				}
				
				// Fragment Shader -----------------------------------------------
				float4 FS_Main(FS_INPUT input) : COLOR
				{

					float4 col = _SpriteTex.Sample(sampler_SpriteTex, input.tex0);

					clip(col.a - _AlphaCutoff);

					return col * input.color * col.a;
				}

			ENDCG
		}
	} 
}
