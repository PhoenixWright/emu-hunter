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

	private BloodRage bloodRage;
	private GUIStyle guiStyle;

	// Use this for initialization
	void Start ()
	{
		health = 3;
		bloodRage = GetComponent<BloodRage>();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
