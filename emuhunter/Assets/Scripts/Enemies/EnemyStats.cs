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
		// checking for Blood Rage!!
		
		bool rage = Camera.main.GetComponent<BloodRageLens>().rageEnabled;
		
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
			health -= rage ? 2 * bullet.damage : bullet.damage;

			if (health < 1) {
				gameState.EmuKilled();
				
				float bulletLifeTime = 1F;
				if(rage) {
					int n = Random.Range (5, 15);
					for(int i = 0; i < n; i++) {
						Vector3 velocityVector = new Vector3(Random.Range (-80.0f, 80.0f), Random.Range (-80.0f, 80.0f), Random.Range (-80.0f, 80.0f));
						Rigidbody instantiatedProjectile = ((GameObject)Instantiate(Resources.Load("Bullet"))).GetComponent<Rigidbody>();
						instantiatedProjectile.velocity = transform.TransformDirection(velocityVector);
						Destroy(instantiatedProjectile.gameObject, bulletLifeTime);
					}

					// explode
					GameObject explosion = (GameObject)Instantiate(Resources.Load("Detonator-Upwards"), transform.position, Quaternion.identity);
					SplitMeshIntoTriangles splitter = GetComponent<SplitMeshIntoTriangles>();
					if (splitter) {
						splitter.SplitMesh();
					}
				}

				Destroy(gameObject);
			}
		}

	}

	private void Attack(PlayerStats player) {
		player.health -= attack;
	}
}
