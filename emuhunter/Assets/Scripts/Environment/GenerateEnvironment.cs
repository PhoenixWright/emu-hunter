using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateEnvironment : MonoBehaviour {

	private GenerateLevel _levelGenerator;
	private Queue<GameObject> _corridor;
	private Vector3 _where;
	public Vector3 FrontSpawnPoint { get; private set; }
	public int LevelLimit { get; set; }

	// Use this for initialization
	void Start () {
		LevelLimit = 100;
		_corridor = new Queue<GameObject> ();
		_levelGenerator = new GenerateLevel();
		Vector3? last = null;
		_where = new Vector3 ();
		var scripts = GameObject.FindGameObjectWithTag("Player");
		var rails = scripts.GetComponent<RailsMovement> ();
		foreach (var p in _levelGenerator.Path) {
			AppendCorridorSegment(p, last);
			last = p;
			if (rails)
				rails.AddWaypoint(_where);
		}
		Debug.Log ("LevelLimit: " + LevelLimit);

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public Vector3 Next ()
	{
		Debug.Log ("Next..." + LevelLimit);
		if (LevelLimit > 0) {
		Debug.Log ("Generating new content...");
		var last = _levelGenerator.Last; // Must be executed before Next()
		var next = _levelGenerator.Next();
		AppendCorridorSegment(next, last);

		int corners = 0;
		foreach (var c in _corridor) {
			if (c.name.Contains("Corner"))
				++corners;
		}
		if (corners >= 4) {
			string name = _corridor.Peek ().name;
			do {
				var obj = _corridor.Dequeue ();
				Destroy(obj);
				name = _corridor.Peek ().name;
			} while (!name.Contains("Corner"));
		}
		}
		return _where;
	}
	
	void AppendCorridorSegment (Vector3 p, Vector3? lastDir)
	{
		Vector3 location = _where;
		int corner_y_rot = 0;
		if (lastDir.HasValue) {
			if (p.normalized == Vector3.forward) {
				corner_y_rot = ((lastDir.Value.normalized == Vector3.right) ? 180 : 270);
			} else if (p.normalized == Vector3.back) {
				corner_y_rot = ((lastDir.Value.normalized == Vector3.right) ? 90 : 0);
			} else if (p.normalized == Vector3.left) {
				corner_y_rot = ((lastDir.Value.normalized == Vector3.forward) ? 90 : 180);
			} else if (p.normalized == Vector3.right) {
				corner_y_rot = ((lastDir.Value.normalized == Vector3.forward) ? 0 : 270);
			}
			var obj = (GameObject)Instantiate(Resources.Load("Corner"));
			var newRotation = Quaternion.LookRotation (obj.transform.position).eulerAngles;
			newRotation.y = corner_y_rot;
			obj.transform.rotation = Quaternion.Euler (newRotation);
			obj.transform.position = location;
			_corridor.Enqueue(obj);
			location += p.normalized * 10;
		} else if (_corridor.Count == 0) {
			var obj = (GameObject)Instantiate(Resources.Load("End"));
			obj.transform.position = location;
			_corridor.Enqueue(obj);
			location += p.normalized * 10;
		}
		for (int i = 0; i < p.magnitude; ++i, location += p.normalized * 10, --LevelLimit) {
			var obj = (GameObject)Instantiate(Resources.Load("Hallway"));
			var newRotation = Quaternion.LookRotation (obj.transform.position).eulerAngles;
			if (p.normalized == Vector3.forward || p.normalized == Vector3.back)
				newRotation.y = 0;
			else
				newRotation.y = 90;
			obj.transform.rotation = Quaternion.Euler (newRotation);
			obj.transform.position = location;
			_corridor.Enqueue(obj);
			FrontSpawnPoint = obj.renderer.bounds.center;
			//Debug.Log("Front spawn point: " + FrontSpawnPoint);
		}
		if (LevelLimit <= 0) {
			corner_y_rot = 0;
			Debug.Log("Last: " + p);
			if (lastDir.HasValue)
				Debug.Log("Next: " + lastDir.Value);
			if (p.normalized == Vector3.forward) {
				corner_y_rot = 180;
			} else if (p.normalized == Vector3.back) {
				corner_y_rot = 0;
			} else if (p.normalized == Vector3.left) {
				corner_y_rot = 90;
			} else if (p.normalized == Vector3.right) {
				corner_y_rot = 270;
			}
			var obj = (GameObject)Instantiate(Resources.Load("LevelEnd"));
			var newRotation = Quaternion.LookRotation (obj.transform.position).eulerAngles;
			newRotation.y = corner_y_rot;
			obj.transform.rotation = Quaternion.Euler (newRotation);
			obj.transform.position = location;
		}

		_where = location;
	}
	
}
