using UnityEngine;

public class MainMenu : MonoBehaviour {
    /// <summary>
    /// Destroys any objects tagged with scene manager or canvas.
    /// </summary>
    private void Start() {
        if (GameObject.FindWithTag("Canvas")) {
            Destroy(GameObject.FindWithTag("Canvas"));
        }

        if (GameObject.FindWithTag("SceneManager")) {
            Destroy(GameObject.FindWithTag("SceneManager"));
        }
    }
}
