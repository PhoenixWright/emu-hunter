using UnityEngine;
using System.Collections;

public enum GameModes {
	Normal,
	BloodRage,
}

public class GameState : MonoBehaviour
{
	public GameObject playerObject;
	public PlayerBehavior playerScript;
	public int emusDestroyed = 0;
	public GameModes gameMode = GameModes.Normal;
	public int CurrentLevelMax { get; set; }
	public static int LevelLimit = 30;

	public float rageValue;

	private BloodRageLens bloodRage;
	private GUIStyle guiStyle;

	// Use this for initialization
	void Start ()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerScript = playerObject.GetComponent<PlayerBehavior>();
		bloodRage = Camera.main.GetComponent<BloodRageLens>();

		StartCoroutine(WaitAndCalmDown());
	}

	// Update is called once per frame
	void Update ()
	{
		if (bloodRage.rageEnabled) {
			gameMode = GameModes.BloodRage;
		}
		else {
			gameMode = GameModes.Normal;
		}
	}

	public void EmuKilled(float rageToAdd = 33.0f) {
		emusDestroyed += 1;

		if (rageValue == 0) {
			StartCoroutine(WaitAndCalmDown());
		}

		rageValue += rageToAdd;
		
		if(bloodRage.rageEnabled) {
			bloodRage.secondsLeft++;
		}
		else {
			if(rageValue >= 100.0f) {
				bloodRage.Enable();
				bloodRage.secondsLeft = 10;
			}
		}
	}

	IEnumerator WaitAndCalmDown() {
		yield return new WaitForSeconds(0.2f);

		if (rageValue > 0) {
			rageValue -= 2.0f;
		}

		if (rageValue != 0) {
			StartCoroutine(WaitAndCalmDown());
		}
	}
}
