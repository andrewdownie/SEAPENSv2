using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Effect : SerializedMonoBehaviour {
    [SerializeField]
    Dictionary<AttributeEnum, int> attributeEffects;
	[SerializeField]
	Dictionary<StatEnum, int> statEffects;
	[SerializeField]
	Dictionary<StatEnum, int> percentStatEffects;
	



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
