using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning, Waiting,Counting
    }
    
    [System.Serializable]
    public class Wave
    {
        [SerializeField] public string name;
        [SerializeField] public List<Transform> enemy = new List<Transform>();
        [SerializeField] public int enemyCount;
        [SerializeField] public float spawnRate;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameManager gameManager;
    
    private int nextWave = 0;

    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points referenced");
        }
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length -1)
        {
            Debug.Log("All waves completed");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy ( _wave.enemy);
            yield return new WaitForSeconds (1f / _wave.spawnRate);
        }

        state = SpawnState.Waiting;
        
        yield break;
    }

    void SpawnEnemy(List<Transform> enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        int spawnEnemy = Random.Range(0, enemy.Count);
        Transform Prefab = Instantiate(enemy[spawnEnemy], sp.position, sp.rotation);
        gameManager.EnemyCount();
    }
    
}
