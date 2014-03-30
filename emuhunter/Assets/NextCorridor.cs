using UnityEngine;
using System.Collections;

public class NextCorridor : MonoBehaviour {

	private bool _cornerTriggered;
	private GenerateEnvironment _generateEnvironment;
	private RailsMovement _railsMovement;

	// Use this for initialization
	void Start () {
		_cornerTriggered = false;
		var scripts = GameObject.FindGameObjectWithTag("GlobalScripts");
		if (scripts) {
			_generateEnvironment = scripts.GetComponent<GenerateEnvironment>();
			_railsMovement = scripts.GetComponent<RailsMovement>();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<PlayerStats> ()) {
			_cornerTriggered = true;
			if (_generateEnvironment) {
				var nextWayPoint = _generateEnvironment.Next();
				if (_railsMovement) {
					_railsMovement.AddWaypoint(nextWayPoint);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
