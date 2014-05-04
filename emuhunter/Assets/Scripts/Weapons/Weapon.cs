using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Weapon : MonoBehaviour {

	protected Texture texture;
	protected List<Texture> textures;
	protected float fps = 20.0F;

	// shot delay
	protected bool canFire = true;

	public abstract WeaponInfo GetInfo();
	public abstract void Attack();

	// Update is called once per frame
	void Update () {
		if (Time.timeScale <= 0) {
			return;
		}
		if (Input.GetButtonDown ("Fire1") && canFire) {
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

	public IEnumerator PlayAnimation () {
		float waitTime = 1.0F / fps;
		canFire = false;
		foreach (var item in textures) {
			texture = item;
			yield return new WaitForSeconds(waitTime);
		}
		canFire = true;
		texture = textures[0];
	}
}
