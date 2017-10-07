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


    public void UpdateSEAComponent(){
		if(statDict == null){
			statDict = new Dictionary<StatEnum, int>();

			foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
				statDict.Add(se, 0);
			}
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
		get{ return statDict[se];}
	}
}


public enum StatEnum{
	walking_speed,
	health,
	dodge,
}
