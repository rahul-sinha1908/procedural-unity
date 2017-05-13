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

		public List<MyRoad> myRoads;
		public List<MyObjectCircle> myCircles;

		public MyGameScript(){
			sizeTerrain=10;
			myCurPos=Vector3.zero;
			myRoads=new List<MyRoad>();
			myCircles=new List<MyObjectCircle>();
			
		}

		public static MyGameScript getInstance(){
			if(instance==null)
				instance=new MyGameScript();
			return instance;
		}
	}
}