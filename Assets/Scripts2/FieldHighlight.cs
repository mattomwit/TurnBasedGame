using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class FieldHighlight {
	public FieldHighlight(){
	}

	public static List<Field> FindHighlight(Field originTile,int movementPoints){
		List<Field> closed = new List<Field> ();
		List<FieldPath> open = new List<FieldPath> ();

		FieldPath originPath = new FieldPath ();
		originPath.addTile (originTile);
		open.Add (originPath);

		while (open.Count > 0) {
			FieldPath current = open[0];
			open.Remove (open[0]);

			if (closed.Contains(current.lastTile)){
				continue;
			}
			if (current.costOfPath > movementPoints + 1) {
				continue;
			}
			closed.Add(current.lastTile);

			foreach (Field t in current.lastTile.listaSasiadow){
				FieldPath newTilePath = new FieldPath(current);
				newTilePath.addTile(t);
				open.Add (newTilePath);
			}
		}
		closed.Remove (originTile);
		return closed;
	}
}
