using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesLeft;
    List<Enemy> enemies = new List<Enemy>();
    private PlayerInputs playerInputs;
    [SerializeField] private Image abilityImage;
    [SerializeField] private float cooldown = 5;
    private bool isCooldown = false;
    public KeyCode ability;
    private int playerHealth;
    [SerializeField] private GameObject[] hearts;


    private void Start()
    {
        abilityImage.fillAmount = 0;
        playerInputs = FindObjectOfType<PlayerInputs>();
    }

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
        playerInputs = FindObjectOfType<PlayerInputs>();
    }

    private void Update()
    {
        playerHealth = playerInputs.health;
        Ability();
        PlayerHealth();
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
           UpdateEnemiesLeftText(); 
        }
    }

    public void EnemyCount()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeft.text = $"Enemies Left: {enemies.Count}";
    }

    public void Ability()
    {
        if (Input.GetKey(ability) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage.fillAmount = 1;
            playerInputs.Lightning();
        }

        if (isCooldown)
        {
            abilityImage.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    private void PlayerHealth()
    {
        switch (playerHealth)
        {
            case 10:
                hearts[5].SetActive(false);
                break;
            case  9:
                hearts[0].SetActive(false);
                hearts[5].SetActive(true);
                break;
            case 8:
                hearts[0].SetActive(false);
                hearts[5].SetActive(false);
                break;
            case 7:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[5].SetActive(true);
                break;
            case 6:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[5].SetActive(false);
                break;
            case 5:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[5].SetActive(true);
                break;
            case 4:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[5].SetActive(false);
                break;
            case 3:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[5].SetActive(true);
                break;
            case 2:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[5].SetActive(false);
                break;
            case 1:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                hearts[5].SetActive(true);
                break;
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                hearts[5].SetActive(false);
                break;
        }
    }

}
