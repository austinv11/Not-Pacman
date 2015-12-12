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

	//Generates or finds a game object linking to this Tile instance
	public GameObject FetchGameObject() {
		string objectName = "Tile_(" + X + "," + Y + ")";
		GameObject gameObject = GameObject.Find(objectName);
		if (gameObject != null) {
			return gameObject;
		}

		gameObject = new GameObject();
		gameObject.name = objectName;
		gameObject.AddComponent<SpriteRenderer>();
		gameObject.transform.Translate(new Vector3(X, Y));
		return gameObject;
	}

	public Tile GetNeighbor(RelativePosition position) {
		BoardController controller = BoardController.Instance;
		Board board = controller.CurrentBoard;
		switch (position) {
		case RelativePosition.TOP:
			return board[X, Y+1];
		case RelativePosition.RIGHT:
			return board[X+1, Y];
		case RelativePosition.BOTTOM:
			return board[X, Y-1];
		case RelativePosition.LEFT:
			return board[X-1, Y];
		}
		//Should never be reached
		return new EmptyTile(new Vector2(X, Y));
	}

	public virtual bool Exists() {
		return !(this.GetType().IsAssignableFrom(typeof(EmptyTile)));
	}

	//Called to notify this tile of a change to neighbors
	public abstract void NotifyOfChange(Tile newNeighbor);
}
