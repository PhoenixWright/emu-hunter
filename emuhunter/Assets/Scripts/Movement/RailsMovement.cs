using UnityEngine;
using System.Collections;

public class RailsMovement : MonoBehaviour {
	private CharacterController controller;
	private Vector3 movement;

	// public members
	public float speed = 4.01F;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController>();
	}

	Vector3 GetDirectionToMove(float xAxis, float yAxis) {
		if (!controller.isGrounded) {
			return Vector3.zero;
		}
		//Vector3 direction = new Vector3(xAxis, 0, yAxis);
		Vector3 direction = this.transform.forward;
		direction.x += xAxis;
		direction.y += yAxis;
		Debug.Log ("basic '" + direction + "'");
		direction = this.transform.TransformDirection (direction);
		Debug.Log ("transformDirection '" + direction + "'");

		//Multiply it by speed.
		direction *= speed;
		Debug.Log ("with '" + direction + "'");
		return direction * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
		float xCoord = Input.GetAxis ("Horizontal");
		float yCoord = Input.GetAxis ("Vertical");

		Vector3 direction = GetDirectionToMove (xCoord, yCoord);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		this.controller.Move (direction);
	}
}
