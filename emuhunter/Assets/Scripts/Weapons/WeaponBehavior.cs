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
	private Weapon emuGun;

	private Weapon equippedWeapon;

	void Start()
	{
		normalGun = Camera.main.gameObject.AddComponent<NormalGun>();
		axeGun = Camera.main.gameObject.AddComponent<AxeGun>();
		axeGun.enabled = false;
		bowGun = Camera.main.gameObject.AddComponent<BowGun>();
		bowGun.enabled = false;
		rocketGun = Camera.main.gameObject.AddComponent<RocketGun>();
		rocketGun.enabled = false;
		emuGun = Camera.main.gameObject.AddComponent<EmuGun>();
		emuGun.enabled = false;

		equippedWeapon = normalGun;
	}

	void Update()
	{
		if (Time.timeScale <= 0) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			equippedWeapon.enabled = false;
			equippedWeapon = normalGun;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			equippedWeapon.enabled = false;
			equippedWeapon = axeGun;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			equippedWeapon.enabled = false;
			equippedWeapon = bowGun;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			equippedWeapon.enabled = false;
			equippedWeapon = rocketGun;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			equippedWeapon.enabled = false;
			equippedWeapon = emuGun;
		}

		equippedWeapon.enabled = true;
	}
}