using UnityEngine;

/// <summary>
/// Moves a rigidbody towards a target position once the pressure plate is triggered.
/// </summary>
public class PressurePlate : MonoBehaviour {
	[SerializeField]
	private Rigidbody gateRigidbody = null;
	[SerializeField]
	private Vector3 targetPosition = new Vector3();
	private const string golfBallTag = "GolfBall";
	private bool triggered = false;

	/// <summary>
	/// Moves the gate's rigidbody towards the target position if the plate has been triggered.
	/// </summary>
	private void FixedUpdate() {
		if (triggered) {
			if (gateRigidbody.position.y < targetPosition.y) {
				gateRigidbody.AddForce(Vector3.up, ForceMode.Acceleration);
			} else {
				gateRigidbody.velocity = Vector3.zero;
			}
		}
	}

	/// <summary>
	/// Checks if the golf ball enters the pressure plate's trigger collider.
	/// </summary>
	/// <param name="other"> The collider entering the pressure plate's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			triggered = true;
		}
	}
}