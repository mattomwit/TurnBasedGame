using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemDatabase: MonoBehaviour{

	public List<Item> databaseItems = new List<Item>();
	public Sprite[] spriteArray;
	public static ItemDatabase instance;

	void Awake(){
		spriteArray = Resources.LoadAll<Sprite>("Inventory");
		instance = this;
	}
	void Start () {
		databaseItems.Add(new Item(0,"Fruit","It smells nice and looks juicy.", 2,spriteArray[0],Item.ItemType.Consumable,true));
		databaseItems.Add(new Item(1,"Sword","Sharp and dangerous.", 1,spriteArray[70],Item.ItemType.Weapon_1h,true));
		databaseItems.Add(new Item(2,"Dagger","Short and pointy weapon.", 1,spriteArray[89],Item.ItemType.Weapon_1h,true));
		databaseItems.Add(new Item(3,"Gloves","Gloves with metal spikes. May serve for defense as well as offense", 1,spriteArray[100],Item.ItemType.Hands,true));
		databaseItems.Add(new Item(4,"Lance","Long weapon superrior to sword with it's danger zone.", 1,spriteArray[122],Item.ItemType.Weapon_2h,true));
		databaseItems.Add(new Item(5,"Wooden Shield","Good for deflecting blows, also used to bash opponents.", 1,spriteArray[170],Item.ItemType.Shield,true));
	}


}
