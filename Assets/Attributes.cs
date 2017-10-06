using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public abstract class Attributes : SerializedMonoBehaviour, ISEA_System{
	[TabGroup("Attribute Dictionary")][SerializeField]
	protected Dictionary<AttributeEnum, int> attributeDict;

	[TabGroup("GetComponent References")][SerializeField]
	protected SEA sea;


    public void UpdatePiece(){
		UpdateLocalPiece();
		sea.UpdateChain(this);
	}

	protected abstract void UpdateLocalPiece();

    void OnValidate(){
		if(attributeDict == null){
			attributeDict = new Dictionary<AttributeEnum, int>();

			foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
				attributeDict.Add(ae, 0);
			}

		}

        sea.UpdateChain(this);
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