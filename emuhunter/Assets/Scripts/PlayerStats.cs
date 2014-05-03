using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;
	private CameraShake cameraShake;

	private GameObject armLeft;
	private Weapon     weaponLeft;
	private GameObject armRight;
	private float pauseEndTime;

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

	private void fireLeftArm() {
		if(armLeft.transform.childCount == 0) { 
			Debug.Log ("Nothing on left arm.");
			return; 
		}
		
		if(weaponLeft != null) {
			Debug.Log ("No weapon equipped.");
			weaponLeft.Attack();
		}

	}

	private void equipLeftArm(Weapon item) {
		item.transform.parent = armLeft.transform;
		item.transform.localPosition = new Vector3(0.0F, 0.0F, 0.0F);
		weaponLeft = (Weapon)armLeft.transform.GetChild (0).GetComponent (typeof(Weapon));
		//Debug.Log ("Equipped weapon: " + weaponLeft);
	}
	
	void OnGUI() {
		if (health < 1) {
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!

			string endMsg = "GAME OVER";
		
			Score scoreComponent = (GameObject.FindGameObjectWithTag ("Interface")).GetComponent<Score>();
			if (scoreComponent.gameState.emusDestroyed == scoreComponent.highKills) {
				endMsg += "\r\nNEW HIGH SCORE";
			}
			if (Time.realtimeSinceStartup > scoreComponent.bestTime) {
				endMsg += "\r\nNEW BEST TIME";
			}

			GUI.skin.label.fontSize = 48;
			GUI.skin.button.fontSize = 48;
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), endMsg);
			if (GUI.Button(new Rect ((Screen.width / 2) - 100, Screen.height - 100, 200, 50), "Restart")){
				Application.LoadLevel(Application.loadedLevel);
				Time.timeScale = 1.0f;
				health = 100;
			}
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

	void OnTriggerEnter(Collider collision) {
		Weapon weapon = collision.gameObject.GetComponent<Weapon> ();
		if (weapon) {
			equipLeftArm(weapon);	
		}	
	}

}
