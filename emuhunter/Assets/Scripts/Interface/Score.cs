using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GameState gameState;
	public int highKills;
	public float bestTime;


	// Use this for initialization
	void Start ()
	{
		gameState = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
		PlayerPrefs.DeleteAll();
		highKills = PlayerPrefs.GetInt ("highKills");
		bestTime = PlayerPrefs.GetFloat ("bestTime");
	}

	// Update is called once per frame
	void Update ()
	{
		if(gameState.emusDestroyed > highKills) {
			PlayerPrefs.SetInt("highKills", gameState.emusDestroyed);
			highKills = gameState.emusDestroyed;
		}
		if(Time.realtimeSinceStartup > bestTime) {
			PlayerPrefs.SetFloat("bestTime", Time.fixedTime);
			bestTime = Time.fixedTime;
		}
	}

	void OnGUI ()
	{
		// bloodrage
		if (gameState.rageValue > 0 && gameState.rageValue < 100.0f && gameState.gameMode != GameModes.BloodRage) {
			GUI.Box(new Rect(0, 0, 150, 50), "RAGE METER\r\n" + gameState.rageValue.ToString() + '%');
		}


		// score
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 100), 
			"Health: " + gameState.playerScript.health.ToString() + 
			"\r\nEmus Murdered: " + gameState.emusDestroyed.ToString() + 
			"\r\nTime survived: " + Time.fixedTime.ToString("N2") +
			"\r\n\r\nMost Kills: " + highKills.ToString() + 
			"\r\nBest Time: " + bestTime.ToString("N2")
			); 
	}
}
