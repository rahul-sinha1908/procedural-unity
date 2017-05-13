using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTerrainSpace;
using MyGame;

public class MapGeneratorScript : MonoBehaviour {

	public int width, height, maxHeight, totalRoads=100;
	public float scale;
	public GameObject MyTerrainPrefab;
	public GameObject myPlayer;
	public string mySeed="Hello";
	public Material myMaterial;
	private GameObject[,] myTerrain;
	private int mapSize;
	private MyTerrainScript[,] myTerrainScript;
	// Use this for initialization
	void Start () {

		mapSize=MyGameScript.getInstance().sizeTerrain;

		myTerrain=new GameObject[mapSize, mapSize];
		myTerrainScript=new MyTerrainScript[mapSize, mapSize];

		// myMaterial.SetColor("_Color1",new Color(0,1,1,0.5f));

		float[,] map= PreMapGenerator.generateMap(width,height,mySeed, totalRoads);
		MyGameScript.getInstance().myMap=map;
		MyGameScript.getInstance().width=width;
		MyGameScript.getInstance().height=height;
		myMaterial.SetInt("_init",1);
	}
	
	void Update()
	{
		Vector3 pos = myPlayer.transform.position;
		int x=Mathf.RoundToInt(pos.x/100);
		int y=Mathf.RoundToInt(pos.z/100);
		
		generateMap(x,y-1);
		generateMap(x,y);
		generateMap(x,y+1);
		generateMap(x-1,y-1);
		generateMap(x-1,y);
		generateMap(x-1,y+1);
		generateMap(x+1,y-1);
		generateMap(x+1,y);
		generateMap(x+1,y+1);
		
	}

	public void generateMap(int x, int y){
		x+=6;
		y+=6;
		if(myTerrain[x,y]==null){
			Vector3 spawnPos=new Vector3();
			spawnPos=new Vector3((x-6)*100, 0, (y-6)*100);
			//TODO Calculate the spawn position
			myTerrain[x,y]=GameObject.Instantiate(MyTerrainPrefab, spawnPos, Quaternion.identity, transform);
			myTerrainScript[x,y]=myTerrain[x,y].GetComponent<MyTerrainScript>();
		}else{
			myTerrainScript[x,y].setActiveState(true);
		}
	}
	
}
