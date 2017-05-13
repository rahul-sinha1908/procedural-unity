Shader "Custom/TerrainShader" {
	Properties {
		_Color1 ("Color1", Color) = (1,1,0,1)
		_Color2 ("Color2", Color) = (1,0,1,1)
		_TexGrass ("Grass", 2D) = "white"{}
		_TexRoads ("Roads", 2D) = "white"{}
		_TexScale ("Texture Scale", float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


		struct Input {
			// float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal;
		};

		fixed4 _Color1, _Color2;
		sampler2D _TexGrass;
		sampler2D _TexRoads;
		float _TexScale;
		

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			// fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// o.Albedo = c.rgb;
			// // Metallic and smoothness come from slider variables
			// o.Metallic = _Metallic;
			// o.Smoothness = _Glossiness;
			// o.Alpha = c.a;
			// o.Albedo = _Color1;
			if(IN.worldPos.x>10){
				o.Albedo = _Color1;
			}else{
				o.Albedo = _Color2;
			}

			float3 blendAxes = abs(IN.worldNormal);
			float3 scaleWorldPos = IN.worldPos/_TexScale;
			blendAxes /= blendAxes.x+blendAxes.y+blendAxes.z;
			float3 blendX = tex2D(_TexGrass, scaleWorldPos.yz) * blendAxes.x;
			float3 blendY = tex2D(_TexGrass, scaleWorldPos.xz) * blendAxes.y;
			float3 blendZ = tex2D(_TexGrass, scaleWorldPos.xy) * blendAxes.z;
			o.Albedo = blendX+blendY+blendZ;
		}
		ENDCG
	}
	FallBack "Diffuse"
}