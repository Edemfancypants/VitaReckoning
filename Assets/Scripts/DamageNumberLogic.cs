using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberLogic : MonoBehaviour {

	public static DamageNumberLogic instance;

	private void Awake()
	{
		instance = this;
	}

	public DamageNumber numberToSpawn;
	public Transform numberCanvas;

	private List<DamageNumber> numberPool = new List<DamageNumber>();

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void SpawnDamage(float damageAmount, Vector3 location)
	{
		int rounded = Mathf.RoundToInt(damageAmount);

		DamageNumber newDamage = GetFromPool();

		newDamage.Setup(rounded);
		newDamage.gameObject.SetActive(true);

		newDamage.transform.position = location;
	}

	public DamageNumber GetFromPool()
	{
		DamageNumber numberToOutput = null;

		if (numberPool.Count == 0)
		{
			numberToOutput = Instantiate(numberToSpawn, numberCanvas);
		} 
		else
		{
			numberToOutput = numberPool[0];
			numberPool.RemoveAt(0);
		}

		return numberToOutput;
	}

	public void PlaceInPool(DamageNumber numberToPlace)
	{
		numberToPlace.gameObject.SetActive(false);
		numberPool.Add(numberToPlace);
	}
}
