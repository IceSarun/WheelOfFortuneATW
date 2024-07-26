using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jumpscare : MonoBehaviour
{
    public AudioSource audioJumpScare;
    public GameObject jumpscare;

    private void Start()
    {
        jumpscare.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
            
        jumpscare.SetActive(true);
        //audio.Play();
       
            
    }
    

 
}
