using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmuGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 10000.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("EmuGun/CHAFA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("EmuGun/CHAFB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("EmuGun/CHANA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("EmuGun/CHANB0", typeof(Texture)));
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
	
	override public void Attack() {
		GameObject emu = (GameObject)Instantiate(Resources.Load("Enemy"));

		EmuBehavior behavior = emu.gameObject.GetComponent<EmuBehavior>();
		Destroy(behavior);

		EnemyMovements movements = emu.gameObject.GetComponent<EnemyMovements>();
		Destroy(movements);

		emu.gameObject.AddComponent(typeof(BulletStats));
		BulletStats stats = emu.gameObject.GetComponent<BulletStats>();
		stats.damage = 100;
		//stats.explode = true;

		emu.gameObject.AddComponent(typeof(Billboard));

		var forward = Camera.main.transform.TransformDirection(Vector3.forward);
		var front = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
		emu.rigidbody.interpolation = RigidbodyInterpolation.None;
		emu.rigidbody.mass = 1;
		emu.rigidbody.position = front;
		emu.rigidbody.velocity = Camera.main.transform.TransformDirection(velocityVector);
		
		Light lightGameObject = emu.gameObject.AddComponent<Light> ();
		lightGameObject.light.color = this.lightColor;
		lightGameObject.light.intensity = this.lightIntensity;
		
		Destroy(emu.gameObject, bulletLifeTime);
		
		StartCoroutine(PlayAnimation());

		// play an audio clip for the emu
		AudioClip clip = (AudioClip)Resources.Load("EmuSounds/GoatScream");
		AudioSource source = emu.gameObject.GetComponent<AudioSource>();
		source.PlayOneShot(clip);
	}
	
}
