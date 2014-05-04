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
			EquipWeapon(normalGun);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			EquipWeapon(axeGun);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			EquipWeapon(bowGun);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			EquipWeapon(rocketGun);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			EquipWeapon(emuGun);
		}
	}

	public IEnumerable<WeaponInfo> WeaponInfos()
	{
		return new List<WeaponInfo>() {
			NormalGun.GetInfo(),
			AxeGun.GetInfo(),
			BowGun.GetInfo(),
			RocketGun.GetInfo(),
			EmuGun.GetInfo()
		};
	}

	public void EquipWeapon(Weapon weapon)
	{
		equippedWeapon.enabled = false;
		equippedWeapon = weapon;
		equippedWeapon.enabled = true;
	}
}