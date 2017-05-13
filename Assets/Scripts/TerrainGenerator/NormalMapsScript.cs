using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NormalMapsScript {
	public static float[,] GenerateNoiseMap(int width, int height, float scale){
		float[,] normalMap=new float[width, height];
		if(scale<=0)
			scale=0.001f;
		

		for(int y=0; y<height; y++){
			for(int x=0; x<width; x++){
				float sampleX=x/scale;
				float sampleY=y/scale;

				float perlin = Mathf.PerlinNoise(sampleX, sampleY);
				normalMap[x,y]=perlin;
			}
		}
		return normalMap;
	}
}
