
public class BaseStat {

	private int _baseValue;		//bazowa wartosc statystyki
	private int _buffValue;		//ilosc premiowa tej statystyki
	private int _expToLevel;	//ilosc exp potrzebna aby awansowac statystyke
	private float _levelModifier;	//modyfikator podnoszacy ilosc potrzebna exp zeby podniesc skill atrybut lub talent

	public BaseStat(){
		_baseValue =0;
		_buffValue =0;
		_levelModifier =1f;
		_expToLevel=100;
	}
#region seters and geters
	//basic setters and getters
	public int BaseValue{
		get{return _baseValue;}
		set{_baseValue=value;}
	}
	public int BuffValue{
		get{return _buffValue;}
		set{_buffValue=value;}
	}
	public int ExpToLevel{
		get{return _expToLevel;}
		set{_expToLevel=value;}
	}
	public float LevelModifier{
		get{return _levelModifier;}
		set{_levelModifier=value;}
	}
#endregion

	private int CalculateExpToLevel(){
		return (int)(_expToLevel * _levelModifier); 
	}
	public void LevelUp(){
		_expToLevel=CalculateExpToLevel();
		_baseValue++;
	}
	public int AdjustedBaseValue{
		get{return _baseValue + _buffValue;}
	} 

}
