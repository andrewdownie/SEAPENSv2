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

    void OnValidate(){
        GatherRefs();
    }
    void Start(){
        GatherRefs();
    }

    void GatherRefs(){
        startingAtts = GetComponent<StartingAttributes>();
        purchasedAtts = GetComponent<PurchasedAttributes>();
    }


    protected override void UpdateLocalPiece()
    {
        foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
            attributeDict[ae] = startingAtts[ae] + purchasedAtts[ae];
        }

    }
}



