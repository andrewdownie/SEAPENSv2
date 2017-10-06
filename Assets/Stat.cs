using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Stat : MonoBehaviour {
	SEA sea;
	Dictionary<StatEnum, int> statDict;


	void Start(){
		sea = GetComponent<SEA>();
	}

}


public enum StatEnum{
	health,
	movement_speed,
}
