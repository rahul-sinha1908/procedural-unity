using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTerrainSpace;
using MyGame;
using System;

public class MyTerrainScript : MonoBehaviour {
	public float[] myLODs;
	public GameObject[] houses, trees;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private int lod;
	private System.Random rand;
	private float maxHieght;
	private int currentLod;
	private Vector3 position;
	private Camera cam;
	private bool objectGenerated;
	// Use this for initialization
	void Start () {
		meshFilter=GetComponent<MeshFilter>();
		meshRenderer=GetComponent<MeshRenderer>();
		maxHieght=10f;

		currentLod=-1;

		MyGameScript.getInstance().myLODs=myLODs;
		rand=new System.Random();

		position=transform.position;
		//transform.position=Vector3.zero;

		cam=Camera.main;

		//TODO Do the changes here
		//generateObjects();
	}

    private void generateObjects(float[,] map)
    {
		
    }

    private GameObject getMyPrefab(MyObjects obj)
    {
		if(obj==MyObjects.House){
			return houses[rand.Next(0,houses.Length)];
		}else if(obj==MyObjects.Trees){
			return trees[rand.Next(0, trees.Length)];
		}
        return null;
    }

    private float convertTypeToArea(MyObjects obj){
		if(obj==MyObjects.House){
			return 25f;
		}else if(obj==MyObjects.Trees){
			return 25f;
		}
		return 1f;
	}
    private void generateMesh(int lod){
		lod=myLODs.Length-lod;
		Debug.Log("Generate at : "+transform.position);
		//meshFilter.mesh = MeshGenerator.generateMesh(NormalMapsScript.GenerateNoiseMap(31, 31, 10.3f, true), 0, 0, maxHieght, lod).createMesh();
		float[,] map= new float[101,101];
		int xt=(int)transform.position.x+MyGameScript.getInstance().width/2;
		int yt=(int)transform.position.z+MyGameScript.getInstance().height/2;
		for(int y=-50 ; y<=50; y++){
			for(int x=-50;x<=50;x++){
				map[x+50, y+50] = MyGameScript.getInstance().myMap[xt+x, yt+y];
			}
		}
		meshFilter.mesh = MeshGenerator.generateMesh(map, 0, 0, 1, lod).createMesh();
		if(!objectGenerated){
			generateObjects(map);
			objectGenerated=false;
		}
	}

	// Update is called once per frame
	void Update () {
		MyGameScript.getInstance().myCurPos=cam.transform.position;
		int temp=expectMyLOD();
		if(currentLod!=temp || temp==myLODs.Length){
			currentLod=temp;
			Debug.Log("My Current LOD : "+position+" : "+currentLod);
			if(currentLod==myLODs.Length){
				setActiveState(false);
			}else{
				generateMesh(currentLod);
			}
		}
	}


	private int expectMyLOD(){
		Vector3 pos=MyGameScript.getInstance().myCurPos;
		pos=pos-position;
		float dist=pos.sqrMagnitude;
		
		for(int i=0;i<myLODs.Length;i++){
			if(dist<myLODs[i]*myLODs[i])
				return i;
		}

		return myLODs.Length;
	}

	private void addObject(GameObject prefab, Vector3 position, Vector3 lookTowards){
		Quaternion qua=new Quaternion();
		qua=Quaternion.identity;
		GameObject.Instantiate(prefab, position, qua, transform);
	}

	public void setActiveState(bool state){
		if(state)
			transform.gameObject.SetActive(state);
		else
			transform.gameObject.SetActive(state);
			// StartCoroutine(setOffState());
	}
	private IEnumerator setOffState(){
		while(true){
			// if(process==0){
			// 	transform.gameObject.SetActive(false);
			// 	yield break;
			// }
			yield return new WaitForEndOfFrame();
		}
	}
}
