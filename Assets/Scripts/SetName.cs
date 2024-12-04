using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetName : MonoBehaviour
{
    public TMP_Text debugText;
    public TMP_Text headText;
    public TMP_InputField userInput;
    public GameObject nameSystem;
    public RankUIManager rankUIManager;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject inputName;
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;

    private void Start()
    {
        debugText.text = $"username: {userInput.text}";

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameSystem.SetActive(false);
            playButton.SetActive(true);
        }
        else
        {
            nameSystem.SetActive(true);
        }
    }

    public void setName()
    {
        debugText.text = $"username: {userInput.text}";
        CheckInput();
    }

    public void resetName()
    {
        debugText.text = "Please enter your name again.";
        headText.text = "Enter your name:";
        inputName.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void CheckInput()
    {
        inputName.SetActive(false);
        yesButton.SetActive(true);
        noButton.SetActive(true);
        headText.text = "Are You Sure About That?";
    }

    public void InputYes()
    {
        PlayerPrefs.SetString("PlayerName", userInput.text);
        PlayerPrefs.Save();
        nameSystem.SetActive(false);
        rankUIManager.SetPlayerName(userInput.text);
        gameManager.ResetScore();
    }

    public void InputNo()
    {
        headText.text = "Please Enter Your Name:";
        inputName.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        debugText.text = $"username: ";
    }
    public void ChangeName()
    {
        nameSystem.SetActive(true);
        headText.text = "Enter your new name:";
    }

}
