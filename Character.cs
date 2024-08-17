using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character 
{
    public string nameChar;
    public int characterCode;
    public Sprite imageCharacter;
    public List<EnumAbilityCode> abilityCode;
    public List<int> value;

    public int getAbilityCount() { 
        return abilityCode.Count;
    }

}
