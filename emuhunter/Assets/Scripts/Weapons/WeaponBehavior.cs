using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponBehavior : MonoBehaviour
{
	private Weapon normalGun;
	private Weapon axeGun;
	private Weapon bowGun;
	private Weapon rocketGun;
	private Weapon emuGun;
	private IList<Weapon> weapons = new List<Weapon> ();
	private Dictionary<KeyCode, Weapon> keyWeaponMap;

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
		
		keyWeaponMap = new Dictionary<KeyCode, Weapon>() {
			{KeyCode.Alpha1, normalGun},
			{KeyCode.Alpha2, axeGun},
			{KeyCode.Alpha3, bowGun},
			{KeyCode.Alpha4, rocketGun},
			{KeyCode.Alpha5, emuGun},
		};

		equippedWeapon = normalGun;
	}

	void Update()
	{
		if (Time.timeScale <= 0) {
			return;
		}
		
		foreach(var pair in keyWeaponMap) {
			if(Input.GetKeyDown(pair.Key)) {
				EquipWeapon(pair.Key);
				break;
			}
		}
	}

	public IEnumerable<WeaponInfo> WeaponInfos()
	{
		return new List<WeaponInfo>() {
			NormalGun.GetInfo(KeyCode.Alpha1),
			AxeGun.GetInfo(KeyCode.Alpha2),
			BowGun.GetInfo(KeyCode.Alpha3),
			RocketGun.GetInfo(KeyCode.Alpha4),
			EmuGun.GetInfo(KeyCode.Alpha5)
		};
	}

	public void EquipWeapon(KeyCode index)
	{
		equippedWeapon.enabled = false;
		equippedWeapon = keyWeaponMap[index];
		equippedWeapon.enabled = true;
	}
}