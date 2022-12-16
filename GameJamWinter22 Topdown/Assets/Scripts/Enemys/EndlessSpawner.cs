using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndlessSpawner : MonoBehaviour
{
    public enum SpawnStateEndless
    {
        Counting
    }
    
    [System.Serializable]
    public class Endless
    {
        [SerializeField] public List<Transform> enemy = new List<Transform>();
    }

    [SerializeField] private Endless[] waves;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject earlyGameMusic;
    [SerializeField] private GameObject midGameMusic;
    [SerializeField] private GameObject lateGameMusic;
    private float spawnRateEndless = 2f;

    private float elapsedTime;
    private float currentTime;
    private int enemyCount;
    private int minutes;
    private int enemyType;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points referenced");
        }
    }

    private void Update()
    {
        currentTime += 1 * Time.deltaTime;
        minutes = (int)(currentTime / 60) % 60;

        enemyCount = gameManager.enemies.Count;
        
        elapsedTime += Time.deltaTime;


        if (elapsedTime > spawnRateEndless)
        {
            elapsedTime = 0;
            StartCoroutine(SpawnWave(waves[minutes]));
        }
    }
    
    IEnumerator SpawnWave(Endless endless)
    {
        SpawnEnemy ( endless.enemy);
        yield return new WaitForSeconds (1f / spawnRateEndless);
        
    }


    void SpawnEnemy(List<Transform> enemy)
    {
        if (enemyCount <= 300)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
            float spawnEnemy = Random.Range(0.0f, 1.0f);

            if (minutes >= 0 && minutes < 2)
            {
                earlyGameMusic.SetActive(false);
                midGameMusic.SetActive(true);
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.33f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.33 && spawnEnemy <= 0.66f)
                {
                    enemyType = 1;
                }
                else
                {
                    enemyType = 2;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 2 && minutes < 5)
            {
                spawnRateEndless = 1.5f;
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
            
            if (minutes >= 5 && minutes < 7)
            {
                spawnRateEndless = 2f;
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.20f)
                {
                    enemyType = 0;
                }
                else if (spawnEnemy >0.20 && spawnEnemy <= 0.40f)
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
                else if (spawnEnemy > 0.80 && spawnEnemy <= 0.90f)
                {
                    enemyType = 4;
                }
                else
                {
                    enemyType = 5;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
        
            if (minutes >= 7 && minutes < 10)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.20f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.20 && spawnEnemy <= 0.40f)
                {
                    enemyType = 2;
                }
                else if (spawnEnemy > 0.40 && spawnEnemy <= 0.60f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy > 0.60 && spawnEnemy <= 0.79f)
                {
                    enemyType = 4;
                }
                else if(spawnEnemy > 0.79 && spawnEnemy <= 0.97f)
                {
                    enemyType = 5;
                }
                else
                {
                    enemyType = 6;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 10 && minutes < 15)
            {
                spawnRateEndless = 2.5f;
                if (spawnEnemy >0.0 && spawnEnemy <= 0.10f)
                {
                    enemyType = 1;
                }
                else if (spawnEnemy > 0.10 && spawnEnemy <= 0.20f)
                {
                    enemyType = 2;
                }
                else if (spawnEnemy > 0.20 && spawnEnemy <= 0.45f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy > 0.45 && spawnEnemy <= 0.70f)
                {
                    enemyType = 4;
                }
                else if (spawnEnemy > 0.70 && spawnEnemy <= 0.90f)
                {
                    enemyType = 5;
                }
                else
                {
                    enemyType = 6;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 15 && minutes < 20)
            {
                spawnRateEndless = 3f;
                midGameMusic.SetActive(false);
                lateGameMusic.SetActive(true);
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.30f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy > 0.30 && spawnEnemy <= 0.60f)
                {
                    enemyType = 4;
                }
                else if (spawnEnemy > 0.60 && spawnEnemy <= 0.90f)
                {
                    enemyType = 5;
                }
                else 
                {
                    enemyType = 6;
                }
                
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 20 && minutes < 25)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.25f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy >0.25 && spawnEnemy <= 0.50f)
                {
                    enemyType = 4;
                }
                else if (spawnEnemy > 0.50 && spawnEnemy <= 0.75f)
                {
                    enemyType = 5;
                }
                else
                {
                    enemyType = 6;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 25 && minutes < 30)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.20f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy >0.20 && spawnEnemy <= 0.40f)
                {
                    enemyType = 4;
                }
                else if (spawnEnemy > 0.40 && spawnEnemy <= 0.60f)
                {
                    enemyType = 5;
                }
                else
                {
                    enemyType = 6;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
            
            if (minutes >= 30)
            {
                if (spawnEnemy >= 0.0 && spawnEnemy <= 0.15f)
                {
                    enemyType = 3;
                }
                else if (spawnEnemy >0.15 && spawnEnemy <= 0.30f)
                {
                    enemyType = 4;
                }
                else if (spawnEnemy > 0.30 && spawnEnemy <= 0.45f)
                {
                    enemyType = 5;
                }
                else
                {
                    enemyType = 6;
                }
                Instantiate(enemy[enemyType], sp.position, sp.rotation);
            }
        }
        
        gameManager.EnemyCount();
    }
    
}
