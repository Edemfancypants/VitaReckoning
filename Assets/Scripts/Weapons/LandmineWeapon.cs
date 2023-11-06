using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandmineWeapon : Weapon {

    public static LandmineWeapon Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject landmine;
    public EnemyDamager damager;
    public LandmineColliderModifier rangeModifier;
    public Transform holder;

    public TMP_Text grenadeText;
    public Image grenadeImage;

    public int mineAmount;
    private int maxAmount;

    [HideInInspector]
    public float armTime;

    private float rearmCounter, rearmTime;
    void Start()
    {
        grenadeImage.sprite = Icon;

        mineAmount = Mathf.RoundToInt(stats[weaponLevel].amount);

        UIController.instance.ShowGrenadeAmmo();

        SetStats();
    }

    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        if (mineAmount > 0 && Input.GetButtonDown("Cross"))
        {
            mineAmount--;
            rearmCounter = rearmTime;
            Instantiate(landmine, holder.transform.position, Quaternion.identity).gameObject.SetActive(true);
        }

        rearmCounter -= Time.deltaTime;
        if (rearmCounter <= 0f && mineAmount < maxAmount)
        {
            rearmCounter = rearmTime;
            mineAmount++;
        }

        grenadeText.text = "Mines: " + mineAmount;
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        rearmTime = stats[weaponLevel].timeBetweenAttacks;
        armTime = stats[weaponLevel].timeUntilActive;
        rangeModifier.SetColliderSize(stats[weaponLevel].range);
        damager.lifeTime = stats[weaponLevel].duration;
        maxAmount = Mathf.RoundToInt(stats[weaponLevel].amount);
        rearmCounter = 0f;
    }
}
