using UnityEngine;
using System.Collections;

public class ballBehavior : MonoBehaviour {

	private Terrain terrain;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {

		//inputXYZ.y = Terrain.activeTerrain.SampleHeight(inputXYZ) 
		//	+ Terrain.activeTerrain.transform.position.y;


		//terrain = GetComponent<Terrain>();

		terrain = Terrain.activeTerrain.GetComponent<Terrain>();

		//terrain.materialType = Terrain.MaterialType.Custom;

		terrain.materialTemplate.SetColor ("_Color", Color.white);

		mainCamera = Camera.main;

		//var overPass = gameObject.GetComponent ("caveOverpass");
	
	}

	bool up = true;
	// Update is called once per frame
	void Update () {
		//if (up) {
			//this.transform.position += Vector3.up*.2f;
		//	up = false;
		//} else {
		//	this.transform.position -= Vector3.up*.2f;
		//	up = true;
		//}
	}

	void OnTriggerEnter (Collider other)
	{
		//if(col.gameObject.name == "prop_powerCube")
		//{
		terrain.materialTemplate.SetColor ("_Color", new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f)));

		mainCamera.backgroundColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));

		Destroy(gameObject);
		//}
	}
}
