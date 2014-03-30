using UnityEngine;
using System.Collections;

public class GroundSplatter : MonoBehaviour {

	public GameObject splat;

	// Update is called once per frame
	public void Splat()
	{
		GameObject theSplat = (GameObject)Instantiate(splat, transform.position, Quaternion.identity);
		Destroy(theSplat, 2);
	}
}