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
	void Update () {if(transform.position.y < -100) {
			health = 0;
		}
	}
	
	void OnGUI() {
		if (health < 1) {
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!

			string endMsg = "GAME OVER";
		
			Score scoreComponent = (GameObject.FindGameObjectWithTag ("Interface")).GetComponent<Score>();
			if(scoreComponent.gameState.emusDestroyed == scoreComponent.highKills) {
				endMsg += "\r\nNEW HIGH SCORE";
			}
			if(Time.realtimeSinceStartup > scoreComponent.bestTime) {
				endMsg += "\r\nNEW BEST TIME";
			}
			
		
			GUI.skin.button.fontSize = 120;
			if(GUI.Button(new Rect (Screen.width/5, Screen.height/3, 4*Screen.width/5, 2*Screen.height/3), endMsg)){
				Application.LoadLevel(Application.loadedLevel);
				Time.timeScale = 1.0f;
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
