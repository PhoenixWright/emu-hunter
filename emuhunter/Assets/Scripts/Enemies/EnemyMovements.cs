using UnityEngine;
using System.Collections;

public class EnemyMovements : MonoBehaviour {
	public GameObject player;
	public float speed;
	private int knockbackLeft;

	private float tilt = 0.2f;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		knockbackLeft = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		var playerPos = player.transform.position;
		
		var frac = speed / Vector3.Distance (transform.position, playerPos);

		Vector3 move = Vector3.Lerp (transform.position, playerPos, frac);

		if (knockbackLeft == 0) {
			rigidbody.MovePosition (move);
		} else {
			var newPlace = transform.position + knockbackLeft * knockbackLeft * (transform.position - move);
			rigidbody.MovePosition (newPlace);
			knockbackLeft -= 1;
		}
	}

	public void AddKnockback () {
		knockbackLeft += 5;
	}
}
