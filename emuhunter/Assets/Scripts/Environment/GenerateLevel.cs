using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel {
	
	public int SegmentMinimum = 1;
	public int SegmentMaximum = 10;
	public uint InitialSegmentCount = 2;
	public Queue<Vector3> Path {
		get { return _path; }
		private set { Path = value; }
	}

	private Queue<Vector3> _path; // int == segment length

	// Use this for initialization
	public GenerateLevel () {
		_path = new Queue<Vector3> ();
		// Initially generate segments
		for (uint i = 0; i < InitialSegmentCount; ++i) {
			AppendValidSegmentToPath();
		}
	}

	void AppendValidSegmentToPath () {
		var segment = GenerateSegment ();
		// Verify that new segment is not the same direction as the most recently generated segment
		while ((_path.Count > 0) && (segment.normalized == _path.Peek().normalized)) {
			segment = GenerateSegment ();
		}
		_path.Enqueue (segment);
	}

	// Create a new segment
	Vector3 GenerateSegment () {
		Vector3 direction = new Vector3(); // Some random direction
		if (_path.Count > 0) {
			switch (Random.Range (0, 4)) {
			case 0:
					direction = Vector3.forward;
					break;
			case 1:
					direction = Vector3.right;
					break;
			case 2:
					direction = Vector3.back;
					break;
			case 3:
					direction = Vector3.left;
					break;
			}
		} else {
			direction = Vector3.forward;
		}
		direction *= Random.Range (SegmentMinimum, SegmentMaximum); // Some random length
		return direction;
	}

	public Vector3 Next() {
		AppendValidSegmentToPath ();
		return _path.Dequeue();
	}

}
