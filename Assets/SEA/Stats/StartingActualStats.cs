using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingActualStats : Stats
{
	ActualAttributes actualAttributes;
	PurchasedStats purchasedStats;
	StartingStats startingStats;

	protected override void GatherRefs()
	{
		actualAttributes = GetComponent<ActualAttributes>();
		purchasedStats = GetComponent<PurchasedStats>();
		startingStats = GetComponent<StartingStats>();
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
				fromAtts = StatAttRatio.GetRatio(se, ae) * actualAttributes[ae];
			}

			statDict[se] += fromAtts + startingStats[se] + purchasedStats[se];
		}
	}






}
