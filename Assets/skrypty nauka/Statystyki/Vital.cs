
public class Vital : ModifiedStat {
	private int _curValue;

	public Vital(){
		_curValue=0;
		ExpToLevel=50;
		LevelModifier=1.1f;
	}
	public int CurValue{
		get{
			if (_curValue>AdjustedBaseValue)
				_curValue=AdjustedBaseValue;
			return _curValue;
		}
		set{_curValue=value;}
	}
}
public enum VitalName{
	Health,		// all characters
	Armor,		// the more armor the less character takes basic dmg
	MagicArmor, // same as armor but with magick attacks
	Mana,		// casting characters
	Mind,		//all characters that are alive
	ActionPoints, //all classes basic pull for movement
	Rage,	//warriors that use brute strength
	Focus  //only archers or rogues maybe monks
}