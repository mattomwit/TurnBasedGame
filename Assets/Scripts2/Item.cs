using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

	public  Item(){
		itemName=null;
	}
	public Item(int id, string name,string desc ,int ammount ,Sprite sprite, ItemType type, bool stack){
		itemID=id;
		itemType = type;
		itemName = name;
		itemDesc = desc;
		itemIcon = sprite;
		itemAmount = ammount;
		stackable = stack;
	}

	public string itemName;
	public int itemID;
	public int itemAmount;
	public string itemDesc;
	public Sprite itemIcon;
	public ItemType itemType;
	public bool stackable;

	public enum ItemType{
		Weapon_1h,
		Weapon_2h,
		Quest,
		Armor,
		Shield,
		Shoes,
		Amulet,
		Erring,
		Helmet,
		Trousers,
		Hands,
		Ring,
		Consumable,
		Usable
	}
}
