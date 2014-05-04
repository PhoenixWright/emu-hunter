using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		fps = 10.0F;
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("BowGun/BOWFA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFD0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFE0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFF0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFG0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFH0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWFI0", typeof(Texture)));	
		textures.Add((Texture)Resources.Load("BowGun/BOWGA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWGB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWGC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("BowGun/BOWGD0", typeof(Texture)));
		texture = textures[0];
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
