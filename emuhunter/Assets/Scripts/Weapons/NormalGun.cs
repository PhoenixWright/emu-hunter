using UnityEngine;
using System.Collections;

public class NormalGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;

	private SpriteRenderer sprite;
	private Texture texture;

	// Use this for initialization
	void Start () {
		Debug.Log("Animator: " + animation);
		sprite = Camera.allCameras[0].GetComponent<SpriteRenderer>();
		sprite.transform.position = Camera.main.ViewportToWorldPoint(Vector3.zero);
		texture = (Texture)Resources.Load("NormalGun/SPRPA0", typeof(Texture));
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0) {
			Debug.Log("Normal Gun Firing");
			Attack();
		}
	}

	void OnGUI() {
		Rect rect = new Rect(((Screen.width / 2) - (texture.width * 2)),
		                     (Screen.height - (texture.height * 2)),
		                     texture.width * 4,
		                     texture.height * 2);
		GUI.DrawTexture(rect, texture);
	}

	override public void Attack() {
		GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
		var forward = Camera.main.transform.TransformDirection(Vector3.forward);
		var front = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
		bullet.rigidbody.position = front;
		bullet.rigidbody.velocity = Camera.main.transform.TransformDirection(velocityVector);
		Debug.Log(bullet.rigidbody.velocity);

		Light lightGameObject = bullet.gameObject.AddComponent<Light> ();
		lightGameObject.light.color = this.lightColor;
		lightGameObject.light.intensity = this.lightIntensity;

		Destroy(bullet.gameObject, bulletLifeTime);
	}
	
}
