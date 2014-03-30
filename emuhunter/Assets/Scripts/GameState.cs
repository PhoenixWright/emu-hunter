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

	public int recentKillCount = 0;

	private BloodRageLens bloodRage;
	private GUIStyle guiStyle;

	// Use this for initialization
	void Start ()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerScript = playerObject.GetComponent<PlayerStats>();
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

	public void EmuKilled() {
		emusDestroyed += 1;

		if (recentKillCount == 0) {
			StartCoroutine(WaitAndCalmDown());
		}

		++recentKillCount;
		
		if(bloodRage.rageEnabled) {
			bloodRage.secondsLeft++;
		}
		else {
			if(recentKillCount >= 3) {
				bloodRage.Enable();
				bloodRage.secondsLeft = 10;
			}
		}
	}

	IEnumerator WaitAndCalmDown() {
		yield return new WaitForSeconds(3);

		if (recentKillCount > 0) {
			recentKillCount -= 1;
		}

		if (recentKillCount != 0) {
			StartCoroutine(WaitAndCalmDown());
		}
	}
}
