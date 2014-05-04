using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 0.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("RocketGun/BG2GA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GD0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GE0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GF0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GG0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GH0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GI0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GJ0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GK0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GL0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GM0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GN0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("RocketGun/BG2GO0", typeof(Texture)));
		texture = textures[0];
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
		BulletStats bulletStats = bullet.GetComponent<BulletStats>();
		bulletStats.damage = 50;
		bulletStats.explode = true;
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
