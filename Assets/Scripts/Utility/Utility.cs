using UnityEngine;
using System.Collections;

public class Utility {

	const string BOARD_CONTROLLER_NAME = "BoardController", BOARD_CONTROLLER_SCRIPT = "Board Controller";

	public static BoardController getBoardController() {
		return GameObject.Find(Utility.BOARD_CONTROLLER_NAME).GetComponent(BOARD_CONTROLLER_SCRIPT) as BoardController;
	}
}
