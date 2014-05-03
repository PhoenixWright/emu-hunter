using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GameState gameState;
	public int highKills;
	public float bestTime;
	
	public Texture sriracha;
	public float startup;
	
	// Use this for initialization
	void Start ()
	{
		gameState = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
		//PlayerPrefs.DeleteAll();
		highKills = PlayerPrefs.GetInt ("highKills");
		bestTime = PlayerPrefs.GetFloat ("bestTime");
		sriracha = (Texture)Resources.Load("sriracha");
		startup = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameState.emusDestroyed > highKills) {
			PlayerPrefs.SetInt("highKills", gameState.emusDestroyed);
			highKills = gameState.emusDestroyed;
		}
		
		float timeSinceRestart = Time.fixedTime - startup;
		if(timeSinceRestart > bestTime) {
			PlayerPrefs.SetFloat("bestTime", timeSinceRestart);
			bestTime = timeSinceRestart;
		}
	}
	
	void OnGUI ()
	{
		// bloodrage
		if (gameState.rageValue > 0 && gameState.rageValue < 100.0f && gameState.gameMode != GameModes.BloodRage) {
			GUI.Box(new Rect(0, 0, 150, 50), "RAGE METER\r\n" + gameState.rageValue.ToString() + '%');
		}
		
		// score
		float timeSinceRestart = Time.fixedTime - startup;

		GUI.DrawTexture(new Rect(Screen.width - 200, 0, 50, 50), sriracha);
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 100), 
		        "Health: " + gameState.playerScript.health.ToString() + 
		        "\r\nEmus Murdered: " + gameState.emusDestroyed.ToString() + 
		        "\r\nTime survived: " + timeSinceRestart.ToString("N2") +
		        "\r\n\r\nMost Kills: " + highKills.ToString() + 
		        "\r\nBest Time: " + bestTime.ToString("N2")
		        ); 
	}
}
