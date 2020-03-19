using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {
	private int totalScore = 0;

	private Text currentLevelScoreText = null;
	private Text totalScoreText = null;
	
	private HUD hud = null;

	private void Start() {
		hud = FindObjectOfType<HUD>();
		totalScoreText = GameObject.FindWithTag("TotalScore").GetComponent<Text>();
	}

	public void UpdateScoreboard() {
		switch (SceneManager.GetActiveScene().buildIndex) {
			case 1: {
				currentLevelScoreText = GameObject.FindWithTag("LevelOneScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
			case 2: {
				currentLevelScoreText = GameObject.FindWithTag("LevelTwoScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
			case 3: {
				currentLevelScoreText = GameObject.FindWithTag("LevelThreeScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
			case 4: {
				currentLevelScoreText = GameObject.FindWithTag("LevelFourScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
			case 5: {
				currentLevelScoreText = GameObject.FindWithTag("LevelFiveScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
			case 6: {
				currentLevelScoreText = GameObject.FindWithTag("LevelSixScore").GetComponent<Text>();
				currentLevelScoreText.text = hud.PuttCount.ToString();
				break;
			}
		}

		totalScore += hud.PuttCount;
		totalScoreText.text = totalScore.ToString();
	}
}
