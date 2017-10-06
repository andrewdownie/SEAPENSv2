using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class ActualAttributes : Attributes
{
	[TabGroup("GetComponent References")][SerializeField]
    BaseAttributes baseAtts;
    //TODO: need to include effects, to add attributes from effects

    protected override void _UpdateSEAComponent()
    {
        foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
            attributeDict[ae] = baseAtts[ae];
        }
    }

    protected override void GatherRefs(){
        baseAtts = GetComponent<BaseAttributes>();
    }
}



