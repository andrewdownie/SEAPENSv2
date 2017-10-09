using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//TODO: need to add damage

public class Effect : SerializedMonoBehaviour {
	[SerializeField]
	Effects effects;
	[SerializeField]
	EffectCategory effectCategory;



    [TabGroup("Attribute Effects")][SerializeField]
    Dictionary<AttributeEnum, int> attributeEffects;
	[TabGroup("Stat Effect")][SerializeField]
	Dictionary<StatEnum, int> statEffects;
	[TabGroup("Percent Stat Effects")][SerializeField]
	Dictionary<StatEnum, int> percentStatEffects;

	void Start(){
		GatherRefs();
		Setup();
		((ISEAComponent)effects).UpdateSEAComponent();
	}
	void OnValidate(){
		GatherRefs();
		Setup();
		((ISEAComponent)effects).UpdateSEAComponent();
	}

	void Setup(){
		if(attributeEffects == null){
			attributeEffects = new Dictionary<AttributeEnum, int>();
			foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
				attributeEffects.Add(ae, 0);
			}
		}

		if(statEffects == null){
			statEffects = new Dictionary<StatEnum, int>();
			foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
				statEffects.Add(se, 0);
			}
		}

		if(percentStatEffects == null){
			percentStatEffects = new Dictionary<StatEnum, int>();
			foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
				percentStatEffects.Add(se, 0);
			}
		}
	}

	void GatherRefs(){
		effects = transform.parent.GetComponent<Effects>();
	}

	public EffectCategory EffectCategory{
		get{return effectCategory;}
	}
	
	
	public void SetupEffect(EffectCategory category, Dictionary<AttributeEnum, int> atts, Dictionary<StatEnum, int> stats, Dictionary<StatEnum, int> percentStats){
		effectCategory = category;
		attributeEffects = atts;
		statEffects = stats;
		percentStatEffects = percentStats;
	}

	public void SetupEffect(Effect exisitingEffect){
		//TODO: how to copy the effect stuff into this effect
		//TODO: create an effect data class that has the dictionaryies that Effect contains but is just a struct that can be passed around?
	}

	///
	///						Tally stats
	///
	public Dictionary<StatEnum, int> TallyStats(Dictionary<StatEnum, int> tallyDict){
		return _TallyStats(statEffects, tallyDict);
	}

	public Dictionary<StatEnum, int> TallyPercentStats(Dictionary<StatEnum, int> tallyDict){
		return _TallyStats(percentStatEffects, tallyDict);
	}

	Dictionary<StatEnum, int> _TallyStats(Dictionary<StatEnum, int> addFromDict, Dictionary<StatEnum, int> tallyDict){
		if(tallyDict == null){
			return null;
		}
		Setup();

		foreach(StatEnum se in addFromDict.Keys){
			if(tallyDict.ContainsKey(se)){
				tallyDict[se] += addFromDict[se];
			}
			else{
				tallyDict.Add(se, addFromDict[se]);
			}
		}

		return tallyDict;
	}


	///
	///						Positive / Negative getters (readonly)
	///
	public Dictionary<StatEnum, int> PositiveStatEffects(){
		Dictionary<StatEnum, int> positiveStatEffects = new Dictionary<StatEnum, int>();

		foreach(KeyValuePair<StatEnum, int> kvp in statEffects){
			if(kvp.Value > 0){
				positiveStatEffects.Add(kvp.Key, kvp.Value);
			}
		}

		return positiveStatEffects;
	}

	public Dictionary<StatEnum, int> NegativeStatEffects(){
		Dictionary<StatEnum, int> negativeStatEffects = new Dictionary<StatEnum, int>();

		foreach(KeyValuePair<StatEnum, int> kvp in statEffects){
			if(kvp.Value < 0){
				negativeStatEffects.Add(kvp.Key, kvp.Value);
			}
		}

		return negativeStatEffects;
	}

	public Dictionary<StatEnum, int> PositivePercentStatEffects(){
		Dictionary<StatEnum, int> positivePercentStatEffects = new Dictionary<StatEnum, int>();

		foreach(KeyValuePair<StatEnum, int> kvp in percentStatEffects){
			if(kvp.Value > 0){
				positivePercentStatEffects.Add(kvp.Key, kvp.Value);
			}
		}

		return positivePercentStatEffects;
	}

	public Dictionary<StatEnum, int> NegativePercentStatEffects(){
		Dictionary<StatEnum, int> negativePercentStatEffects = new Dictionary<StatEnum, int>();

		foreach(KeyValuePair<StatEnum, int> kvp in percentStatEffects){
			if(kvp.Value > 0){
				negativePercentStatEffects.Add(kvp.Key, kvp.Value);
			}
		}

		return negativePercentStatEffects;
	}



	///
	///						Basic Getters (readonly)
	///
	public int AttributeEffect(AttributeEnum attEnum){
		if(attributeEffects.ContainsKey(attEnum)){
			return attributeEffects[attEnum];
		}

		return 0;
	}

	public int StatEffect(StatEnum statEnum){
		if(statEffects.ContainsKey(statEnum)){
			return statEffects[statEnum];
		}

		return 0;
	}

	public int PercentStatEffect(StatEnum statEnum){
		if(percentStatEffects.ContainsKey(statEnum)){
			return percentStatEffects[statEnum];
		}

		return 0;
	}

}


public enum EffectCategory{
	equipment,
	active,
	instant,
	toggle,
}