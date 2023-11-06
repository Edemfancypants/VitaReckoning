using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpButton : MonoBehaviour {

	public TMP_Text upgradeDescText, nameLevelText;
	public Image weaponIcon;

	private Weapon assignedWeapon;
	public void UpdateButtonDisplay(Weapon theWeapon)
	{
		if (theWeapon.gameObject.activeSelf == true || PlayerController.instance.activeWeapons.Contains(theWeapon))
		{
			upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
			weaponIcon.sprite = theWeapon.Icon;

			nameLevelText.text = theWeapon.name + " - lvl " + theWeapon.weaponLevel;
		}
		else if (!PlayerController.instance.activeWeapons.Contains(theWeapon))
		{
			upgradeDescText.text = "Unlock " + theWeapon.name;
			weaponIcon.sprite= theWeapon.Icon;

			nameLevelText.text= theWeapon.name;
		}

		assignedWeapon = theWeapon;
	}

	public void SelectUpgrade()
	{
		if (assignedWeapon != null)
		{
			if (assignedWeapon.gameObject.activeSelf == true || PlayerController.instance.activeWeapons.Contains(assignedWeapon))
			{
				assignedWeapon.LevelUp();
			}
			else if (!PlayerController.instance.activeWeapons.Contains(assignedWeapon))
            {
				PlayerController.instance.AddWeapon(assignedWeapon);
			}

			UIController.instance.levelUpPanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}

}
