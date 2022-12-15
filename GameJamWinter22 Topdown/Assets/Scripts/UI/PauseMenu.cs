using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButtons;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private bool isPaused;
    
    private PlayerControls playerControls;
    private PlayerInputs player;
    private InputAction menu;



    #endregion

    #region UnityFunktions

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseButtons.SetActive(true);
        settingsMenu.SetActive(false);
    }
    private void Awake()
    {
        player = FindObjectOfType<PlayerInputs>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        menu = playerControls.UI.Escape;
        menu.Enable();

        menu.performed += PauseGame;
    }

    private void OnDisable()
    {
        menu.Disable();
    }



    private void PauseGame(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            player.DisableInput();
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(false);
        pauseButtons.SetActive(true);
        settingsMenu.SetActive(false);
        player.EnableInput();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    #endregion

}
