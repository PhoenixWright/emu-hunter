using UnityEngine;
using System.Collections;

public class EnemyMovements : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		Debug.Log ("Enemy start");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// goals
		// 1. turn towards player
		// 2. move slowly and ominously towards player
		// 3. get clicked and die
		var playerPos = player.transform.position;
		//transform.LookAt (playerPos);
		var newRotation = Quaternion.LookRotation (playerPos - transform.position).eulerAngles;
		newRotation.x = 90;
		transform.rotation = Quaternion.Euler (newRotation);
	}
}
