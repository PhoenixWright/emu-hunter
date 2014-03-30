using UnityEngine;
using System.Collections;

public class EnemyMovements : MonoBehaviour {
	public GameObject player;
	public float speed;
	private int knockbackLeft;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		knockbackLeft = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		// TODO: move this into a coroutine that is time dependent and uses smaller values
	
		// goals
		// 1. turn towards player
		// 2. move slowly and ominously towards player
		// 3. get clicked and die

		var playerPos = player.transform.position;
		//transform.LookAt (playerPos);
		var newRotation = Quaternion.LookRotation (playerPos - transform.position).eulerAngles;
		newRotation.x = 90;
		transform.rotation = Quaternion.Euler (newRotation);
		
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
