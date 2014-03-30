using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour, Weapon {
	public Rigidbody projectile;
	public float speed = 80;

	private Transform ammoExit;

	// Use this for initialization
	void Start () {
		ammoExit = transform.Find ("Barrel").FindChild ("AmmoExit");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Attack () {
		Rigidbody instantiatedProjectile = Instantiate(projectile,
		                                               ammoExit.position,
		                                               ammoExit.rotation) as Rigidbody;

		instantiatedProjectile.velocity = transform.TransformDirection(
			new Vector3(0, 0, speed));

		Destroy(instantiatedProjectile.gameObject, 3F);

	}
}