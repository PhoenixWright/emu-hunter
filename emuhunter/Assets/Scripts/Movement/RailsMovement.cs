using UnityEngine;
using System.Collections;

public class RailsMovement : MonoBehaviour {
	private CharacterController playerController;
	private Vector3 moveDirection;
	private float horizontalAxis;
	private float verticalAxis;

	// public members
	public float speed = 6.0F;

	// Use this for initialization
	void Start () {
		this.playerController = GetComponent<CharacterController>();
		this.moveDirection = Vector3.one;
		//this.horizontalAxis = Input.GetAxis ("Horizontal");
		//this.verticalAxis = Input.GetAxis ("Vertical");
	}

	// Update is called once per frame
	void Update () {
		this.horizontalAxis = Input.GetAxis ("Horizontal");
		this.verticalAxis = Input.GetAxis ("Vertical");

		Vector3 direction = new Vector3(this.horizontalAxis, 0, this.verticalAxis);
		this.moveDirection = this.transform.TransformDirection (direction);
		Debug.LogWarning (this.moveDirection);

		//Multiply it by speed.
		this.moveDirection *= speed;
		this.playerController.Move (moveDirection);// * Time.deltaTime);

		Debug.Log (this.moveDirection.x + " " + this.moveDirection.y + " " + this.moveDirection.z);
	}
}
