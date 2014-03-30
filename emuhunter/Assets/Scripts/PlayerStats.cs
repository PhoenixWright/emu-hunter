using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;

	private CameraShake cameraShake;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

		if (enemy) {
			if (cameraShake) {
				cameraShake.Shake();
			}
		}
	}
}
