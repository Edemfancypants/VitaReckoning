using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineTrigger : MonoBehaviour {

    public GameObject landmineDamager;
    public float armTime;

    private CircleCollider2D triggerCollider;

    void Start()
    {
        armTime = LandmineWeapon.Instance.armTime;

        triggerCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(ArmTime());
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            landmineDamager.gameObject.SetActive(true);
        }
    }

    private IEnumerator ArmTime ()
    {
        yield return new WaitForSeconds(armTime);
        triggerCollider.enabled = true;
    }
}
