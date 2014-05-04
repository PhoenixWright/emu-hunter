using UnityEngine;

public struct WeaponInfo
{
	public readonly string Name;
	public readonly string Description;
	public readonly Texture Texture;

	public WeaponInfo(string name, string description, Texture texture)
	{
		this.Name = name;
		this.Description = description;
		this.Texture = texture;
	}
}