using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailsMovement : MonoBehaviour {
	private CharacterController controller;
	private GenerateEnvironment environmentGenerator;
	private Vector3 next;

	// public members
	public float speed = 1.01F;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController>();
		this.environmentGenerator = GameObject.FindGameObjectWithTag ("GlobalScripts")
			.GetComponent<GenerateEnvironment> ();
		next = this.environmentGenerator.Next ();
	}

	Vector3 GetRotationForCamera() {
		float xCoord = Input.GetAxis ("Horizontal");
		float zCoord = Input.GetAxis ("Vertical");
		return new Vector3 (xCoord, 0.0F, zCoord);
	}

	Vector3 GetMovement(Transform transform, Vector3 nextPosition) {
		Vector3 directionToMove = nextPosition - transform.position;
		Vector3 normalizedDirection = directionToMove.normalized;
		/// Multiply the direction, speed, based on the framerate
		Vector3 movement = normalizedDirection * speed * Time.deltaTime;
		return movement;
	}

	Vector3 GetTargetPosition(Transform transform) {
		float difference = (transform.position - next).magnitude;
		if (difference < 1.0F) {
			next = this.environmentGenerator.Next ();
		}
		return next;
	}

	// Update is called once per frame
	void Update () {
		Vector3 nextPosition = GetTargetPosition(this.controller.transform);
		Vector3 movement = GetMovement (this.controller.transform, nextPosition);
		this.controller.Move (movement);
	}
}
