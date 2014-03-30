using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	GameState gameState;

	public int health;
	public int attack; // damage done by emu

	private GroundSplatter splatter;

	// Use this for initialization
	void Start () {
		var obj = GameObject.FindGameObjectWithTag("GlobalScripts");
		gameState = obj.GetComponent<GameState>();

		splatter = GetComponent<GroundSplatter>();
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
			EnemyMovements movements = GetComponent<EnemyMovements>();
			movements.AddKnockback();
		}
		else if (bullet) {
			health -= bullet.damage;

			// blood splatter
			splatter.Splat();

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
