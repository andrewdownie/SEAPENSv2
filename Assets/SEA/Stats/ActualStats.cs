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


		//
		//					Gather and setup the data we need
		//
		List<Effect> equipmentEffects = effects.EffectsOfCategory(EffectCategory.equipment);
		List<Effect> instantEffects = effects.EffectsOfCategory(EffectCategory.instant);
		List<Effect> activeEffects = effects.EffectsOfCategory(EffectCategory.active);
		List<Effect> toggleEffects = effects.EffectsOfCategory(EffectCategory.toggle);


		Dictionary<StatEnum, int> resultingStats;
		Dictionary<StatEnum, int> resultingPercentStats;



		//
		//					Go through equipment effect stats
		//
		resultingStats = SEA.InitStatDict();
		resultingPercentStats = SEA.InitStatDict();
		foreach(Effect e in equipmentEffects){
			resultingStats = e.TallyStats(resultingStats);
			resultingPercentStats = e.TallyPercentStats(resultingPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += resultingStats[se] + (startingActualStats[se] * resultingPercentStats[se] / 100);
		}


		//
		//					Go through active effect stats
		//
		resultingStats = SEA.InitStatDict();
		resultingPercentStats = SEA.InitStatDict();
		foreach(Effect e in activeEffects){
			resultingStats = e.TallyStats(resultingStats);
			resultingPercentStats = e.TallyPercentStats(resultingPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += resultingStats[se] + (startingActualStats[se] * resultingPercentStats[se] / 100);
			Debug.Log(resultingPercentStats[se]);
			//TODO: this is coming out as zero, even though adding an effect with a percent modifier is chaning actual stats,
		}

		//
		//					Go through instant effect stats
		//
		resultingStats = SEA.InitStatDict();
		resultingPercentStats = SEA.InitStatDict();
		foreach(Effect e in instantEffects){
			resultingStats = e.TallyStats(resultingStats);
			resultingPercentStats = e.TallyPercentStats(resultingPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += resultingStats[se] + (startingActualStats[se] * resultingPercentStats[se] / 100);
		}


		//
		//					Go through toggle effect stats
		//
		resultingStats = SEA.InitStatDict();
		resultingPercentStats = SEA.InitStatDict();
		foreach(Effect e in toggleEffects){
			resultingStats = e.TallyStats(resultingStats);
			resultingPercentStats = e.TallyPercentStats(resultingPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += resultingStats[se] + (startingActualStats[se] * resultingPercentStats[se] / 100);
		}


		//
		//					Add the starting actual stats, and clamp min value to 1
		//
		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += startingActualStats[se];

			if(statDict[se] < 1){
				statDict[se] = 1;
			}
		}
    }

}
