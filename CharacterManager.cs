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
        selectOption += 1;
        if (selectOption >=  characterDB.characterCount()) {
            selectOption = 0;
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
        abilityText.text = "";
        Character character = characterDB.getCharacter(selectOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        nameText.text = character.nameChar;
        checkAbility(character.abilityCode.ToString(), character.value);
         
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

    public void checkAbility(string textAbi, int value) {
        switch (textAbi)
        {
            case "UNSPECIFIED":
                abilityText.text += "No Ability";
                break;

            case "MAX_SPEED":
                abilityText.text += " + "+ value.ToString() + " Max Speed";
                break;

            case "WEATHER_RAIN":
                abilityText.text += "Create rain in map and Decrease the opponent's max speed by " + value.ToString();
                break;

            case "WEATHER_WIND":
                abilityText.text += "Create wind in map and Decrease the opponent's max speed by " + value.ToString();
                break;

            case "WEATHER_SMOKE":
                abilityText.text += "Create smoke in map and Decrease the opponent's max speed by " + value.ToString();
                break;

            case "WEATHER_SNOW":
                abilityText.text += "Create snow in map and Decrease the opponent's max speed by " + value.ToString();
                break;

            case "ADD_TIME":
                abilityText.text += "There's a " + value.ToString() + "% chance to reduce the time by 10 seconds when picking up a negative item.";
                break;

            case "SUB_TIME":
                abilityText.text += "There's a " + value.ToString() + "% chance to gain 10 extra seconds when picking up a positive item.";
                break;

        }

    }
}
