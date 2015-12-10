using UnityEngine;
using System.Collections;

public class Wall : Tile {

	WallRenderState renderState;

	public WallRenderState RenderState {
		get {
			return renderState;
		}
		private set {
			renderState = value;
		}
	}

	public Wall(Vector2 coords) : base(coords) {

	}

	public override void NotifyOfChange(Tile oldNeighbor, Tile newNeighbor) {
		//TODO: calculate rendering states
	}

	public static Sprite GetSpriteForRenderState(WallRenderState state) {
		BoardController controller = Utility.getBoardController();
		switch (state) {
		case WallRenderState.DISCONNECTED:
			return controller.disconnectedWallSprite;
		case WallRenderState.SINGLE_CONNECTED:
			return controller.singleConnectedWallSprite;
		case WallRenderState.DOUBLE_CONNECTED_OPPOSITE:
			return controller.doubleConnectedOppositeWallSprite;
		case WallRenderState.DOUBLE_CONNECTED_ADJACENT:
			return controller.doubleConnectedAdjacentWallSprite;
		case WallRenderState.TRIPLE_CONNECTED:
			return controller.tripleConnectedWallSprite;
		case WallRenderState.CONNECTED:
			return controller.connectedSprite;
		default:
			Debug.LogWarning("WallRenderState " + state + " doesn't have a set sprite!");
			return null;
		}
	}

	public enum WallRenderState {
		DISCONNECTED, SINGLE_CONNECTED, DOUBLE_CONNECTED_OPPOSITE, DOUBLE_CONNECTED_ADJACENT, TRIPLE_CONNECTED, CONNECTED //CONNECTED = QUADRUPLE_CONNECTED, but quadruple is a weird word so :P
	}
}
