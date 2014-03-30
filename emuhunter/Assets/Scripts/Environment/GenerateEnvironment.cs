using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateEnvironment : MonoBehaviour {

	private GenerateLevel _levelGenerator;
	private Queue<GameObject> _corridor;
	private Vector3 _where;

	// Use this for initialization
	void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
	}

	public Vector3 Next ()
	{
		Vector3 direction = new Vector3();
		var last = _levelGenerator.Path.Peek(); // Must be executed before Next()
		var next = _levelGenerator.Next();
		AppendCorridorSegment(next, last);

		string name = _corridor.Peek ().name;
		while (name != "Corner") {
			var obj = _corridor.Dequeue();
			Destroy(obj);
		}

		direction = next;
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
			Debug.Log ("Creating corner at " + location + " with rotation " + obj.transform.rotation);
			_corridor.Enqueue(obj);
			location += p.normalized * 10;
		}
		for (int i = 0; i < p.magnitude; ++i, location += p.normalized * 10) {
			var obj = (GameObject)Instantiate(Resources.Load("Hallway"));
			var newRotation = Quaternion.LookRotation (obj.transform.position).eulerAngles;
			if (p.normalized == Vector3.forward || p.normalized == Vector3.back)
				newRotation.y = 0;
			else
				newRotation.y = 90;
			Debug.Log ("Normal: " + p.normalized + ", Position: " + obj.transform.position + ", Angle: " + newRotation.y);
			obj.transform.rotation = Quaternion.Euler (newRotation);
			obj.transform.position = location;
			Debug.Log ("Creating hallway at " + location + " with rotation " + obj.transform.rotation);
			_corridor.Enqueue(obj);
		}
		_where = location;
	}
}
