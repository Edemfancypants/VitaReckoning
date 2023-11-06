using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

	private void Awake()
	{
		instance = this;
	}

	private bool gameActive;
	public float timer;

	public float waitUntilEnd = 1f;

	void Start () 
	{
		gameActive = true;
	}
	
	void Update () 
	{
		if (gameActive == true)
		{
			timer += Time.deltaTime;
			UIController.instance.UpdateTimer(timer);
		}
	}

	public void LevelCompleted(string levelToAdd)
	{
		if (SaveSystem.instance.saveData.levelsUnlocked.Contains(levelToAdd) == false)
		{
            SaveSystem.instance.saveData.levelsUnlocked.Add(levelToAdd);
        }
    }

	public void EndLevel()
	{
		gameActive = false;

		StartCoroutine(EndLevelCo());
    }

	IEnumerator EndLevelCo()
	{
		yield return new WaitForSeconds(waitUntilEnd);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        if (minutes < 1)
        {
            UIController.instance.endTimeText.text = seconds.ToString("00" + " secs");

        }
        else
        {
            UIController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");

        }
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
