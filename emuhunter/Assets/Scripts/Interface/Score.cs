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
		GUI.Box(new Rect(Screen.width - 150, 0, 150, 50), "Health: " + gameState.health.ToString() + "\r\nEmus Murdered: " + gameState.emusDestroyed.ToString()); 
	}
}
