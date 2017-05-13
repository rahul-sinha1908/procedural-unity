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
	private float maxHieght;
	private int currentLod;
	// Use this for initialization
	void Start () {
		meshFilter=GetComponent<MeshFilter>();
		meshRenderer=GetComponent<MeshRenderer>();
		maxHieght=10f;

		currentLod=-1;

		MyGameScript.getInstance().myLODs=myLODs;

		generateObjects();
	}

    private void generateObjects()
    {
        
    }

    private void generateMesh(int lod){
		lod=myLODs.Length-lod;
		meshFilter.mesh = MeshGenerator.generateMesh(NormalMapsScript.GenerateNoiseMap(100, 100, 10.3f, true), transform.position.x, transform.position.y, maxHieght, lod).createMesh();
	}

	// Update is called once per frame
	void Update () {
		if(currentLod!=expectMyLOD()){
			currentLod=expectMyLOD();
			generateMesh(currentLod);
		}
	}


	private int expectMyLOD(){
		Vector3 pos=MyGameScript.getInstance().myCurPos;
		pos=pos-transform.position;
		float dist=pos.sqrMagnitude;
		
		for(int i=0;i<myLODs.Length;i++){
			if(dist<myLODs[i])
				return i;
		}

		return myLODs.Length;
	}

	private void addObject(GameObject prefab, Vector3 position, Vector3 lookTowards){
		Quaternion qua=new Quaternion();
		GameObject.Instantiate(prefab, position, qua, transform);
	}
}
