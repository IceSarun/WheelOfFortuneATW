using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerItem : MonoBehaviour
{

    //private int speed;
    private int timeItem;
    private Rigidbody body;
    public CharacterCreation characterDB;
    private Character character;
    private RandomItem randomItem;
    private bool haveAddTimeAbility = false;
    private bool haveSubTimeAbility = false;
    private int[] percent;
    public Image stopImage;
    public Image stopBackground;
    public TMP_Text timeADDSUB;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        stopImage.enabled = false;
        stopBackground.enabled = false;
        timeADDSUB.enabled = false;
        /*character = characterDB.getCharacter();
        for (int i = 1; i<= character.getAbilityCount();i++) 
        {
            if (character.abilityCode[i].ToString() == "ADD_TIME") {
                haveAddTimeAbility = true;
                percent[0] = character.value[i];
            }
            if (character.abilityCode[i].ToString() == "SUB_TIME")
            {
                haveSubTimeAbility = true;
                percent[1] = character.value[i];
            }
        }*/


    }

    public void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Item")) 
        {
            timeItem = collider.gameObject.GetComponentInParent<RandomItem>().getAbility();

            //show value time 
            timeADDSUB.enabled = true;
            int showTime = 0;
            showTime = timeItem * 10;

            //Debug.Log(timeItem);
            if (timeItem < 0) {
                /*if (haveAddTimeAbility && Random.Range(0,100) < percent[0]) {
                    timeItem -= 1;
                }*/
                showTime = timeItem * 10;
                timeADDSUB.text = showTime.ToString()+"s";
                timeADDSUB.color = Color.green;
                int count = 0;
                if (true) {
                    while ((timeItem * (-1)) > count) {
                        Timer.SubtractTime();
                        count++;
                    } }
                //speed
                StartCoroutine(FreezTime(5));
            }
            else if (timeItem > 0)
            {
                /*if (haveSubTimeAbility && Random.Range(0, 100) < percent[1])
                {
                    timeItem -= 1;
                }*/
                
                timeADDSUB.text = "+" + showTime.ToString()+"s";
                timeADDSUB.color = Color.red;
                int count = 0;
                while (timeItem > count)
                {
                    Timer.AddTime();
                    count++;
                }
                StartCoroutine(showTextWhenAddTime(5));
            }

            Destroy(collider.gameObject);
            
        }
    }

    IEnumerator FreezTime(int cooldown)
    {
        body.constraints = RigidbodyConstraints.FreezePosition;
        stopImage.enabled = true;
        stopBackground.enabled = true;
        yield return new WaitForSeconds(cooldown);
        body.constraints = RigidbodyConstraints.None;
        stopImage.enabled = false;
        stopBackground.enabled = false;
        timeADDSUB.enabled = false;
    }

    IEnumerator showTextWhenAddTime(int cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        timeADDSUB.enabled = false;
    }


}
