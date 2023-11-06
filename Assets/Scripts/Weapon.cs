using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Weapon : MonoBehaviour {

	public string weaponName;

	[Header("Weapon Stats")]
	public List<WeaponStats> stats;
	public int weaponLevel;

	[HideInInspector]
	public bool statsUpdated;

	public Sprite Icon;

	public void LevelUp()
	{
		if (weaponLevel < stats.Count - 1)
		{
			weaponLevel++;

			statsUpdated = true;

			if (weaponLevel >= stats.Count - 1)
			{
				PlayerController.instance.fullyLevelledWeapons.Add(this);
				PlayerController.instance.assignedWeapons.Remove(this);
			}
		}
	}
}

[System.Serializable]
public class WeaponStats
{
	[Header("Base Settings")]
	public float speed;
	public float damage;
	public float range;
	public float timeBetweenAttacks;
	public float amount;
	public float duration;
    public string upgradeText;

    [Header("Fireball Burn Settings")]
	public float burnDamage;
	public float burnTime;
    public bool shouldBurn;

    [Header("Landmine Setting")]
	public float timeUntilActive;

	[Header("Molotov Settings")]
	public float throwDistance;
    public float timeBetweenDamage;
}
