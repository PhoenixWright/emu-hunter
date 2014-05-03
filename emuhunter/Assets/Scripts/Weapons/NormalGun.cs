using UnityEngine;
using System.Collections;

public class NormalGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);

	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private Transform ammoExit;

	// Use this for initialization
	void Start () {
		ammoExit = this.transform;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0) {
			Debug.Log("Normal Gun Firing.");
			Attack();
		}
	}


	override public void Attack() {
		GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
		bullet.rigidbody.position = ammoExit.position;
		bullet.rigidbody.rotation = Quaternion.identity;
		bullet.rigidbody.velocity = transform.TransformDirection(velocityVector);

		Light lightGameObject = bullet.gameObject.AddComponent<Light> ();
		lightGameObject.light.color = this.lightColor;
		lightGameObject.light.intensity = this.lightIntensity;

		Destroy(bullet.gameObject, bulletLifeTime);
	}
	
}
