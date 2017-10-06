using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class BaseAttributes : Attributes
{
	[TabGroup("GetComponent References")][SerializeField]
    StartingAttributes startingAtts;
	[TabGroup("GetComponent References")][SerializeField]
    PurchasedAttributes purchasedAtts;

    protected override void GatherRefs(){
        startingAtts = GetComponent<StartingAttributes>();
        purchasedAtts = GetComponent<PurchasedAttributes>();
    }


    protected override void _UpdateSEAComponent()
    {
        foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
            Debug.Log("updating base attributes sea component");
            attributeDict[ae] = startingAtts[ae] + purchasedAtts[ae];
        }

    }
}



