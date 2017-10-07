using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : Stats{
	StartingStats startingStats;
	BaseAttributes baseAtts;

    protected override void GatherRefs()
    {
		startingStats = GetComponent<StartingStats>();
		baseAtts = GetComponent<BaseAttributes>();
    }

    protected override void _UpdateSEAComponent()
    {



		List<StatEnum> keys = new List<StatEnum>(statDict.Keys);	
		foreach(StatEnum se in keys){
			if(statDict.ContainsKey(se)){
				statDict[se] = 0;
			}
			else{
				statDict.Add(se, 0);
			}
		}

		foreach(StatEnum se in StatAttRatio.StatEnumKeys()){
			foreach(AttributeEnum ae in StatAttRatio.AttributeEnumKeys(se)){
				Debug.Log(se + " " + ae);
				statDict[se] += StatAttRatio.GetRatio(se, ae) * baseAtts[ae] + startingStats[se];
			}
		}


    }

}
