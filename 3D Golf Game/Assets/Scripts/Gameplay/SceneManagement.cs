using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
	/// <summary>
	/// Loads the next scene in the build order.
	/// </summary>
    internal void LoadNextScene() {
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
		}
    }
}
