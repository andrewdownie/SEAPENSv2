using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualStats : Stats
{
	Effects effects;
	StartingActualStats startingActualStats;


    protected override void GatherRefs()
    {
		effects = GetComponent<Effects>();
		startingActualStats = GetComponent<StartingActualStats>();
    }


    protected override void _UpdateSEAComponent()
    {
		//Copy the startingActualStats statsDict (this is slow, but it is readonly, which is nice)
		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			if(statDict.ContainsKey(se)){
				statDict[se] = startingActualStats[se];
			}
			else{
				statDict.Add(se, startingActualStats[se]);
			}
		}


		List<Effect> equipmentEffects = effects.EffectsOfCategory(EffectCategory.equipment);
		List<Effect> instantEffects = effects.EffectsOfCategory(EffectCategory.instant);
		List<Effect> activeEffects = effects.EffectsOfCategory(EffectCategory.active);
		List<Effect> toggleEffects = effects.EffectsOfCategory(EffectCategory.toggle);


		Dictionary<StatEnum, int> resultingStats = new Dictionary<StatEnum, int>();
		Dictionary<StatEnum, int> resultingPercentStats = new Dictionary<StatEnum, int>();

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			resultingStats.Add(se, 0);
		}
		
		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			resultingPercentStats.Add(se, 0);
		}


		// Go through positive and negative equipment effect stats
		foreach(Effect e in equipmentEffects){
			resultingStats = e.TallyStats(resultingStats);
			resultingPercentStats = e.TallyPercentStats(resultingPercentStats);
		}

		foreach(StatEnum se in startingActualStats.Keys()){
			statDict[se] = startingActualStats[se] + resultingStats[se] + (startingActualStats[se] * resultingPercentStats[se] / 100);
		}


		// Go through positive and negative active effect stats
		// Go through positive and negative instant effect stats
		// Go through positive and negative toggle effect stats

    }

}
