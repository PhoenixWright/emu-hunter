using UnityEngine;

public class EmuBuilder
{
	private int health = 0;
	private int attack = 0;
	private Vector3 position = new Vector3();
	private Vector3 localScale = new Vector3();

	public EmuBuilder withAttack(int attack)
	{
		this.attack = attack;
		return this;
	}

	public EmuBuilder withHealth(int health)
	{
		this.health = health;
		return this;
	}

	public EmuBuilder withPosition(Vector3 position)
	{
		this.position = position;
		return this;
	}

	public EmuBuilder withLocalScale(Vector3 localScale)
	{
		localScale = localScale;
		return this;
	}

	public Emu build() {
		var obj = GameObject.FindGameObjectWithTag ("GlobalScripts");
		GameState gameState = obj.GetComponent<GameState> ();
		BloodSplatter bloodSplatter = obj.GetComponent<BloodSplatter>();
		EmuBehavior emuBehavior = new EmuBehavior ();
		emuBehavior.transform.position = this.position;
		Emu emu = new Emu (emuBehavior, bloodSplatter, gameState, this.health, this.attack);
		emuBehavior.emu = emu;
		return emu;
	}
}