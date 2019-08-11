Shader "My Shaders/2D Lights/HidePlatformShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Distance("Distance of light", Range(0, 30)) = 0.5
		_NoFadeDistance("", Range(0, 30)) = 0.5

		_ScrollXSpeed("X scroll speed", float) = 2
		_ScrollYSpeed("Y scroll speed", float) = 2
	}

	
	SubShader {
		Tags { "Queue" = "Transparent+100" "RenderType"="Transparent" }

		GrabPass{}

		Pass{

			Blend SrcAlpha OneMinusSrcAlpha
			//Blend OneMinusSrcAlpha SrcAlpha
			//BlendOp Add
			

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			sampler2D _MainTex;
			sampler2D _GrabTexture;
			float4 _Color;

			float _ScrollXSpeed;
			float _ScrollYSpeed;
			
			struct vertInput{
				float4 vertex : POSITION;

				float3 texcoord : TEXCOORD0;
			};

			struct vertOutput{
				float4 vertex : POSITION;
				float4 uvGrab : TEXCOORD1;

				float4 worldPos : TEXCOORD2;

				float2 texcoord : TEXCOORD0;
				
			};

			vertOutput vert(vertInput i){
				
				vertOutput o;
				o.vertex = UnityObjectToClipPos( i.vertex);

				o.uvGrab = ComputeGrabScreenPos(o.vertex);

				//The texture coordinates for the main texture
				o.texcoord = i.texcoord;

				o.worldPos = mul(unity_ObjectToWorld, i.vertex);

				return o;

			}
			
			half4 frag(vertOutput o) : COLOR{

				half4 c = half4(1, 1, 1, 1);

				fixed xScroll = _ScrollXSpeed * _Time;
				fixed yScroll = _ScrollYSpeed * _Time;

				o.texcoord.xy += float2(xScroll, yScroll);

				//Given a 4-component vector, this returns a Texture coordinate suitable for projected Texture reads. 
				//On most platforms this returns the given value directly.

				half4 grabColor = tex2D(_GrabTexture, UNITY_PROJ_COORD(o.uvGrab));

				half4 texColor = tex2D(_MainTex, o.texcoord);

				//return c * grabColor * _Color + half4(0, 0, 0.5, 0.5);
				//return half4(grabColor.xyz, 1) * half4(0, 0, 0, 0.2);
				

				return  half4(_Color.xyz, grabColor.a) + half4(grabColor.rgb, 0)  * half4(texColor.rgb, 0);
				//return  half4(_Color.xyz * grabColor.rgb * texColor.rgb, grabColor.a*1.5);

			}



			ENDCG
		}

		
	}
	FallBack "Diffuse"
}
