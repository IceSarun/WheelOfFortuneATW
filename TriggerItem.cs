using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TriggerItem : MonoBehaviour
{

    //private int speed;
    private int time;
    
    public void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("item")) {
            // speed


            // time
            time = Random.Range(-9,9);
            if (time < 0) {
                int count = 0;
                while ((time*(-1)) > count ) {
                    Timer.SubtractTime();
                    count++;
                }
            }
            else if (time > 0)
            {
                int count = 0;
                while (time > count)
                {
                    Timer.AddTime();
                    count++;
                }
            }
            Destroy(collider.gameObject);
            

        }
    }



}
