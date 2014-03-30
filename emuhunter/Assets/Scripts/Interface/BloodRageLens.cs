using UnityEngine;
using System.Collections;

public class BloodRageLens : MonoBehaviour {

	public bool rageEnabled = false;
	public int secondsLeft = 0;
	public int bloodRageLength = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Enable() {
		rageEnabled = true;
		secondsLeft = 10;
		StartCoroutine(WaitAndDisable());
	}

	public void Disable() {
		rageEnabled = false;
	}
	
	void OnGUI () {
		if (rageEnabled) {
			GUI.backgroundColor = Color.red;
			GUI.Button(new Rect (0, 0, Screen.width, Screen.height), secondsLeft.ToString());
		}
	}

	private IEnumerator WaitAndDisable() {
		yield return new WaitForSeconds(1);
		bloodRageLength -= 1;

		if (secondsLeft != 0) {
			secondsLeft -= 1;
			StartCoroutine(WaitAndDisable());
		}
		else {
			Disable();
		}
	}
}
