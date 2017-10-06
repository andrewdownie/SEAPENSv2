using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StatEffects : SerializedMonoBehaviour {
	[SerializeField]
	protected Dictionary<StatEnum, int> statEffects;


	public int this[StatEnum statsEnum]{
		get{return statEffects[statsEnum];}
	}


	public Dictionary<StatEnum, int> PositiveStatEffects{
		get{
			Dictionary<StatEnum, int> output = new Dictionary<StatEnum, int>();

			foreach(KeyValuePair<StatEnum, int> kvp in statEffects){
				if(kvp.Value > 0){
					output.Add(kvp.Key, kvp.Value);
				}
			}

			return output;
		}
	}

	public Dictionary<StatEnum, int> NegativeStatEffects{
		get{
			Dictionary<StatEnum, int> output = new Dictionary<StatEnum, int>();

			foreach(KeyValuePair<StatEnum, int> kvp in statEffects){
				if(kvp.Value < 0){
					output.Add(kvp.Key, kvp.Value);
				}
			}

			return output;
		}
	}



}
