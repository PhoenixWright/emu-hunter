using UnityEngine;
using System.Collections;

public class GenerateEnemies : MonoBehaviour {
	private int emuSpawnCount = 20;
	private GenerateEnvironment _generateEnvironment;
	// Use this for initialization
	void Start () {
		var scripts = GameObject.FindGameObjectWithTag("GlobalScripts");
		if (scripts) {
			_generateEnvironment = scripts.GetComponent<GenerateEnvironment>();
		}

		StartCoroutine(SpawnEmus (8));
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private IEnumerator SpawnEmus(int seconds) {
		yield return new WaitForSeconds(seconds);

		Vector3 where = new Vector3 ();
		if (_generateEnvironment)
			where = _generateEnvironment.FrontSpawnPoint;
		switch (Random.Range (0, 4)) {
		case 0:
			addTinyEmu(where);
			break;
		case 1:
			addNormalEmu(where);
			break;
		case 2:
			addNormalEmu(where);
			break;
		case 3:
			addNormalEmu(where);
			break;
		}
		StartCoroutine(SpawnEmus (1));
	}
	
	void addTinyEmu(Vector3 pos) {
		addEmu (pos, 0.3f, 0.1f, 1, 3);
	}
	
	void addBigEmu(Vector3 pos) {
		addEmu (pos, 0.05f, 0.3f, 15, 15);
	}
	
	void addNormalEmu(Vector3 pos) {
		addEmu (pos, 0.1f, 0.2f, 5, 10);
	}
	
	void addTankEmu(Vector3 pos) {
		addEmu (pos, 0.05f, 0.5f, 25, 5);
	}
	
	void addEmu(Vector3 pos, float speed, float size, int health, int attack) {
		/// The instantiate call above automatically adds the Emu to the scene.
		new EmuBuilder ()
			.withPosition (pos)
			.withLocalScale(new Vector3(size, (float)(2.0 * size), size))
			.withSpeed(speed)
			.withAttack (attack)
			.withHealth (health)
			.build ();
	}
}
