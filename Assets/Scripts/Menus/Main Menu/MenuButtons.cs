using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	[SerializeField]
	private GameObject settingsScreen = null;
	private const string levelOneName = "Level 1";
	private const string tutorialName = "Tutorial";

	private void PlayGame() {
		SceneManager.LoadScene(levelOneName, LoadSceneMode.Single);
	}

	private void LoadTutorial() {
		SceneManager.LoadScene(tutorialName, LoadSceneMode.Single);
	}

	private void ToggleSettings() {
		if (settingsScreen.activeInHierarchy) {
			settingsScreen.SetActive(false);
		} else {
			settingsScreen.SetActive(true);
		}
	}
}