using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GameState gameState;
	public PlayerBehavior player;
	
	public int[] highKills;
	public string[] highKillsNames;
	
	public Texture sriracha;
	
	private static bool gamePaused = true;
	private static bool showScores = false;
	private static bool startingGame = true;
	private static string pauseMsg = "New Game";
	
	private bool weaponsMenu = false;
	private bool gameOver = false;
	private bool askName = false;
	private int rank;
	private string playerName = "";
	
	private float widthRatio = Screen.width / 1920.0f;
	private float heightRatio = Screen.height / 1080.0f;
	
	// Use this for initialization
	void Start ()
	{
		gameState = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
		player = (PlayerBehavior)GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
		//PlayerPrefs.DeleteAll();
		
		highKills = new int[10];
		highKillsNames = new string[10];
		for(int i = 0; i < 10; i++) {
			highKills[i] = PlayerPrefs.GetInt ("highKills" + i.ToString());
			highKillsNames[i] = PlayerPrefs.GetString ("highKillsNames" + i.ToString());
			Debug.Log("High score rank " + i + " name " + highKillsNames[i] + " kills " + highKills[i]);
		}
		
		if(startingGame) {
			pauseGame ();
		}
		
		
		sriracha = (Texture)Resources.Load("sriracha");
	}
	
	void pauseGame() {
		// pause game, show menu
		gamePaused = true;
		Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!
		Debug.Log("Game Paused");
		
		foreach(MouseLook mouseLook in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<MouseLook>()) {
			mouseLook.enabled = false;
		}
		
		startingGame = false;
	}
	
	void unpauseGame() {
		// unpause game
		gamePaused = false;
		Time.timeScale = 1.0f;
		Debug.Log("Game Unpaused");
		
		foreach(MouseLook mouseLook in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<MouseLook>()) {
			mouseLook.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!gamePaused && Time.timeScale > 0) {
				pauseMsg = "Game Paused";
				pauseGame ();
			}
			else if(weaponsMenu) {
				weaponsMenu = false;
			}
			else if(gamePaused) {
				unpauseGame ();
			}
		}
		
		if (player.health < 1 && !gameOver) {
			Debug.Log("Game Over");
			gameOver = true;
			Time.timeScale = 0.0f; // this DISABLES MOVEMENT AND UPDATES OF EVERYTHING!
			
			askName = true;
			
			BloodRageLens rage = (GameObject.FindGameObjectWithTag ("MainCamera")).GetComponent<BloodRageLens>();
			rage.Disable();
		}
	}
	
	void OnGUI ()
	{
		// bloodrage
		if (gameState.rageValue > 0 && gameState.rageValue < 100.0f && gameState.gameMode != GameModes.BloodRage) {
			GUI.Box(new Rect(0, 0, 150, 50), "RAGE METER\r\n" + gameState.rageValue.ToString() + '%');
		}
		
		// score
		GUI.DrawTexture(new Rect(Screen.width - 200, 0, 50, 50), sriracha);
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 100), 
		        "Health: " + gameState.playerScript.health.ToString() + 
		        "\r\nEmus Murdered: " + gameState.emusDestroyed.ToString() + 
		        "\r\n\r\nMost Kills: " + highKills[0].ToString()
		        ); 
		        
		if(gameOver) {
			if(askName) {
				drawAskName ();
			}
			else {
				drawGameOver ();
			}
		}
		
		if (startingGame) {
			drawPauseMenu ();
		}
		
		if (gamePaused) {
			if(weaponsMenu) {
				drawWeaponsMenu ();
			}
			else {
				drawPauseMenu ();
			}
		}
		
		if(showScores) {
			drawScores();
		}
	}
	
	
	void drawWeaponsMenu() {
		WeaponBehavior wb = (GameObject.FindGameObjectWithTag ("MainCamera")).GetComponent<WeaponBehavior>();
		// todo: get list of weapons
		// each weapon has icon, description
		// also know which weapon is equipped, and able to tell wb to equip a given weapon
		
		float buttonLeft = (Screen.width / 2) - (500.0f * widthRatio);
		float buttonWidth = 150.0f * widthRatio;
		float rowHeight = 150.0f * heightRatio;
		float descLeft = (Screen.width / 2) - (330.0f * widthRatio);
		float descWidth = 830.0f * widthRatio;
		
		for(int i = 0; i < 5; i++) {
			GUI.skin.label.fontSize = 24;
			GUI.skin.button.fontSize = 24;
			if (GUI.Button(new Rect (buttonLeft, rowHeight * (1 + 1.1f * i), buttonWidth, rowHeight), (Texture)Resources.Load("AxeGun/AXEGA0", typeof(Texture)))){
			
			}
			GUI.Label(new Rect(descLeft, rowHeight * (1 + 1.1f * i), descWidth, rowHeight), "Weapon Name\r\n\r\nWeapon Description goes here mate");
		}
	}
	
	
	void drawAskName() {
		GUI.skin.label.fontSize = 48;
		GUI.skin.textField.fontSize = 48;
		
		GUI.Label(new Rect((Screen.width / 2) - (300.0f * widthRatio), (200.0f * heightRatio), (600.0f * widthRatio), 75), "Enter name");
		
		playerName = GUI.TextField(new Rect((Screen.width / 2) - (300.0f * widthRatio), (400.0f * heightRatio), (600.0f * widthRatio), 75), playerName, 3).ToUpper();
		
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (600.0f * heightRatio), (600.0f * widthRatio), 75), "Save")){
			askName = false;
			Score scoreComponent = (GameObject.FindGameObjectWithTag ("Interface")).GetComponent<Score>();
			rank = scoreComponent.SaveScore();
		}
	}
	
	void drawScores() {
		GUI.skin.label.fontSize = 48;
		GUI.skin.button.fontSize = 48;
		string scoreString = BuildScoresString();
		
		GUI.Label(new Rect(50, 50, Screen.width, Screen.height), scoreString);
		
	}
	
	void drawPauseMenu() {
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - (300.0f * widthRatio), (100.0f * heightRatio), (600.0f * widthRatio), 75), pauseMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (250.0f * heightRatio), (600.0f * widthRatio), 75), "High Scores")){
			showScores = !showScores;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (400.0f * heightRatio), (600.0f * widthRatio), 75), "Choose Weapon")){
			weaponsMenu = true;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (550.0f * heightRatio), (600.0f * widthRatio), 75), "New Game")){
			Application.LoadLevel(Application.loadedLevel);
			unpauseGame ();
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (700.0f * heightRatio), (600.0f * widthRatio), 75), "Exit Game")){
			Application.Quit();
		}
	}
	
	void drawGameOver() {
		string endMsg = "GAME OVER";
		if (rank < 11) {
			endMsg += "\r\nNEW HIGH SCORE\r\n" + gameState.emusDestroyed + " EMUS DESTROYED\r\n" + "#" + rank + " SCORE";
		}
		
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 72;
		GUI.Label(new Rect((Screen.width / 2) - 150, 0, Screen.width, Screen.height), endMsg);
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (300.0f * heightRatio), (600.0f * widthRatio), 75), "Restart")){
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale = 1.0f;
			player.health = 100;
		}
		if (GUI.Button(new Rect ((Screen.width / 2) - (300.0f * widthRatio), (450.0f * heightRatio), (600.0f * widthRatio), 75), "Exit Game")){
			Application.Quit();
		}
	}
	
	public string BuildScoresString() {
		string result = "";
		for(int i = 0; i < 10; i++) {
			result += (i + 1).ToString() + ".    " + highKillsNames[i].ToString() + "   " + highKills[i].ToString() + "\r\n";
		}
		return result;
	}
	
	public int SaveScore() {
		// update high scores list, save prefs
		
		// did we get onto the top 10 scores?
		int rank = 10;
		while(gameState.emusDestroyed > highKills[rank - 1]) {
			rank--;
			if(rank == 0) {
				break;
			}
		}
		Debug.Log ("high score rank " + rank.ToString());
		if(rank < 10) {
			for(int i = 9; i > rank; i--) {
				highKills[i] = highKills[i - 1];
				highKillsNames[i] = highKillsNames[i - 1];
				Debug.Log("High score rank " + i + " name " + highKillsNames[i] + " kills " + highKills[i]);
			}
			highKills[rank] = gameState.emusDestroyed;
			highKillsNames[rank] = playerName;
		}
		
		for(int i = 0; i < 10; i++) {
			PlayerPrefs.SetInt ("highKills" + i.ToString(), highKills[i]);
			PlayerPrefs.SetString ("highKillsNames" + i.ToString(), highKillsNames[i]);
			Debug.Log("High score rank " + i + " name " + highKillsNames[i] + " kills " + highKills[i]);
		}
		PlayerPrefs.Save();
		return rank + 1;
	}
}
