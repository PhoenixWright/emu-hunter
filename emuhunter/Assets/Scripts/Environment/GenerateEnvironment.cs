using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateEnvironment : MonoBehaviour {

	private GenerateLevel _levelGenerator;
	private Queue<GameObject> _corridor;

	// Use this for initialization
	void Start () {
		_corridor = new Queue<GameObject> ();
		_levelGenerator = new GenerateLevel();
		Vector3? last = null;
		foreach (var p in _levelGenerator.Path) {
			AppendCorridorSegment(p, last);
			last = p;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public Vector3 Next ()
	{
		Vector3 direction = new Vector3();
		if (_levelGenerator) {
			var last = _levelGenerator.Path.Peek(); // Must be executed before Next()
			var next = _levelGenerator.Next();
			AppendCorridorSegment(next, last);
			// TODO: Remove unused corridor game objects
			direction = next;
		}
		return direction;
	}
	
	void AppendCorridorSegment (Vector3 p, Vector3? lastDir)
	{
		if (_levelGenerator) {
			Vector3 location = new Vector3();
			if (_corridor.Count > 0) {
				var last = _corridor.Peek();
				location = last.transform.position;
			}
			int corner_y_rot = 0;
			if (lastDir.HasValue) {
				if (p.normalized == Vector3.forward) {
					corner_y_rot = lastDir.Value == Vector3.right ? 90 : -180;
				} else if (p.normalized == Vector3.back) {
					corner_y_rot = lastDir.Value == Vector3.left ? -90 : 0;
				} else if (p.normalized == Vector3.left) {
					corner_y_rot = lastDir.Value == Vector3.forward ? 0 : 90;
				} else if (p.normalized == Vector3.right) {
					corner_y_rot = lastDir.Value == Vector3.forward ? -90 : -180;
				}
				var obj = (GameObject)Instantiate(Resources.Load("Corner"));
				obj.transform.TransformPoint(location);
				obj.transform.Rotate(new Vector3(0, corner_y_rot, 0));
				_corridor.Enqueue(obj);
				location += p.normalized * 10;
			}
			for (int i = 0; i < p.magnitude; ++i, location += p.normalized * 10) {
				var obj = (GameObject)Instantiate(Resources.Load("Hallway"));
				obj.transform.Rotate(new Vector3(0, Vector3.Angle(obj.transform.position, location.normalized + obj.transform.position), 0));
				obj.transform.TransformPoint(location);
				_corridor.Enqueue(obj);
			}
		}
	}
}
