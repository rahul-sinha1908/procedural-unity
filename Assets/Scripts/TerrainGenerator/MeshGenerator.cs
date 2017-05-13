using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTerrainSpace{
	public static class MeshGenerator {

		public static MyMeshData generateMesh(float[,] normalMaps, float cenX, float cenY, float heightMax, int lod){

			//TODO Do the LOD component
			
			int width=normalMaps.GetLength(0);
			int height=normalMaps.GetLength(1);

			float offsetX=cenX-(width-1)/2f;
			float offsetY=cenY-(height-1)/2f;

			MyMeshData meshData=new MyMeshData(width, height);
			for(int y=0;y<height;y++){
				for(int x=0;x<width;x++){
					int i=y*width+x;
					meshData.vertices[i]=new Vector3(offsetX+x, normalMaps[x,y]*heightMax, offsetY+y);
				}
			}
			return meshData;
		}
	}

	public class MyMeshData{
		public Vector3[] vertices;
		public int[] triangles;
		public Vector2[] uv;
		private Mesh mesh;

		private int triangleInd;
		public MyMeshData(int width, int height){
			initMeshData(width, height);
		}
		public MyMeshData(int sides){
			initMeshData(sides, sides);
		}
		private void initMeshData(int width, int height){
			vertices=new Vector3[width*height];
			uv=new Vector2[width*height];
			triangles=new int[(width-1)*(height-1)*6];

			triangleInd=0;
			for(int y=0;y<height-1;y++){
				for(int x=0;x<width-1;x++){
					int i=y*width+x;
					AddTriangle(i, i+width, i+width+1);
					AddTriangle(i+width+1, i+1, i);
				}
			}
			for(int y=0;y<height;y++){
				for(int x=0;x<width;x++){
					int i=y*width+x;
					uv[i]=new Vector2(x*1f/width, y*1f/height);
				}
			}

		}
		private void AddTriangle(int a, int b, int c){
			triangles[triangleInd]=a;
			triangles[triangleInd+1]=b;
			triangles[triangleInd+2]=c;

			triangleInd+=3;
		}

		public Mesh createMesh(){
			mesh=new Mesh();
			mesh.vertices=vertices;
			mesh.triangles=triangles;
			mesh.uv=uv;

			mesh.RecalculateNormals();

			return mesh;
		}
	}
}
