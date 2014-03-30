using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health;
	private float endTime = -1.0f;
	private CameraShake cameraShake;

	private GameObject armLeft;
	private Weapon     weaponLeft;
	private GameObject armRight;

	// Use this for initialization
	void Start () {
		cameraShake = Camera.main.GetComponent<CameraShake>();
		armLeft = transform.FindChild ("ArmLeft").gameObject;
		armRight = transform.FindChild ("ArmRight").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0) {
			fireLeftArm ();
		}
	}

	private void fireLeftArm() {
		if(armLeft.transform.childCount == 0) { 
			Debug.Log ("Nothing on left arm.");
			return; 
		}
		
		if(weaponLeft != null) {
			Debug.Log ("No weapon equipped.");
			weaponLeft.Attack();
		}

	}

	private void equipLeftArm(Weapon item) {
		item.transform.parent = armLeft.transform;
		item.transform.localPosition = new Vector3(0.0F, 0.0F, 0.0F);
		weaponLeft = (Weapon)armLeft.transform.GetChild (0).GetComponent (typeof(Weapon));
		Debug.Log ("Equipped weapon: " + weaponLeft);
	}
	
	void OnGUI() {
		if (health < 1) {
			GUI.skin.label.fontSize = 120;
			GUI.Label(new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height), "GAME OVER");
			
			if(endTime == -1.0f) {
				Time.timeScale = 0;
				endTime = Time.realtimeSinceStartup;
			}
			
			if((endTime + 5.0f) < Time.realtimeSinceStartup) {
				Application.Quit();
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
		if (enemy) {
			if (cameraShake) {
				cameraShake.Shake();
			}
		}
	}

	void OnTriggerEnter(Collider collision) {
		Weapon weapon = collision.gameObject.GetComponent<Weapon> ();
		if (weapon) {
			equipLeftArm(weapon);	
		}	
	}

}
