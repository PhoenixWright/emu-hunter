using UnityEngine;
using System.Collections;

public class BloodRage : MonoBehaviour {

	public bool enabled = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Enable() {
	}

	void OnGUI () {
		if (enabled) {
			GUI.backgroundColor = Color.red;
			GUI.Button(new Rect (0, 0, Screen.width, Screen.height), "");
		}
	}
}
