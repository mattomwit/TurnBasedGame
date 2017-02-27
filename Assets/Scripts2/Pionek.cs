using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pionek : MonoBehaviour {
	int Gracz;

//plecak pionka
	public int numberOfSlots=10;
	public List<Item> charInventory = new List<Item>();

	public string Nazwa="Patryk";
	public Color kolorGracza = Color.white;
	public Vector2 pozycjaGracza = Vector2.zero;
	public int inicjatywa;
	public int HP =25;
	public int dmg = 5;
	public float szansa_ataku =0.75f;
	public float dmg_red=0.15f;
	public int rzut_k = 6;
	public int AP=5;
	public float moveSpeed = 10.0f;
	public bool moving = false;
	public bool attacking = false;
	public Field pole_pionka=null;
	public int zasieg_dystans = 1;

	public Vector3 moveDestination;

	void Awake(){
		createEmptyInventory(numberOfSlots);
		moveDestination = transform.position;
	}
	void Start(){
		charInventory[1]= (ItemDatabase.instance.databaseItems[1]);
		charInventory[4]= (ItemDatabase.instance.databaseItems[2]);
		charInventory[0]= (ItemDatabase.instance.databaseItems[5]);
	}
//wołane z Game co każdy Update() wołane tylko dla aktywnego gracza
	public void TurnUpdate(){
		if(Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex]==this){
			Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].GetComponent<SpriteRenderer>().color=Color.green;
		}

		if (Vector2.Distance(moveDestination, transform.position) >0.1f){
			transform.position +=(moveDestination - transform.position).normalized*moveSpeed* Time.deltaTime;

			if (Vector2.Distance(moveDestination, transform.position) <=0.1f){
				transform.position = moveDestination;
			}
		}
	}
//tworzy pusty plecak o ilosci slotow numberOfslots inventory
	private void createEmptyInventory(int nOfSlots){
		for(int i = 0; i < nOfSlots;i++){
			charInventory.Add(new Item());
		}
	}


	public void TurnOnGUI(){
		float buttonHeight =50;
		float buttonWidth =150;
//move butt
		Rect buttonRect = new Rect(0,Screen.height - buttonHeight*3,buttonWidth,buttonHeight);
		if(GUI.Button (buttonRect,"Move")){
			if(!moving){
				Game.instance.removeTileHighlights();
				moving=true;
				attacking=false;
				Game.instance.highlightTilesAt(pole_pionka,Color.blue,5);
			}else{
				moving=false;
				attacking=false;
				Game.instance.removeTileHighlights();
			}
		}
//attack butt
		buttonRect = new Rect(0,Screen.height - buttonHeight*2,buttonWidth,buttonHeight);
		if(GUI.Button(buttonRect,"Attack")){
			if(!attacking){
				Game.instance.removeTileHighlights();
				moving=false;
				attacking = true;
				Game.instance.highlightTilesAt(pole_pionka,Color.red,zasieg_dystans);
			}else{
				moving =false;
				attacking =false;
				Game.instance.removeTileHighlights();
			}
		}
//end turn butt
		buttonRect = new Rect(0,Screen.height - buttonHeight*1,buttonWidth,buttonHeight);
		if(GUI.Button(buttonRect,"End Turn")){
			Game.instance.removeTileHighlights();
			Game.instance.nextTurn();

		}
	}
}
