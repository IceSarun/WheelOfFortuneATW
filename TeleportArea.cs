using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public int code;
    public float cooldown =5f;
    private GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){

            StartCoroutine(CooldownTime(other));
           
        }
    }

    IEnumerator CooldownTime(Collider other) {

        //Debug.Log("wait");
        foreach (TeleportArea tp in FindObjectsOfType<TeleportArea>())
        {
            if (tp.code == code && tp != this)
            { 
                Vector3 position = tp.gameObject.transform.position;
                position.x += 12;
                parent = GameObject.FindGameObjectWithTag("Player");
                //Debug.Log(parent.name);
                //teleport
                parent.gameObject.transform.position = position;
                // fix cooldown non use

            }
        }
        yield return new WaitForSeconds(cooldown);
    }

  

     

}
