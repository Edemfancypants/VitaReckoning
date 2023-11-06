using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("Player Stats")]
    public int playerMaxHealthLevel;
    public int playerSpeedLevel;
    public int playerPickupRangeLevel;
    public int playerMaxWeaponsLevel;
    public int playerMaxWeapons;

    [Header("Weapon Lists")]
    public List<string> availableWeapons;
    public List<string> loadout;

    [Header("Avalible Levels")]
    public List<string> levelsUnlocked;

    [Header("Gems")]
    public int gems;
}
