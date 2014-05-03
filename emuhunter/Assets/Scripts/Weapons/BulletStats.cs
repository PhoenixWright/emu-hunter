using UnityEngine;
using System.Collections;

public class BulletStats : MonoBehaviour {

	public int damage = 1;
	public bool explode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (explode) {
			MonoBehaviour.Instantiate(Resources.Load("Detonator-Upwards"), transform.position, Quaternion.identity);
		}
	}
}
