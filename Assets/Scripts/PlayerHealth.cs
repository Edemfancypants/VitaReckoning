using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public static PlayerHealth instance;

	private void Awake()
	{
		instance = this;
	}

	public float currentHealth, maxHealth;
	public Slider healthSlider;

	public GameObject deathEffect;

	void Start () 
	{
		maxHealth = PlayerStatLogic.instance.health[0].value;

		currentHealth = maxHealth;

		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}
	
	void Update () 
	{
		
	}

	public void TakeDamage(float damageToTake) 
	{ 
		currentHealth -= damageToTake;

		if (currentHealth <= 0)
		{
			gameObject.SetActive(false);

			LevelManager.instance.EndLevel();

			Instantiate(deathEffect, transform.position, transform.rotation);

			SFXLogic.Instance.PlaySFX(3);
        }

        healthSlider.value = currentHealth;
	}

	public void AddHealth(float HealAmount)
	{
		currentHealth += HealAmount;

		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}

        healthSlider.value = currentHealth;
    }
}
