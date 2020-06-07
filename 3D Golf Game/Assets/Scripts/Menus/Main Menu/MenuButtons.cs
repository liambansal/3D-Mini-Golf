using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	[SerializeField]
	private GameObject settingsScreen = null;

	public void PlayGame() {
		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
	}

	public void ToggleSettings() {
		if (settingsScreen.activeInHierarchy) {
			settingsScreen.SetActive(false);
		} else {
			settingsScreen.SetActive(true);
		}
	}

	public void QuitGame() {
		Application.Quit();
	}
}