using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Extension;

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
        sprite.sprite = selectCharacter.characterSprite;
        nameText.text = selectCharacter.characterName;
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
        Debug.Log("Buy index "+selectedOption);
        Debug.Log(product.definition.id);
    }
}
