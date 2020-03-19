using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages loading new scenes and the state of objects inside the current 
/// scene.
/// </summary>
public class SceneManagement : MonoBehaviour {
	private const int references = 2;  // Number of objects with a reference to the scoreboard.
	private int counter = 0; // Tracks how many objects have found the scoreboard.

	private GameObject scoreboard = null;

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	private void Start() {
		scoreboard = GameObject.FindWithTag("Scoreboard");
		FoundScoreboard();
	}

	/// <summary>
	/// Loads the next scene in the project's build order.
	/// </summary>
	internal void LoadNextScene() {
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
		}
    }

	/// <summary>
	/// Increases the scoreboard's reference counter and enables it once it has 
	/// been found by all GameObjects with a reference to it.
	/// </summary>
	public void FoundScoreboard() {
		++counter;

		if (counter >= references) {
			scoreboard.SetActive(false);
		}
	}
}
