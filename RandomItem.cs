using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public GameObject[] Items;
    private int randomItem;
    public GameObject itemParent;
    private Vector3 scaleChange;
    private int spinSpeed = 30;
    private GameObject obj;
    private int ability;
    private int cooldown = 30;
    private bool isFinish = false;

    void Start()
    {
        createItem();
    }

    private void Update()
    {
        if (obj != null)
        {
            obj.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
        else {
            StartCoroutine(cooldownTimeForSpawnItem());
        }

        if (isFinish  && obj == null) { 
            createItem();
            isFinish = false;
        }
    }
    public void createItem()
    {
        randomItem = Random.Range(0, Items.Length);

        obj = Instantiate(Items[randomItem] , new Vector3( 260,412,-507 ), Items[randomItem].transform.rotation);
        obj.transform.SetParent(itemParent.transform);
        // Random location x , z with 
        int randomNumberX = Random.Range(1, 10);
        int randomNumberZ = Random.Range(1, 10);
        // Set Ability
        ability = Random.Range(-6, 3);
        switch (obj.gameObject.name)
        {
            case "sign(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x+randomNumberX , itemParent.transform.position.y , itemParent.transform.position.z+randomNumberZ); ;
                //scale
                scaleChange = new Vector3(0.05f, 0.05f, 0.05f);
                break;

            case "rice(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 1.9f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
                break;

        }

        //set ตุณสมบัติ item
        obj.gameObject.AddComponent<BoxCollider>();
        obj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        obj.gameObject.GetComponent<BoxCollider>().size = new Vector3(20,20,20);
        obj.gameObject.AddComponent<Item>();
        obj.gameObject.GetComponent<Item>().setName(obj.gameObject.name);
        obj.gameObject.GetComponent<Item>().setAbility(ability);
        /*
        Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getNameItem());
        Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getAbility());
        */

        Light lightComp = obj.AddComponent<Light>();
        lightComp.range = 10;
        lightComp.color = Random.ColorHSV();
        lightComp.intensity = 5;
        obj.transform.localScale = scaleChange;
        obj.tag = "Item";
    }

    IEnumerator cooldownTimeForSpawnItem()

    {
        //Debug.Log("wait a minute");
        //Debug.Log(cooldown);
        yield return new WaitForSeconds(cooldown);
        isFinish = true;
        

    }

    

}
