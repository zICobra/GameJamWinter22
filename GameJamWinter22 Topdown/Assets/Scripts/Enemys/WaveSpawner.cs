using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning, Waiting,Counting,Finished
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
    [SerializeField] private TextMeshProUGUI nextWaveText;
    [SerializeField] private GameObject nextWaveParent;
    
    [SerializeField] private int nextWave = 0;

    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;

    [SerializeField] private GameObject earlyGameMusic;
    [SerializeField] private GameObject midGameMusic;
    [SerializeField] private GameObject lateGameMusic;
    [SerializeField] private GameObject gameWonMusic;

    private int enemyType;
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
        if (state == SpawnState.Finished)
        {
            return;
        }
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
            nextWaveParent.SetActive(false);
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            nextWaveParent.SetActive(true);
            waveCountdown -= Time.deltaTime;
            nextWaveText.text = $"Next Wave : {Mathf.Round(waveCountdown)}";
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length -1)
        {
            state = SpawnState.Finished;
            gameWonMusic.SetActive(true);
            gameManager.WonGame();
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
            gameManager.EnemyCount();
        }

        state = SpawnState.Waiting;
    }

    void SpawnEnemy(List<Transform> enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (nextWave + 1 <= 2)
        {
            int spawnEnemy = Random.Range(0, enemy.Count);
            Instantiate(enemy[spawnEnemy], sp.position, sp.rotation);
        }
        else
        {
            float spawnEnemy = Random.Range(0.0f, 1.0f);
            if (nextWave + 1 == 3)
            {
                earlyGameMusic.SetActive(false);
                midGameMusic.SetActive(true);
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.30f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.30 && spawnEnemy <= 0.60f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.60 && spawnEnemy <= 0.9f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 3;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 4)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.25f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.25 && spawnEnemy <= 0.57f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.57 && spawnEnemy <= 0.88f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 3;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 5)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.23f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.23 && spawnEnemy <= 0.46f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.46 && spawnEnemy <= 0.69f)
                {
                    enemyType = 2;
                }
                else if (spawnEnemy > 0.69 && spawnEnemy <= 0.85f)
                {
                    enemyType = 3;
                }
                else
                {
                    enemyType = 4;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 6)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.23f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.23 && spawnEnemy <= 0.46f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.46 && spawnEnemy <= 0.69f)
                {
                    enemyType = 2;
                }
                else if (spawnEnemy > 0.69 && spawnEnemy <= 0.85f)
                {
                    enemyType = 3;
                }
                else
                {
                    enemyType = 4;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 7)
            {
                midGameMusic.SetActive(false);
                lateGameMusic.SetActive(true);
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.20f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy > 0.20 && spawnEnemy <= 0.40f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.40 && spawnEnemy <= 0.60f)
                {
                    enemyType = 2;
                }
                else if (spawnEnemy > 0.60 && spawnEnemy <= 0.80f)
                {
                    enemyType = 3;
                }
                else
                {
                    enemyType = 4;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 8)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.32f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.32 && spawnEnemy <= 0.64f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.64 && spawnEnemy <= 0.98f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 3;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 9)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.31f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.31 && spawnEnemy <= 0.62f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.62 && spawnEnemy <= 0.96f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 3;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (nextWave + 1 == 10)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.30f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.30 && spawnEnemy <= 0.60f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.60 && spawnEnemy <= 0.90f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 3;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
        }
    }
    
}
