using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform cameraTransform;
    public float speed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;

    void FixedUpdate()
     {
     Vector3 dPos = cameraTransform.position + dist;
     Vector3 sPos = Vector3.Lerp(transform.position, dPos, Time.deltaTime * speed);
     transform.position = sPos;
     transform.LookAt(lookTarget.position);
    }

    

   }
