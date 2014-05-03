using UnityEngine;
using System.Collections;

public class EmuBehavior : MonoBehaviour {
	public Emu emu { private get; set; }
	
	// Update is called once per frame
	void Update ()
	{
		// Delegate!
		emu.Update ();
	}

	void OnCollisionEnter(Collision collision)
	{
		// Delegate!
		emu.OnCollisionEnter (collision);
	}
}
