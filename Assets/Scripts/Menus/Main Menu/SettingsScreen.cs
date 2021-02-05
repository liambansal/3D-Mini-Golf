using UnityEngine;

/// <summary>
/// Provides functionality for the settings screen's buttons.
/// </summary>
public class SettingsScreen : MonoBehaviour {
	private float cameraSensitivity = 20.0f;
	private const string sensitivityString = "Sensitivity";

	private void ChangeSensitivity(float sensitivity) {
		cameraSensitivity = sensitivity;
	}

	private void ApplySettings() {
		PlayerPrefs.SetFloat(sensitivityString, cameraSensitivity);
	}

	/// <summary>
	/// Goes back to the main menu.
	/// </summary>
	private void Back() {
		gameObject.SetActive(false);
	}
}