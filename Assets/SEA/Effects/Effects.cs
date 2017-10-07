using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Effects : MonoBehaviour, ISEAComponent{

	[SerializeField]
	GameObject effectGameObject;
	[SerializeField]
	SEA sea;

	[SerializeField]
	List<Effect> effectList;


	void Start(){
		GatherEffects();
		sea.UpdateSEAComponentChain(this);
	}
	void OnValidate(){
		GatherEffects();
		sea.UpdateSEAComponentChain(this);
	}

	void GatherEffects(){
		/*
			Gathers effects that are already on the effect gameobject, and adds them to the effectList
		*/

		if(effectList == null){
			effectList = new List<Effect>();
		}
		
		Effect[] effects = GetComponents<Effect>();
		foreach(Effect e in effects){
			Debug.Log(e);
			if(!effectList.Contains(e)){
				effectList.Add(e);
			}
		}

	}


	public void AddEffect(
		EffectCategory category,
		Effect existingEffect)
	{
		Effect addedEffect = effectGameObject.AddComponent<Effect>();
		addedEffect.SetupEffect(existingEffect);
		effectList.Add(addedEffect);
	}

	public void AddEffect(
		EffectCategory category,
		Dictionary<AttributeEnum, int> attributes,
		Dictionary<StatEnum, int> stats,
		Dictionary<StatEnum, int> percentStats)
	{
		Effect addedEffect = effectGameObject.AddComponent<Effect>();
		addedEffect.SetupEffect(category, attributes, stats, percentStats);
		effectList.Add(addedEffect);
	}

	public List<Effect> EffectsOfCategory(EffectCategory targetCategory){
		List<Effect> effectsOfCategory = new List<Effect>();
		foreach(Effect e in effectList){
			if(e.EffectCategory == targetCategory){
				effectsOfCategory.Add(e);
			}
		}
		return effectsOfCategory;
	}

    void ISEAComponent.UpdateSEAComponent()
    {
		sea.UpdateSEAComponentChain(this);
    }

    //TODO: there has to be a better way... right? Look into iterators
    public List<Effect> EffectList{
		get{return effectList;}
	}



}


