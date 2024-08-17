using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class CharacterManager : MonoBehaviour
{
    public CharacterCreation characterDB;
    public TMP_Text nameText;
    public TMP_Text abilityText;
    public Sprite artworkSprite;
    public Image charImage; 
    private int selectOption = 0;
    private int abilityCount = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption")) {
            selectOption = 0;
        }
        else{
            load();
        }
        updateCharacter(selectOption);
    }

    public void nextOption() { 
        selectOption+=1;
        if (selectOption >=  characterDB.characterCount()) {
            selectOption=0;
        }
        updateCharacter(selectOption);
        save();
    }

    public void backOption()
    {
        selectOption -= 1;
        if (selectOption < 0)
        {
            selectOption = characterDB.characterCount() -1;
        }
        updateCharacter(selectOption);
        save();
    }

    public void updateCharacter(int selectOption) { 
        Character character = characterDB.getCharacter(selectOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        nameText.text = character.nameChar;
        abilityCount = character.getAbilityCount();
        //abilityText.text = character.abilityCode[abilityCount].ToString();
        /*for (int i = 1; i <= abilityCount ;i++) {
            if (i + 1 > abilityCount)
            {
                abilityText.text = abilityText.text + character.abilityCode[i].ToString();
            }
            else {
                abilityText.text = character.abilityCode[i].ToString() + ", ";
            }
            
        }*/
    }

    private void load()
    {
        selectOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void save() { 
        PlayerPrefs.SetInt("selectedOption", selectOption);
    }

    public void changeScene() {
        SceneManager.LoadScene("Lobby");
    }
}
