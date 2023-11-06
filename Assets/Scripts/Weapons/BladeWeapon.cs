using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeWeapon : Weapon {

	public EnemyDamager damager;
	public EnemyDamager blade;
    public Transform holder;

    public float rotateSpeed;

    private float attackCounter, direction;

	void Start () 
	{
		SetStats();
	}
	
	void Update () 
	{
        direction = Input.GetAxisRaw("Horizontal");

        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

		attackCounter -= Time.deltaTime;
		if (attackCounter <= 0)
		{
			attackCounter = stats[weaponLevel].timeBetweenAttacks;

            if (direction != 0 && blade == null)
            {
                if (direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }

            if (stats[weaponLevel].amount == 1)
            {
                blade = Instantiate(damager, damager.transform.position, damager.transform.rotation, holder);
                blade.gameObject.SetActive(true);
            }
            else
            {
                Instantiate(damager, damager.transform.position, damager.transform.rotation, holder).gameObject.SetActive(true);
            }

            for (int i = 1; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f,damager.transform.rotation.eulerAngles.z + rot), holder).gameObject.SetActive(true);
            }

            SFXLogic.Instance.PlaySFXPitched(9);
        }

        if (blade != null && attackCounter >= stats[weaponLevel].timeBetweenAttacks - 0.25f)
        {
            if (direction > 0)
            {
                blade.transform.rotation = Quaternion.identity;
            }
            else
            {
                blade.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }

        if (stats[weaponLevel].amount >= 2)
        {
            holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));
        }
    }

	void SetStats()
	{
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
		attackCounter = 0f;
    }
}
