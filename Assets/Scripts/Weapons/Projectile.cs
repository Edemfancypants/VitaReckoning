using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public EnemyDamager damager;

	public float moveSpeed, lifeTime;
	public bool isGrenade;
	
	void Update () 
	{
		transform.position += transform.up * moveSpeed * Time.deltaTime;

		if (isGrenade == true)
		{
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Instantiate(damager, transform.position, Quaternion.identity).gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
	}

	public void OnTriggerEnter2D (Collider2D collision)
	{
		if (isGrenade == true)
		{
			if (collision.tag == "Enemy")
			{ 
				Instantiate(damager, transform.position, Quaternion.identity).gameObject.SetActive(true);
				Destroy(gameObject);
			}
		}
	}
}
