using UnityEngine;

public class Hole : MonoBehaviour {
	[SerializeField]
	Scoreboard scoreboard = null;

	/// <summary>
	/// Makes a call to display the scoreboard.
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("GolfBall")) {
			scoreboard.DisplayScoreboard();
		}
    }
}
