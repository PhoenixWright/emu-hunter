using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public int health;
	
	private CameraShake cameraShake;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100) {
			health = 0;
		}
	}
	
	void OnGUI() {
	}
	
	void OnCollisionEnter(Collision collision) {
		EmuBehavior enemy = collision.gameObject.GetComponent<EmuBehavior>();
		if (enemy) {
			if (cameraShake) {
				cameraShake.Shake();
			}
		}
	}
}
