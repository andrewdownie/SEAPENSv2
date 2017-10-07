using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : Stats{
	StartingStats startingStats;
	PurchasedStats purchasedStats;
	BaseAttributes baseAtts;

    protected override void GatherRefs()
    {
		startingStats = GetComponent<StartingStats>();
		purchasedStats = GetComponent<PurchasedStats>();
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
			int fromAtts = 0;

			foreach(AttributeEnum ae in StatAttRatio.AttributeEnumKeys(se)){
				fromAtts += (StatAttRatio.GetRatio(se, ae) * baseAtts[ae]);
			}

			statDict[se] += fromAtts + startingStats[se] + purchasedStats[se];
		}


    }

}
