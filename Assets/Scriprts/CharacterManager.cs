using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase CharacterDB;
    public SpriteRenderer CharacterSprite;
    private int SelectedOption=0;

void Start()
{
    if(!PlayerPrefs.HasKey("selectedOption"))
    {
        SelectedOption=0;
    }
    else
    {
        Load();
    }

     UpdateCharacter(SelectedOption);
}


public void NextOption()
{
    SelectedOption++;
    if(SelectedOption >= CharacterDB. CharacterCount)
    {
        SelectedOption=0;
    }
    UpdateCharacter(SelectedOption);
    Save();
}

public void backOption()
{
    SelectedOption--;

    if(SelectedOption<0)
    {
        SelectedOption=CharacterDB.CharacterCount-1;
    }

     UpdateCharacter(SelectedOption);
     Save();
}
    

private void UpdateCharacter(int SelectedOption)
{
    Character character= CharacterDB.GetCharacter(SelectedOption);
    CharacterSprite.sprite= character. characterSprite;
}

private void Load()
{
    SelectedOption=PlayerPrefs.GetInt("selectedOption");
}

private void Save()
{
    PlayerPrefs.SetInt("selectedOption", SelectedOption);
}

public void ChangeScene(int SceneID)
{
    SceneManager.LoadScene(2);
}
}