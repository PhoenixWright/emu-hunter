using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour {

	public Texture splat;

	// Update is called once per frame
	public void Splat()
	{
		GameObject theSplat = (GameObject)Instantiate(splat, transform.position, Quaternion.FromToRotation(Vector3.up, transform.position));
		Destroy(theSplat, 2);
	}
}