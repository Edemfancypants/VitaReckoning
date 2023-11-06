using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : Weapon {

	public EnemyDamager damager;
	public Transform holder;

	private float spawnTime, spawnCounter;

	void Start () 
	{
		SetStats();
	}
	
	void Update () 
	{
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

		spawnCounter -= Time.deltaTime;
		if (spawnCounter <= 0f)
		{
			spawnCounter = spawnTime;
			Instantiate(damager, holder.transform.position, Quaternion.identity, holder.transform).gameObject.SetActive(true);

			SFXLogic.Instance.PlaySFXPitched(10);
        }
    }

	void SetStats()
	{
		damager.damageAmount = stats[weaponLevel].damage;
		damager.lifeTime = stats[weaponLevel].duration;
		damager.timeBetweenDamage = stats[weaponLevel].speed;
		damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
		spawnTime = stats[weaponLevel].timeBetweenAttacks;
		spawnCounter = 0;
    }
}
