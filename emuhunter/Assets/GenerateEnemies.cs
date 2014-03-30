using UnityEngine;
using System.Collections;

public class GenerateEnemies : MonoBehaviour {
	private int emuSpawnCount = 20;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEmus (10));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private IEnumerator SpawnEmus(int seconds) {
		yield return new WaitForSeconds(seconds);
		
		addNormalEmu (new Vector3(0f,3f,0f));
		emuSpawnCount -= 1;
		if(emuSpawnCount > 0) {
			StartCoroutine(SpawnEmus (1));
		}
	}
	
	void addTinyEmu(Vector3 pos) {
		addEmu (pos, 0.3f, 0.1f, 3, 3);
	}
	
	void addBigEmu(Vector3 pos) {
		addEmu (pos, 0.05f, 0.3f, 15, 15);
	}
	
	void addNormalEmu(Vector3 pos) {
		addEmu (pos, 0.1f, 0.2f, 10, 10);
	}
	
	void addTankEmu(Vector3 pos) {
		addEmu (pos, 0.05f, 0.5f, 25, 5);
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
}
