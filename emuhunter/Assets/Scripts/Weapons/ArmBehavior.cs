using UnityEngine;
using System.Collections;

public class ArmBehavior : MonoBehaviour {
	private Weapon currentWeapon = null;

	// Use this for initialization
	void Start () {
		Debug.Log("Loading arm...");
		Equip (Instantiate( Resources.Load("Gun"), this.transform.position, this.transform.rotation ) as GameObject);
		Equip (null);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if(currentWeapon == null) {
				Debug.Log("No weapons is currently selected!");
			} else {
				currentWeapon.Attack();
			}
		}
	}

	public void Equip(GameObject item) {
		Debug.Log ("Equipping " + item);
		if (transform.childCount > 0) {
			transform.DetachChildren();
		}

		if (item == null) {
			Debug.Log ("No weapon equipped. Defautling to fist.");
			currentWeapon = null;
			return;
		}

		item.transform.parent = transform;
		currentWeapon = (Weapon)item.GetComponent(typeof(Weapon));
	}

}
