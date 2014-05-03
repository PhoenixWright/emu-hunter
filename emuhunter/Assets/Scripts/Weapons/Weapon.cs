﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {

	protected Texture texture;
	protected List<Texture> textures;
	protected float fps = 20.0F;

	public abstract void Attack();

	protected IEnumerator PlayAnimation () {
		float waitTime = 1.0F / fps;
		for (int idx = 0; idx < textures.Count; ++idx) {
			texture = textures[idx];
			yield return new WaitForSeconds(waitTime);
		}

		texture = textures[0];
	}

}
