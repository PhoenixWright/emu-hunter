using UnityEngine;
using System.Collections;

public class BulletStats : MonoBehaviour {

	public int damage = 1;
	public bool explode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (explode) {
			MonoBehaviour.Instantiate(Resources.Load("Detonator-Upwards"), transform.position, Quaternion.identity);
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0F);
			int i = 0;
			while (i < hitColliders.Length) {
				EmuBehavior emuBehavior = hitColliders[i].gameObject.GetComponent<EmuBehavior>();
				if (emuBehavior) {
					emuBehavior.Damage(damage);
				}
				i++;
			}
		}
	}
}
