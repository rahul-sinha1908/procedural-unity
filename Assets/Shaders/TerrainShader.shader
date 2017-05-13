Shader "Custom/TerrainShader" {
	Properties {
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

		sampler2D _TexGrass;
		sampler2D _TexRoads;
		float _TexScale;

		const int maxPixels = 100;
		int actualSize;
		int _init=0;
		float4 roadTex[100];
		float4 temp;
		
		float isNear(float3 pos, float4 r1, float4 r2){
			float dist=(0-pos.x)*(0-pos.x)+(0-pos.z)*(0-pos.z);
			if(dist<10*10)
				return dist/100;
			return -1;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			// fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// o.Albedo = c.rgb;
			// // Metallic and smoothness come from slider variables
			// o.Metallic = _Metallic;
			// o.Smoothness = _Glossiness;
			// o.Alpha = c.a;
			// o.Albedo = _Color1;
			float3 blendAxes = abs(IN.worldNormal);
			float3 scaleWorldPos = IN.worldPos/_TexScale;
			blendAxes /= blendAxes.x+blendAxes.y+blendAxes.z;
			if(_init==0){
				float3 blendX = tex2D(_TexGrass, scaleWorldPos.yz) * blendAxes.x;
				float3 blendY = tex2D(_TexGrass, scaleWorldPos.xz) * blendAxes.y;
				float3 blendZ = tex2D(_TexGrass, scaleWorldPos.xy) * blendAxes.z;
				o.Albedo = blendX+blendY+blendZ;
			}else{
				bool b=false;
				// for(int i=0;i<actualSize;i+=2){
				// 	float myVal=isNear(IN.worldPos, roadTex[i], roadTex[i+1]);
				// 	if(myVal!=-1){
				// 		float3 blendX = tex2D(_TexRoads, scaleWorldPos.yz) * blendAxes.x;
				// 		float3 blendY = tex2D(_TexRoads, scaleWorldPos.xz) * blendAxes.y;
				// 		float3 blendZ = tex2D(_TexRoads, scaleWorldPos.xy) * blendAxes.z;
				// 		o.Albedo = blendX+blendY+blendZ;
				// 		b=true;
				// 		break;
				// 	}
				// }
				float myVal=isNear(IN.worldPos, temp, temp);
				if(myVal!=-1){
					float3 blendX = tex2D(_TexGrass, scaleWorldPos.yz) * blendAxes.x;
					float3 blendY = tex2D(_TexGrass, scaleWorldPos.xz) * blendAxes.y;
					float3 blendZ = tex2D(_TexGrass, scaleWorldPos.xy) * blendAxes.z;
					o.Albedo = blendX+blendY+blendZ;

					blendX = tex2D(_TexRoads, scaleWorldPos.yz) * blendAxes.x;
					blendY = tex2D(_TexRoads, scaleWorldPos.xz) * blendAxes.y;
					blendZ = tex2D(_TexRoads, scaleWorldPos.xy) * blendAxes.z;

					o.Albedo = o.Albedo*myVal + (blendX+blendY+blendZ)*(1-myVal);
					b=true;
				}
				if(!b){
					float3 blendX = tex2D(_TexGrass, scaleWorldPos.yz) * blendAxes.x;
					float3 blendY = tex2D(_TexGrass, scaleWorldPos.xz) * blendAxes.y;
					float3 blendZ = tex2D(_TexGrass, scaleWorldPos.xy) * blendAxes.z;
					o.Albedo = blendX+blendY+blendZ;
				}
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}