using UnityEngine;

/// <summary>
/// Signals the end of a level, displaying the scoreboard, when the golf ball collides with a hole.
/// </summary>
public class Hole : MonoBehaviour {
	private float scoreboardDisplayTime = 4.0f;
	private bool ballPutted = false;
	private SceneManagement sceneManager = null;
	private HUD hud = null;
	private GameObject scoreboard = null;

	/// <summary>
	/// Finds uninitialized script references.
	/// </summary>
	private void Awake() {
		scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
		sceneManager = FindObjectOfType<SceneManagement>();
		hud = FindObjectOfType<HUD>();
	}

	/// <summary>
	/// Disables the scoreboard.
	/// </summary>
	private void Start() {
		scoreboard.SetActive(false);
	}

	/// <summary>
	/// Loads the next scene after set time but only if the golf ball has been putted.
	/// </summary>
	private void Update() {
		if (ballPutted) {
			scoreboardDisplayTime -= Time.deltaTime;

			if (scoreboardDisplayTime <= 0.0f) {
				hud.ResetPuttCounter();
				sceneManager.LoadNextScene();
				scoreboardDisplayTime = 0.0f;
			}
		}
	}

	/// <summary>
	/// Displays and updates the scoreboard once the golf ball has been putted.
	/// </summary>
	/// <param name="other"> The collider that entered the gameObject's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("GolfBall")) {
			ballPutted = true;
			scoreboard.SetActive(true);
			scoreboard.GetComponent<Scoreboard>().UpdateScoreboard();
		}
    }
}