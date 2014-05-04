﻿using UnityEngine;
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
		textures = new List<Texture>(Resources.LoadAll<Texture>("RocketGun/"));
		texture = textures[0];
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

	public static WeaponInfo GetInfo(KeyCode index)
	{
		return new WeaponInfo ("Rocket-Gun", "Odin.", Resources.LoadAll<Texture>("RocketGun/")[0], index);
	}
}
