using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovWeapon : Weapon {

	public EnemyDamager damager;
	public Projectile projectile;

	public Transform holder;
    public Transform spawnLocation;

	private float rotationSpeed = 5f;
	private float shotCounter;

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

        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            holder.gameObject.SetActive(true);

            if (Input.GetButton("Right Shoulder"))
            {
                shotCounter = stats[weaponLevel].timeBetweenAttacks;

                Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation).gameObject.SetActive(true);

            }
        }
        else
        {
            holder.gameObject.SetActive(false);
        }
    }

	public void SetStats()
	{
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.timeBetweenDamage = stats[weaponLevel].timeBetweenDamage;
        projectile.moveSpeed = stats[weaponLevel].speed;
        projectile.lifeTime = stats[weaponLevel].throwDistance;   
        shotCounter = 0f;
    }
}
