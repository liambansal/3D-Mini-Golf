using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public void Play() {
		SceneManager.LoadScene("Test Level", LoadSceneMode.Single);
	}

	public void Options() {
		
	}

	public void Quit() {
		Application.Quit();
	}
}
