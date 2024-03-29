﻿using UnityEngine;
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
		}
		scripts = GameObject.FindGameObjectWithTag("Player");
		if (scripts) {
			_railsMovement = scripts.GetComponent<RailsMovement>();
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (!_cornerTriggered && collision.gameObject.GetComponent<PlayerBehavior> ()) {
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
