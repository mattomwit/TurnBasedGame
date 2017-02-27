using UnityEngine;
using System.Collections;
using System.Collections.Generic; //added to access lists
using System; 				//added to access the enum class

public class BaseCharacter : MonoBehaviour {

	private string _name;
	private int _level;
	private string _characterClass;
	private uint _freeExp;

	private Attribute[] _primaryAttributes;
	private Vital[] _vitals;
	private Skill[] _skills;

	void Awake(){
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;

	//assign how many attributes are there (take the number from class Attribute from Enum containing names of attributes)
		_primaryAttributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vitals = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skills = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
		SetupVitalModifiers();
	}


	public string Name{
		get{return _name;}
		set{_name=value;}
	}
	public int Level{
		get{return _level;}
		set{_level=value;}
	}
	public string CharacterClass{
		get{return _characterClass;}
		set{_characterClass=value;}
	}
	public uint FreeExp{
		get{return _freeExp;}
		set{_freeExp=value;}
	}

	//getter for primary attributes of the character it returns one of attributes
	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttributes[index];
	}
	public Vital GetVital(int index){
		return _vitals[index];
	}
	public Skill GetSkill(int index){
		return _skills[index];
	}

	private void SetupPrimaryAttributes(){
		for(int cnt =0; cnt< _primaryAttributes.Length; cnt++){
			_primaryAttributes[cnt] = new Attribute();
		}
	}

	private void SetupVitals(){
		for(int cnt =0; cnt< _vitals.Length; cnt++){
			_vitals[cnt] = new Vital();
		}
	}

	private void SetupSkills(){
		for(int cnt =0; cnt< _skills.Length; cnt++){
			_skills[cnt] = new Skill();
		}
	}

	private void SetupVitalModifiers(){
		//health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Endurance), .5f));

		//energy
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Inteligence), 1f));

	}
	private void SetupSkillModifiers(){

	}
	public void StatUpdate(){
		for(int cnt = 0; cnt <_vitals.Length; cnt++){
			_vitals[cnt].Update();
		}
		for(int cnt = 0; cnt <_skills.Length; cnt++){
			_skills[cnt].Update();
		}
	}

}
