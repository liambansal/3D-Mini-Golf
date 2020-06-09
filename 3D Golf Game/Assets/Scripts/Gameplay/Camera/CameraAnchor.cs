using UnityEngine;

/// <summary>
/// Positions the camera's anchor relative to the golf ball's position to help clamp the camera's position.
/// </summary>
public class CameraAnchor : MonoBehaviour {
	public float GolfBallDistance { get; private set; } = 0.0f;

	private const float minimumDistanceToGolfBall = 2.0f;
	private const float maximumDistanceToGolfBall = 12.0f;
	private const float minimumGolfBallSpeed = 0.15f;
	private const string golfBallTag = "GolfBall";
	private Vector3 ballOffset = new Vector3();
	private Rigidbody golfBallRigidbody = null;

	/// <summary>
	/// Gets the golf ball GameObject's rigidbody, the distance to it and the offset position from it.
	/// </summary>
	private void Awake() {
		golfBallRigidbody = GameObject.FindGameObjectWithTag(golfBallTag).GetComponent<Rigidbody>();
		ballOffset = transform.position - golfBallRigidbody.position;
		GolfBallDistance = Vector3.Distance(transform.position, golfBallRigidbody.position);
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
		if (GolfBallDistance > minimumDistanceToGolfBall) {
			transform.position += moveDirection;
			GolfBallDistance = Vector3.Distance(transform.position, golfBallRigidbody.position);
		}
	}

	private void MoveAwayFromGolfBall(Vector3 moveDirection) {
		if (GolfBallDistance < maximumDistanceToGolfBall) {
			transform.position -= moveDirection;
			GolfBallDistance = Vector3.Distance(transform.position, golfBallRigidbody.position);
		}
	}

	private void RotateAroundGolfBall(float xTranslation) {
		transform.RotateAround(golfBallRigidbody.position, Vector3.up, xTranslation);
	}
}