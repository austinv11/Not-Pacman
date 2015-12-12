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
		Update();
	}

	public override void NotifyOfChange(Tile newNeighbor) {
		Update();
	}

	private void Update() {
		Tile top = GetNeighbor(RelativePosition.TOP), right = GetNeighbor(RelativePosition.RIGHT), 
		bottom = GetNeighbor(RelativePosition.BOTTOM), left = GetNeighbor(RelativePosition.LEFT);
		
		int count = 0;
		if (top.Exists())
			count++;
		if (right.Exists())
			count++;
		if (bottom.Exists())
			count++;
		if (left.Exists())
			count++;
		
		switch (count) {
		case 1:
			RenderState = WallRenderState.SINGLE_CONNECTED;
			break;
		case 2:
			if ((top.Exists() && bottom.Exists()) || (left.Exists() && right.Exists())) {
				RenderState = WallRenderState.DOUBLE_CONNECTED_OPPOSITE;
			} else {
				RenderState = WallRenderState.DOUBLE_CONNECTED_ADJACENT;
			}
			break;
		case 3:
			RenderState = WallRenderState.TRIPLE_CONNECTED;
			break;
		case 4:
			RenderState = WallRenderState.CONNECTED;
			break;
		case 0:
		default:
			RenderState = WallRenderState.DISCONNECTED;
			break;
		}
		
		UpdateRendering();
	}

	private void UpdateRendering() {
		Tile top = GetNeighbor(RelativePosition.TOP), right = GetNeighbor(RelativePosition.RIGHT), 
		bottom = GetNeighbor(RelativePosition.BOTTOM), left = GetNeighbor(RelativePosition.LEFT);
		GameObject gameObject = FetchGameObject();
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

		renderer.enabled = true;
		renderer.sprite = GetSpriteForRenderState(renderState);

		//Additional transformations (if needed)
		renderer.transform.rotation = new Quaternion();
		switch (renderState) {
		case WallRenderState.SINGLE_CONNECTED:
			if (top.Exists()) {
				renderer.transform.Rotate(Vector3.forward * 90);
			} else if (bottom.Exists()) {
				renderer.transform.Rotate(Vector3.forward * -90);
			} else if (left.Exists()) {
				renderer.transform.Rotate(Vector3.forward * 180);
			}
			break;
		case WallRenderState.DOUBLE_CONNECTED_OPPOSITE:
			if (top.Exists()) {
				renderer.transform.Rotate(Vector3.forward * 90);
			}
			break;
		case WallRenderState.DOUBLE_CONNECTED_ADJACENT:
			if (top.Exists()) {
				if (left.Exists()) {
					renderer.transform.Rotate(Vector3.forward * 180);
				} else {
					renderer.transform.Rotate(Vector3.forward * 90);
				}
			} else if (left.Exists()) {
				renderer.transform.Rotate(Vector3.forward * -90);
			}
			break;
		case WallRenderState.TRIPLE_CONNECTED:
			if (!right.Exists()) {
				renderer.transform.Rotate(Vector3.forward * -90);
			} else if (!bottom.Exists()) {
				renderer.transform.Rotate(Vector3.forward * -180);
			} else if (!left.Exists()) {
				renderer.transform.Rotate(Vector3.forward * 90);
			}
			break;
		}
	}

	public static Sprite GetSpriteForRenderState(WallRenderState state) {
		BoardController controller = BoardController.Instance;
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
