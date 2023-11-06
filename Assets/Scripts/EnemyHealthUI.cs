using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public EnemyController enemyController;
    private float currentHealth, maxHealth;
    public Slider enemyHealthSlider;

    void Start()
    {
        maxHealth = enemyController.health;

        currentHealth = maxHealth;

        enemyHealthSlider.maxValue = maxHealth;
        enemyHealthSlider.value = currentHealth;
    }

    void Update()
    {
        if (enemyController.health != currentHealth)
        {
            currentHealth = enemyController.health;
            enemyHealthSlider.value = currentHealth;
        }
    }
}
