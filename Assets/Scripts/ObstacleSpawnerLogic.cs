using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerLogic : MonoBehaviour {

	public List<GameObject> obstaclePrefabs;
	public GameObject obstaclePrefab;

	// Use this for initialization
	void Start () 
	{
		float chance = Random.Range(0f, 1f);

		if (chance >= 0.5f)
		{
			obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
			Instantiate(obstaclePrefab, transform).SetActive(true);
		}
		else
		{
			obstaclePrefab = null;
			Destroy(gameObject);
		}
	}
}
