using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesLeft;
    List<Enemy> enemies = new List<Enemy>();


    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }

    private void Awake()
    {
        EnemyCount();
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
           UpdateEnemiesLeftText(); 
        }
    }

    private void EnemyCount()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeft.text = $"Enemies Left: {enemies.Count}";
    }
}
