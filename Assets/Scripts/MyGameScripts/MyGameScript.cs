using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{

	public enum MyObjects{
		House, Trees
	}
	public struct MyRoad{
		public Vector3 start, end;
	}
	public struct MyObjectCircle{
		public Vector3 center;
		public float radius;
		public float density;
		public MyObjects type;
	}
	public class MyGameScript{

		private static MyGameScript instance;

		public int sizeTerrain;
		public bool[,] trackTerrain;

		public float[] myLODs;

		public Vector3 myCurPos;
		public int width, height;
		public float[,] myMap;
		public MyGameScript(){
			sizeTerrain=12;
			myCurPos=Vector3.zero;

		}

		public static MyGameScript getInstance(){
			if(instance==null)
				instance=new MyGameScript();
			return instance;
		}
	}
}