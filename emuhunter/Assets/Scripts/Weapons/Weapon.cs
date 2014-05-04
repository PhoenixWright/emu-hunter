using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {

	protected Texture texture;
	protected List<Texture> textures;
	protected float fps = 20.0F;

	public abstract void Attack();

	// Update is called once per frame
	void Update () {
		if (Time.timeScale <= 0) {
			return;
		}
		if (Input.GetButtonDown ("Fire1")) {
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
		foreach (var item in textures) {
			texture = item;
			yield return new WaitForSeconds(waitTime);
		}
		texture = textures[0];
	}
}
