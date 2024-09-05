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
    public GameObject EndGameCanvas;
    public GameObject WinTemplete;
    public GameObject LoseTemplete;


    public TMP_Text roundText;
    public TMP_Text speed;

    private String textCheck;
    //public TMP_Text checkText;
    Timer timer;

    public int speedOfCar;
    public bool isStop = false;
    private int countRound = 0;
    private int countAllCheckpointWithTrigger = 0;
    private int[] checkCountOfCheckpoint = new int[2];


    private void Start()
    {   
        textCheck = "Lose";
        //Debug.Log(round.roundToWin);
        //use round to win from AI enenmy
        roundText.text = countRound.ToString() + "/" + round.roundToWin + " Round";
        realPlayer = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(realPlayer.name);
        timer = FindObjectOfType<Timer>();

        for (int i = 0; i < 2 ;i++) {
            checkCountOfCheckpoint[i] = 0;

        }
    }
    public void Update()
    {   
        if (realPlayer.GetComponent<PrometeoCarController>().carSpeed <= 0 ) {
            speed.text = "0 KM./HR.";
        }
        speedOfCar = (int) realPlayer.GetComponent<PrometeoCarController>().carSpeed;
        speed.text = speedOfCar.ToString() + " km/h ";

        //Debug.Log(round.getTimeAIWhenWin());
        
        //if (round.getTimeAIWhenWin() > 0) {
            //Debug.Log("player " + (int)timer.timeToDisplay);
        //}

        //Detected กรณีที่ผู้เล่นยังไม่เข้าเส้นชัยสักที
        if ((round.getTimeAIWhenWin() > 0) && (round.getTimeAIWhenWin() + 60 <= (int)timer.timeToDisplay)) {
            textCheck = "End";
            LoseTemplete.SetActive(true);
            Time.timeScale = 0;
            //SceneManager.LoadScene("Lose");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CheckReturn"))
        {
            countAllCheckpointWithTrigger++;
            //Debug.Log("All" + countAllCheckpointWithTrigger);
            checkCountOfCheckpoint[collider.GetComponent<Checkpoint>().checkpointID]++;
            //Debug.Log("CheckPoint [ " + collider.GetComponent<Checkpoint>().checkpointID + " ] = " + checkCountOfCheckpoint[collider.GetComponent<Checkpoint>().checkpointID]);
            
            //case วนเข้าออกแลปแรก
            if (checkCountOfCheckpoint[0] >= 2 && checkCountOfCheckpoint[1] == 0) {
                checkCountOfCheckpoint[0] = 1;
            }
            //case วนเข้าออกแลปสอง
            if (checkCountOfCheckpoint[0] >= 1 && checkCountOfCheckpoint[1] >=1)
            {
                checkCountOfCheckpoint[1] = 1;
            }
            //case วนตรงตามเงื่อนไข
            if (checkCountOfCheckpoint[0] == 2 && checkCountOfCheckpoint[1] == 1) {
                countRound++;
                checkCountOfCheckpoint[0] = 1;
                checkCountOfCheckpoint[1] = 0;
            }
            roundText.text = countRound.ToString() + "/" + round.roundToWin + " Round";

        }
        
        //checkText.text = timer.timeToDisplay.ToString();
        if (collider.CompareTag("Stop")) {
            if (countRound >= round.roundToWin)
            {
                textCheck = "End";
                EndGameCanvas.SetActive(true);
                if ((textCheck == "End" && round.getTimeAIWhenWin() == 0) || round.getTimeAI() > (int)timer.timeToDisplay)
                {
                    //Debug.Log("AI = "+ round.getTimeAI());
                    //Debug.Log("Player = " + (int)timer.timeToDisplay);
                    WinTemplete.SetActive(true);
                    Time.timeScale = 0;
                    //SceneManager.LoadScene("Win");
                }
                else if (textCheck == "End" && round.getTimeAI() <= (int)timer.timeToDisplay)
                {
                    //Debug.Log("AI = " + round.getTimeAI());
                    //Debug.Log("Player = " + (int)timer.timeToDisplay);
                    LoseTemplete.SetActive(true);
                    Time.timeScale = 0;
                    //SceneManager.LoadScene("Lose");
                }

                //timer.timeToDisplay //return time value when real player win!!
            }
        }
        
    }
    public int getCountCheckpoint() { 
        return countAllCheckpointWithTrigger;
    }

}
