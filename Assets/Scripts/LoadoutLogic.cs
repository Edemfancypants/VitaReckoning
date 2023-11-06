using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutLogic : MonoBehaviour {

    public static LoadoutLogic instance;

    private void Awake()
    {
        instance = this;
    }

	public List<LoadoutButton> weaponList;
	public List<Button> buttonList;
    public List<Button> unlockButtonList;
    public List<Toggle> weaponToggles;

    public TMP_Text gemAmount;

	private void Start()
	{
		UpdateWeaponButtons();
        UpdateGemAmount();
	}

	public void UpdateWeaponButtons()
	{
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (SaveSystem.instance.saveData.availableWeapons.Contains(weaponList[i].weapon))
            {
                unlockButtonList[i].gameObject.SetActive(false);
                buttonList[i].gameObject.SetActive(true);
            }
            else
            {
                unlockButtonList[i].gameObject.SetActive(true);
                buttonList[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateLoadout()
    {
        if (SaveSystem.instance.saveData.loadout.Count >= SaveSystem.instance.saveData.playerMaxWeapons)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (SaveSystem.instance.saveData.loadout.Contains(weaponList[i].weapon))
                {
                    buttonList[i].interactable = true;
                }
                else
                {
                    buttonList[i].interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                buttonList[i].interactable = true;
            }
        }

        SaveSystem.instance.Save();
    }

    public void UpdateGemAmount()
    {
        gemAmount.text = "Gems: " + SaveSystem.instance.saveData.gems.ToString();
    }

    public void UpdateToggleStatus()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (SaveSystem.instance.saveData.loadout.Contains(weaponList[i].weapon))
            {
                weaponToggles[i].isOn = true;
            }
            else
            {
                weaponToggles[i].isOn = false;
            }
        }
    }

}
