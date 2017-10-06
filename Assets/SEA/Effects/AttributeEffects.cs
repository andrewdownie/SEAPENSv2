using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AttributeEffects : SerializedMonoBehaviour{
    [SerializeField]
    Dictionary<AttributeEnum, int> attributeEffects;


    public int this[AttributeEnum attEnum]{
        get{return attributeEffects[attEnum];}
    }
}
