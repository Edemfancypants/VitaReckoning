using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockWeaponButton : MonoBehaviour {

    public Image image;
    public Sprite sprite;
    public TMP_Text costText;

    public string weapon;

    public int unlockCost;

    private void Start()
    {
        image.sprite = sprite;
        costText.text = unlockCost.ToString();
    }
	
    public void UnlockWeapon()
    {
        if (unlockCost <= SaveSystem.instance.saveData.gems)
        {
            SaveSystem.instance.saveData.gems -= unlockCost;
            SaveSystem.instance.saveData.availableWeapons.Add(weapon);
            SaveSystem.instance.Save();

            LoadoutLogic.instance.UpdateWeaponButtons();
            LoadoutLogic.instance.UpdateGemAmount();
        }
    }

}
