using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon {

	public EnemyDamager damager;

	public Transform holder;

	private float rotationSpeed = 5f;
    private float spawnCounter;

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

        Vector3 joystickInputDirection = new Vector3(Input.GetAxisRaw("Right Stick Vertical"), Input.GetAxisRaw("Right Stick Horizontal"), 0);

        if (joystickInputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, joystickInputDirection);

            holder.rotation = Quaternion.Slerp(holder.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].timeBetweenAttacks;

            Instantiate(damager, holder.transform.position, holder.transform.rotation, holder).gameObject.SetActive(true);

        }
    }

	public void SetStats()
	{
        damager.damageAmount = stats[weaponLevel].damage;   
        damager.lifeTime = stats[weaponLevel].duration;   
    }
}
