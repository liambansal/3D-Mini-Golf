using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Updates the scoreboard with the player's putt count for each level.
/// </summary>
public class Scoreboard : MonoBehaviour {
	/// <summary>
	/// Difference between the first level's build index and 0th build index.
	/// </summary>
	private const int indexOffset = 3;
	private const int levelCount = 3;
	private int totalScore = 0;
	private Text[] currentLevelScoreText = new Text[levelCount];
	private Text totalScoreText = null;
	private HUD hud = null;

	/// <summary>
	/// Gets references for unassigned variables.
	/// </summary>
	private void Awake() {
		hud = FindObjectOfType<HUD>();
		totalScoreText = GameObject.FindWithTag("TotalScore").GetComponent<Text>();
		int levelNumber = 1;

		for (int index = 0; index < levelCount; ++index, ++levelNumber) {
			currentLevelScoreText[index] = GameObject.FindWithTag("Level" + levelNumber.ToString() + "Score").GetComponent<Text>();
		}
	}

	/// <summary>
	/// Updates the scoreboard with the player's putt count for each level and their overall score.
	/// </summary>
	public void UpdateScoreboard() {
		int buildIndex = SceneManager.GetActiveScene().buildIndex - indexOffset;

		if (buildIndex < levelCount) {
			currentLevelScoreText[buildIndex].text = hud.PuttCount.ToString();
		}

		totalScore += hud.PuttCount;
		totalScoreText.text = totalScore.ToString();
	}
}