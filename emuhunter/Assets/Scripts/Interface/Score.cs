using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GameState gameState;
	public int[] highKills;
	public string[] highKillsNames;
	
	
	public Texture sriracha;
	public float startup;
	
	// Use this for initialization
	void Start ()
	{
		gameState = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
		//PlayerPrefs.DeleteAll();
		
		highKills = new int[10];
		highKillsNames = new string[10];
		for(int i = 0; i < 10; i++) {
			highKills[i] = PlayerPrefs.GetInt ("highKills" + i.ToString());
			highKillsNames[i] = PlayerPrefs.GetString ("highKillsNames" + i.ToString());
			Debug.Log("High score rank " + i + " name " + highKillsNames[i] + " kills " + highKills[i]);
		}
		
		
		sriracha = (Texture)Resources.Load("sriracha");
		startup = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
			highKillsNames[rank] = "PWN";
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
