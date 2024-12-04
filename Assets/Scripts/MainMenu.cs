using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() 
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    public void Characters()
    {
        SceneManager.LoadScene("Characters");
    }
    public void Store()
    {
        SceneManager.LoadScene("Store");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
