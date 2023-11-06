using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharaterUpgradeLogic : MonoBehaviour {

    public static CharaterUpgradeLogic instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Upgrade Displays")]
    public CharacterStatDisplay moveSpeedUpgradeDisplay;
    public CharacterStatDisplay healthUpgradeDisplay;
    public CharacterStatDisplay pickupRangeUpgradeDisplay;
    public CharacterStatDisplay maxWeaponsUpgradeDisplay;

    [Header("Player Stats")]
    public PlayerStatLogic playerStats;

    [Header("Gems")]
    public TMP_Text gemAmount;

    void Start () 
    {
        UpdateDisplay();
        UpdateGemAmount();
    }
	
    public void UpdateDisplay()
    {
        if (SaveSystem.instance.saveData.playerSpeedLevel < playerStats.moveSpeed.Count - 1)
        {
            moveSpeedUpgradeDisplay.UpdateDisplay(playerStats.moveSpeed[SaveSystem.instance.saveData.playerSpeedLevel + 1].cost, playerStats.moveSpeed[SaveSystem.instance.saveData.playerSpeedLevel].value, playerStats.moveSpeed[SaveSystem.instance.saveData.playerSpeedLevel + 1].value);
        }
        else
        {
            moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (SaveSystem.instance.saveData.playerMaxHealthLevel < playerStats.health.Count - 1)
        {
            healthUpgradeDisplay.UpdateDisplay(playerStats.health[SaveSystem.instance.saveData.playerMaxHealthLevel + 1].cost, playerStats.health[SaveSystem.instance.saveData.playerMaxHealthLevel].value, playerStats.health[SaveSystem.instance.saveData.playerMaxHealthLevel + 1].value);
        }
        else
        {
            healthUpgradeDisplay.ShowMaxLevel();
        }

        if (SaveSystem.instance.saveData.playerPickupRangeLevel < playerStats.pickupRange.Count - 1)
        {
            pickupRangeUpgradeDisplay.UpdateDisplay(playerStats.pickupRange[SaveSystem.instance.saveData.playerPickupRangeLevel + 1].cost, playerStats.pickupRange[SaveSystem.instance.saveData.playerPickupRangeLevel].value, playerStats.pickupRange[SaveSystem.instance.saveData.playerPickupRangeLevel + 1].value);
        }
        else
        {
            pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (SaveSystem.instance.saveData.playerMaxWeaponsLevel < playerStats.maxWeapons.Count - 1)
        {
            maxWeaponsUpgradeDisplay.UpdateDisplay(playerStats.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel + 1].cost, playerStats.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel].value, playerStats.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel + 1].value);
        }
        else
        {
            maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }
    public void UpdateGemAmount()
    {
        gemAmount.text = "Gems: " + SaveSystem.instance.saveData.gems.ToString();
    }

    public void PurchaseMoveSpeed()
    {
        int unlockCost = playerStats.moveSpeed[SaveSystem.instance.saveData.playerSpeedLevel + 1].cost;

        if (unlockCost <= SaveSystem.instance.saveData.gems)
        {
            SaveSystem.instance.saveData.gems -= unlockCost;
            SaveSystem.instance.saveData.playerSpeedLevel++;
            SaveSystem.instance.Save();

            UpdateDisplay();
            UpdateGemAmount();
        }
    }

    public void PurchaseMaxHealth()
    {
        int unlockCost = playerStats.health[SaveSystem.instance.saveData.playerMaxHealthLevel + 1].cost;

        if (unlockCost <= SaveSystem.instance.saveData.gems)
        {
            SaveSystem.instance.saveData.gems -= unlockCost;
            SaveSystem.instance.saveData.playerMaxHealthLevel++;
            SaveSystem.instance.Save();

            UpdateDisplay();
            UpdateGemAmount();
        }
    }

    public void PurchaseRange()
    {
        int unlockCost = playerStats.pickupRange[SaveSystem.instance.saveData.playerPickupRangeLevel + 1].cost;

        if (unlockCost <= SaveSystem.instance.saveData.gems)
        {
            SaveSystem.instance.saveData.gems -= unlockCost;
            SaveSystem.instance.saveData.playerPickupRangeLevel++;
            SaveSystem.instance.Save();

            UpdateDisplay();
            UpdateGemAmount();
        }
    }

    public void PurchaseMaxWeapons()
    {
        int unlockCost = playerStats.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel + 1].cost;

        if (unlockCost <= SaveSystem.instance.saveData.gems)
        {
            SaveSystem.instance.saveData.gems -= unlockCost;
            SaveSystem.instance.saveData.playerMaxWeaponsLevel++;
            SaveSystem.instance.Save();

            UpdateDisplay();
            UpdateMaxWeapons();
            UpdateGemAmount();
        }
    }

    public void UpdateMaxWeapons()
    {
        SaveSystem.instance.saveData.playerMaxWeapons = Mathf.CeilToInt(playerStats.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel].value);
    }
}
