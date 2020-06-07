using UnityEngine;

public class MainMenu : MonoBehaviour {
	/// <summary>
	/// Deletes all the player preferences.
	/// </summary>
	private void Awake() {
		PlayerPrefs.DeleteAll();
	}

	/// <summary>
	/// Destroys objects carried through from the levels.
	/// </summary>
	private void Start() {
        if (GameObject.FindWithTag("GameplayCanvas")) {
            Destroy(GameObject.FindWithTag("GameplayCanvas"));
        }

        if (GameObject.FindWithTag("SceneManager")) {
            Destroy(GameObject.FindWithTag("SceneManager"));
        }
    }
}