using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    //public GameObject[] Items;
    public ItemCreation itemDB;
    public int abilityItem;
    private int randomItem;
    public GameObject itemParent;
    private Vector3 scaleObject;
    private Vector3 scaleCollider;
    private int spinSpeed = 30;
    private GameObject obj;
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
        randomItem = Random.Range(0, itemDB.itemCount());
        Item itemInstantiate = itemDB.getItem(randomItem);

        //obj = Instantiate(Items[randomItem] , new Vector3( 260,412,-507 ), Items[randomItem].transform.rotation);
        obj = Instantiate(itemDB.getItem(randomItem).getObjectItem(), new Vector3(260, 412, -507), itemDB.getItem(randomItem).getObjectItem().transform.rotation);
        obj.transform.SetParent(itemParent.transform);

        // Random location x , z with 
        int randomNumberX = Random.Range(1, 10);
        int randomNumberZ = Random.Range(1, 10);

        // Set Ability
        abilityItem = Random.Range(-6, 6);

        //set ตุณสมบัติ item
        switch (obj.gameObject.name)
        {
            case "water(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x+randomNumberX , itemParent.transform.position.y , itemParent.transform.position.z+randomNumberZ); ;
                //scale
                scaleObject = new Vector3(0.004f, 0.004f, 0.004f);
                scaleCollider = new Vector3(300,300,300);
                break;

            case "rice(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 1.9f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(0.2f, 0.2f, 0.2f);
                scaleCollider = new Vector3(10, 10, 10);
                break;
            case "banana(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 2.0f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(0.3f, 0.3f, 0.3f);
                scaleCollider = new Vector3(12, 12, 12);
                break;
            case "garland(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 1.9f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(3f, 3f, 3f);
                scaleCollider = new Vector3(0.5f,0.5f ,0.5f);
                break;

        }

        //add collider
        obj.gameObject.AddComponent<BoxCollider>();
        obj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        obj.gameObject.GetComponent<BoxCollider>().size = scaleCollider;

        //obj.gameObject.GetComponent<Item>().setName(obj.gameObject.name);
        //obj.gameObject.GetComponent<Item>().setAbility(ability);
        
        //Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getNameItem());
        //Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getAbility());
        
        Light lightComp = obj.AddComponent<Light>();
        lightComp.range = 10;
        lightComp.color = Random.ColorHSV();
        lightComp.intensity = 5;
        obj.transform.localScale = scaleObject;
        obj.tag = "Item";
    }

    IEnumerator cooldownTimeForSpawnItem()

    {
        //Debug.Log("wait a minute");
        //Debug.Log(cooldown);
        yield return new WaitForSeconds(cooldown);
        isFinish = true;
        

    }

    public int getAbility() {
        return abilityItem;
    }

}
