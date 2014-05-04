using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AxeGun : Weapon {
	/// constants
	public Vector3 velocityVector = new Vector3(0.0F, 0.0F, 100.0F);
	private float bulletLifeTime = 3F;
	private float lightIntensity = 10.0F;
	private Color lightColor = Color.red + Color.yellow;
	
	private SpriteRenderer sprite;
	
	
	
	// Use this for initialization
	void Start () {
		textures = new List<Texture>();
		textures.Add((Texture)Resources.Load("AxeGun/AXEGA0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGB0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGC0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGD0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGE0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGF0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGG0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGH0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGI0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGJ0", typeof(Texture)));
		textures.Add((Texture)Resources.Load("AxeGun/AXEGK0", typeof(Texture)));
		texture = textures[0];
	}
	
	override public void Attack() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0F);
		int i = 0;
		while (i < hitColliders.Length) {
			EmuBehavior emuBehavior = hitColliders[i].gameObject.GetComponent<EmuBehavior>();
			if (emuBehavior) {
				emuBehavior.Damage(100);
			}
			i++;
		}

		StartCoroutine(PlayAnimation());
	}
	
}
