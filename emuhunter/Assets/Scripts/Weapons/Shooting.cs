using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public Rigidbody projectile;
	public float speed = 80;


	// Use this for initialization
	void Start () {
	
	}

	void Attack() {
		Rigidbody instantiatedProjectile = Instantiate(projectile,
		                                               transform.position,
		                                               transform.rotation) as Rigidbody;
		instantiatedProjectile.velocity = transform.TransformDirection(
			new Vector3(0, 0, speed));

		//GameObject lightGameObject = new GameObject("The Light");
		Light lightGameObject = instantiatedProjectile.gameObject.AddComponent<Light> ();
		//lightGameObject.AddComponent<Light>();
		lightGameObject.light.color = Color.red;
		lightGameObject.light.intensity = 20;
		lightGameObject.light.transform.position = instantiatedProjectile.position;

		Destroy(instantiatedProjectile.gameObject, 3F);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			Attack();
		}
	}
}
