using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public Canvas invent;
	public GameObject slot;
	public GameObject invBackground;
	private bool isShowing;		//if "i" has been pressed and inventory is showing
	public List<Item> inventInstance = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();
	public int numOfslots;
	public int numOfrows;
	public ItemDatabase database;
	public string currentSelectedCharacter;
	public bool showTooltip=false; 
	public GameObject toolTip;
	public GameObject draggedItemImage;
	public bool draggingItem=false;
	public Item draggedItem=null;
	public Slot draggedItemsSlot=null;
	public Slot currentMouseOverSlot=null;

	void Start(){
	}
	void Awake(){
		database = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDatabase>();
		invent.gameObject.SetActive(false);
	}

	void Update () {
		if (Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].Nazwa !=currentSelectedCharacter){
			currentSelectedCharacter=Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].Nazwa;
			LinkInventory(ref Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].charInventory);
		}
		if(showTooltip){
			toolTip.transform.position = Input.mousePosition+ new Vector3(10,-12,0);
		}

		if (Input.GetKeyDown("i")){
			isShowing =!isShowing;
			if(isShowing){
				DrawInventory();
			}
			else if(isShowing==false){
				clearInventory ();
			}
			invent.gameObject.SetActive(isShowing);
		}
		if(isShowing && draggingItem){
			draggedItemImage.transform.position = Input.mousePosition+ new Vector3(10,-12,0);
		}
		else{
			draggedItemImage.gameObject.SetActive(false);
		}	
	}

	void clearInventory(){
		for(int i=0;i<slots.Count;i++){
			GameObject.Destroy(slots[i],0.01f);
		}
		slots.Clear();
	}

	void DrawInventory(){

		for(int i=0; i<inventInstance.Count;i++){
			GameObject s = (GameObject)Instantiate(slot);
			s.transform.SetParent(invBackground.gameObject.transform,false);
			s.GetComponent<RectTransform>().localPosition = new Vector3(0+i*110,0,0);
			s.GetComponent<Slot>().item = inventInstance[i];
			s.GetComponent<Slot>().slotID=i;
			if(inventInstance[i].itemName!=null){
				s.gameObject.transform.GetChild(0).GetComponent<Image>().sprite=inventInstance[i].itemIcon;
				s.gameObject.transform.GetChild(0).GetComponent<Image>().enabled =true;
			}
			slots.Add(s);
		}
	}
	void refreshInventory(){
		for(int i=0; i<inventInstance.Count;i++){
			slots[i].GetComponent<Slot>().item = inventInstance[i];
			slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite=inventInstance[i].itemIcon;
			if(inventInstance[i].itemName!=null){
				slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled =true;
			}
		}
	}

	void AddItem(int id){
		for (int i=0; i<inventInstance.Count;i++){
			if (inventInstance[i].itemName == null){
				for (int j = 0; j< database.databaseItems.Count; j++){
					if(database.databaseItems[j].itemID == id){
					inventInstance[i] = database.databaseItems[id];
						break;
					}
				}
				break;
			}
		}
	}

	bool InventoryContains(int id){
		bool result=false;
		for (int i=0; i < inventInstance.Count; i++){
			result=inventInstance[i].itemID == id;
			if(result){
				break;
			}
		}
		return result; 
	}    

	void RemoveItem(int id){
		for (int i=0; i<inventInstance.Count;i++){
			if(inventInstance[i].itemID==id){
				inventInstance[i] = new Item();
				break;
			}
		}
	}
	void ClearSlot(int slotNumber){
		inventInstance[slotNumber] = new Item();
		slots[slotNumber].GetComponent<Slot>().item =inventInstance[slotNumber];
		slots[slotNumber].gameObject.transform.GetChild(0).GetComponent<Image>().enabled =false;
	}

	void LinkInventory(ref List<Item> inv){
		inventInstance = inv;
	}
	public void displayTooltip(Item itm){
		showTooltip=true;
		if(draggingItem==false){
			toolTip.transform.GetChild(0).GetComponent<Text>().text = itm.itemName;
			toolTip.transform.GetChild(1).GetComponent<Text>().text = itm.itemDesc;
			toolTip.gameObject.SetActive(true);
		}
	}

	public void hideTooltip(){
		showTooltip=false;
		toolTip.gameObject.SetActive(false);
	}
	public void dragItem(Item item,int slotId){
		if(isShowing){
			if(draggingItem==false){
				draggingItem=true;
				draggedItem = item;
				draggedItemImage.gameObject.GetComponent<Image>().sprite=draggedItem.itemIcon;
				ClearSlot(slotId);
				draggedItemImage.gameObject.SetActive(true);
			}
		}
	}

	public void setDraggedItemsSlot(Slot slot){
		draggedItemsSlot = slot;
	}
	public void dropItem(){
//jesli upuszczam przedmiot na puste pole
		if(currentMouseOverSlot.item.itemName==null && draggedItem.itemName!=null){
			inventInstance[currentMouseOverSlot.slotID] = draggedItem;
			draggedItem =new Item();
		}
//jesli upuszczam przedmiot na istniejacy przemiot
		else if(currentMouseOverSlot.item.itemName!=null && draggedItem.itemName!=null){
			inventInstance[draggedItemsSlot.slotID] = currentMouseOverSlot.item;
			inventInstance[currentMouseOverSlot.slotID] = draggedItem;
		}
		draggedItemsSlot =null;
		draggingItem=false;
		refreshInventory();
	}
}