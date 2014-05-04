using UnityEngine;
using System.Collections;

public class TextureManager : MonoBehaviour {
	static Texture texture;
	public float scrollSpeed = 0.5F;

	void Start() {
		texture = Resources.Load<Texture>("dark_mixed_old_brick_texture_02");
		renderer.material.SetTexture(0, texture);
	}

	void Update() {
		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));//offset, 0));
	}
}
