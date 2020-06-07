using UnityEngine;

public class SettingsScreen : MonoBehaviour {
	private float cameraSensitivity = 20.0f;
	private string sensitivityString = "Sensitivity";

	public void ChangeSensitivity(float sensitivity) {
		cameraSensitivity = sensitivity;
	}

	private void ApplySettings() {
		PlayerPrefs.SetFloat(sensitivityString, cameraSensitivity);
	}

	private void Back() {
		gameObject.SetActive(false);
	}
}