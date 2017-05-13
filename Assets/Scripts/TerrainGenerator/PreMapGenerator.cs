using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreMapGenerator{
	public static System.Random rand;
	private static int width,height;
	private static List<Point> lpos;
	public struct Dir{
		public Vector2 dir;
		public float dist;
	}
	public struct Point{
		public int x,y;
		public static Point getPoint(int a,int b){
			Point p=new Point();
			p.x=a;
			p.y=b;
			return p;
		}
		public static Point getPoint(Vector2 vect){
			Point p=new Point();
			p.x=Mathf.RoundToInt(vect.x);
			p.y=Mathf.RoundToInt(vect.y);
			return p;
		}
		public static Point add(Point a, Point b){
			a.x=a.x+b.x;
			a.y=a.y+b.y;
			return a;
		}
	}

	public static float[,] generateMap(int width, int height, string seed, int totalroads){
		float[,] myMap=new float[width,height];
		rand=new System.Random(seed.GetHashCode());
		lpos=new List<Point>();
		PreMapGenerator.width=width;
		PreMapGenerator.height=height;

		Point startPos=Point.getPoint(0, height/2);//rand.Next(0, height));
		Vector2 startDir=new Vector2(1,0);
		lpos.Add(startPos);
		for(int i=0; i< totalroads; i++){
			
			Dir tempD=getDirection(startPos, startDir);
			startDir=tempD.dir;
			startDir.Normalize();
			Point tstartPos=Point.add(startPos,Point.getPoint(startDir*tempD.dist));
			Debug.Log("Start Position : "+(startPos.x-500)+","+(startPos.y-500)+" : "+(tstartPos.x-500)+","+(tstartPos.y-500));
			if(colorRoads(myMap, startPos, tstartPos, 40, startDir)){
				startPos = tstartPos;
				lpos.Add(startPos);
				//startPos = lpos[rand.Next(0, lpos.Count)];
			}
			// }else{
			// 	startPos = lpos[myLastPos];
			// 	myLastPos++;
			// }
			
		}
		
		return myMap;
	}
	private static bool validatePoint(Point p){
		if(p.x<0 || p.y<0)
			return false;
		if(p.x>=width || p.y>=height)
			return false;
		return true;
	}
	public static bool colorRoads(float[,] map, Point startI, Point endI, int width, Vector2 dir){
		bool cont=false;
		if(validatePoint(startI) && validatePoint(endI)){
			cont=true;
			Point s=startI;
			Point e=endI;
			map[s.x, s.y]=1f;
			map[e.x, e.y]=1f;
			int x1=s.x, y1=s.y, x2=e.x, y2=e.y;

			float totalPoints=(x1-x2)*(x1-x2)+(y1-y2)*(y1-y2);
			for(int i=0;i<=totalPoints;i++){
				Point p=Point.getPoint(Mathf.RoundToInt(x1+i/totalPoints*(x1<x2?1:-1)), Mathf.RoundToInt(y1+i/totalPoints*(y1<y2?1:-1)));
				Debug.Log("Extract : "+p.x+" : "+p.y+" : "+x1+","+y1+" : "+x2+","+y2);
				Debug.Log("Mul VAl : "+(i/totalPoints)+" : "+i+" : "+totalPoints);
				if(validatePoint(p))
					map[p.x, p.y]=1f;
			}
			
		}
		return cont;
	}

// cont=true;
			// for(int i=-width/2;i<=width/2;i++){
			// // for(int i=0;i<=0;i++){
			// 	Point start=startI;
			// 	Point end=endI;
			// 	Debug.Log("Check Position : "+(start.x-500)+","+(start.y-500)+" : "+(end.x-500)+","+(end.y-500));
			// 	float angle = Vector2.Angle(Vector2.zero, dir);
			// 	start.x=startI.x+(int)(i*Mathf.Sin(angle*180/Mathf.PI));
			// 	start.y=startI.y+(int)(i*Mathf.Cos(angle*180/Mathf.PI));
			// 	end.x=endI.x+(int)(i*Mathf.Sin(angle*180/Mathf.PI));
			// 	end.y=endI.y+(int)(i*Mathf.Cos(angle*180/Mathf.PI));
			// 	float myVal=1f-Mathf.Abs(i)*2f/width;
			// 	if(validatePoint(start) && validatePoint(end)){
			// 		int maxIt = (start.x-end.x)*(start.x-end.x) + (start.y-end.y)*(start.y-end.y);
			// 		float incX=(start.x-end.x)*1f/maxIt;
			// 		float incY=(start.y-end.y)*1f/maxIt;
			// 		for(int j=0; j<maxIt; j++){
			// 			int mX=start.x+Mathf.RoundToInt(incX*j);
			// 			int mY=start.y+Mathf.RoundToInt(incY*j);
			// 			if(validatePoint(Point.getPoint(mX, mY))){
			// 				map[mX, mY]=myVal;
			// 				// if(map[mX, mY]>myVal){
			// 				// 	if(i==0 && map[mX,mY]>0.5){
			// 				// 		//cont=false;
			// 				// 		return false;
			// 				// 	}
			// 				// }else{
			// 				// 	map[mX, mY]=myVal;
			// 				// }
			// 			}
			// 		}
			// 	}
			// }
	public static Dir getDirection(Point strt, Vector2 dir){
		int x = rand.Next(0, 2);

		int angle=0;
		if(x==0){
			angle=45;
		}else if(x==1){
			angle=-45;
		}else if(x==2){
			angle=90;
		}else if(x==3){
			angle=-90;
		}else if(x==4){
			angle=0;
		}
		float curAngle=Mathf.Atan(dir.y/dir.x)*180/Mathf.PI;
		//float finA=curAngle+angle;
		float finA=angle;
		
		Vector2 fDir=new Vector2(Mathf.Cos(finA*Mathf.PI/180f), Mathf.Sin(finA*Mathf.PI/180f));
		Dir d=new Dir();
		d.dir=fDir;
		d.dist=rand.Next(15, 100);

		return d;
	}
	
}
