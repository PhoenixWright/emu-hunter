using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailsMovement : MonoBehaviour {
	private Queue<Vector3> positions = new Queue<Vector3>();
	private CharacterController controller;
	private Vector3 next;

	// public members
	public float speed = 1.01F;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController>();

		positions.Enqueue (new Vector3(1.0f, 1.0f, 0.0f));
		positions.Enqueue (new Vector3(0.0f, 1.0f, 10.0f));
		positions.Enqueue (new Vector3(10.0f, 1.0f, 10.0f));
		positions.Enqueue (new Vector3(0.0f, 1.0f, 0.0f));
		next = positions.Dequeue ();
	}

	Vector3 GetRotationForCamera() {
		float xCoord = Input.GetAxis ("Horizontal");
		float zCoord = Input.GetAxis ("Vertical");
		return new Vector3 (xCoord, 0.0F, zCoord);
	}

	Vector3 GetMovement(Transform transform, Vector3 nextPosition) {
		Vector3 directionToMove = nextPosition - transform.position;
		Vector3 normalizedDirection = directionToMove.normalized;
		Vector3 movement = normalizedDirection * speed * Time.deltaTime;
		return movement;
	}

	// THIS will be the fn I call of Johns
	Vector3 GetNextPosition(Transform transform) {
		float difference = (transform.position - next).magnitude;
		Debug.Log ("difference '" + difference + "'");
		if (difference < 1.0F && positions.Count > 0) {
			Debug.LogWarning ("next");
			next = positions.Dequeue();
		} else {
			Debug.LogWarning ("same");
		}
		return next;
	}

	// Update is called once per frame
	void Update () {
		Vector3 nextPosition = GetNextPosition(this.controller.transform);
		Vector3 movement = GetMovement (this.controller.transform, nextPosition);
		Debug.Log ("pos '" + this.controller.transform.position + "'");
		this.controller.Move (movement);
	}
}
