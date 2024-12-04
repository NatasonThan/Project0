using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Extension;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterManagerStore : MonoBehaviour
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
        SelectCharacter selectCharacter = characterDatabase.GetCharacter(selectOption, "store");
        if (selectCharacter != null)
        {
            sprite.sprite = selectCharacter.characterSprite;
            nameText.text = selectCharacter.characterName;
        }
        else
        {
            Debug.LogWarning($"Character not found for index {selectOption}. Ensure the character exists in the database.");

            sprite.sprite = null;
            nameText.text = "No More Package";
        }
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(product.definition.id);
        Debug.Log(failureDescription.reason);
    }

    public void OnPurchaseCharacterComplete(Product product)
    {
        characterDatabase.RemoveCharacter(selectedOption);
        UpdateCharacter(selectedOption);
        Save();
        int score = PlayerPrefs.GetInt("HighestScore", 0);
        score += 50;
        PlayerPrefs.SetInt("HighestScore", score);
        PlayerPrefs.Save();
        Debug.Log("Buy index " + selectedOption);
        Debug.Log(product.definition.id);

        if (product == null)
        {
            Debug.LogError("Product is null. Purchase might have failed.");
            return;
        }
    }
}