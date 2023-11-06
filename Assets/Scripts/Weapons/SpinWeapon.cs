using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon {

	[Header("Fireball")]
	public float rotateSpeed;

	[Header("Spawn Settings")]
	public Transform holder;
	public Transform fireballSpawn;
	public float spawnTime;
	private float spawnCounter;

	[Header("Damager")]
	public EnemyDamager damager;

	void Start () 
	{
		SetStats();
	}
	
	void Update () 
	{
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));


        spawnCounter -= Time.deltaTime;
		if (spawnCounter <= 0)
		{
			spawnCounter = spawnTime;

			for (int i = 0; i < stats[weaponLevel].amount; i++)
			{
				float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(fireballSpawn, holder.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }

            SFXLogic.Instance.PlaySFXPitched(8);
        }

        if (statsUpdated == true)
		{
			statsUpdated = false;
			SetStats();
		}
	}

	public void SetStats()
	{
		damager.damageAmount = stats[weaponLevel].damage;   //set weapon damage
		transform.localScale = Vector3.one * stats[weaponLevel].range;   //set weapon range
		spawnTime = stats[weaponLevel].timeBetweenAttacks;   //time between weapon spawns
		damager.lifeTime = stats[weaponLevel].duration;   //set lifetime of weapon
		damager.shouldBurn = stats[weaponLevel].shouldBurn;
		damager.burnTime = stats[weaponLevel].burnTime;
		damager.burnDamage = stats[weaponLevel].burnDamage;
		spawnCounter = 0f;
	}
}
