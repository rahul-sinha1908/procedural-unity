using UnityEngine;
using System.Collections;

//Class that demonstrates how to instantiate prefabs
public class CityBuilder : MonoBehaviour 
{
	public int numBlocks = 5;
	public int numFlatLandApt = 40;
	public int numTerrainApt = 500;

	public static int[] numApartment;

	// Use this for initialization
	void Start () {
		BuildHouses ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BuildHouses()
	{
		GameObject building;

		for (int i = 1; i <= numBlocks; i++) {
			building = (GameObject) Instantiate(Resources.Load ("prefabs/TownHouse"));
			building.name = "townHouse_" + i.ToString();
			building.transform.position = new Vector3 (Random.Range (-4f, 4f), 0.5f, Random.Range (-4f, 4f));
			building.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f)*Vector3.up);
		}

		for (int i = 1; i <= numBlocks; i++) {
			building = (GameObject) Instantiate(Resources.Load ("prefabs/SkyScraper"));
			building.name = "skyHouse_" + i.ToString();
			building.transform.position = new Vector3 (Random.Range (-4f, 4f), 1.156f, Random.Range (-4f, 4f));
			building.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f)*Vector3.up);
		}

		numApartment = new int[5];

		int id;
		for (int i = 1; i <= numFlatLandApt; i++) {
			id = Random.Range (1, 5);
			numApartment [id]++;
			building = (GameObject) Instantiate(Resources.Load ("prefabs/House"+id.ToString()));
			building.name = "urbanHouse_" + i.ToString();
			building.transform.position = new Vector3 (Random.Range (-5f, -15f), 0f, Random.Range (-4f, 4f));
			building.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f)*Vector3.up);
		}

		//plant houses on terrain
		for (int i = 1; i <= numTerrainApt; i++) {
			id = Random.Range (1, 5);
			numApartment [id]++;
			building = (GameObject) Instantiate(Resources.Load ("prefabs/House"+id.ToString()));
			building.name = "terrainHouse_" + i.ToString();
			Vector3 pos = new Vector3 (Random.Range (60f, 170f), 0f, Random.Range (40f, 160f));
			pos.y = Terrain.activeTerrain.SampleHeight(pos);

			building.transform.position = pos;
			building.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f)*Vector3.up);
		}


	}
}
