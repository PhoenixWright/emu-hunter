using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public int health;
	
	private CameraShake cameraShake;
	private static bool startingGame = true;

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
		if (startingGame) {
			drawStartGame ();
		}
	}
	
	void drawStartGame() { 
		Time.timeScale = 0.0f;
		if (GUI.Button(new Rect((Screen.width / 2) - 300, Screen.height - 300, 600, 75), "New Game")) {
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			health = 100;
			startingGame = false;
		}
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
