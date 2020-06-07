using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
	private void LoadMainMenu() {
		SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
	}
}