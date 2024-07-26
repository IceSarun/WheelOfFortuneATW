using UnityEngine;
using UnityEngine.UIElements;

public class Move: MonoBehaviour {

    public float speedY = 0.5f;
    public float speedX = 0f;
    public float speedZ = 0f;
    public float height = 0.2f;


    void Update()
    {
        if (speedX != 0) {
            //calculate what the new X position will be
            float newX = Mathf.Sin(Time.time * speedX) * height + transform.position.x;
            //set the object's X to the new calculated X
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else if (speedY != 0)
        {
            //calculate what the new Y position will be
            float newY = Mathf.Sin(Time.time * speedY) * height + transform.position.y;
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (speedZ != 0)
        {
            //calculate what the new Z position will be
            float newZ = Mathf.Sin(Time.time * speedZ) * height + transform.position.z;
            //set the object's Z to the new calculated Z
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
    }
}
