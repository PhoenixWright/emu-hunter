using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public int health;
	public Score score;
	public GameState state;
	public int rank;
	
	private CameraShake cameraShake;
	
	private bool gamePaused = false;
	private bool gameOver = false;
	private bool showScores = false;
	private static bool startingGame = true;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
		score = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Score>();
		state = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100) {
			health = 0;
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
		
		if (health < 1 && !gameOver) {
			Debug.Log("Game Over");
			gameOver = true;
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!
			
			Score scoreComponent = (GameObject.FindGameObjectWithTag ("Interface")).GetComponent<Score>();
			rank = scoreComponent.SaveScore();
			
			BloodRageLens rage = (GameObject.FindGameObjectWithTag ("MainCamera")).GetComponent<BloodRageLens>();
			rage.Disable();
		}
	}
	
	void OnGUI() {
		if (startingGame) {
			drawStartGame ();
		}
		if(gameOver) {
			drawGameOver ();
		}
		
		if(gamePaused) {
			drawPauseMenu ("Game Paused");
		}
		
		if(showScores) {
			drawScores();
		}
	}
	
	void drawScores() {
		GUI.skin.label.fontSize = 48;
		GUI.skin.button.fontSize = 48;
		string scoreString = score.BuildScoresString();
		
		GUI.Label(new Rect(50, 50, Screen.width, Screen.height), scoreString);
		
	}
	
	void drawPauseMenu(string titleMsg) {
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - 200, 0, Screen.width, Screen.height), titleMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 600, 600, 75), "High Scores")){
			showScores = !showScores;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 450, 600, 75), "Choose Weapon")){
		
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 300, 600, 75), "New Game")){
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			health = 100;
			score.startup = Time.realtimeSinceStartup;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 150, 600, 75), "Exit Game")){
			Application.Quit();
		}
	}
	
	void drawGameOver() {
		string endMsg = "GAME OVER";
		if (rank < 11) {
			endMsg += "\r\nNEW HIGH SCORE\r\n" + state.emusDestroyed + " EMUS DESTROYED\r\n" + "#" + rank + " SCORE";
		}
		
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - 150, 0, Screen.width, Screen.height), endMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 300, 600, 75), "Restart")){
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			health = 100;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 150, 600, 75), "Exit Game")){
			Application.Quit();
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