using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Effects : MonoBehaviour {
	[SerializeField]
	GameObject effectGameObject;

	[SerializeField]
	List<Effect> effectList;


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

	//TODO: there has to be a better way... right? Look into iterators
	public List<Effect> EffectList{
		get{return effectList;}
	}



}


