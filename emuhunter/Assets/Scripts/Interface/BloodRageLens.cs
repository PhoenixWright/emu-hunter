using UnityEngine;
using System.Collections;

public class BloodRageLens : MonoBehaviour {

	public bool rageEnabled = false;

	public int secondsLeft = 0;
	
	public int bloodRageLength = 10;

	public AudioSource[] announceThisShit;

	// Use this for initialization
	void Start () {
		announceThisShit = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Enable() {
		rageEnabled = true;
		secondsLeft = bloodRageLength;

		// play sounds
		foreach(AudioSource a in announceThisShit) {
			a.volume = 1.0f;
			a.Play();
		}

		StartCoroutine(WaitAndDisable());
	}

	public void Disable() {
		rageEnabled = false;
		
		foreach(AudioSource a in announceThisShit) {
			if(a.isPlaying) {
				StartCoroutine(FadeOut(a));				
			}
		}
	}
	
	private IEnumerator FadeOut(AudioSource a) {	
		yield return new WaitForSeconds(0.01f);	
		if (a.volume > 0) {
			a.volume -= 0.01f;
			StartCoroutine(FadeOut(a));
		}
		else {
			a.Stop();
		}
	}
	
	void OnGUI () {
		if (!rageEnabled) {
			/*if (GUI.Button(new Rect (0, 0, 200, 200), "Blood Bonus")) {
				Enable();
			}*/
		}

		if (rageEnabled) {
			GUI.backgroundColor = Color.red;
			GUI.skin.label.fontSize = 60;

			if (secondsLeft % 2 == 0) {
				GUI.Label(new Rect (0, 0, Screen.width, Screen.height), "BLOOD BONUS");
			}

			GUI.Button(new Rect (0, 0, Screen.width, Screen.height), secondsLeft.ToString());
		}
	}

	private IEnumerator WaitAndDisable() {
		yield return new WaitForSeconds(1);

		bloodRageLength -= 1;

		if (secondsLeft > 0) {
			secondsLeft -= 1;
			StartCoroutine(WaitAndDisable());
		}
		else {
			Disable();
		}
	}
}
