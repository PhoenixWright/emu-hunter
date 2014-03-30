using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GameState gameState;


	// Use this for initialization
	void Start ()
	{
		gameState = (GameState)GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
		// bloodrage
		if (gameState.recentKillCount > 0 && gameState.recentKillCount < 3 && gameState.gameMode != GameModes.BloodRage) {
			float percent = gameState.recentKillCount * 33;
			GUI.Box(new Rect(0, 0, 150, 50), "RAGE METER\r\n" + percent.ToString() + '%');
		}


		// score
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 50), "Health: " + gameState.playerScript.health.ToString() + "\r\nEmus Murdered: " + gameState.emusDestroyed.ToString()); 
	}
}
