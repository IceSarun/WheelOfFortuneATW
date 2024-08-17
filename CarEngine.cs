using GLTF.Schema;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class CarEngine : MonoBehaviour
{
    public Transform[] allPath = null;
    private int numberOfPath;
    private Transform path;
    private Dictionary<int, Transform> pathList;
    public float turnSpeed = 30f;
    private float timer;
    private int timeWhenWin = 0;

    //set wheel angle
    private int correctNode = 0;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    //speed of car
    public float maxMotorTouqe = 100f;
    public float currentSpeed;
    public float maxSpeed = 150f;


    //braking
    public bool isBraking = false;
    public float maxBrakeTouqe = 150f;

    [Header("Sensors")]
    public float sensorLength = 1f;
    public Vector3 frontSensorVector = new Vector3(0, 0.2f, 0.5f);
    public float frontSenSor = 0.5f;
    public float sideSenSor = 0.5f;
    public float sensorAngle = 30;
    private bool avoiding = false;
    //private float targetSteerAngle = 0;

    //Item
    private bool item = false;

    //Check Player
    public GameObject realPlayer;
    private bool isReturn = true;
    public int[] requirePoint = null;

    //Check Round
    public int roundToWin;
    private int countCheckReturn = 0;
    private int countCheckpoint = 0;
    private int countAllCheckpointInPath;
    private bool isEnd = false;
    //int stopCount = 0;
    //public Lap lapOfRealPlayer;

    //Character
    public CharacterCreation characterDB;
    public Sprite artworkSprite;
    public Image charImage;
    private int randomOption = 0;

    void Start()
    {

        //set Time
        timer = 0f;
        //set character
        randomOption = Random.Range(0, characterDB.characterCount());
        Character character = characterDB.getCharacter(randomOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        //set Path
        numberOfPath = Random.Range(0, allPath.Length - 1);
        //Debug.Log(requirePoint[numberOfPath]);
        //path = allPath[numberOfPath];
        path = allPath[0];
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        countAllCheckpointInPath = pathTransforms.Length;
        pathList = new Dictionary<int, Transform>();
        for (int i = 0; i < pathTransforms.Length - 1; i += 1)
        {
            if (i == pathTransforms.Length - 1)
            {
                pathList.Add(i, pathTransforms[0]);
            }
            else
            {
                pathList.Add(i, pathTransforms[i + 1]);
            }

        }

    }
    private void Update(){
        timer += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckPlayer();
        CheckWaypointDistance();
        Braking();
        CheckLapCount();
        //LerpToSteerAngle();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CheckReturn"))
        {
            isReturn = !isReturn;
            countCheckReturn += 1;
            //Debug.Log("return!");
            //Debug.Log("Return =" + countCheckReturn);
           
        }
        if (collider.CompareTag("Stop"))
        {

            //stopCount += 1;
            //Debug.Log("stopCount = "+ stopCount);
            if (isEnd == true) {
                timeWhenWin = (int)timer;
                //Debug.Log("Win at "+ timeWhenWin);
                Destroy(this);
                //SceneManager.LoadScene("UniversalMap");
            }
        }
    }

    private void Sensors()
    {
        RaycastHit hit; // laser
        //Vector3 sensorsStartPos = transform.position + frontSensorVector;
        Vector3 sensorsStartPos = transform.position ;
        sensorsStartPos += transform.forward * frontSensorVector.z;
        sensorsStartPos += transform.up * frontSensorVector.y;
        float avoidMultiplyer = 0;
        avoiding = false;

        //Right
        sensorsStartPos += transform.right * sideSenSor;
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        {//show sensor
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                //เลี้ยวหลบ
                avoidMultiplyer -= 1f;
            }
        }
        //Right angle sensor
        else if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer -= 0.5f;
            }
        }


        //Left
        sensorsStartPos -= transform.right * sideSenSor * 2;
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        { //show sensor
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer += 1f;
            }
        }
        //Left angle sensor
        else if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidMultiplyer += 0.5f;
            }
        }

        //fornt center sensor
        if (avoidMultiplyer == 0) {
            //Center
            if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
            {   //show sensor
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Debug.DrawLine(sensorsStartPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplyer = -1;
                    }
                    else {
                        avoidMultiplyer = 1;
                    }
                }
            }

        }

        
        if (avoiding)
        {
           //smooth turn
           //targetSteerAngle = maxSteerAngle * avoidMultiplyer;
           wheelFL.steerAngle = maxSteerAngle * avoidMultiplyer;
           wheelFR.steerAngle = maxSteerAngle * avoidMultiplyer;

        }
    
    }

    private void ApplySteer() {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(pathList[correctNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude)* maxSteerAngle;
        //targetSteerAngle = newSteer;
        wheelFL.steerAngle= newSteer;
        wheelFR.steerAngle= newSteer;
   
    }

    private void Drive() {
        currentSpeed = 4 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000; 

        if (0 < currentSpeed && currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTouqe;
            wheelFR.motorTorque = maxMotorTouqe;
            
        }
        else if (item)
        {
            wheelFL.motorTorque = maxMotorTouqe*2;
            wheelFR.motorTorque = maxMotorTouqe*2;


        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }

    }

    private void CheckWaypointDistance() {
        
        if (pathList.ContainsKey(correctNode) && Vector3.Distance(transform.position, pathList[correctNode].position) < 10f) {
            if (correctNode == pathList.Count - 1)
            {
                correctNode = 0;
                countCheckpoint += 1;
                
            }
            
            else {
                correctNode++;
                countCheckpoint += 1;
            }
            //Debug.Log("corectNode current = " + correctNode);
            //Debug.Log("allCheckpoint =" + countCheckpoint);
        }
    }

    private void Braking() {
        if (isBraking)
        {
            wheelRL.brakeTorque = maxBrakeTouqe;
            wheelRR.brakeTorque = maxBrakeTouqe;
        }
        else {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;

        }
    }

    //private void LerpToSteerAngle() {
    //  wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);
    //  wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);

    //}

    private void CheckPlayer() {

        if (correctNode == requirePoint[numberOfPath] + 1 || correctNode == 0)
        {
            Vector3 positionPlayer = realPlayer.transform.position;
            Vector3 positionAI = transform.position;
            Vector3 trackDir;

            // ระยะห่างระหว่างจุด checkpoint ปัจจุบันกับอันถัดไป
            if (pathList.Count < correctNode + 1)
            {
                trackDir = pathList[correctNode].position - pathList[correctNode + 1].position;
            }
            else
            {
                trackDir = pathList[correctNode].position - pathList[0].position;
            }
            Vector3 distancePosition = positionAI - positionPlayer;
            // ระยะห่างระหว่างจุด checkpoint ปัจจุบันกับผู้เล่น หรือ AI
            float distance = Vector3.Dot(distancePosition, trackDir);

            //Debug.Log(positionPlayer);
            //Debug.Log(positionAI);
            
            
            if ((distance > 0 && isReturn == false) || (distance < 0 && isReturn == true))
            {
                  if ((correctNode != 0 || correctNode != requirePoint[numberOfPath] || correctNode + 1 != requirePoint[numberOfPath]))
                  {
                         Debug.Log("วัตถุ A อยู่ด้านหน้าวัตถุ B");

                        //correctNode -= 2;
                        //countCheckpoint -= 2;
                        path = allPath[1];
                        Transform[] pathTransformsNew = path.GetComponentsInChildren<Transform>();
                        //Debug.Log(path.GetComponent<Transform>().name);
                        for (int i = correctNode; i < pathTransformsNew.Length; i += 1)
                        {
                            if (pathTransformsNew[i] != path.transform)
                            {
                                pathList[i] = pathTransformsNew[i];
                            }
                        }

                    //Debug.Log("corectNode = " + correctNode);
                    //Debug.Log("countCheckpoint = " + countCheckpoint);


                    //Debug.Log("corectNode = " + correctNode);
                  }
            }
            else if ((distance < 0 && isReturn == false) || (distance > 0 && isReturn == true))
            {
                   if ((correctNode != 0 || correctNode != requirePoint[numberOfPath] || correctNode + 1 != requirePoint[numberOfPath]))
                   {
                            Debug.Log("วัตถุ A อยู่ด้านหลังวัตถุ B");
                            //correctNode += 1;
                            //countCheckpoint += 1;
                            //Debug.Log("countCheckpoint = " + countCheckpoint);
                            path = allPath[2];
                            Transform[] pathTransformsNew = path.GetComponentsInChildren<Transform>();
                            //Debug.Log(path.GetComponent<Transform>().name);
                            for (int i = correctNode; i < pathTransformsNew.Length; i += 1)
                            {
                                if (pathTransformsNew[i] != path.transform)
                                {
                                    pathList[i] = pathTransformsNew[i];
                                }       
                            }
                    
                    }   
            }
            else
            {
                        //Debug.Log("วัตถุ A ตั้งฉากกับวัตถุ B");

            }
                    //Debug.Log("allCheckpoint =" + countCheckpoint);
         }
        
        
    }

    private void CheckLapCount() {
        int allCheckpoint = (roundToWin * countAllCheckpointInPath) - (roundToWin * 1) ;
        int allReturn = (roundToWin * 2) + 1 ;

        //Debug.Log(allCheckpoint);
        for (int j = 3; j < allReturn ;j+=2) {
            if(countCheckReturn == j) {
                path = allPath[0];
                Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
                pathList = new Dictionary<int, Transform>();
                for (int i = 0; i < pathTransforms.Length-1; i += 1)
                {
                    if (i == pathTransforms.Length - 1)
                    {
                        pathList.Add(i, pathTransforms[0]);
                    }
                    else {
                        pathList.Add(i, pathTransforms[i+1]);
                    }
                    
                }
            }
            
        }
        if (countCheckpoint >= allCheckpoint && countCheckReturn >= allReturn) {
            isEnd = true;
            //Debug.Log("WIN");
        }
    }

    public int getTimeAI() {
        return timeWhenWin;
    }
}
