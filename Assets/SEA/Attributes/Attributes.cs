using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public abstract class Attributes : SerializedMonoBehaviour, ISEAComponent{
	///
	///					The actual data for this class
	///
	[TabGroup("Attribute Dictionary")][SerializeField]
	protected Dictionary<AttributeEnum, int> attributeDict;

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
		if(attributeDict == null){
			attributeDict = new Dictionary<AttributeEnum, int>();

			foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
				attributeDict.Add(ae, 0);
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
	public int this[AttributeEnum ae]{
		get{ return attributeDict[ae];}
	}

}




public enum AttributeEnum{
	strength,
	intelligence,
	dexterity,
	constitution,
	agility,
}