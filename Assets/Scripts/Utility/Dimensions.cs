using UnityEngine;
using System.Collections;

public struct Dimensions {

	int width, height;

	public int Width {
		get {
			return width;
		}
		private set {
			width = value;
		}
	}

	public int Height {
		get {
			return height;
		}
		private set {
			height = value;
		}
	}

	public Dimensions(int Width, int Height) {
		this.width = Width;
		this.height = Height;
	}
}
