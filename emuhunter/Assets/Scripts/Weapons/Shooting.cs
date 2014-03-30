using UnityEngine;
using System.Collections;

public class Shooting : Weapon {
	/// constants
	private readonly float bulletLifeTime = 3F;
	private readonly float lightIntensity = 10.0F;
	private readonly Color lightColor = Color.red + Color.yellow;
	private readonly Vector3 velocityVector = new Vector3(0.0F, 0.0F, 180.0F);

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
