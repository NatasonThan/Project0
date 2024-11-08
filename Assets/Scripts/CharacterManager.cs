using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public TextMeshProUGUI nameText;
    public SpriteRenderer sprite;
    private int selectedOption = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else 
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    public void NextOption() 
    {
        selectedOption++;

        if (selectedOption >= characterDatabase.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }
    public void BackOption() 
    {
        selectedOption--;
        if (selectedOption < 0) 
        {
            selectedOption = characterDatabase.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }
    public void Load() 
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    private void Save() 
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
    public void ChangeScene(int sceneID) 
    {
        SceneManager.LoadScene(sceneID);
    }
    private void UpdateCharacter(int selectOption) 
    {
        SelectCharacter selectCharacter = characterDatabase.GetCharacter(selectOption);
        sprite.sprite = selectCharacter.characterSprite;
        nameText.text = selectCharacter.characterName;
    }

}
