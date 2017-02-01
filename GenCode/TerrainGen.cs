//attach this to the terrain
//Does two things: generate heightmap, and detail map on load


using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour {

	public int tileRate = 1;
	private const int details = 5; //number of details in scene
	public int treeCount = 5;
	public GameObject omiObj;
	public Terrain myT;

	void Start () {
		if (this.GetComponent<Terrain>())
		{
			cleanUp ();
			//genTerrain(this.GetComponent<Terrain>(), tileRate);
			placeTrees (myT, treeCount);
			int prob = Random.Range (8800, 9000);
			genDetails (myT, tileRate, 0, prob);
			genDetails (myT, tileRate, 1, prob);
			genDetails (myT, tileRate, 2, prob);
			genDetails (myT, tileRate, 3, prob);
			genDetails (myT, tileRate, 4, prob);

			Color col = new Color (Random.value, Random.value, Random.value);
			omiObj.GetComponentInChildren<Camera> ().backgroundColor = col;
			myT.materialTemplate.color = new Color (Random.value, Random.value, Random.value);
			//omiObj.transform.position = new Vector3(omiObj.transform.position.x, myT.SampleHeight(omiObj.transform.position), omiObj.transform.position.z);
			//cleanUp ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.F)) {
			Color col = new Color (Random.value, Random.value, Random.value);
			omiObj.GetComponentInChildren<Camera> ().backgroundColor = col;

			myT.materialTemplate.color = new Color (Random.value, Random.value, Random.value);
		}
	}

	void OnApplicationQuit(){
		print ("quit");
	}

	public Object treefab;
	void placeTrees(Terrain terrain, int num){
		//attach terrain to prefab
		//Random.
		for (int i = 0; i < num; ++i) {
			int x = Random.Range (0, terrain.terrainData.heightmapWidth);
			int z = Random.Range (0, terrain.terrainData.heightmapHeight);
			Vector3 newpos = new Vector3 (x, 0, z);
			newpos.y = terrain.SampleHeight (newpos);
			GameObject tTree = (GameObject)Instantiate(treefab, newpos, Quaternion.identity);
//			tTree.GetComponent<treeBehavior5> ().terrain = terrain;
		}

	}

	//based on: http://wiki.unity3d.com/index.php/TerrainPerlinNoise
	void genTerrain(Terrain terrain, float tileSize) {
		float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

		for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
			{
				heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize)/10.0f;
			}
		}

		terrain.terrainData.SetHeights(0, 0, heights);
		print (terrain.SampleHeight (omiObj.transform.position));
		omiObj.transform.position = new Vector3(omiObj.transform.position.x, terrain.SampleHeight(omiObj.transform.position) + 5f, omiObj.transform.position.z);
	}

	//take prob and obj num
	void genDetails(Terrain terrain, float tileSize, int obj, int prob){
		//float[,] detailMap = new float[terrain.terrainData.detailWidth, terrain.terrainData.detailHeight];
		var map = terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, obj);
		for (int i = 0; i < terrain.terrainData.detailWidth; i++)
		{
			for (int k = 0; k < terrain.terrainData.detailHeight; k++)
			{
				int tester = Random.Range (0, prob);
				if (tester > prob - 5)
					map [i, k] = 1;
				else {
					map [i, k] = 0;
				}
				//map[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.detailWidth) * tileSize, ((float)k / (float)terrain.terrainData.detailWidth) * tileSize)/10.0f;
			}
		}
		terrain.terrainData.SetDetailLayer(0, 0, obj, map);
	}

	void cleanUp(){
				
		if (myT) {
			Terrain terrain = myT;
			var map = terrain.terrainData.GetDetailLayer (0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);
			for (int i = 0; i < terrain.terrainData.detailWidth; i++) {
				for (int k = 0; k < terrain.terrainData.detailHeight; k++) {
					map [i, k] = 0;
					//map[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.detailWidth) * tileSize, ((float)k / (float)terrain.terrainData.detailWidth) * tileSize)/10.0f;
				}
			}
			for (int i = 0; i < details; ++i) {
				terrain.terrainData.SetDetailLayer (0, 0, i, map);
			}
		}
			/*
			float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

			for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
			{
				for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
				{
					heights [i, k] = 0;
					//heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize)/10.0f;
				}
			}

			terrain.terrainData.SetHeights(0, 0, heights);

			var element = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapLayers];

			for (int i = 0; i < terrain.terrainData.alphamapWidth; i++)
			{
				for (int k = 0; k < terrain.terrainData.alphamapHeight; k++)
				{
					element [i, k, 0] = 1;
					element [i, k, 1] = 0;
				}
			}

			terrain.terrainData.SetAlphamaps(0, 0, element);
		}
		*/
	}
}
