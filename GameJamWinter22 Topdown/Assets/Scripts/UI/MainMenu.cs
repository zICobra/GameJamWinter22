using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region UnityFunktions

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayEndlessMode()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    #endregion
}
