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
		if (gameState.rageValue > 0 && gameState.rageValue < 100.0f && gameState.gameMode != GameModes.BloodRage) {
			GUI.Box(new Rect(0, 0, 150, 50), "RAGE METER\r\n" + gameState.rageValue.ToString() + '%');
		}


		// score
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 50), "Health: " + 
			gameState.playerScript.health.ToString() + "\r\nEmus Murdered: " + 
			gameState.emusDestroyed.ToString() + "\r\nTime survived: " + Time.realtimeSinceStartup.ToString("N2")); 
	}
}
