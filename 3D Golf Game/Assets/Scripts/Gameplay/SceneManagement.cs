using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages loading scenes from a level.
/// </summary>
public class SceneManagement : MonoBehaviour {
	private const string mainMenuName = "Main Menu";
	
	/// <summary>
	/// Stops the scene manager from being destroyed when loading a new scene.
	/// </summary>
	private void Awake() {
		DontDestroyOnLoad(this);
	}

	/// <summary>
	/// Loads the next scene in the project's build order until the last scene is loaded, in which
	/// case loads the main menu.
	/// </summary>
	public void LoadNextScene() {
		const int indexAddition = 1;
		int maximumBuildIndex = SceneManager.sceneCountInBuildSettings - 1;

		if (SceneManager.GetActiveScene().buildIndex < maximumBuildIndex) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + indexAddition, LoadSceneMode.Single);
		} else {
			SceneManager.LoadScene(mainMenuName, LoadSceneMode.Single);
		}
	}
}