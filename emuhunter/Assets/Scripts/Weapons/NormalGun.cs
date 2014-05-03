using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;

	private SpriteRenderer sprite;

	private Texture texture;
	private List<Texture> textures;
	private float fps = 20.0F;

	// Use this for initialization
	void Start () {
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("NormalGun/SPRPA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPD0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPE0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPF0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("NormalGun/SPRPG0", typeof(Texture)));
		texture = textures[0];
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

	IEnumerator PlayAnimation () {
		float waitTime = 1.0F / fps;
		for (int idx = 0; idx < textures.Count; ++idx) {
			texture = textures[idx];
			yield return new WaitForSeconds(waitTime);
		}
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

		StartCoroutine(PlayAnimation());
	}
	
}
