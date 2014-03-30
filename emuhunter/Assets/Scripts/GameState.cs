using UnityEngine;
using System.Collections;

public enum GameModes {
	Normal,
	BloodRage,
}

public class GameState : MonoBehaviour
{
	public GameObject playerObject;
	public PlayerStats playerScript;
	public int emusDestroyed = 0;
	public GameModes gameMode = GameModes.Normal;

	private BloodRageLens bloodRage;
	private GUIStyle guiStyle;
	private int emuSpawnCount = 10;

	// Use this for initialization
	void Start ()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerScript = playerObject.GetComponent<PlayerStats>();
		bloodRage = Camera.main.GetComponent<BloodRageLens>();
		
		StartCoroutine(SpawnEmus (10));
	}
	
	private IEnumerator SpawnEmus(int seconds) {
		yield return new WaitForSeconds(seconds);
		
		addEmu (new Vector3(0f,3f,0f), 0.3f, 0.1f, 3, 3);
		emuSpawnCount -= 1;
		if(emuSpawnCount > 0) {
			StartCoroutine(SpawnEmus (3));
		}
		
		
	}

	// Update is called once per frame
	void Update ()
	{
	}
	
	void addEmu(Vector3 pos, float speed, float size, int health, int attack) {
		GameObject enemy = (GameObject)Instantiate(Resources.Load("Enemy"));
		Transform trans = enemy.GetComponent<Transform>();
		trans.position = pos;
		trans.localScale = new Vector3(size, (float)(2.0 * size), size);
		EnemyStats stats = enemy.GetComponent<EnemyStats>();
		stats.health = health;
		stats.attack = attack;
		EnemyMovements moves = enemy.GetComponent<EnemyMovements>();
		moves.speed = speed;
	}

	public void EmuKilled() {
		emusDestroyed += 1;
		if (emusDestroyed == 3) {
			bloodRage.Enable();
		}
	}
}
