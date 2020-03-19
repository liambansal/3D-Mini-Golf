using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	public void Play() {
		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
	}

	public void Options() {
		
	}

	public void Quit() {
		Application.Quit();
	}
}
