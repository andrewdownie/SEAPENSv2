using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public abstract class Attributes : SerializedMonoBehaviour, ISEAComponent{
	[TabGroup("Attribute Dictionary")][SerializeField]
	protected Dictionary<AttributeEnum, int> attributeDict;

	[TabGroup("GetComponent References")][SerializeField]
	protected SEA sea;


    public void UpdateSEAComponent(){
		if(attributeDict == null){
			attributeDict = new Dictionary<AttributeEnum, int>();

			foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
				attributeDict.Add(ae, 0);
			}
		}

		_UpdateSEAComponent();
		sea.UpdateSEAComponentChain(this);
	}

	protected abstract void _UpdateSEAComponent();

	void OnValidate(){
		GatherRefs();
		UpdateSEAComponent();
    }
	void Start(){
		GatherRefs();
		UpdateSEAComponent();
	}

	protected abstract void GatherRefs();



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