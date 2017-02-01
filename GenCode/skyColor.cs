using UnityEngine;
using System.Collections;

public class SkyColorBehaviourScript : MonoBehaviour {

	// used to track the index of the background to display
	public int cycleIndex = 0;
	Color[] skyColors = new Color[3];

	Camera camera;

	void Start () {
		// init the sky colors array

		camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;


		skyColors [0] = new Color (255, 0, 0);    // red
		skyColors [1] = new Color (0, 255, 0);    // green 
		skyColors [2] = new Color (0, 0, 255);    // blue
	}

	// Update is called once per frame
	void Update () {
		// cycle the camera background color
		if (Input.GetKeyDown ("g")) {

			camera.backgroundColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}

	}
}