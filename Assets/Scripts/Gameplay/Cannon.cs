using UnityEngine;

/// <summary>
/// Controls when the cannon's animation is played.
/// </summary>
public class Cannon : MonoBehaviour {
	[SerializeField]
	private Transform firingPosition = null;
	private const int shotForce = 20;
	private const string golfBallTag = "GolfBall";
	/// <summary>
	/// The name of the bool that triggers the firing animation.
	/// </summary>
	private const string firingAnimationTrigger = "playFiring";
	private Rigidbody golfBallRigidbody = null;

	/// <summary>
	/// Freezes the golf ball inside the cannon and plays it's firing animation.
	/// </summary>
	/// <param name="other"> The collider that enters the cannon's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			other.GetComponent<Controller>().enabled = false;
			golfBallRigidbody = other.GetComponent<Rigidbody>();
			golfBallRigidbody.useGravity = false;
			golfBallRigidbody.position = firingPosition.position;
			golfBallRigidbody.velocity = Vector3.zero;
			golfBallRigidbody.freezeRotation = true;
			GetComponent<Animator>().SetBool(firingAnimationTrigger, true);
		}
	}

	private void FireGolfBall() {
		golfBallRigidbody.position = firingPosition.position;
		golfBallRigidbody.AddForce((transform.position - firingPosition.position) * shotForce, ForceMode.Impulse);
		golfBallRigidbody.useGravity = true;
		golfBallRigidbody.freezeRotation = false;
		golfBallRigidbody.gameObject.GetComponent<Controller>().enabled = true;
	}
}