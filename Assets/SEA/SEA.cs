using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//Stat, effect, attribute


//TODO: How do I prevent users from changing values in the inspector that should not be directly editable?
//		The problem is, the exposed variables need to be exposed in the base classes, but not the dependent / children classes

public class SEA : MonoBehaviour{
	[TabGroup("Update Chain")][SerializeField]
	Dictionary<ISEAComponent, List<ISEAComponent>> updateChain;//When a class that is part of the SEA system updates, it will call a method on SEA saying it updated, and pass its type. This passed in type would then be put into the dictionary to get a list of dependent class, 
											// each dependent class would then be gone through, and their update function would be called as well.


	[TabGroup("GetComponent References")][SerializeField]
	public Effects effects;
	[TabGroup("GetComponent References")][SerializeField]
	public StartingAttributes startingAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public PurchasedAttributes purchasedAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public BaseAttributes baseAtts;
	[TabGroup("GetComponent References")][SerializeField]
	public ActualAttributes actualAtts;


	[TabGroup("GetComponent References")][SerializeField]
	public StartingStats startingStats;
	[TabGroup("GetComponent References")][SerializeField]
	public PurchasedStats purchasedStats;
	[TabGroup("GetComponent References")][SerializeField]
	public BaseStats baseStats;
	[TabGroup("GetComponent References")][SerializeField]
	public StartingActualStats startingActualStats;
	[TabGroup("GetComponent References")][SerializeField]
	public ActualStats actualStats;

	void OnValidate(){
		GatherRefs();
	}

	void Awake() {
		GatherRefs();
	}
	


	void GatherRefs(){
		effects = GetComponent<Effects>();

		startingAtts = GetComponent<StartingAttributes>();	
		purchasedAtts = GetComponent<PurchasedAttributes>();	
		baseAtts = GetComponent<BaseAttributes>();	
		actualAtts = GetComponent<ActualAttributes>();	

		baseStats = GetComponent<BaseStats>();
		purchasedStats = GetComponent<PurchasedStats>();
		startingStats = GetComponent<StartingStats>();
		startingActualStats = GetComponent<StartingActualStats>();
		actualStats = GetComponent<ActualStats>();
		
	}


	/////
	/////						Update chain related methods
	/////


	void SetupSEAComponentUpdateChain(){
		updateChain = new Dictionary<ISEAComponent, List<ISEAComponent>>();

		SetupUpdateChainItem(effects, new List<ISEAComponent>{actualAtts, actualStats});

		SetupUpdateChainItem(startingAtts, new List<ISEAComponent>{baseAtts});
		SetupUpdateChainItem(purchasedAtts, new List<ISEAComponent>{baseAtts});


		SetupUpdateChainItem(baseAtts, new List<ISEAComponent>{actualAtts, baseStats});
		SetupUpdateChainItem(startingStats, new List<ISEAComponent>{baseStats, startingActualStats});


		SetupUpdateChainItem(actualAtts, new List<ISEAComponent>{startingActualStats});


		SetupUpdateChainItem(purchasedStats, new List<ISEAComponent>{baseStats, startingActualStats});


		SetupUpdateChainItem(startingActualStats, new List<ISEAComponent>{actualStats});


	}

	public void UpdateSEAComponentChain(ISEAComponent sea_system){
		/* 
			This gets called everytime a system piece finishes updating,
			It will trigger dependent system pieces to update as well
		*/
		SetupSEAComponentUpdateChain();



		if(updateChain == null){
			updateChain = new Dictionary<ISEAComponent, List<ISEAComponent>>();
		}

		if(updateChain.ContainsKey(sea_system)){
			List<ISEAComponent> toCall = updateChain[sea_system];

			foreach(ISEAComponent s in toCall){
				s.UpdateSEAComponent();
			}
		}
	}

	void SetupUpdateChainItem(ISEAComponent caller, List<ISEAComponent> callees){

		if(updateChain.ContainsKey(caller)){
			updateChain[caller] = callees;
		}
		else{
			updateChain.Add(caller, callees);
		}

	}

	public static Dictionary<StatEnum, int> InitStatDict(){
		/*
			Creates a new dict with every key added, and set to zero.
		*/
		Dictionary<StatEnum, int> dict = new Dictionary<StatEnum, int>();


		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			dict[se] = 0;
		}

		return dict;
	}

	public static Dictionary<AttributeEnum, int> InitAttDict(){
		/*
			Creates a new dict with every key added, and set to zero.
		*/
		Dictionary<AttributeEnum, int> dict = new Dictionary<AttributeEnum, int>();


		foreach(AttributeEnum se in System.Enum.GetValues(typeof(AttributeEnum))){
			dict[se] = 0;
		}

		return dict;
	}

}


