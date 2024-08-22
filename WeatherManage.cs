using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using Image = UnityEngine.UI.Image;
using TMPro;

public class WeatherManage : MonoBehaviour
{
    //public Player player;
    public CharacterCreation characterDB;
    private Character playerCharacter;
    private GameObject Ai;
    private GameObject objForCreateWeaTher; // Player
    private GameObject parent;
    public WeatherCreation weatherDB;
    private Weather weatherUse;
    public Image image;
    public TMP_Text weatherText;
    private bool isAbilityWeather = false;
    private bool isHaveWeather = false;
    private int selectOption = 0;

    void Start()
    {
        //player = gameObject.GetComponent<Player>();
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectOption = 0;
        }
        else
        {
            load();
        }
        updateCharacter(selectOption);
        playerCharacter = characterDB.getCharacter(selectOption);
        Ai = GameObject.FindGameObjectWithTag("Bot");
        parent = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("1 : " + Ai.gameObject.GetComponent<AI>()..ToString() + " d ");
        //Debug.Log("1 : "+Ai.gameObject.GetComponent<AI>().character.nameChar + " ok ");

        checkWeater();
        if (isAbilityWeather)
        {
            createWeather();
        }

    }

    public void createWeather()
    {
        switch (playerCharacter.abilityCode.ToString())
        {
            case "WEATHER_RAIN":
                if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() != "WEATHER_WIND")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Rain")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.gameObject.GetComponent<RainScript>().Camera = parent.GetComponentInChildren<Camera>();
                            objForCreateWeaTher.gameObject.GetComponent<RainScript>().RainIntensity = 0.4f;
                            weatherText.text = "Weather: Rain";
                            weatherText.color = Color.green;
                        }
                    }
                }
                else if(Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() == "WEATHER_WIND") {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Wind")
                        {
                            image.enabled = false;
                            weatherUse = weatherDB.getWeather(i);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x + 10, parent.transform.position.y + 10, parent.transform.position.z + 10), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.transform.localScale = new Vector3(2, 2, 2);
                            objForCreateWeaTher.transform.Rotate(0, 0, 90);

                            weatherText.text = "Weather: Wind";
                            weatherText.color = Color.red;
                        }
                    }
                }
                break;

            case "WEATHER_WIND":
                if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() != "WEATHER_SMOKE")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Wind")
                        {
                            image.enabled = false;
                            weatherUse = weatherDB.getWeather(i);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x + 10, parent.transform.position.y + 10, parent.transform.position.z + 10), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.transform.localScale = new Vector3(2, 2, 2);
                            objForCreateWeaTher.transform.Rotate(0, 0, 90);

                            weatherText.text = "Weather: Wind";
                            weatherText.color = Color.green;
                        }
                    }
                }
                else if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() == "WEATHER_SMOKE")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Smoke")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.12f);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y - 2, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.transform.localScale = new Vector3(7, 7, 7);
                            weatherText.text = "Weather: Smoke";
                            weatherText.color = Color.red;
                        }
                    }
                }
                break;

            case "WEATHER_SMOKE":
                if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() != "WEATHER_SNOW")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Smoke")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.12f);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y-2, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.transform.localScale = new Vector3(7, 7, 7);
                            weatherText.text = "Weather: Smoke";
                            weatherText.color = Color.green;
                        }
                    }
                }
                else if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() == "WEATHER_SNOW")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Snow")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.12f);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            weatherText.text = "Weather: Snow";
                            weatherText.color = Color.red;
                        }
                    }
                }
                break;

            case "WEATHER_SNOW":
                if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() != "WEATHER_RAIN")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Snow")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.12f);
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x+7, parent.transform.position.y, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.transform.localScale = new Vector3(2, 2, 2);
                            weatherText.text = "Weather: Snow";
                            weatherText.color = Color.green;
                        }
                    }
                }
                else if (Ai.gameObject.GetComponent<CarEngine>().character.abilityCode.ToString() == "WEATHER_RAIN")
                {
                    for (int i = 0; i < weatherDB.wheathersCount(); i++)
                    {
                        if (weatherDB.getWeather(i).weatherName == "Rain")
                        {
                            weatherUse = weatherDB.getWeather(i);
                            image.sprite = weatherUse.imageWeather;
                            objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z), weatherUse.weather.transform.rotation);
                            objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                            objForCreateWeaTher.gameObject.GetComponent<RainScript>().Camera = parent.GetComponentInChildren<Camera>();
                            objForCreateWeaTher.gameObject.GetComponent<RainScript>().RainIntensity = 0.4f;
                            weatherText.text = "Weather: Rain";
                            weatherText.color = Color.red;
                        }
                    }
                }
                break;
        }
    }

    public void checkWeater()
    {
        //int randomWheater = Random.Range(0,weatherDB.wheathersCount());
        //image.enabled = true;
        //isHaveWeather = true;
        if (playerCharacter.abilityCode.ToString() == "WEATHER_RAIN" || playerCharacter.abilityCode.ToString() == "WEATHER_WIND" || playerCharacter.abilityCode.ToString() == "WEATHER_SMOKE" || playerCharacter.abilityCode.ToString() == "WEATHER_SNOW")
        {
            isAbilityWeather = true;
            image.enabled = true;
        }
        else {
            weatherText.text = "Weather: Sunny";
        }
    }

    public void updateCharacter(int selectOption)
    {
        playerCharacter = characterDB.getCharacter(selectOption);

    }

    private void load()
    {
        selectOption = PlayerPrefs.GetInt("selectedOption");
    }


}
