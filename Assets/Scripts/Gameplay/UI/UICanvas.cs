using UnityEngine;

/// <summary>
/// Stops a canvas from being destroyed upon loading a new scene.
/// </summary>
public class UICanvas : MonoBehaviour {
	/// <summary>
	/// Stops the canvas from being destoyed when loading a new scene.
	/// </summary>
	private void Awake() {
        DontDestroyOnLoad(this);
    }
}