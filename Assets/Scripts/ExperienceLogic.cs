using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLogic : MonoBehaviour {

	public static ExperienceLogic instance;

	private void Awake()
	{
		instance = this;
	}

	public int currentExperience;

	public ExperiencePickup pickup;

	public List<int> expLevels;
	public int currentLevel = 1, levelCount = 100;

	public List<Weapon> weaponsToUpgrade;

	void Start()
	{
		while (expLevels.Count < levelCount)
		{
			expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
		}
	}

	void Update()
	{

	}

	public void GetExp(int amountToGet)
	{
		currentExperience += amountToGet;

		if (currentExperience >= expLevels[currentLevel])
		{
			LevelUp();
		}

		UIController.instance.UpdateExp(currentExperience, expLevels[currentLevel], currentLevel);
        SFXLogic.Instance.PlaySFXPitched(2);
    }

    public void SpawnExp(Vector3 position, int expValue)
	{
		Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
	}

	void LevelUp()
	{
		currentExperience -= expLevels[currentLevel];

		currentLevel++;

		if (currentLevel >= expLevels.Count)
		{
			currentLevel = expLevels.Count - 1;
		}

		UIController.instance.levelUpPanel.SetActive(true);

		Time.timeScale = 0;

		weaponsToUpgrade.Clear();
		List<Weapon> avalibleWeapons = new List<Weapon>();
		avalibleWeapons.AddRange(PlayerController.instance.assignedWeapons);

		if(avalibleWeapons.Count > 0)
		{
			int selected = Random.Range(0, avalibleWeapons.Count);
			weaponsToUpgrade.Add(avalibleWeapons[selected]);
			avalibleWeapons.RemoveAt(selected);
		}

		if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
		{
            avalibleWeapons.AddRange(PlayerController.instance.unassignedWeapons);

        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
		{
            if (avalibleWeapons.Count > 0)
            {
                int selected = Random.Range(0, avalibleWeapons.Count);
                weaponsToUpgrade.Add(avalibleWeapons[selected]);
                avalibleWeapons.RemoveAt(selected);
            }

        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
		{
			UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
		}

		for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
		{
			if (i < weaponsToUpgrade.Count)
			{
				UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
			}
			else
			{
				UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
			}
		}

		PlayerStatLogic.instance.UpdateDisplay();

    }
}
