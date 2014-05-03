using UnityEngine;

public class Emu
{
	/**
	 * Immutable members
	 */
	private readonly Component selfUnityReference;
	private readonly Transform transform;
	private readonly GameState gameState;
	private readonly int attack; // damage done by emu

	/**
	 * Mutable members
	 */
	private int health;

	public Emu(Component unitySelfComponent, BloodSplatter bloodSplatter, GameState state, int health, int attack)
	{
		this.selfUnityReference = unitySelfComponent;
		this.transform = unitySelfComponent.transform;
		this.gameState = state;
		this.health = health;
		this.attack = attack;
	}

	public void Update ()
	{
		if(transform.position.y < -100) {
			health = 0;
			MonoBehaviour.Destroy(selfUnityReference.gameObject);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
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
			EnemyMovements movements = selfUnityReference.GetComponent<EnemyMovements>();
			movements.AddKnockback();
		}
		else if (bullet) {
			health -= rage ? 2 * bullet.damage : bullet.damage;
			if (health < 1) {
				gameState.EmuKilled();
				//splatter.Splat();
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
					MonoBehaviour.Instantiate(Resources.Load("Detonator-Upwards"), transform.position, Quaternion.identity);
					SplitMeshIntoTriangles splitter = selfUnityReference.GetComponent<SplitMeshIntoTriangles>();
					if (splitter) {
						splitter.SplitMesh();
					}
				}
				MonoBehaviour.Destroy(selfUnityReference.gameObject);
			}
		}
	}

	private void Attack(PlayerStats player)
	{
		player.health -= attack;
	}
}