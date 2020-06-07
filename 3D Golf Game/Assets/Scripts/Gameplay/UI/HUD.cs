using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public int PuttCount { get; private set; } = 0;

	private Text puttText = null;

	/// <summary>
	/// Finds the putt display text and initializes it to zero.
	/// </summary>
	private void Start() {
		puttText = GetComponentInChildren<Text>();
		puttText.text = ("Putts: " + PuttCount.ToString());
	}

	/// <summary>
	/// Increases/resets the putt count value and updates the putt count text.
	/// </summary>
	/// <param name="score"> Negative values reset the putt counter and positive values increase the putt counter by one. </param>
	public void UpdatePuttCounter(int score) {
		if (score == 0) {
			PuttCount = score;
		} else {
			++PuttCount;
		}

		puttText.text = ("Putts: " + PuttCount.ToString());
	}
}
