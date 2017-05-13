using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTerrainSpace;
using MyGame;

public class MyTerrainScript : MonoBehaviour {
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private int lod;
	private float maxHieght;
	// Use this for initialization
	void Start () {
		meshFilter=GetComponent<MeshFilter>();
		meshRenderer=GetComponent<MeshRenderer>();
		maxHieght=10f;
		generateMesh(1);
	}
	
	private void generateMesh(int lod){
		meshFilter.mesh = MeshGenerator.generateMesh(NormalMapsScript.GenerateNoiseMap(100, 100, 10.3f), transform.position.x, transform.position.y, maxHieght).createMesh();

	}
	// Update is called once per frame
	void Update () {
		
	}

	private void addObject(GameObject prefab, Vector3 position, Vector3 lookTowards){
		Quaternion qua=new Quaternion();
		GameObject.Instantiate(prefab, position, qua, transform);
	}
}
