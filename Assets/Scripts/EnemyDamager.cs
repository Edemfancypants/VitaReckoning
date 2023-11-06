using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {

	[Header("Damage Settings")]
	public float damageAmount;

	[Header("Grow Settings")]
	public bool shouldGrow;
	public float lifeTime;
	public float growSpeed = 5f;
	private Vector3 targetSize;

	[Header("Knockback Settings")]
	public bool knockback;

	[Header("Destroy Parent Settings")]
	public bool destroyParent;

	[Header("Damage Over Time Settings")]
	public bool damageOverTime;
	public float timeBetweenDamage;
	private float damageCounter;

	[Header("Burn Settings")]
	public bool shouldBurn;
	public float burnDamage;
	public float burnTime;

	[Header("Destroy On Impact Settings")]
    public bool destroyOnImpact;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();

	void Start () 
	{
		if (shouldGrow == true)
		{
			targetSize = transform.localScale;
			transform.localScale = Vector3.zero;
		}
	}
	
	void Update () 
	{
		if (shouldGrow == true)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

			lifeTime -= Time.deltaTime;
			if (lifeTime <= 0)
			{
				targetSize = Vector3.zero;

				if (transform.localScale.x == 0f)
				{
					Destroy(gameObject);

					if (destroyParent == true)
					{
						Destroy(transform.parent.gameObject);
					}
				}
			}
		}
		else
		{
			lifeTime -= Time.deltaTime;
			if (lifeTime <= 0)
			{
				Destroy(gameObject);

                if (destroyParent == true)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
		}

		if (damageOverTime == true)
		{
			damageCounter -= Time.deltaTime;

			if (damageCounter <= 0)
			{
				damageCounter = timeBetweenDamage;

				for (int i = 0; i < enemiesInRange.Count; i++)
				{
					if (enemiesInRange[i] != null)
					{
						enemiesInRange[i].TakeDamage(damageAmount, knockback);
					}
					else
					{
						enemiesInRange.RemoveAt(i);
						i--;
					}
				}
			}
		}

	}

	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (damageOverTime == false)
		{
			if (collision.tag == "Enemy")
			{
				collision.GetComponent<EnemyController>().TakeDamage(damageAmount, knockback);

				if (destroyOnImpact)
				{
					Destroy(gameObject);
				}

                if (shouldBurn == true)
                {
                    if (Random.Range(0f, 1f) <= 0.2f)
                    {
						collision.GetComponent<EnemyController>().burnTime = burnTime;
						collision.GetComponent<EnemyController>().burnDamage = burnDamage;
                    }
                }
            }
		}
		else
		{
			if (collision.tag == "Enemy")
			{
				enemiesInRange.Add(collision.GetComponent<EnemyController>());
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (damageOverTime == true)
		{
			if (collision.tag == "Enemy")
			{
				enemiesInRange.Remove(collision.GetComponent<EnemyController>());
			}
		}
	}
}
