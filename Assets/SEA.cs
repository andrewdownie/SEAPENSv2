using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//Stat, effect, attribute


//TOOD: the problem I'm on right now, is: how do I share info between the system pieces? I could make everything public, but that feels dirty

public class SEA : MonoBehaviour{
	[TabGroup("Update Chain")][SerializeField]
	Dictionary<ISEA_System, List<ISEA_System>> updateChain;//When a class that is part of the SEA system updates, it will call a method on SEA saying it updated, and pass its type. This passed in type would then be put into the dictionary to get a list of dependent class, 
											// each dependent class would then be gone through, and their update function would be called as well.


	[TabGroup("GetComponent References")][SerializeField]
	public StartingAttributes startingAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public PurchasedAttributes purchasedAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public BaseAttributes baseAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public ActualAttributes actualAtts;

	void OnValidate(){
		GatherRefs();
	}

	void Awake() {
		GatherRefs();
	}
	


	void GatherRefs(){
		startingAtts = GetComponent<StartingAttributes>();	
		purchasedAtts = GetComponent<PurchasedAttributes>();	
		baseAtts = GetComponent<BaseAttributes>();	
		actualAtts = GetComponent<ActualAttributes>();	
		
	}


	/////
	/////						Update chain related methods
	/////
	public void UpdateChain(ISEA_System sea_system){
		/* 
			This gets called everytime a system piece finishes updating,
			It will trigger dependent system pieces to update as well
		*/
		SetupUpdateChain();



		if(updateChain == null){
			updateChain = new Dictionary<ISEA_System, List<ISEA_System>>();
		}

		if(updateChain.ContainsKey(sea_system)){
			List<ISEA_System> toCall = updateChain[sea_system];

			foreach(ISEA_System s in toCall){
				s.UpdatePiece();
			}
		}
	}


	void SetupUpdateChain(){
		if(updateChain == null){
			updateChain = new Dictionary<ISEA_System, List<ISEA_System>>();
		}


		//BaseAttributes
		SetupUpdateChainItem(startingAtts, new List<ISEA_System>{baseAtts});
		SetupUpdateChainItem(purchasedAtts, new List<ISEA_System>{baseAtts});


		//ActualAttributes
		SetupUpdateChainItem(baseAtts, new List<ISEA_System>{actualAtts});
		//Need to add  effects to grab atts from effects
	}

	void SetupUpdateChainItem(ISEA_System caller, List<ISEA_System> callees){

		if(updateChain.ContainsKey(caller)){
			updateChain[caller] = callees;
		}
		else{
			updateChain.Add(caller, callees);
		}

	}

}


