Shader "My Shaders/2D Lights/Light" {
	Properties{
		_Color("Light Color", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}

		_LightPosition("Light Position", Vector) = (1, 1, 1, 1)	//This will be set in the c# script
		_LightDirection("Light Direction", Vector) = (0 , 0, 0, 0) //This will also be set inside the script

		_RightVector("Right Vector", Vector) = (0, 0, 0, 0)
		_LeftVector("Left Vector", Vector) = (0, 0, 0, 0)

		_RightFogVector("", Vector) = (0, 0, 0, 0)
		_LeftFogVector("", Vector) = (0, 0, 0, 0)

		_Distance("Distance of light", Range(0, 30)) = 0.5
		_NoFadeDistance("", Range(0, 30)) = 0.5
	}

	SubShader{
		ZWrite Off

		Blend DstColor Zero
		//Blend SrcAlpha OneMinusSrcAlpha
		//Blend One One
		//Blend DstColor Zero
		//Blend DstColor SrcColor

		//BlendOp Add

		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}

		LOD 200

		CGPROGRAM

		#pragma surface surf Lambert alpha

		float4 _Color;
		sampler2D _MainTex;

		float4 _LightPosition;
		float4 _LightDirection;

		float4 _RightVector;
		float4 _LeftVector;

		float4 _RightFogVector;
		float4 _LeftFogVector;

		float _Distance;
		float _NoFadeDistance;

		struct Input{
			float2 uv_MainTex;

			float3 worldPos;	//This provides us with the world position of each pixel
		};

		void surf(Input IN, inout SurfaceOutput o){
			
			//get the distance from the point of the light to the pixel
			float reductionAlpha = smoothstep(_Distance, _NoFadeDistance, distance(_LightPosition, IN.worldPos));

			float4 n = tex2D(_MainTex, IN.uv_MainTex);

			//Get the direction of the pixel WRT the light position
			float3 pixelRelToLight = IN.worldPos - _LightPosition.xyz;

			float d1 = dot(_RightVector, normalize(pixelRelToLight));
			float d2 = dot(_LeftVector, normalize(pixelRelToLight));

			float d3 = dot(_RightFogVector, normalize(pixelRelToLight));
			float d4 = dot(_LeftFogVector, normalize(pixelRelToLight));

			
			if(d1 > 0 && d2 > 0){
				o.Albedo =  _Color;
				o.Alpha = reductionAlpha;
			}
			else{
				o.Alpha = 0;
			}

			if(d3 > 0 && d1 < 0 ){
				float sm = smoothstep(abs(dot(_RightVector, _RightFogVector)), 0,
										 abs(dot(normalize(pixelRelToLight) , _RightFogVector)));
				o.Albedo =  _Color;
				o.Alpha = (1 - sm) * reductionAlpha;
			}

			if(d4 > 0 && d2 < 0){
				float sm = smoothstep(abs(dot(_LeftVector, _LeftFogVector)), 0,
										 abs(dot(normalize(pixelRelToLight) , _LeftFogVector)));
				o.Albedo = _Color;
				o.Alpha = (1-sm) * reductionAlpha;
			}
		}

		ENDCG

		

	}
}
