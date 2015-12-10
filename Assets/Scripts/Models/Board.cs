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
			Tile oldTile = tiles[x, y];
			tiles[x, y] = value;

			//Nearby tiles get notified of the change if enabled
			if (NotifyChanges) {
				for (int neighborX = x-1; neighborX <= x+1; neighborX++) {
					for (int neighborY = y-1; neighborY <= y+1; neighborY++) {
						if (neighborX == x && neighborY == y)
							continue;
						this[neighborX, neighborY].NotifyOfChange(oldTile, value);
					}
				}
			}
 		}
	}

	public Board(Dimensions dimensions) {
		this.dimensions = dimensions;
		tiles = new Tile[dimensions.Width,dimensions.Height];
	}

	public Board() {
		this.dimensions = new Dimensions(DEFAULT_WIDTH, DEFAULT_HEIGHT);
	}

	//TODO: Gets the default board from pacman
	public static Board Default() {
		Board board = new Board();
		board.NotifyChanges = false; //Prevents unnecessary overhead
		board.NotifyChanges = true;
		return board;
	}

	//TODO: Gets a randomly generated board
	public static Board Randomized() {
		Board board = new Board();
		board.NotifyChanges = false; //Prevents unnecessary overhead
		board.NotifyChanges = true;
		return board;
	}
}
