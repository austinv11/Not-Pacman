using UnityEngine;
using System.Collections;

//Represents anything in game
public abstract class Tile {

	private Vector2 coords;

	public int X {
		get {
			return (int) coords.x;
		}
	}

	public int Y {
		get {
			return (int) coords.y;
		}
	}

	public Tile(Vector2 coords) {
		this.coords = coords;
	}

	//Called to notify this tile of a change to neighbors
	public abstract void NotifyOfChange(Tile oldNeighbor, Tile newNeighbor);
}
