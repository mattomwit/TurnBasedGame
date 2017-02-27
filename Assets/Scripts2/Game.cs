using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	//odniesienie klasy do samej siebie
	public static Game instance;

	//objekt kamery
	public GameObject camera;

	//objekt pola oraz jego sprite
	public GameObject FieldObject;
	public Sprite Teren;
	//obiekt pionka oraz jego sprite
	public GameObject Pionek;
	public Sprite Rycerz;
	public List <Pionek> listaPionkowG;
	public List <Pionek> listaPionkowAI;
	public List <List<Field>> newmap;
	public List <Pionek> listaInicjatywy;
	public int rozmiarMapyX = 50;
	public int rozmiarMapyY = 50;
	public int currentPlayerIndex = 0;
	public List <Field> highlightedTiles = new List<Field>();

	void Awake(){
		instance=this;
	}
	void Start () {
		stworzMape();
		stworzGracza();
		stworzAI();
		stworzKolejke();
	}
	
	// Update is called once per frame
	void Update () {
		listaInicjatywy[currentPlayerIndex].TurnUpdate();
	}
	// tile highlight calls FieldHighlight to finde fields to highlight
	public void highlightTilesAt(Field originLocation, Color highlightColor,int distance){
		highlightedTiles = FieldHighlight.FindHighlight (originLocation,distance);

		foreach (Field t in highlightedTiles) {
			t.highlightTile(highlightColor);
		}
	}

	public void removeTileHighlights(){
		foreach (Field t in highlightedTiles) {
			t.removeHighlight();
		}
	}
	void OnGUI(){
		listaInicjatywy[currentPlayerIndex].TurnOnGUI();
	}
	public void nextTurn(){
		listaInicjatywy[Game.instance.currentPlayerIndex].moving = false;
		listaInicjatywy[Game.instance.currentPlayerIndex].attacking = false;
		listaInicjatywy[Game.instance.currentPlayerIndex].GetComponent<SpriteRenderer>().color=listaInicjatywy[Game.instance.currentPlayerIndex].kolorGracza;
		if (currentPlayerIndex +1 <listaInicjatywy.Count){
			currentPlayerIndex++;
			listaInicjatywy[currentPlayerIndex].AP=2;
		}
		else {
			currentPlayerIndex= 0;
			listaInicjatywy[currentPlayerIndex].AP=2;
		}
		//ustawia kamere po wcisnieciu endofturn
		camera.transform.position =new Vector3( listaInicjatywy [Game.instance.currentPlayerIndex].transform.position.x,listaInicjatywy [Game.instance.currentPlayerIndex].transform.position.y,camera.transform.position.z);
	}
#region Generator mapy
//funkcja tworząca mapę o podanym rozmiarze
	public void stworzMape(){
		float TileWidth=Teren.bounds.extents.x;
		float TileHeigth=Teren.bounds.extents.y;
		newmap = new List<List<Field>>();
		for (int i=0; i<rozmiarMapyX;i++){
			List<Field> rzad = new List<Field>();
			for(int j=0; j<rozmiarMapyY;j++){
				Field field = (((GameObject)Instantiate(FieldObject,new Vector2((i-j)*TileWidth,(i+j)*TileHeigth), Quaternion.Euler(new Vector2()))).AddComponent<SpriteRenderer>()).GetComponent<Field>();
				field.GetComponent<SpriteRenderer>().sprite = Teren;
				field.GetComponent<SpriteRenderer>().sortingOrder= 1;
				field.pozycjaPola=new Vector2(i,j);
				rzad.Add(field);
			}
			newmap.Add (rzad);
		}
	}
#endregion

#region Tworzenie pionków

// funkcja tworząca pionkiGracza na mapie 
	public void stworzGracza(){
		listaPionkowG = new List<Pionek>();
		for(int j=0; j<1;j++){
			Pionek pionekGracza;

			pionekGracza = (((GameObject)Instantiate(Pionek,new Vector2(newmap[2][3].transform.position.x,newmap[2][3].transform.position.y+0.2f), Quaternion.Euler(new Vector2()))).AddComponent<SpriteRenderer>()).GetComponent<Pionek>();
			pionekGracza.GetComponent<SpriteRenderer>().sprite = Rycerz;
			pionekGracza.GetComponent<SpriteRenderer>().sortingOrder=2;
			pionekGracza.pozycjaGracza= new Vector2(2,3);
			pionekGracza.pole_pionka= newmap[2][3];
			pionekGracza.Nazwa="Kawa";
			listaPionkowG.Add(pionekGracza);

			pionekGracza = (((GameObject)Instantiate(Pionek,new Vector2(newmap[1][1].transform.position.x,newmap[1][1].transform.position.y+0.2f), Quaternion.Euler(new Vector2()))).AddComponent<SpriteRenderer>()).GetComponent<Pionek>();
			pionekGracza.GetComponent<SpriteRenderer>().sprite = Rycerz;
			pionekGracza.GetComponent<SpriteRenderer>().sortingOrder=2;
			pionekGracza.pozycjaGracza= new Vector2(1,1);
			pionekGracza.pole_pionka= newmap[1][1];
			pionekGracza.Nazwa="UNO";
			listaPionkowG.Add(pionekGracza);
		}
	}
// funkcja tworząca pionki AI (poki co wrogów i sojuszników)
	public void stworzAI(){
		listaPionkowAI = new List<Pionek>();
		for(int j=0; j<1;j++){
			Pionek pionekAI;
			
			pionekAI = (((GameObject)Instantiate(Pionek,new Vector2(newmap[2][5].transform.position.x,newmap[2][5].transform.position.y+0.2f), Quaternion.Euler(new Vector2()))).AddComponent<SpriteRenderer>()).GetComponent<Pionek>();
			pionekAI.GetComponent<SpriteRenderer>().sprite = Rycerz;
			pionekAI.GetComponent<SpriteRenderer>().sortingOrder=2;
			pionekAI.GetComponent<SpriteRenderer>().color = Color.red;
			pionekAI.kolorGracza= Color.red;
			pionekAI.pole_pionka= newmap[2][5];
			pionekAI.pozycjaGracza= newmap[2][5].transform.position;
			listaPionkowAI.Add(pionekAI);

			pionekAI = (((GameObject)Instantiate(Pionek,new Vector2(newmap[3][2].transform.position.x,newmap[3][2].transform.position.y+0.2f), Quaternion.Euler(new Vector2()))).AddComponent<SpriteRenderer>()).GetComponent<Pionek>();
			pionekAI.GetComponent<SpriteRenderer>().sprite = Rycerz;
			pionekAI.GetComponent<SpriteRenderer>().sortingOrder=2;
			pionekAI.GetComponent<SpriteRenderer>().color = Color.red;
			pionekAI.kolorGracza= Color.red;
			pionekAI.pozycjaGracza= new Vector2(3,2);
			pionekAI.pole_pionka= newmap[3][2];
			pionekAI.Nazwa="Zdzich";
			listaPionkowAI.Add(pionekAI);
		}
	}
#endregion

#region inicjatywa tworzenie kolejki i losowanie pionków
//tworzy listę inicjatywy według której rozgrywa się walka
	public void stworzKolejke(){
		listaInicjatywy= new List<Pionek>();
		int dlugoscListyMax;
// sprawdza która lista jest większa i zwraca długość większej
		if (listaPionkowG.Count>listaPionkowAI.Count){
			dlugoscListyMax = listaPionkowG.Count;
		}
		else{
			dlugoscListyMax = listaPionkowAI.Count;
		}
//sprawdza inicjatywe pionków a następnie rozpisuje - nie dziala poniewaz musza byc jeszcze sortowane inicjatywy postaci w liscie 
		for (int i=0;i<dlugoscListyMax;i++){
			if (listaPionkowG[i].inicjatywa>listaPionkowAI[i].inicjatywa){
				listaInicjatywy.Add(listaPionkowG[i]);
				listaInicjatywy.Add(listaPionkowAI[i]);
			}
			else{
				listaInicjatywy.Add(listaPionkowAI[i]);
				listaInicjatywy.Add(listaPionkowG[i]);
			}
		}
	}
#endregion

#region funkcja ruchu
	 //funkcja ruchu
	public void moveCurrentPlayer(Field destTile){
		if (destTile.GetComponent<SpriteRenderer>().color != Color.white){
			listaInicjatywy[currentPlayerIndex].AP--;
			listaInicjatywy[currentPlayerIndex].moveDestination = destTile.transform.position+(0.7f* Vector3.up*Teren.bounds.extents.x);
			listaInicjatywy[currentPlayerIndex].pozycjaGracza=destTile.pozycjaPola;

		//przypisz polu pionek oraz usun pionka z pola
		//przemiesc pionka

			listaInicjatywy[currentPlayerIndex].pole_pionka.pionek_na_polu=null;
			listaInicjatywy[currentPlayerIndex].pole_pionka=destTile;
			destTile.pionek_na_polu=listaInicjatywy[currentPlayerIndex];

		//sprawdza czy po ruchu pozostało AP jesli nie to end of turn
			if(listaInicjatywy[currentPlayerIndex].AP==0){
				nextTurn();
			}
		} else {
			Debug.Log("Destination invalid!");
		}
	}
#endregion

	#region funkcja ataku
	//funkcja ataku
	public void AttackWithCurrentPlayer(Field destTile){
		if (destTile.GetComponent<SpriteRenderer> ().color != Color.white) {

			Pionek target = null;
			foreach (Pionek p in listaInicjatywy) {
				if (p.pozycjaGracza == destTile.pozycjaPola) {
					target = p;
				}
			}
			if (target != null) {
				if (listaInicjatywy [currentPlayerIndex].pozycjaGracza.x >= target.pozycjaGracza.x - 1 && listaInicjatywy [currentPlayerIndex].pozycjaGracza.x <= target.pozycjaGracza.x + 1 && 
					listaInicjatywy [currentPlayerIndex].pozycjaGracza.y >= target.pozycjaGracza.y - 1 && listaInicjatywy [currentPlayerIndex].pozycjaGracza.y <= target.pozycjaGracza.y + 1) {
//koszt pktow akcji
					listaInicjatywy [currentPlayerIndex].AP--;
//attack logick
//hit logic
					bool hit = Random.Range (0.0f, 1.0f) <= listaInicjatywy [currentPlayerIndex].szansa_ataku;
//jesli atak sie powiedzie
					if (hit) {
//damage logic
						int obrazenia = listaInicjatywy [currentPlayerIndex].dmg + Random.Range (0, listaInicjatywy [currentPlayerIndex].rzut_k);
						target.HP -= obrazenia;
						Debug.Log (listaInicjatywy [currentPlayerIndex].Nazwa + " trafił " + target.Nazwa + " zadając " + obrazenia + " obrażeń!");
						if (target.HP <= 0) {
//sprawia ze zabity cel znika
							Debug.Log (listaInicjatywy [currentPlayerIndex].Nazwa + " zabił " + target.Nazwa + "!");
							listaInicjatywy.Remove (target); /*ok czyli gracz o currentPlayerIndex zabija gracza o indeksie z nizsza liczbą, następnie usuwa go z listy
							                                 co się dzieje dalej? usunięty gracz wypada z listy a pozostali gracze zostają przesunięci w kolejce
							                                 w tym gracz który był aktywny. Zmienia się jego numer currentPlayerIndex i teraz funkcja update wysiada. Gdy postać ginie 
							                                 powinna zostawać w liście ale być pomijana podczas kolejki*/  

							listaPionkowAI.Remove (target);
							listaPionkowG.Remove (target);
							GameObject.Destroy (target.gameObject, 1);
						}
//jesli nie trafi
					} else {
						Debug.Log (listaInicjatywy [currentPlayerIndex].Nazwa + " chybił " + target.Nazwa + "!");
					}
//sprawdz ilosc ap jesli 0 end of turn
				if (listaInicjatywy [currentPlayerIndex].AP == 0) {
					nextTurn ();
				}
			} else {
				Debug.Log ("Cel poza zasięgiem. linia 236");
			}
			
		}
		else{
			Debug.Log ("Invalid target! linia241");
		}
	}
	}
#endregion
}