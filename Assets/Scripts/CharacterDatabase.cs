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

    public SelectCharacter GetCharacter(int index , string type)
    {
        if (index < 0 || index >= characters.Count)
        {
            throw new System.IndexOutOfRangeException("Index is out of range.");
        }
        if (type == "store")
        {
            return characters[index];
        }
        else
        {
            return owned[index];
        }
    }

    public void AddCharacter(SelectCharacter newCharacter)
    {
        owned.Add(newCharacter);
    }

    public void RemoveCharacter(int index)
    {
        if (index < 0 || index >= characters.Count)
        {
            throw new System.IndexOutOfRangeException("Index is out of range.");
        }
        AddCharacter(GetCharacter(index, "store"));
        characters.RemoveAt(index);

    }

}
