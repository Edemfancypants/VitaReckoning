using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;

	private void Awake()
	{
		instance = this;
	}

	[Header("Spawn Settings")]
	public Transform minSpawn;
	public Transform maxSpawn;
    public int maxEnemies;
	public int enemyCount;
	public int enemiesKilled;
    [HideInInspector]
    public bool bossKilled;
    [HideInInspector]
    public bool bossSpawned;
	[HideInInspector]
	public int enemiesLeft;
    private float spawnCounter;
    private Transform target;

	[Header("Despawn Settings")]
    public int checkPerFrame;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float deSpawnDistance;
	private int enemyToCheck;

	[Header("Waves")]
	public List<WaveInfo> waves;
	public int currentWave;
	private float waveCounter;

	[Header("Level Settings")]
	public bool isLevel;
	public string nextLevel;

	void Start () 
	{
		target = PlayerHealth.instance.transform;

		deSpawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;

		currentWave = -1;
		NextWave();
	}
	
	void Update () 
	{
		if (PlayerHealth.instance.gameObject.activeSelf && spawnedEnemies.Count < maxEnemies)
		{
			if (currentWave < waves.Count)
			{
				waveCounter -= Time.deltaTime;
				if (enemiesKilled >= waves[currentWave].enemiesToKill && waveCounter <= 0)
				{
					if (waves[currentWave].isBossRound == true)
					{
						if (bossKilled == true)
						{
							bossKilled = false;
							NextWave();
						}
					}
					else
					{
						NextWave();
                    }
				}

				spawnCounter -= Time.deltaTime;
				if (spawnCounter <= 0)
				{
					spawnCounter = waves[currentWave].timeBetweenSpawn;

                    waves[currentWave].enemyToSpawn = waves[currentWave].enemies[Random.Range(0, waves[currentWave].enemies.Count)];

                    if (waves[currentWave].isBossRound == true && waves[currentWave].bossEnemy != null && bossSpawned == false)
					{
						waves[currentWave].enemyToSpawn = waves[currentWave].bossEnemy;
						bossSpawned = true;
                    }

					GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
					spawnedEnemies.Add(newEnemy);
				}
			}
		}

		transform.position = target.position;

		int checkTarget = enemyToCheck + checkPerFrame;

		while (enemyToCheck < checkTarget)  //Despawn distant enemies
		{
			if (enemyToCheck < spawnedEnemies.Count)
			{
				if (spawnedEnemies[enemyToCheck] != null)
				{
					if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > deSpawnDistance)
					{
						Destroy(spawnedEnemies[enemyToCheck]);
						spawnedEnemies.RemoveAt(enemyToCheck);
						checkTarget--;
					}
					else
					{
						enemyToCheck++;
					}
				}
				else
				{
					spawnedEnemies.RemoveAt(enemyToCheck);
					checkTarget--;
				}
			}
			else
			{
				enemyToCheck = 0;
				checkTarget = 0;
			}
		}

		enemyCount = spawnedEnemies.Count;

		enemiesLeft = waves[currentWave].enemiesToKill - enemiesKilled;
	}

	public Vector3 SelectSpawnPoint()
	{
		Vector3 spawnPoint = Vector3.zero;

		if (Random.Range(0f, 1f) > .5f)
		{
			spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

			if (Random.Range(0f, 1f) > .5f) 
			{
				spawnPoint.x = maxSpawn.position.x;
			}
			else
			{
				spawnPoint.x = minSpawn.position.x;
			}
		}
		else
		{
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

		return spawnPoint;
	}

	public void NextWave()
	{
		currentWave++;

		if (currentWave >= waves.Count)
		{
			if (isLevel == true)
			{
				LevelManager.instance.LevelCompleted(nextLevel);
			}

			currentWave = waves.Count - 1;
			enemiesLeft = waves[currentWave].enemiesToKill;
		}

		waveCounter = waves[currentWave].waveLength;
		enemiesKilled = 0;
		maxEnemies = waves[currentWave].maxEnemies;
		spawnCounter = waves[currentWave].timeBetweenSpawn;

		UIController.instance.UpdateWave();
	}

}

[System.Serializable]
public class WaveInfo
{
	[Header("Enemy Settings")]
	public List<GameObject> enemies;
	[HideInInspector]
	public GameObject enemyToSpawn;

	[Header("Boss round Settings")]
	public GameObject bossEnemy;
    public bool isBossRound;

	[Header("Wave settings")]
    public float waveLength = 10f;
	public int enemiesToKill;
	public int maxEnemies;
	public float timeBetweenSpawn = 1f;
}
