using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject leaderBoard;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        if (audioManager == null && GameObject.FindGameObjectWithTag("Audio") != null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu is not assigned in the Inspector!");
        }
    }

    public void Pause()
    {
        if (audioManager != null)
        {
            audioManager.PauseBG();
        }
        else
        {
            Debug.LogWarning("AudioManager is not set.");
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("PauseMenu object is missing!");
        }

        Time.timeScale = 0;
    }

    public void Continue()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        audioManager.PlayBG();
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LeaderBoard()
    {
        if (leaderBoard != null)
        {
            leaderBoard.SetActive(true);
        }
        else
        {
            Debug.LogError("LeaderBoard object is missing!");
        }
    }
}
