using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public int code;
    private float countdownTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (countdownTimer > 0) {
            countdownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && countdownTimer<=0) {
            foreach (TeleportArea tp in FindObjectsOfType<TeleportArea>()) {
                if (tp.code == code && tp != this) {
                    // cool down
                    tp.countdownTimer = 2;
                    Vector3 position = tp.gameObject.transform.position;
                    position.y +=2;
                    other.gameObject.transform.position = position;
                }
            }
        }
    }

}
