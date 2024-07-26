using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public GameObject[] Items;
    private int randomItem;
    public GameObject itemParent;
    
    void Start()
    {
        createItem();
    }

    public void createItem()
    {
        randomItem = Random.Range(0, Items.Length);

        GameObject obj = Instantiate(Items[randomItem] , new Vector3( 260,412,-507 ), Items[randomItem].transform.rotation);
        obj.transform.SetParent(itemParent.transform); 
        obj.tag = "item";
    }



}
