using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[Header("Movement settings")]
	public Rigidbody2D body;
	public float moveSpeed;
	private Transform target;

	[Header("Damage Settings")]
	public float damage;
	public float hitWaitTime = 1f;
	private float hitCounter;

	[Header("Health Settings")]
	public float health = 5f;

	[Header("Boss Settings")]
	public bool isBoss;
	public GameObject deathEffect;

	[Header("Knockback Settings")]
	public float knockbackTime = .5f;
	private float knockbackCounter;

	[Header("Burn Settings")]
    [HideInInspector]
    public float burnDamage;
    [HideInInspector]
    public float burnTime;
	private float timeBetweenBurn = 1f;
	private float burnCounter;

	[Header("Experience Settings")]
	public int expDrop = 1;

	[Header("Coin Settings")]
	public int coinValue = 1;
	public float coinDropRate = .5f;

	[Header("Gem Settings")]
	public int gemValue = 1;
	public float gemDropRate = .1f;

	[Header("Medkit Settings")]
	public MedkitPickup medkit;
	public float medkitDropChance = 0.05f;

	void Start()
	{
		target = PlayerHealth.instance.transform;
	}

	void Update()
	{
		if (PlayerController.instance.gameObject.activeSelf == true)
		{
			if (knockbackCounter > 0)
			{
				knockbackCounter -= Time.deltaTime;

				if (moveSpeed > 0)
				{
					moveSpeed = -moveSpeed * 2f;
				}

				if (knockbackCounter <= 0)
				{
					moveSpeed = Mathf.Abs(moveSpeed * .5f);
				}
			}

			burnTime -= Time.deltaTime;
            if (burnTime >= 0)
            {
                burnCounter -= Time.deltaTime;

                if (burnCounter <= 0)
                {
                    burnCounter = timeBetweenBurn;
					TakeDamage(burnDamage);
                }
            }

            body.velocity = (target.position - transform.position).normalized * moveSpeed;

			if (hitCounter > 0)
			{
				hitCounter -= Time.deltaTime;
			}
		}
		else
		{
			body.velocity = Vector2.zero;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && hitCounter <= 0)
		{
			PlayerHealth.instance.TakeDamage(damage);
			hitCounter = hitWaitTime;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && hitCounter <= 0)
		{
			PlayerHealth.instance.TakeDamage(damage);
			hitCounter = hitWaitTime;

		}
	}

	public void TakeDamage(float damageToTake)
	{
		health -= damageToTake;

		if (health <= 0f)
		{
			Destroy(gameObject);
			EnemySpawner.instance.enemiesKilled++;
			ExperienceLogic.instance.SpawnExp(transform.position, expDrop);

			if (Random.value <= coinDropRate)
			{
				CoinController.instance.DropCoin(transform.position, coinValue);
			}

			if (Random.value <= gemDropRate)
			{
				CoinController.instance.DropGem(transform.position, gemValue);
			}

			if (Random.value <= medkitDropChance)
			{
                Instantiate(medkit, transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f), Quaternion.identity);
            }

            if (isBoss == true)
			{
				EnemySpawner.instance.bossKilled = true;
				EnemySpawner.instance.waves[EnemySpawner.instance.currentWave].bossEnemy = null;
				EnemySpawner.instance.bossSpawned = false;
                Instantiate(deathEffect, transform.position, transform.rotation);

                Instantiate(medkit, transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f), Quaternion.identity);
            }

            SFXLogic.Instance.PlaySFXPitched(0);
		}
		else
		{
			SFXLogic.Instance.PlaySFXPitched(1);
        }

        DamageNumberLogic.instance.SpawnDamage(damageToTake, transform.position);
	}

	public void TakeDamage(float damageToTake, bool knockback)
	{
		TakeDamage(damageToTake);

		if (knockback == true)
		{
			knockbackCounter = knockbackTime;
		}
	}
}
