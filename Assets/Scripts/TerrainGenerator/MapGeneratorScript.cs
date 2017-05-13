using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTerrainSpace;
using MyGame;

public class MapGeneratorScript : MonoBehaviour {

	public int width, height, maxHeight;
	public float scale;
	public GameObject MyTerrainPrefab;
	public GameObject myPlayer;
	public Material myMaterial;
	private GameObject[,] myTerrain;
	private int mapSize;

	private int process;
	private MyTerrainScript[,] myTerrainScript;
	// Use this for initialization
	void Start () {
		process=1;

		mapSize=MyGameScript.getInstance().sizeTerrain;

		myTerrain=new GameObject[mapSize, mapSize];
		myTerrainScript=new MyTerrainScript[mapSize, mapSize];

		generateMap(0,0);

		// myMaterial.SetColor("_Color1",new Color(0,1,1,0.5f));
		
		

		process--;
	}
	
	void Update()
	{
		
	}

	public void generateMap(int x, int y){
		if(myTerrain[x,y]==null){
			Vector3 spawnPos=new Vector3();
			spawnPos=Vector3.zero;
			//TODO Calculate the spawn position
			myTerrain[x,y]=GameObject.Instantiate(MyTerrainPrefab, spawnPos, Quaternion.identity, transform);
			myTerrainScript[x,y]=myTerrain[x,y].GetComponent<MyTerrainScript>();
		}else{
			myTerrain[x,y].SetActive(true);
		}
	}
	public void setActiveState(bool state){
		if(state)
			transform.gameObject.SetActive(state);
		else if(process==0)
			StartCoroutine(setOffState());
	}
	public IEnumerator setOffState(){
		while(true){
			if(process==0){
				transform.gameObject.SetActive(true);
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
