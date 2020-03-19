using UnityEngine;

public class Hole : MonoBehaviour {
	private float displayTime = 4.0f;

	private bool ballPutted = false;

	private SceneManagement sceneManager = null;
	private Scoreboard scoreboard = null;
	private HUD hud = null;

	private void Start() {
		sceneManager = FindObjectOfType<SceneManagement>();
		scoreboard = FindObjectOfType<Scoreboard>();
		hud = FindObjectOfType<HUD>();
		sceneManager.FoundScoreboard();
	}

	private void Update() {
		if (ballPutted) {
			displayTime -= Time.deltaTime;

			if (displayTime <= 0.0f) {
				hud.UpdatePuttCounter(0);
				sceneManager.LoadNextScene();
				displayTime = 0.0f;
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("GolfBall")) {
			ballPutted = true;
			scoreboard.gameObject.SetActive(true);
			scoreboard.UpdateScoreboard();
		}
    }
}
