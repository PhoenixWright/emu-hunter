using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;
	private float endTime = -1.0f;
	private CameraShake cameraShake;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if (health < 1) {
			GUI.skin.label.fontSize = 120;
			GUI.Label(new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height), "GAME OVER");
			
			if(endTime == -1.0f) {
				Time.timeScale = 0;
				endTime = Time.realtimeSinceStartup;
			}
			
			if((endTime + 5.0f) < Time.realtimeSinceStartup) {
				Application.Quit();
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
