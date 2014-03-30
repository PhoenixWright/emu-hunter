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
	private Queue killTimes = new Queue();

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
		if (bloodRage.rageEnabled) {
			gameMode = GameModes.BloodRage;
		}
		else {
			gameMode = GameModes.Normal;
		}
	}

	public void EmuKilled() {
		emusDestroyed += 1;
		killTimes.Enqueue(Time.fixedTime);
		
		if(bloodRage.rageEnabled) {
			bloodRage.secondsLeft++;
		}
		else {
			while(killTimes.Count > 3) {
				killTimes.Dequeue();
			}
			if(killTimes.Count == 3) {
				var thirdRecentKill = (float)killTimes.Dequeue();
				if(thirdRecentKill + 5.0f > Time.fixedTime) {
					bloodRage.Enable();
					bloodRage.secondsLeft = 10;	
				}
			}
		}
	}
}
