﻿using System.Collections.Generic;
public static class StatAttRatio{
	static Dictionary<StatEnum, Dictionary<AttributeEnum, int>> statAttRatio;

	public static int GetRatio(StatEnum statEnum, AttributeEnum attributeEnum){
		if(statAttRatio == null){
			Setup();
		}
		return statAttRatio[statEnum][attributeEnum];
	}


	public static List<StatEnum> StatEnumKeys(){
		if(statAttRatio == null){
			Setup();
		}
		return new List<StatEnum>(statAttRatio.Keys);
	}


	public static List<AttributeEnum> AttributeEnumKeys(StatEnum statEnum){
		if(statAttRatio == null){
			Setup();
		}
		return new List<AttributeEnum>(statAttRatio[statEnum].Keys);
	}


	static void Setup(){
		statAttRatio = new Dictionary<StatEnum, Dictionary<AttributeEnum, int>>();

		foreach(StatEnum se in System.Enum.GetValues(typeof(StatEnum))){
			statAttRatio[se] = new Dictionary<AttributeEnum, int>();

			foreach(AttributeEnum ae in System.Enum.GetValues(typeof(AttributeEnum))){
				statAttRatio[se][ae] = 0;
			}

		}


		CreateRatios();
	}


	public static void CreateRatios(){

		statAttRatio[StatEnum.max_health][AttributeEnum.constitution] = 25;
		statAttRatio[StatEnum.max_health][AttributeEnum.strength] = 5;


		statAttRatio[StatEnum.dodge][AttributeEnum.agility] = 25;
		statAttRatio[StatEnum.dodge][AttributeEnum.dexterity] = 5;
	}
}