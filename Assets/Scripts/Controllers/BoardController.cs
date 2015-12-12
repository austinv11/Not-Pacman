using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {

	public Sprite disconnectedWallSprite, singleConnectedWallSprite, doubleConnectedOppositeWallSprite, 
	doubleConnectedAdjacentWallSprite, tripleConnectedWallSprite, connectedSprite;

	static BoardController instance;

	public static BoardController Instance {
		get {
			return instance;
		}
		private set {
			instance = value;
		}
	}

	Board currentBoard;

	public Board CurrentBoard {
		get {
			return currentBoard;
		}
		private set {
			currentBoard = value;
		}
	}

	// Use this for initialization
	void Start () {
		BoardController.Instance = this;

		CurrentBoard = new Board();
		CurrentBoard.Randomize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
