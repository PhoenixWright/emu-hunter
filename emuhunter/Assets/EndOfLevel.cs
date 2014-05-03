using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

	private bool _triggered;
	private GenerateEnvironment _generateEnvironment;
	private RailsMovement _railsMovement;
	
	// Use this for initialization
	void Start () {
		_triggered = false;
		var scripts = GameObject.FindGameObjectWithTag("GlobalScripts");
		if (scripts) {
			_generateEnvironment = scripts.GetComponent<GenerateEnvironment>();
		}
		scripts = GameObject.FindGameObjectWithTag("Player");
		if (scripts) {
			_railsMovement = scripts.GetComponent<RailsMovement>();
		}
	}
	
	void OnTriggerEnter(Collider collision) {
		if (!_triggered && collision.gameObject.GetComponent<PlayerStats> ()) {
			_triggered = true;
			if (_generateEnvironment) {
				// TODO end of level
				Debug.Log ("Reached end of level");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
