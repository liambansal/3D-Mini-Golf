using UnityEngine;

/// <summary>
/// Handles parenting the turntables to the golf ball when colliding.
/// </summary>
public class Turntable : MonoBehaviour {
	private const string golfBallTag = "GolfBall";
	private Vector3 golfBallScale = new Vector3(1.0f, 1.0f, 1.0f);

	/// <summary>
	/// Parents the turntable to the golf ball when they collide.
	/// </summary>
	/// <param name="other"> The collider that entered the turntable's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			other.transform.parent = transform.parent;
			other.transform.localScale = golfBallScale;
		}
	}

	/// <summary>
	/// Un-parents the turntable from the golf ball when it leaves it's trigger collider.
	/// </summary>
	/// <param name="other"> The collider that exited the turntable's trigger collider. </param>
	private void OnTriggerExit(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			other.transform.parent = null;
			other.transform.localScale = golfBallScale;
		}
	}
}