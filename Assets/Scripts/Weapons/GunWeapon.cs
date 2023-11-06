using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunWeapon : Weapon {

	public EnemyDamager damager;
	public Projectile projectile;

    public Transform sprite;
	public Transform holder;
    public Transform spawnLocation;

    public int ammo;
    public TMP_Text ammoText;
    public Image ammoImage;
    private int maxAmmo;

	private float rotationSpeed = 5f;
	private float reloadTime, reloadCounter;

	void Start () 
	{
        ammoImage.sprite = Icon;

        ammo = Mathf.RoundToInt(stats[weaponLevel].amount);

        UIController.instance.ShowBulletAmmo();

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

        if (ammo > 0)
        {
            if (Input.GetButtonDown("Right Shoulder"))
            {
                ammo--;
                reloadCounter = reloadTime;
                Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation).gameObject.SetActive(true);
            }

            if (Input.GetButtonDown("Square") || Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo)
            {
                ammo = 0;
                reloadCounter = reloadTime;
            }
        }
        else if (ammo <= 0)
        {
            reloadCounter -= Time.deltaTime;
            if (reloadCounter <= 0)
            {
                reloadCounter = reloadTime;
                ammo = maxAmmo;
            }
        }

        ammoText.text = "Ammo: " + ammo;
    }

	public void SetStats()
	{
        damager.damageAmount = stats[weaponLevel].damage;   
        damager.lifeTime = stats[weaponLevel].duration;   
        projectile.moveSpeed = stats[weaponLevel].speed;
        reloadTime = stats[weaponLevel].timeBetweenAttacks;
        maxAmmo = Mathf.RoundToInt(stats[weaponLevel].amount);
        reloadCounter = 0f;
    }
}
