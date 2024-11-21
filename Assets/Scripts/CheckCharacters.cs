using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCharacters : MonoBehaviour
{
    public List<GameObject> characterObjects;

    void Start()
    {
        if (PlayerPrefs.HasKey("selectedOption"))
        {
            int selectedOption = PlayerPrefs.GetInt("selectedOption");

            for (int i = 0; i < characterObjects.Count; i++)
            {
                if (i == selectedOption)
                {
                    characterObjects[i].SetActive(true);
                }
                else
                {
                    characterObjects[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("No character selected.");
            characterObjects[0].SetActive(true);
        }
    }
}
