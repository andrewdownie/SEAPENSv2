using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class ActualAttributes : Attributes
{
	[TabGroup("GetComponent References")][SerializeField]
    BaseAttributes baseAtts;
    [TabGroup("GetComponent References")][SerializeField]
    Effects effects;

    protected override void _UpdateSEAComponent()
    {
        foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
            attributeDict[ae] = baseAtts[ae];
        }

        // Go through each effect, and add their attributes to our player
        foreach(Effect e in effects.EffectList){
            foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
                attributeDict[ae] += e.AttributeEffect(ae);
            }
        }
    }

    protected override void GatherRefs(){
        baseAtts = GetComponent<BaseAttributes>();
        effects = GetComponent<Effects>();
    }
}



