using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Stats : SerializedMonoBehaviour, ISEAComponent
{
	///
	///					The actual data for this class
	///
	[TabGroup("Stat Dictionary")][SerializeField]
	protected Dictionary<StatEnum, int> statDict;

	///
	///					Protected references
	///
	[TabGroup("GetComponent References")][SerializeField]
	protected SEA sea;

	///
	///					Abstract methods
	///
	protected abstract void _UpdateSEAComponent();
	protected abstract void GatherRefs();


	///
	///					Setup on start, and respond to changes in editor
	///						DO NOT USE THESE METHODS IN CHILD CLASSES
	void OnValidate(){
		GatherRefs();
		UpdateSEAComponent();
    }
	void Start(){
		GatherRefs();
		UpdateSEAComponent();
	}
	void Setup(){
		statDict = new Dictionary<StatEnum, int>();

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statDict.Add(se, 0);
		}
	}

	public List<StatEnum> Keys(){
		return new List<StatEnum>(statDict.Keys);
	}

    public void UpdateSEAComponent(){
		if(statDict == null){
			Setup();
		}

		if(sea == null){
			sea = GetComponent<SEA>();
		}

		_UpdateSEAComponent();
		sea.UpdateSEAComponentChain(this);
	}


	///
	///					Indexer
	///
	public int this[StatEnum se]{
		get{
			if(statDict == null){
				Setup();
			}
			return statDict[se];
		}
	}
}


public enum StatEnum{
	walking_speed,
	max_health,
	dodge,
}
