using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterManager : MonoBehaviour
{

    public CharacterDatabase characterDatabase;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI highestScore;
    public SpriteRenderer sprite;
    private GameManager gameManager;
    private int selectedOption = 0;
    private int score;


    void Start()
    {
        score = PlayerPrefs.GetInt("HighestScore", 0);
        Debug.Log("Highest Score is: " + score);
        highestScore.text = $"Your Highest Score is: {score}";

        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }

        if (characterDatabase.owned.Count == 0)
        {
            Debug.Log("Adding a default character to owned list.");
            characterDatabase.AddCharacter(characterDatabase.GetCharacter(0, "store"));
        }

        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        if (characterDatabase.owned.Count == 0)
        {
            Debug.LogError("No owned characters available!");
            return;
        }

        selectedOption++;
        if (selectedOption >= characterDatabase.owned.Count)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        if (characterDatabase.owned.Count == 0)
        {
            Debug.LogError("No owned characters available!");
            return;
        }

        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterDatabase.owned.Count - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    public void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        if (selectedOption < 0 || selectedOption >= characterDatabase.owned.Count)
        {
            Debug.LogWarning($"Loaded selectedOption ({selectedOption}) is out of range. Resetting to 0.");
            selectedOption = 0;
        }
    }
    private void Save() 
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
    public void ChangeScene(int sceneID) 
    {
        if (selectedOption == 0 && score >= 0) 
        {
            SceneManager.LoadScene(sceneID);
        }
        else if (selectedOption == 1 && score >= 10)
        {
            SceneManager.LoadScene(sceneID);
        }
        else if (selectedOption == 2 && score >= 20)
        {
            SceneManager.LoadScene(sceneID);
        }
        else if (selectedOption == 3 && score >= 30)
        {
            SceneManager.LoadScene(sceneID);
        }
        else
        {
            nameText.text = "Your Score is Not Enough";
        }
    }

    private void UpdateCharacter(int selectOption)
    {
        if (characterDatabase.owned.Count == 0)
        {
            Debug.LogError("No owned characters available!");
            return;
        }

        if (selectOption < 0 || selectOption >= characterDatabase.owned.Count)
        {
            Debug.LogError($"Invalid selectOption: {selectOption}. Owned Count: {characterDatabase.owned.Count}");
            return;
        }

        SelectCharacter selectCharacter = characterDatabase.GetCharacter(selectOption, "owned");
        sprite.sprite = selectCharacter.characterSprite;
        nameText.text = selectCharacter.characterName;
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
