using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Move: MonoBehaviour {

    // transform
    public float hightY = 0.5f;
    public float hightX = 0f;
    public float hightZ = 0f;
    //rotate
    public float rotateY = 0f;
    public float rotateX = 0f;
    public float rotateZ = 0f;
    //speed
    public float speed = 0.2f;

    //type
    enum ObstacelType { Wall, Slow }
    [SerializeField] private ObstacelType moveType;
   
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (hightX != 0)
            {
                //calculate what the new X position will be
                float newX = Mathf.Sin(Time.time * hightX) * speed + transform.position.x;
                //set the object's X to the new calculated X
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else if (hightY != 0)
            {
                //calculate what the new Y position will be
                float newY = Mathf.Sin(Time.time * hightY) * speed + transform.position.y;
                //set the object's Y to the new calculated Y
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else if (hightZ != 0)
            {
                //calculate what the new Z position will be
                float newZ = Mathf.Sin(Time.time * hightZ) * speed + transform.position.z;
                //set the object's Z to the new calculated Z
                transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            }
            else if (rotateY != 0) {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }
            else if (rotateX != 0)
            {
                transform.Rotate(speed * Time.deltaTime, 0, 0);
            }
            else if (rotateZ != 0)
            {
                transform.Rotate(0, 0, speed * Time.deltaTime);
            }

        }
      
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bot")) {
            if (moveType == ObstacelType.Wall) {
                gameObject.GetComponent<Collider>().isTrigger = true;
                StartCoroutine(CooldownTime(5));
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (moveType == ObstacelType.Slow)
            {
                StartCoroutine(FreezBody(5,other));
            }
        }
    }


    IEnumerator CooldownTime(int cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        gameObject.GetComponent<Collider>().isTrigger = false;
    }

    IEnumerator FreezBody(int cooldownTime, Collision other) {
        other.body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        GameObject parent = GameObject.FindGameObjectWithTag("Player");
        parent.transform.position = new Vector3(parent.transform.position.x + 30 , parent.transform.position.y, parent.transform.position.z + 30 );
        yield return new WaitForSeconds(cooldownTime);
        other.body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}

