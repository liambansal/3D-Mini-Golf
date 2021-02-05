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

	public void IncreasePuttCounter() {
		++PuttCount;
		puttCountText.text = ("Putts: " + PuttCount.ToString());
	}

	public void ResetPuttCounter() {
		PuttCount = 0;
		puttCountText.text = ("Putts: " + PuttCount.ToString());
	}
}