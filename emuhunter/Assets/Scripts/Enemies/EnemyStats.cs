using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	GameState gameState;

	public int health;
	public int attack; // damage done by emu

	// Use this for initialization
	void Start () {
		var obj = GameObject.FindGameObjectWithTag("GlobalScripts");
		gameState = obj.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		// grab all components we want to try
		PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
		BulletStats bullet = collision.gameObject.GetComponent<BulletStats>();
		//Weapon weapon = etc.

		// goals
		// handle knockback
		// player damage
		// emu damage

		if (player) {
			Attack(player);
		}
		else if (bullet) {
			health -= bullet.damage;
			if (health < 1) {
				gameState.EmuKilled();
				Destroy(gameObject);
			}
		}

	}

	private void Attack(PlayerStats player) {
		player.health -= attack;
	}
}
