using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the HUD's elements.
/// </summary>
public class HUD : MonoBehaviour {
	/// <summary>
	/// Tracks how many putts the player has performed.
	/// </summary>
	public int PuttCount { get; private set; } = 0;
	private Text puttCountText = null;

	/// <summary>
	/// Finds the putt text text and initializes it to zero.
	/// </summary>
	private void Start() {
		puttCountText = GetComponentInChildren<Text>();
		puttCountText.text = ("Putts: " + PuttCount.ToString());
	}

	/// <summary>
	/// Increases/resets the putt count value/text.
	/// </summary>
	/// <param name="score"> Negative values reset the putt counter and positive values increase
	/// the putt counter by one. </param>
	public void UpdatePuttCounter(int score) {
		if (score == 0) {
			PuttCount = score;
		} else {
			++PuttCount;
		}

		puttCountText.text = ("Putts: " + PuttCount.ToString());
	}
}