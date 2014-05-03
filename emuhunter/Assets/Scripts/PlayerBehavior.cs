using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public int health;
	public Score score;
	
	private CameraShake cameraShake;

	private GameObject armLeft;
	private Weapon     weaponLeft;
	private GameObject armRight;
	
	private bool gamePaused = false;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
		armLeft = transform.FindChild ("ArmLeft").gameObject;
		armRight = transform.FindChild ("ArmRight").gameObject;
		score = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100) {
			health = 0;
		}

		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0) {
			fireLeftArm ();
		}
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!gamePaused && Time.timeScale > 0) {
				// pause game, show menu
				gamePaused = true;
				Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!
				Debug.Log("Game Paused");
				
				foreach(MouseLook mouseLook in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<MouseLook>()) {
					mouseLook.enabled = false;
				}
			}
			else if(gamePaused) {
				// unpause game
				gamePaused = false;
				Time.timeScale = 1.0f;
				Debug.Log("Game Unpaused");
				
				foreach(MouseLook mouseLook in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<MouseLook>()) {
					mouseLook.enabled = true;
				}
			}
		}
		
		if (health < 1) {
			Debug.Log("Game Over");
			gameOver = true;
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!
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
		if(gameOver) {
			drawGameOver ();
		}
		
		if(gamePaused) {
			drawPauseMenu ("Game Paused");
		}
	}
	
	void drawPauseMenu(string titleMsg) {
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - 200, 0, Screen.width, Screen.height), titleMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 450, 600, 75), "High Scores")){
		
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 300, 600, 75), "Choose Weapon")){
		
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 150, 600, 75), "New Game")){
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			health = 100;
			score.startup = Time.realtimeSinceStartup;
		}
	}
	
	void drawGameOver() {
		string endMsg = "GAME OVER";
		
		Score scoreComponent = (GameObject.FindGameObjectWithTag ("Interface")).GetComponent<Score>();
		if (scoreComponent.gameState.emusDestroyed == scoreComponent.highKills) {
			endMsg += "\r\nNEW HIGH SCORE";
		}
		if (Time.realtimeSinceStartup > scoreComponent.bestTime) {
			endMsg += "\r\nNEW BEST TIME";
		}
		
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - 150, 0, Screen.width, Screen.height), endMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - 100, Screen.height - 100, 200, 50), "Restart")){
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			health = 100;
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

	void OnTriggerEnter(Collider collision) {
		Weapon weapon = collision.gameObject.GetComponent<Weapon> ();
		if (weapon) {
			equipLeftArm(weapon);	
		}	
	}

}
