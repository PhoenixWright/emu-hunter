using UnityEngine;

public class EmuBuilder
{
	private int health;
	private int attack;
	private float speed;
	private Vector3 position;
	private Vector3 localScale ;

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
		this.localScale = localScale;
		return this;
	}

	public EmuBuilder withSpeed(float speed)
	{
		this.speed = speed;
		return this;
	}

	///  Normally you would return the object you are building here, but unit does not require us
	/// to do that. Calling Initialize automatically adds it to the scene.
	public void build() {
		var globalScripts = GameObject.FindGameObjectWithTag ("GlobalScripts");
		var loadedEnemyObject = Resources.Load ("Enemy");

		GameObject emu = (GameObject)MonoBehaviour.Instantiate (loadedEnemyObject);
		emu.transform.position = this.position;
		emu.transform.localScale = this.localScale;

		EmuBehavior emuBehavior = emu.GetComponent<EmuBehavior> ();
		emuBehavior.gameState = globalScripts.GetComponent<GameState> ();
		emuBehavior.attack = this.attack;
		emuBehavior.health = this.health;

		EnemyMovements moves = emu.GetComponent<EnemyMovements>();
		moves.speed = this.speed;
	}
}