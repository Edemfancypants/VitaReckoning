using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public static CoinController instance;

	private void Awake()
	{
		instance = this;
	}

	public int currentCoins;
	public int currentGems;

	public CoinPickup coin;
	public GemPickup gem;

	private void Start()
	{
		currentGems = SaveSystem.instance.saveData.gems;
		UIController.instance.UpdateGems();
	}

	public void AddCoins(int coinsToAdd)
	{
		currentCoins += coinsToAdd;
		UIController.instance.UpdateCoins();
        SFXLogic.Instance.PlaySFXPitched(2);
    }

	public void AddGems(int gemsToAdd)
	{
		currentGems += gemsToAdd;
		SaveSystem.instance.saveData.gems = currentGems;
		SaveSystem.instance.Save();
        UIController.instance.UpdateGems();
        SFXLogic.Instance.PlaySFXPitched(2);
    }

    public void DropCoin(Vector3 position, int value)
	{
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f), Quaternion.identity);
		newCoin.coinValue = value;
		newCoin.gameObject.SetActive(true);
	}

    public void DropGem(Vector3 position, int value)
    {
        GemPickup newGem = Instantiate(gem, position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0f), Quaternion.identity);
        newGem.gemValue = value;
        newGem.gameObject.SetActive(true);
    }

    public void SpendCoins(int coinsToSpend)
	{
		currentCoins -= coinsToSpend;
		UIController.instance.UpdateCoins();
	}
}
