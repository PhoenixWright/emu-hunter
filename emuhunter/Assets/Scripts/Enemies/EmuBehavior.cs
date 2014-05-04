using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmuBehavior : MonoBehaviour {
	/**
	 * Immutable members
	 */
	public GameState gameState { private get; set; }

	/**
	 * Mutable members
	 */
	public int attack { private get; set; } // damage done by emu
	public int health { private get; set; }

	private List<string> explosionTypes = new List<string>();

	void Start() {
		explosionTypes.Add("Detonator-Base");
		explosionTypes.Add("Detonator-Chunks");
		explosionTypes.Add("Detonator-Crazysparks");
		explosionTypes.Add("Detonator-Ignitor");
		explosionTypes.Add("Detonator-Insanity");
		explosionTypes.Add("Detonator-MultiExample");
		explosionTypes.Add("Detonator-MushroomCloud");
		explosionTypes.Add("Detonator-Simple");
		explosionTypes.Add("Detonator-Sounds");
		explosionTypes.Add("Detonator-Spray");
		explosionTypes.Add("Detonator-Tiny");
		explosionTypes.Add("Detonator-Upwards");
		explosionTypes.Add("Detonator-Wide");
	}

	// Update is called once per frame
	void Update ()
	{
		if(transform.position.y < -100) {
			health = 0;
			MonoBehaviour.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// checking for Blood Rage!!

		// grab all components we want to try
		PlayerBehavior player = collision.gameObject.GetComponent<PlayerBehavior>();
		BulletStats bullet = collision.gameObject.GetComponent<BulletStats>();
		//Weapon weapon = etc.
		// goals
		// handle knockback
		// player damage
		// emu damage
		if (player) {
			Attack(player);
			EnemyMovements movements = this.GetComponent<EnemyMovements>();
			movements.AddKnockback();
		}
		else if (bullet) {
			Damage(bullet.damage);
		}
	}

	public void Damage(int amount) {
		bool rage = Camera.main.GetComponent<BloodRageLens>().rageEnabled;
		health -= rage ? 2 * amount : amount;
		if (health < 1) {
			gameState.EmuKilled();
			float bulletLifeTime = 1F;
			if(rage) {
				int n = Random.Range (5, 15);
				for(int i = 0; i < n; i++) {
					Vector3 velocityVector = new Vector3(Random.Range (-80.0f, 80.0f), Random.Range (-80.0f, 80.0f), Random.Range (-80.0f, 80.0f));
					Rigidbody instantiatedProjectile = ((GameObject)MonoBehaviour.Instantiate(Resources.Load("Bullet"))).GetComponent<Rigidbody>();
					instantiatedProjectile.velocity = transform.TransformDirection(velocityVector);
					MonoBehaviour.Destroy(instantiatedProjectile.gameObject, bulletLifeTime);
				}
				// explode
				int explosionIdx = (int)Random.Range(0, explosionTypes.Count);
				MonoBehaviour.Instantiate(Resources.Load(explosionTypes[explosionIdx]), transform.position, Quaternion.identity);
				SplitMeshIntoTriangles splitter = this.GetComponent<SplitMeshIntoTriangles>();
				if (splitter) {
					splitter.SplitMesh();
				}
			}
			MonoBehaviour.Destroy(this.gameObject);
		}
	}

	private void Attack(PlayerBehavior player)
	{
		player.health -= attack;
	}
}
