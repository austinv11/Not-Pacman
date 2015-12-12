using UnityEngine;
using System.Collections;

//Represents the "world" the game takes place in
public class Board {

	Dimensions dimensions;
	Tile[,] tiles;
	Character character;
	bool notifyChanges = true; //This might want to be disabled when the board is initializing

	public Dimensions Dimensions {
		get {
			return dimensions;
		}
	}

	public Character Character {
		get {
			return character;
		}
		set {
			character = value;
		}
	}

	public bool NotifyChanges {
		get {
			return notifyChanges;
		}
		set {
			notifyChanges = value;
		}
	}

	const int DEFAULT_WIDTH = 21;
	const int DEFAULT_HEIGHT = 21;

	public Tile this[int x, int y] {
		get {
			if (x < 0 || x >= dimensions.Width || y < 0 || y >= dimensions.Height)
				return new EmptyTile(new Vector2(x, y)); //A fake tile to prevent the need for sanity chacking

			return tiles[x, y] != null ? tiles[x, y] : (this[x, y] = new EmptyTile(new Vector2(x, y))); //Empty tile initialized on access to offload overhead
		}
		set {
			tiles[x, y] = value;

			//Nearby tiles get notified of the change if enabled
			if (NotifyChanges) {
				for (int neighborX = x-1; neighborX <= x+1; neighborX++) {
					for (int neighborY = y-1; neighborY <= y+1; neighborY++) {
						if (neighborX == x && neighborY == y)
							continue;
						this[neighborX, neighborY].NotifyOfChange(value);
					}
				}
			}
 		}
	}

	public Board(Dimensions dimensions) {
		this.dimensions = dimensions;
		tiles = new Tile[dimensions.Width,dimensions.Height];

		NotifyChanges = false;
		for (int x = 0; x < dimensions.Width; x++) {
			for (int y = 0; y < dimensions.Height; y++) {
				tiles[x, y] = new EmptyTile(new Vector2(x, y));
			}
		}
		NotifyChanges = true;
	}

	public Board() : this(new Dimensions(DEFAULT_WIDTH, DEFAULT_HEIGHT)) {
		
	}

	//TODO: Gets the default board from pacman
	public void Default() {

	}

	//TODO: Gets a randomly generated board
	public void Randomize() {
		for (int x = 0; x < Dimensions.Width; x++) {
			for (int y = 0; y < Dimensions.Height; y++) {
				if (Random.Range(0, 2) == 1 || x == 0 || y == 0 || x >= Dimensions.Width-1 || y >= Dimensions.Height-1) {
					this[x, y] = new Wall(new Vector2(x, y));
				}
			}
		}
	}
}
