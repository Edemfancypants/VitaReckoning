using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour {

	public static UIController instance;

	private void Awake()
	{
		instance = this;
	}

	[Header("Experience Bar")]
	public Slider expSlider;
	public TMP_Text levelText;

	[Header("Level Up Panel")]
	public GameObject levelUpPanel;
	public LevelUpButton[] levelUpButtons;

	[Header("Panel GameObjects")]
	public GameObject pauseScreen;
    public GameObject levelEndScreen;

    [Header("Text GameObjects")]
	public TMP_Text coinText;
	public TMP_Text gemText;
	public TMP_Text timeText;
	public TMP_Text waveText;
	public TMP_Text enemyText;
    public TMP_Text endTimeText;

	[Header("Ammo Text")]
	public GameObject bulletAmmo;
	public GameObject grenadeAmmo;

	[Header("Level Manager")]
	public string menuName;

	[Header("Upgrade Displays")]
	public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay;
	public PlayerStatUpgradeDisplay healthUpgradeDisplay;
	public PlayerStatUpgradeDisplay pickupRangeUpgradeDisplay;
	public PlayerStatUpgradeDisplay maxWeaponsUpgradeDisplay;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
		{
			Pause();
		}

		enemyText.text = "Enemies left: " + Mathf.RoundToInt(EnemySpawner.instance.enemiesLeft).ToString();
	}

	public void UpdateExp(int currentExp, int levelExp, int currentLevel)
	{
		expSlider.maxValue = levelExp;
		expSlider.value = currentExp;

		levelText.text = "level: " + currentLevel;
	}

	public void SkipLevelUp()
	{
		levelUpPanel.SetActive(false);
		Time.timeScale = 1.0f;
	}

	public void UpdateCoins()
	{
		coinText.text = "Coins: " + CoinController.instance.currentCoins;
	}

    public void UpdateGems()
    {
        gemText.text = "Gems: " + CoinController.instance.currentGems;
    }

    public void UpdateWave()
	{
		int currentWave = EnemySpawner.instance.currentWave + 1;

        waveText.text = "Wave: " + currentWave;
	}

	public void ShowBulletAmmo()
	{
		bulletAmmo.gameObject.SetActive(true);
	}

	public void ShowGrenadeAmmo()
	{
		grenadeAmmo.gameObject.SetActive(true);
	}

    public void PurchaseMoveSpeed()
    {
		PlayerStatLogic.instance.PurchaseMoveSpeed();
		SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatLogic.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatLogic.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatLogic.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

	public void UpdateTimer(float time)
	{
		float minutes = Mathf.FloorToInt(time / 60f);
		float seconds = Mathf.FloorToInt(time % 60);

		timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(menuName);
        Time.timeScale = 1.0f;
    }

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

	public void QuitGame()
	{
		Application.Quit();
	}

	public void Pause()
	{
		if (pauseScreen.activeSelf == false)
		{
			pauseScreen.SetActive(true);
			Time.timeScale = 0f;

			SFXLogic.Instance.PlayBGM(false);
		}
		else
		{
			pauseScreen.SetActive(false);
			if (levelUpPanel.activeSelf == false)
			{
				Time.timeScale = 1f;
			}

			SFXLogic.Instance.PlayBGM(true);
		}
	}
}
