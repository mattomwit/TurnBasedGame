
public class Skill : ModifiedStat {
	private bool _known;


	public Skill(){
		_known = false;
		ExpToLevel = 1;
		LevelModifier =0f;
	}

	public bool Known{
		get{return _known;}
		set{_known=value;}
	}
}


public enum SkillName{
	Melee,
	Ranged,
	Magic,
	Defense_Mele,
	Defense_Ranged,
	Defense_Magic
}