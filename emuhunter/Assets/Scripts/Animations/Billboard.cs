using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var newRotation = Quaternion.LookRotation (Camera.main.transform.position - transform.position).eulerAngles;
		newRotation.x = 90;
		transform.rotation = Quaternion.Euler(newRotation);
	}
}
