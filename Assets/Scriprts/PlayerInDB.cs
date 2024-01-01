using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInDB : MonoBehaviour
{

    public CharacterDatabase CharacterDB;
    public SpriteRenderer CharacterSprite;
    private int SelectedOption=0;
    // Start is called before the first frame update
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
    

    private void UpdateCharacter(int SelectedOption)
{
    Character character= CharacterDB.GetCharacter(SelectedOption);
    CharacterSprite.sprite= character. characterSprite;
}

private void Load()
{
    SelectedOption=PlayerPrefs.GetInt("selectedOption");
}

}

