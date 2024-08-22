using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public CharacterCreation characterDB;
    public Sprite artworkSprite;
    public Image charImage;
    private int selectOption = 0;
    private Character character;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectOption = 0;
        }
        else
        {
            load();
        }
        updateCharacter(selectOption);
    }

    public void updateCharacter(int selectOption)
    {
        character = characterDB.getCharacter(selectOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;

    }

    private void load()
    {
        selectOption = PlayerPrefs.GetInt("selectedOption");
    }


}
