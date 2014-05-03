using UnityEngine;
using System.Collections;

public class EmuBehavior : MonoBehaviour {
	private Emu emu;

	public int health;
	public int attack; // damage done by emu

	// Use this for initialization
	void Start ()
	{
		var obj = GameObject.FindGameObjectWithTag("GlobalScripts");
		GameState gameState = obj.GetComponent<GameState>();

		BloodSplatter bloodSplatter = GetComponent<BloodSplatter>();
		emu = new Emu (this, bloodSplatter, gameState, health, attack);
	}
	
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
