using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBehavior : MonoBehaviour
{
	private IList<Weapon> weapons = new List<Weapon> ();
	private Weapon normalGun;
	private Weapon axeGun;
	private Weapon bowGun;
	private Weapon rocketGun;

	private Weapon equippedWeapon;

	void Start()
	{
		weapons.Add (Camera.main.GetComponent<NormalGun> ());
		weapons.Add (Camera.main.GetComponent<AxeGun> ());
		weapons.Add (Camera.main.GetComponent<BowGun> ());
		weapons.Add (Camera.main.GetComponent<RocketGun> ());

		equippedWeapon = Camera.main.GetComponent<NormalGun> ();
		equippedWeapon.enabled = true;
	}

	void Update()
	{
		if (Time.timeScale <= 0) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			equippedWeapon.enabled = false;
			equippedWeapon = Camera.main.GetComponent<NormalGun>();
		}
		else if (Input.GetKeyDown (KeyCode.A)) {
			equippedWeapon.enabled = false;
			equippedWeapon = Camera.main.GetComponent<AxeGun>();
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			equippedWeapon.enabled = false;
			equippedWeapon = Camera.main.GetComponent<BowGun>();
		}
		else if (Input.GetKeyDown(KeyCode.D)) {
			equippedWeapon.enabled = false;
			equippedWeapon = Camera.main.GetComponent<RocketGun>();
		}
		equippedWeapon.enabled = true;
	}
}