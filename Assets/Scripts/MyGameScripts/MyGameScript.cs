using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class MyGameScript{

		public static MyGameScript instance;

		public int sizeTerrain;
		public bool[,] trackTerrain;

		public MyGameScript(){
			sizeTerrain=10;
		}

		public static MyGameScript getInstance(){
			if(instance==null)
				instance=new MyGameScript();
			return instance;
		}
	}
}