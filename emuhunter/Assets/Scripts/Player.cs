using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int health = 0;

	// Use this for initialization
	void Start () {
		health = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		health -= 1;
	}
}
