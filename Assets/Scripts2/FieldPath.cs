using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldPath{
	public List<Field> listOfTiles = new List<Field>();
	public Field lastTile;
	
	public int costOfPath = 0;
	public FieldPath(){}
	
	public FieldPath(FieldPath tp){
		listOfTiles = tp.listOfTiles;
		costOfPath = tp.costOfPath;
		lastTile = tp.lastTile;
	}
	public void addTile(Field t){
		costOfPath += t.movementCost;
		listOfTiles.Add (t);
		lastTile = t;
	}
}