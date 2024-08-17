using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string nameItem ="Unknow";
    private int ability = 0;

    public string getNameItem() 
    { 
        return nameItem; 
    }
    public void setName(string name)
    {
        nameItem = name;
    }

    public int getAbility() 
    { 
        return ability;
    }

    public void setAbility(int value)
    {
        ability = value;
    }

    

}
