using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    // public SelectCharacter[] characters;

    // public int CharacterCount 
    // { 
    //     get { return characters.Length; } 
    // }

    // public SelectCharacter GetCharacter(int index) 
    // {
    //     return characters[index];
    // }

    public List<SelectCharacter> characters = new List<SelectCharacter>();

    public List<SelectCharacter> owned = new List<SelectCharacter>();

    public int CharacterCount 
    { 
        get { return characters.Count; } 
    }

    public SelectCharacter GetCharacter(int index, string type)
    {
        if (type == "store")
        {
            if (index < 0 || index >= characters.Count)
            {
                Debug.LogWarning($"Invalid index {index} for 'store'. Valid range: 0 to {characters.Count - 1}. Returning null.");
                return null;
            }
            return characters[index];
        }
        else
        {
            if (index < 0 || index >= owned.Count)
            {
                Debug.LogWarning($"Invalid index {index} for 'owned'. Valid range: 0 to {owned.Count - 1}. Returning null.");
                return null;
            }
            return owned[index];
        }
    }

    public void AddCharacter(SelectCharacter newCharacter)
    {
        owned.Add(newCharacter);
    }

}
