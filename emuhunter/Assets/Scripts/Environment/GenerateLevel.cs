using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel : MonoBehaviour {
	
	public int SegmentMinimum = 1;
	public int SegmentMaximum = 10;
	public uint InitialSegmentCount = 2;

	private enum Direction {
		NorthDirection = 0,
		EastDirection,
		SouthDirection,
		WestDirection,
		DirectionMax
	};

	private Queue<KeyValuePair<Direction, int>> _path; // int == segment length
	private GameObject _camera;

	// Use this for initialization
	void Start () {
		_path = new Queue<KeyValuePair<Direction, int>> ();
		_camera = GameObject.Find ("FirstPersonController");
		// Initially generate segments
		for (uint i = 0; i < InitialSegmentCount; ++i) {
			AppendValidSegmentToPath();
		}
	}
	
	// Update is called once per frame
	void Update () {
		_camera.collider.
	}

	void AppendValidSegmentToPath () {
		var segment = GenerateSegment ();
		// Verify that new segment is not the same direction as the most recently generated segment
		while ((_path.Count == 0) || (segment.Key != _path.Peek ().Key)) {
			segment = GenerateSegment ();
		}
		_path.Enqueue (segment);
	}

	// Create a new segment
	KeyValuePair<Direction, int> GenerateSegment () {
		return new KeyValuePair<Direction, int> (
			(Direction)Random.Range((int)Direction.NorthDirection, (int)Direction.DirectionMax), // Some random direction
			Random.Range(SegmentMinimum, SegmentMaximum) // Some random length
			); 
	}
}
