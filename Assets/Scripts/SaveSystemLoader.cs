using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemLoader : MonoBehaviour
{

    public static SaveSystemLoader instance;

    public SaveSystem systemToLoad;
    public SaveSystem baseSaveSystem;

    private void Awake()
    {
        instance = this;

        if (SaveSystem.instance == null)
        {
            Instantiate(systemToLoad).SetupInstance();
        }
    }

    private void Start()
    {
        SaveSystem.instance.Load();
    }

    public void resetSaveData()
    {
        //Reset save system
        systemToLoad = baseSaveSystem;
        SaveSystem.instance.Save();
        SaveSystem.instance.Load();
        SaveSystem.instance.DestroySaveSystem();
        Instantiate(systemToLoad).SetupInstance();

        //Reset Loadout screen
        LoadoutLogic.instance.UpdateLoadout();
        LoadoutLogic.instance.UpdateWeaponButtons();
        LoadoutLogic.instance.UpdateToggleStatus();
        LoadoutLogic.instance.UpdateGemAmount();

        //Reset Character screen
        CharaterUpgradeLogic.instance.UpdateDisplay();
        CharaterUpgradeLogic.instance.UpdateGemAmount();

        //Reset Levels screen
        LevelMenuLogic.instance.UpdateLevelButtons();
    }
}
