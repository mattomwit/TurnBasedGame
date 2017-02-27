using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : MonoBehaviour {

	public Vector2 pozycjaPola = Vector2.zero;
	public Sprite teren;
	public Pionek pionek_na_polu=null;
	public List <Field> listaSasiadow;
	public int movementCost = 1;
	public Color previous_color=Color.white;

/*
	void OnMouseEnter(){
		if(Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].moving){
			GetComponent<SpriteRenderer>().color = Color.blue;

		} else if (Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].attacking){
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		Debug.Log("my position is X:" + pozycjaPola.x + ",Y:" + pozycjaPola.y);
	}
	void OnMouseExit(){
		GetComponent<SpriteRenderer>().color = previous_color;
	}
*/
	void OnMouseDown(){
		if(Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].moving){
			if(this.pionek_na_polu==null){
				Game.instance.moveCurrentPlayer(this);
			}
		} else if (Game.instance.listaInicjatywy[Game.instance.currentPlayerIndex].attacking){
			Game.instance.AttackWithCurrentPlayer(this);
		}
	}
	public void highlightTile(Color color){
		previous_color=GetComponent<SpriteRenderer>().color;
		GetComponent<SpriteRenderer>().color = color;
	}
	public void removeHighlight(){
		previous_color = Color.white;
		GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void Start(){
		GenerujSasiadow();
	}
	//generuj sasiadów
	void GenerujSasiadow(){
	//gora
		if(pozycjaPola.y<Game.instance.rozmiarMapyY-1){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x][(int)pozycjaPola.y+1]);
		}
	//dół
		if(pozycjaPola.y>0){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x][(int)pozycjaPola.y-1]);
		}
	//lewo
		if(pozycjaPola.x>0){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x-1][(int)pozycjaPola.y]);
		}
	//prawo
		if(pozycjaPola.x<Game.instance.rozmiarMapyX-1){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x+1][(int)pozycjaPola.y]);
		}
	//gora-lewo
		if(pozycjaPola.x>0 && pozycjaPola.y<Game.instance.rozmiarMapyY-1){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x-1][(int)pozycjaPola.y+1]);
		}
	//gora-prawo
		if(pozycjaPola.x<Game.instance.rozmiarMapyX-1 && pozycjaPola.y<Game.instance.rozmiarMapyY-1){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x+1][(int)pozycjaPola.y+1]);
		}
	//dół-lewo
		if(pozycjaPola.y>0 && pozycjaPola.x>0){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x-1][(int)pozycjaPola.y-1]);
		}
	//dół-prawo
		if(pozycjaPola.y>0 && pozycjaPola.x<Game.instance.rozmiarMapyX-1){
			listaSasiadow.Add (Game.instance.newmap[(int)pozycjaPola.x+1][(int)pozycjaPola.y-1]);
		}
	}
}
// Update is called once per frame
//		map.x = (screen.x / TILE_WIDTH_HALF + screen.y / TILE_HEIGHT_HALF) /2;
//	map.y = (screen.y / TILE_HEIGHT_HALF -(screen.x / TILE_WIDTH_HALF)) /2;