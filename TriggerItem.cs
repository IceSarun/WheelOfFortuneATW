using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TriggerItem : MonoBehaviour
{

    //private int speed;
    private int timeItem;
    private Rigidbody body;
    private Player player;
    private Character character;
    private bool haveAddTimeAbility = false;
    private bool haveSubTimeAbility = false;
    private int[] percent;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        /*character = player.getPlayerCharacter();
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

    private void Update()
    {
        
    }

    public void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Item")) 
        {
            timeItem = collider.gameObject.GetComponent<Item>().getAbility();
            //Debug.Log(timeItem);
            if (timeItem < 0) {
                /*if (haveAddTimeAbility && Random.Range(0,100) < percent[0]) {
                    timeItem -= 1;
                }*/
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
                int count = 0;
                while (timeItem > count)
                {
                    Timer.AddTime();
                    count++;
                }
            }

            Destroy(collider.gameObject);
            
        }
    }

    IEnumerator FreezTime(int cooldown)
    {
        body.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(cooldown);
        body.constraints = RigidbodyConstraints.None;
    }



    }
