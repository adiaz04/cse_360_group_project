using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public string mainMenu;

    public bool isPaused;

    public GameObject pauseMenuCanvas;

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    /// <summary>
    /// Resume-sets isPaused to false which turns off the PauseMenu and sets the timeScale back to normal 
    /// </summary>
	public void Resume()
    {
        isPaused = false;
    }

    /// <summary>
    /// QuitToMainMenu-Calls UnityEngine API to load Main Menu scene
    /// </summary>
	public void QuitToMainMenu()
    {
        Application.LoadLevel(mainMenu);
    }

    /// <summary>
    /// QuitGame-Calls UnityEngine API to Exit game
    /// </summary>
	public void QuitGame()
    {
        Application.Quit();
    }
}
