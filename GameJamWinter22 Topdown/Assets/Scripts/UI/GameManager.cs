using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesLeft;
    [SerializeField] private Image abilityImage;
    [SerializeField] private float cooldown = 5;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private GameObject loseScreen;
    public KeyCode ability;
    public KeyCode abilityController;
    
    private int playerHealth;
    private bool isCooldown = false;
    public List<Enemy> enemies = new List<Enemy>();
    private PlayerInputs playerInputs;
    private float currentTime = 0f;


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
        TimeTaken();
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
        enemies = FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeft.text = $"Enemies Left: {enemies.Count}";
    }

    public void Ability()
    {
        if (FindObjectOfType<PlayerInputs>())
        {
            if ((Input.GetKey(ability) || Input.GetKey(abilityController)) && isCooldown == false)
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
            case <=0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                hearts[5].SetActive(false);
                break;
        }
    }

    void TimeTaken()
    {
        currentTime += 1 * Time.deltaTime;

        int seconds = (int)(currentTime % 60);
        int minutes = (int)(currentTime / 60) % 60;
        int hours = (int)(currentTime / 3600) % 24;

        string timerString = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

        timerText.text = $"Time: {timerString}";
    }

    public void WonGame()
    {
        playerInputs.DisableInput();
        finishScreen.SetActive(true);
    }

    public void LoseScreen()
    {
        playerInputs.DisableInput();
        loseScreen.SetActive(true);
    }
}
