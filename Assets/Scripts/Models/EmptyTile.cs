using UnityEngine;
using System.Collections;

public class EmptyTile : Tile {

	public EmptyTile(Vector2 coords) : base(coords) {

	}

	public override void NotifyOfChange(Tile oldNeighbor, Tile newNeighbor) {
		//Nothing
	}
}
