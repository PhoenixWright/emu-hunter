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

	public IEnumerator PlayAnimation () {
		float waitTime = 1.0F / fps;
		foreach (var item in textures) {
			texture = item;
			yield return new WaitForSeconds(waitTime);
		}
		texture = textures[0];
	}
}
