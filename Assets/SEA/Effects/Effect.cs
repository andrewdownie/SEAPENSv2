using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
		((ISEAComponent)effects).UpdateSEAComponent();
	}
	void OnValidate(){
		GatherRefs();
		((ISEAComponent)effects).UpdateSEAComponent();
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