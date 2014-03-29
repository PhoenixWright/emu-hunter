using UnityEngine;
using System.Collections;

public class ProjectileTarget : MonoBehaviour {

	GameState gameState;

	// Use this for initialization
	void Start () {
		GameObject scripts = GameObject.FindGameObjectWithTag("GlobalScripts");
		gameState = scripts.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown() {
		gameState.EmuKilled();
	}
}
