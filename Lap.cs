using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Lap : MonoBehaviour
{
    public CarEngine round;
    private GameObject realPlayer;
    public int speedOfCar;
    public TMP_Text roundText;
    public TMP_Text speed;
    private String textCheck;
    private GameObject Ai;
    //public TMP_Text checkText;
    Timer timer;
    public bool isStop = false;
    private int countRound = 0;
    private int countAllCheckpointWithTrigger = 0;
    private int[] checkCountOfCheckpoint = new int[2];
    
    private void Start()
    {   
        textCheck = "Lose";
        Debug.Log(round.roundToWin);
        roundText.text = countRound.ToString() + "/" + round.roundToWin + " Round";
        Ai = GameObject.FindGameObjectWithTag("Bot");
        realPlayer = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(Ai.name);

        for (int i = 0; i < 2 ;i++) {
            checkCountOfCheckpoint[i] = 0;
        }
    }
    public void Update()
    {
        if (realPlayer.GetComponent<PrometeoCarController>().carSpeed <= 0 ) {
            speed.text = "0 KM./Hr.";
        }
        speedOfCar = (int) realPlayer.GetComponent<PrometeoCarController>().carSpeed;
        speed.text = speedOfCar.ToString() + " KM./Hr.";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CheckReturn"))
        {
            countAllCheckpointWithTrigger++;
            checkCountOfCheckpoint[collider.GetComponent<Checkpoint>().checkpointID]++;
            if (checkCountOfCheckpoint[0] == 2 && checkCountOfCheckpoint[1] == 1) {
                countRound++;
                checkCountOfCheckpoint[0] = 1;
                checkCountOfCheckpoint[1] = 0;
            }
            roundText.text = countRound.ToString() + "/" + round.roundToWin + " Round";

        }
        timer = FindObjectOfType<Timer>(); ;
        //checkText.text = timer.timeToDisplay.ToString();

        if (countRound == round.roundToWin)
            {
                textCheck = "Win";
                SceneManager.LoadScene("Win");
            //timer.timeToDisplay //return time value when real player win!!
        }

    }
    public int getCountCheckpoint() { 
        return countAllCheckpointWithTrigger;
    }

}
