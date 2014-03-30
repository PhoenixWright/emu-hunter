using UnityEngine;
using System.Collections;

public class Shooting : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);

	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private Transform ammoExit;

	/// mutables
	public Rigidbody projectile;

	// Use this for initialization
	void Start () {
		ammoExit = this.transform.FindChild ("Barrel").FindChild ("AmmoExit");
	}

	// Update is called once per frame
	void Update () {
		
	}


	override public void Attack() {
		Rigidbody instantiatedProjectile = Instantiate(projectile, ammoExit.position, Quaternion.identity)
			as Rigidbody;
		instantiatedProjectile.velocity = transform.TransformDirection(velocityVector);

		Light lightGameObject = instantiatedProjectile.gameObject.AddComponent<Light> ();
		lightGameObject.light.color = this.lightColor;
		lightGameObject.light.intensity = this.lightIntensity;

		Destroy(instantiatedProjectile.gameObject, bulletLifeTime);
	}
	
}
