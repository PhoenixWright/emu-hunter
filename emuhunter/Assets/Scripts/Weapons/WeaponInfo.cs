using UnityEngine;

public struct WeaponInfo
{
	public readonly string Name;
	public readonly string Description;
	public readonly Texture Texture;
	public readonly KeyCode Index;

	public WeaponInfo(string name, string description, Texture texture, KeyCode index)
	{
		this.Index = index;
		this.Name = name;
		this.Description = description;
		this.Texture = texture;
	}
}