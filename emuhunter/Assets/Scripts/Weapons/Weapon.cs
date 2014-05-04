using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Weapon : MonoBehaviour {

	protected Texture texture;
	protected List<Texture> textures;
	protected float fps = 20.0F;

	// reloading
	protected int clipSize = 15;
	protected int ammo = 2;
	protected float reloadTime = 4.0F;
	protected bool reloading = false;
	protected float reloadOffset = 0.0F;

	// shot delay
	protected bool canFire = true;

	public abstract void Attack();

	// Update is called once per frame
	void Update () {
		if (Time.timeScale <= 0) {
			return;
		}
		if (Input.GetButtonDown ("Fire1") && canFire && !reloading) {
			Attack();
			if (--ammo == 0) {
				StartCoroutine(Reload());
			}
		}
	}

	void OnGUI() {
		Rect rect = new Rect(((Screen.width / 2) - (texture.width * 2)),
		                     (Screen.height - (texture.height * 2)) + reloadOffset,
		                     texture.width * 4,
		                     texture.height * 2);
		GUI.DrawTexture(rect, texture);
	}

	public IEnumerator Reload() {
		reloading = true;

		float length = reloadTime / fps;
		Debug.Log("Reloading for " + reloadTime + " seconds");
		while (length < reloadTime) {
			yield return new WaitForSeconds(reloadTime);
			length += length;

			if (length > reloadTime / 2) {
				reloadOffset -= texture.height / fps;
			}
			else {
				reloadOffset += texture.height / fps;
			}
		}

		ammo = clipSize;
		reloading = false;
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
