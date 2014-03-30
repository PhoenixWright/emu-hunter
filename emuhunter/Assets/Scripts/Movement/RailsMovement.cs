using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailsMovement : MonoBehaviour {
	private CharacterController controller;
	private Vector3 nextWaypoint;

	private Queue<Vector3> waypoints = new Queue<Vector3>();

	// public members
	public float speed = 1.01F;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController>();
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
		return normalizedDirection * speed * Time.deltaTime;
	}

	Vector3 GetTargetPosition(Transform transform) {
		float difference = (transform.position - nextWaypoint).magnitude;
		if (difference < 1.0F) {
			nextWaypoint = waypoints.Dequeue();
		}
		return nextWaypoint;
	}

	public void AddWaypoint(Vector3 waypoint) {
		waypoints.Enqueue(waypoint);
	}

	// Update is called once per frame
	void Update () {
		Vector3 nextPosition = GetTargetPosition(this.controller.transform);
		Vector3 movement = GetMovement(this.controller.transform, nextPosition);
		this.controller.Move (movement);
	}
}
