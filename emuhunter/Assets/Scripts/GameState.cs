using UnityEngine;
using System.Collections;

public enum GameModes {
	Normal,
	BloodRage,
}

public class GameState : MonoBehaviour
{
	public GameObject playerObject;
	public PlayerStats playerScript;
	public int emusDestroyed = 0;
	public GameModes gameMode = GameModes.Normal;

	private BloodRageLens bloodRage;
	private GUIStyle guiStyle;

	// Use this for initialization
	void Start ()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerScript = playerObject.GetComponent<PlayerStats>();
		bloodRage = Camera.main.GetComponent<BloodRageLens>();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void EmuKilled() {
		emusDestroyed += 1;
		if (emusDestroyed == 3) {
			bloodRage.Enable();
		}
	}
}
