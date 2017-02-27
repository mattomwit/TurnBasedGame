using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterGenerator : MonoBehaviour {

	public List<Text> textAttribute = new List<Text>();
	public List<Text> textVital = new List<Text>();
	public List<Text> textVitalNumbers = new List<Text>();
	public List<Text> textAttributeNumbers = new List<Text>();
	public List<Text> skill = new List<Text>();
	
	private int _pointsLeftToSpend = 300;
	public Text pointsLeft_Text; 
	public GameObject playerCharacter;

	#region setters&getters
	public int PointsLeftToSpend{
		get{ return _pointsLeftToSpend; }
		set{ _pointsLeftToSpend = value;}
	}
	#endregion

	void Awake(){
		CreateInterface();
	}

	// Use this for initialization
	void Start () {
		RefreshNumbers();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SaveCharacter(){
		Debug.Log ("SaveCharacter.");
	}

	public void GenerateRandomCharacter(){
		_pointsLeftToSpend=300;
		List<int> randomAttributes = new List<int>();
		int tempInt;
		int tempPointsToSpend = _pointsLeftToSpend;
		//generate 6 random integers
		for (int cnt=0; cnt<Enum.GetNames(typeof(AttributeName)).Length;cnt++){

				if(tempPointsToSpend>100){
					tempInt = UnityEngine.Random.Range(0,101);
					randomAttributes.Add (tempInt);
					tempPointsToSpend-=tempInt;
				}
				else{
					tempInt = UnityEngine.Random.Range (0,tempPointsToSpend+1);
					randomAttributes.Add (tempInt);
					tempPointsToSpend-=tempInt;
				}
		}
	//przypisz statystyki
		for(int cnt = 0; cnt<Enum.GetNames(typeof(AttributeName)).Length;cnt++){
			tempInt = UnityEngine.Random.Range (0,randomAttributes.Count+1);
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue =randomAttributes[tempInt];
			_pointsLeftToSpend-=randomAttributes[tempInt];
			randomAttributes.RemoveAt(tempInt);
		}
		_pointsLeftToSpend=tempPointsToSpend;
		RefreshNumbers();
		Debug.Log ("GenerateRandom.");
	}

	private void CreateInterface(){
		//VitalNames
		for(int cnt = 0; cnt<Enum.GetNames(typeof(VitalName)).Length;cnt++){
			textVital[cnt].GetComponent<Text>().text = ((VitalName)cnt).ToString();
		}
	}
	private void RefreshVitalNumbers(){
		for(int cnt = 0; cnt<Enum.GetNames(typeof(VitalName)).Length;cnt++){
			textVitalNumbers[cnt].GetComponent<Text>().text = playerCharacter.GetComponent<PlayerCharacter>().GetVital(cnt).BaseValue.ToString();
		}
	}
	private void RefreshAttributeNumbers(){
		for(int cnt = 0; cnt<Enum.GetNames(typeof(AttributeName)).Length;cnt++){
			textAttributeNumbers[cnt].GetComponent<Text>().text = playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue.ToString();
		}
	}
	private void RefreshNumbers(){
		RefreshAttributeNumbers();
		RefreshVitalNumbers();
		pointsLeft_Text.text = _pointsLeftToSpend.ToString();
	}

	#region Adding and removing attributes
	public void AddStr(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(0).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(0).BaseValue+= 1; 
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveStr(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(0).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(0).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	public void AddDex(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(1).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(1).BaseValue+= 1; 
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveDex(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(1).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(1).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	public void AddEnd(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(2).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(2).BaseValue+= 1;
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveEnd(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(2).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(2).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	public void AddInt(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(3).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(3).BaseValue+= 1; 
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveInt(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(3).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(3).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	public void AddWill(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(4).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(4).BaseValue+= 1; 
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveWill(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(4).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(4).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	public void AddLuck(){
		if (_pointsLeftToSpend>0 && playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(5).BaseValue<100){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(5).BaseValue+= 1; 
			PointsLeftToSpend-=1;
			RefreshNumbers();
		}
	}
	public void RemoveLuck(){
		if(playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(5).BaseValue>0){
			playerCharacter.GetComponent<PlayerCharacter>().GetPrimaryAttribute(5).BaseValue-= 1; 
			PointsLeftToSpend+=1;
			RefreshNumbers();
		}
	}
	#endregion
	public void ChangeCharacterClass(){
		//after changing class in generator recalculate all vital modifiers from attributes
	}

}