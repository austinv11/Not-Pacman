using UnityEngine;
using System.Collections;

public class EmptyTile : Tile {

	public EmptyTile(Vector2 coords) : base(coords) {

	}

	public override void NotifyOfChange(Tile newNeighbor) {
		//Nothing
	}

	public override bool Exists() {
		return false;
	}
}
