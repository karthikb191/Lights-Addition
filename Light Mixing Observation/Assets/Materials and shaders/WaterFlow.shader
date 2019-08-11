﻿// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "MyShaders/unity cookbook/Chapter2/Waterflow" {
	Properties {
		_MainTex("Main Texture", 2D) = "white" {}
		_MainTint("Tint", Color) = (1, 1, 1, 1)
		_ScrollXSpeed("X scroll speed", float) = 2
		_ScrollYSpeed("Y scroll speed", float) = 2
 	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _MainTint;
		float _ScrollXSpeed;
		float _ScrollYSpeed;

		struct Input{
			fixed2 uv_MainTex;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed2 scrolledUV = IN.uv_MainTex;

			fixed xScroll = _ScrollXSpeed * _Time;
			fixed yScroll = _ScrollYSpeed * _Time;

			scrolledUV += float2(xScroll, yScroll);

			fixed4 c = tex2D(_MainTex, scrolledUV);

			o.Albedo = c.rgb * _MainTint;
			o.Alpha = c.a;
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
