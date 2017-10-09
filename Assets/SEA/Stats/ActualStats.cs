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
		//TODO: this happens at the bottom now
		
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


		Dictionary<StatEnum, int> talliedStats;
		Dictionary<StatEnum, int> talliedPercentStats;



		//
		//					Go through equipment effect stats
		//
		talliedStats = SEA.InitStatDict();
		talliedPercentStats = SEA.InitStatDict();
		foreach(Effect e in equipmentEffects){
			talliedStats = e.TallyStats(talliedStats);
			talliedPercentStats = e.TallyPercentStats(talliedPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += talliedStats[se] + (startingActualStats[se] * talliedPercentStats[se] / 100);
			Debug.Log(statDict[se]);
			//TODO: this comes out as 0 and 10000...
		}


		//
		//					Go through active effect stats
		//
		talliedStats = SEA.InitStatDict();
		talliedPercentStats = SEA.InitStatDict();
		foreach(Effect e in activeEffects){
			talliedStats = e.TallyStats(talliedStats);
			talliedPercentStats = e.TallyPercentStats(talliedPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += talliedStats[se] + (startingActualStats[se] * talliedPercentStats[se] / 100);
		}

		//
		//					Go through instant effect stats
		//
		talliedStats = SEA.InitStatDict();
		talliedPercentStats = SEA.InitStatDict();
		foreach(Effect e in instantEffects){
			talliedStats = e.TallyStats(talliedStats);
			talliedPercentStats = e.TallyPercentStats(talliedPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += talliedStats[se] + (startingActualStats[se] * talliedPercentStats[se] / 100);
		}


		//
		//					Go through toggle effect stats
		//
		talliedStats = SEA.InitStatDict();
		talliedPercentStats = SEA.InitStatDict();
		foreach(Effect e in toggleEffects){
			talliedStats = e.TallyStats(talliedStats);
			talliedPercentStats = e.TallyPercentStats(talliedPercentStats);
		}

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict[se] += talliedStats[se] + (startingActualStats[se] * talliedPercentStats[se] / 100);
		}


		//
		//					Add the starting actual stats, and clamp min value to 1
		//
		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			//statDict[se] += startingActualStats[se];

			if(statDict[se] < 1){
				statDict[se] = 1;
			}
		}
    }

}
