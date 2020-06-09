using UnityEngine;

/// <summary>
/// Positions the camera's anchor relative to the golf ball's position to help clamp the camera's position.
/// </summary>
public class CameraAnchor : MonoBehaviour {
	private const float minimumDistanceToGolfBall = 2.0f;
	private const float maximumDistanceToGolfBall = 12.0f;
	private const float minimumGolfBallSpeed = 0.15f;
	private const string golfBallTag = "GolfBall";
	private Vector3 ballOffset = new Vector3();
	private Rigidbody golfBallRigidbody = null;

	/// <summary>
	/// Gets the golf ball GameObject's rigidbody and offset position from it.
	/// </summary>
	private void Awake() {
		golfBallRigidbody = GameObject.FindGameObjectWithTag(golfBallTag).GetComponent<Rigidbody>();
		ballOffset = transform.position - golfBallRigidbody.position;
	}

	/// <summary>
	/// Moves the anchor to follow the golf ball when it's moving.
	/// </summary>
	private void Update() {
		if (golfBallRigidbody.velocity != Vector3.zero) {
			MoveAnchor();
		}
	}

	/// <summary>
	/// Forces the camera anchor to face the golf ball and calculate the difference in positions.
	/// </summary>
	private void FixedUpdate() {
		if (golfBallRigidbody.velocity.magnitude <= minimumGolfBallSpeed) {
			transform.LookAt(golfBallRigidbody.position, transform.up);
			ballOffset = transform.position - golfBallRigidbody.position;
		}
	}

	/// <summary>
	/// Moves the camera anchor towards the golf ball.
	/// </summary>
	private void MoveAnchor() {
		transform.position = golfBallRigidbody.position + ballOffset;
	}

	private void MoveTowardsGolfBall(Vector3 moveDirection) {
		if (DistanceToGolfBall() > minimumDistanceToGolfBall) {
			transform.position += moveDirection;
		}
	}

	private void MoveAwayFromGolfBall(Vector3 moveDirection) {
		if (DistanceToGolfBall() < maximumDistanceToGolfBall) {
			transform.position -= moveDirection;
		}
	}

	private void RotateAroundGolfBall(float xTranslation) {
		transform.RotateAround(golfBallRigidbody.position, Vector3.up, xTranslation);
	}

	private float DistanceToGolfBall() {
		return Vector3.Distance(transform.position, golfBallRigidbody.position);
	}
}