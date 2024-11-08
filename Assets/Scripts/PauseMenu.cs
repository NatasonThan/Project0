using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;

    public void Pause() 
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue() 
    {
        PausePanel.SetActive(false);
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
}
