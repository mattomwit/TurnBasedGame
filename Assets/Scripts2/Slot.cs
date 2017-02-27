using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

 public class Slot : MonoBehaviour , IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler,IDragHandler,IPointerUpHandler{

	public Inventory inventory;
	public Item item;
	public int slotID;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerDown(PointerEventData data){
		inventory.setDraggedItemsSlot(this);
	}
	public void OnPointerEnter(PointerEventData data){
		inventory.currentMouseOverSlot=this;
		if(item.itemName!=null){
			inventory.displayTooltip(item);
		}
	}
	public void OnPointerExit(PointerEventData data){
		inventory.hideTooltip();
	}
	public void OnDrag(PointerEventData data){
		if(item.itemName!=null){
			inventory.dragItem(item,slotID);
		}
	}
	public void OnPointerUp(PointerEventData data){
		inventory.dropItem();
	}
}
