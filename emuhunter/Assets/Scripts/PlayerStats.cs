using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;
	private CameraShake cameraShake;

	private float pauseEndTime;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if (health < 1) {
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!

			GUI.skin.label.fontSize = 64;
			GUI.Label(new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height), "GAME OVER");
			
			if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, Screen.width / 2 + 100, Screen.height / 2 + 25),
			              "I SWEAR I DON'T SUCK")) {
				Time.timeScale = 1.0f;
				Application.LoadLevel(Application.loadedLevel);
				health = 100;
			}
		}
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
