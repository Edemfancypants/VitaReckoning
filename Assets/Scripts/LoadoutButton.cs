using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutButton : MonoBehaviour {

	public Image image;
	public Sprite sprite;

	public string weapon;

	private void Start()
	{
		image.sprite = sprite;

        LoadoutLogic.instance.UpdateToggleStatus();
    }

    public void SetWeapon()
	{
		if (SaveSystem.instance.saveData.loadout.Contains(weapon) == false)
		{
			SaveSystem.instance.saveData.loadout.Add(weapon);
		}
		else
		{
			SaveSystem.instance.saveData.loadout.Remove(weapon);
		}

		LoadoutLogic.instance.UpdateToggleStatus();

		LoadoutLogic.instance.UpdateLoadout();
	}
}
