using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
	private const string mainMenuName = "Main Menu";

	private void LoadMainMenu() {
		SceneManager.LoadScene(mainMenuName, LoadSceneMode.Single);
	}
}