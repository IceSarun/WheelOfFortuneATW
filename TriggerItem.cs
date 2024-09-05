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
    public Player player;
    private Character character;
    private RandomItem randomItem;
    private int percent;
    public Image stopImage;
    public Image stopBackground;
    public TMP_Text timeADDSUB;
    public TMP_Text timeADDSUBWithItem;
    public TMP_Text skillActiveText;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        stopImage.enabled = false;
        stopBackground.enabled = false;
        timeADDSUB.enabled = false;
        timeADDSUBWithItem.enabled = false;
        skillActiveText.enabled = false;

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
            if (timeItem < 0)
            {
                if (player.percentAddOrSub() && player.checkSkill() == EnumAbilityCode.ADD_TIME)
                {
                    timeItem -= 1;
                    skillActiveText.enabled = true;
                    timeADDSUBWithItem.enabled = true;
                    skillActiveText.text = "Skill Active!";
                    timeADDSUBWithItem.text = "- 10";
                    skillActiveText.color = Color.yellow;
                    timeADDSUBWithItem.color = Color.yellow;

                }
                showTime = timeItem * 10;
                timeADDSUB.text = (showTime + 10).ToString() + "s";
                timeADDSUB.color = Color.green;
                int count = 0;
                if (true)
                {
                    while ((timeItem * (-1)) > count)
                    {
                        Timer.SubtractTime();
                        count++;
                    }
                }
                //speed
                StartCoroutine(FreezTime(5));
            }
            else if (timeItem > 0)
            {
                if (player.percentAddOrSub() && player.checkSkill() == EnumAbilityCode.SUB_TIME)
                {
                    timeItem -= 1;
                    skillActiveText.enabled = true;
                    timeADDSUBWithItem.enabled = true;
                    skillActiveText.text = "Skill Active!";
                    timeADDSUBWithItem.text = "- 10";
                    skillActiveText.color = Color.yellow;
                    timeADDSUBWithItem.color = Color.yellow;

                }
                timeADDSUB.text = "+" + (showTime + 10).ToString() + "s";
                timeADDSUB.color = Color.red;
                int count = 0;
                while (timeItem > count)
                {
                    Timer.AddTime();
                    count++;
                }
                StartCoroutine(showTextWhenAddTime(5));
            }
            else { 
                timeADDSUB.enabled = false;
                timeADDSUB.text = "0";
                timeADDSUB.color = Color.white;
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
        skillActiveText.enabled = false;
        timeADDSUBWithItem.enabled = false;
    }

    IEnumerator showTextWhenAddTime(int cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        timeADDSUB.enabled = false;
        skillActiveText.enabled = false;
        timeADDSUBWithItem.enabled = false;
    }


}
