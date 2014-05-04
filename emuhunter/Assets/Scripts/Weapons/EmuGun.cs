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
		textures = new List<Texture>(Resources.LoadAll<Texture>("EmuGun/"));
		texture = textures[0];
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

	public override WeaponInfo GetInfo()
	{
		return new WeaponInfo ("Emu-Gun", "An Emu gun! Will eat dragons for breakfast!", textures [0]);
	}
	
}
