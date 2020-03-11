using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {
	[SerializeField]
	private Text puttText = null;
	[SerializeField]
	private Text levelOneScore = null;
	[SerializeField]
	private Text levelTwoScore = null;
	[SerializeField]
	private Text levelThreeScore = null;
	[SerializeField]
	private Text levelFourScore = null;
	[SerializeField]
	private Text levelFiveScore = null;
	[SerializeField]
	private Text levelSixScore = null;
	[SerializeField]
	private Text finalScore = null;

	private int puttCount = 0;

	private float displayTime = 8.0f;

	private SceneManagement sceneManager = null;

	/// <summary>
	/// Initializes the putt dislpay text and gets the SceneManager script.
	/// </summary>
	private void Start() {
		sceneManager = FindObjectOfType<SceneManagement>();
		puttText.text = ("Putts: " + puttCount.ToString());
	}

	private void Update() {
		if (gameObject.activeSelf) {
			displayTime -= Time.deltaTime;

			if (displayTime <= 0.0f) {
				sceneManager.LoadNextScene();
			}
		}
	}

	/// <summary>
	/// Adds 1 to the putt counter and updates the display text.
	/// </summary>
	internal void IncreasePutts() {
		++puttCount;
		puttText.text = ("Putts: " + puttCount.ToString());
	}

	private void UpdateScores() {
		levelOneScore.text = puttCount.ToString();
	} 

	/// <summary>
	/// Updates the scoreboards score and displays the results.
	/// </summary>
	internal void DisplayScoreboard() {
		UpdateScores();
		gameObject.SetActive(true);
	}
}
