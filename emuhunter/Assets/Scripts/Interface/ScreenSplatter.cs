using UnityEngine;
using System.Collections;

public class ScreenSplatter : MonoBehaviour {

	public GameObject splat;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 cameraPosition = Camera.main.transform.position;
			GameObject theSplat = (GameObject)Instantiate(splat, cameraPosition, Quaternion.identity);
			Destroy (theSplat, 2);
		}
	}
}
