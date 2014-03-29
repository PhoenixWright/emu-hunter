using UnityEngine;
using System.Collections;

public enum GameModes {
	Normal,
	BloodRage,
}

public class GameState : MonoBehaviour
{
	public int health = 0; // hits left
	public int emusDestroyed = 0;

	public GameModes gameMode = GameModes.Normal;

	private GUIStyle guiStyle;

	// Use this for initialization
	void Start ()
	{
		health = 3;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI () {
		if (gameMode == GameModes.BloodRage) {
			GUI.backgroundColor = Color.red;
			GUI.Button(new Rect (0, 0, Screen.width, Screen.height), "");
		}
	}
}
