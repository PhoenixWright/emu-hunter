using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private SpriteRenderer sprite;
	
	
	
	// Use this for initialization
	void Start () {
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("AxeGun/AXEGA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGD0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGE0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGF0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGG0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGH0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGI0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGJ0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGK0", typeof(Texture)));
		texture = textures[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0) {
			Debug.Log("Axe Gun Firing");
			Attack();
		}
	}
	
	void OnGUI() {
		Rect rect = new Rect(((Screen.width / 2) - (texture.width)),
		                     (Screen.height - (texture.height * 2)),
		                     texture.width * 2,
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
		
		StartCoroutine(PlayAnimation());
	}
	
}
