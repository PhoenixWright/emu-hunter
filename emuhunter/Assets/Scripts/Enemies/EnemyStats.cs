using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public int health;
	public int attack; // damage done by emu

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		// grab all components we want to try
		PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
		//Bullet bullet = collision.gameObject.GetComponent<>();
		//Weapon weapon = etc.

		// goals
		// handle knockback
		// player damage
		// emu damage

		if (player) {
			Attack(player);
		}
		//else if (bullet) {
			//  get hurn
		//}

	}

	private void Attack(PlayerStats player) {
		player.health -= attack;
	}
}
