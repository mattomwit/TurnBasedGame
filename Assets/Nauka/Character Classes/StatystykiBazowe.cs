
public class StatystykiBazowe{
	private int	_bazowaWartosc;
	private int	_wartoscModyfikatora;
	private int	_expToLevel;
	private float _levelModyfier;

	public StatystykiBazowe(){
		_bazowaWartosc=0;
		_wartoscModyfikatora=0;
		_levelModyfier=1.1f;
		_expToLevel=100;
	}
	#region Basic setters and getters
//basic setters and getters
	public int WartoscBazowa{
		get{return _bazowaWartosc;}
		set{_bazowaWartosc=value;}
	}
	public int WartoscModyfikatora{
		get{return _wartoscModyfikatora;}
		set{_wartoscModyfikatora=value;}
	}
	public int ExpToLevel{
		get{return _expToLevel;}
		set{_expToLevel=value;}
	}
	public float LevelModyfier{
		get{return _levelModyfier;}
		set{_levelModyfier=value;}
	}
	#endregion

	private int CalculateExpToLevel(){
		return (int)(_expToLevel*_levelModyfier);
	}
	public void LevelUp(){
		_expToLevel=CalculateExpToLevel();
		_bazowaWartosc++;
	}

	public int AdjustedBaseValue{
		get {return _bazowaWartosc + _wartoscModyfikatora;}
	}
}
