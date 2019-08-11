Shader "Custom/text shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		
		Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
		GrabPass{}

		Pass{
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 _Color;
				sampler2D _MainTex;
				sampler2D _GrabTexture;

				struct vertInput{
				};
				struct vertOutput{\
					float4 uvGrab : TEXCOORD1;
				};

				vertOutput vert(vertInput i){
					vertOutput o;
					//o.uvGrab = ComputeGrabScreenPos(o.vertex);

					return o;
				}

				half4 frag(vertOutput o) : COLOR{

					fixed4 c = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(o.uvGrab)) ;

					float4 finalColor = c;

					return finalColor * _Color;
				}

			ENDCG
		}


	}
	FallBack "Diffuse"
}
