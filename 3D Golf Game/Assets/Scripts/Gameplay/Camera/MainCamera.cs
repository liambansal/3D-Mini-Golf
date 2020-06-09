using UnityEngine;

/// <summary>
/// Manipulates the camera by listening for touch input.
/// </summary>
public class MainCamera : MonoBehaviour {
	/// <summary>
	/// Touches needed to rotate the camera.
	/// </summary>
	private const int rotateTouchCount = 1;
	/// <summary>
	/// Touches needed to zoom the camera.
	/// </summary>
	private const int zoomTouchCount = 2;

	private const float minimumDistanceToGolfBall = 2.0f;
	private const float maximumDistanceToGolfBall = 12.0f;
	private const float minimumGolfBallSpeed = 0.15f;
	private float rotateSensitivity = 20.0f;
	/// <summary>
	/// Angle to rotate around the golf ball by.
	/// </summary>
	private float xTranslation = 0.0f;
	/// <summary>
	/// Angle to rotate around the golf ball by.
	/// </summary>
	private float yTranslation = 0.0f;
	/// <summary>
	/// Distance between the two touch positions.
	/// </summary>
	private float touchDistance = 0.0f;
	private float lastTouchDistance = 0.0f;
	private float anchorDistanceToGolfBall = 0.0f;
	private float maximumDistanceToAnchor = 0.0f;

	private const string sensitivityString = "Sensitivity";

	private Vector3 ballOffset = new Vector3();
	private Rigidbody golfBallRigidbody = null;
	private GameObject cameraAnchor = null;

	/// <summary>
	/// Gets the camera's sensitivity if it's set in PlayerPrefs.
	/// </summary>
	private void Awake() {
		if (PlayerPrefs.GetFloat(sensitivityString) > 0.0f) {
			rotateSensitivity = PlayerPrefs.GetFloat(sensitivityString);
		}
	}

	/// <summary>
	/// Assigns some camera variables and forces it to look at the golf ball.
	/// </summary>
	private void Start() {
		golfBallRigidbody = GameObject.FindGameObjectWithTag("GolfBall").GetComponent<Rigidbody>();
		cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor");
		CalculateDistances();
		transform.LookAt(golfBallRigidbody.transform);
	}

	/// <summary>
	/// Handles responding to touch input for controlling the camera.
	/// </summary>
	private void FixedUpdate() {
		MoveCamera();

		// Only respond to input while the golf ball isn't moving.
		if (golfBallRigidbody.velocity.magnitude <= minimumGolfBallSpeed) {
			// Check if the player wants to rotate the camera.
			if (Input.touchCount == rotateTouchCount) {
				Touch touch = Input.touches[0];

				switch (touch.phase) {
					case TouchPhase.Moved: {
						// Sets the angles to rotate around the golf ball by.
						xTranslation = touch.deltaPosition.x / rotateSensitivity;
						yTranslation = touch.deltaPosition.y / rotateSensitivity;
						RotateCamera();
						// Rotates the camera anchor around the golfBall.
						cameraAnchor.SendMessage("RotateAroundGolfBall", xTranslation);
						ClampCameraPosition();
						// Makes the camera's forward direction point towards the golfBall.
						transform.LookAt(golfBallRigidbody.position, transform.up);
						CalculateDistances();
						break;
					}
					default: {
						break;
					}
				}
			}

			// Check if the player wants to zoom the camera.
			if (Input.touchCount == zoomTouchCount) {
				for (int i = 0; i < zoomTouchCount; ++i) {
					Touch touch = Input.touches[i];

					switch (touch.phase) {
						case TouchPhase.Moved: {
							touchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

							if (DistanceToGolfBall() > minimumDistanceToGolfBall &&
								touchDistance > lastTouchDistance) {
								Vector3 routeToGolfBall = Zoom();
								// Moves the camera towards the golf ball.
								transform.position += routeToGolfBall;
								cameraAnchor.SendMessage("MoveTowardsGolfBall", routeToGolfBall);
							} else if (DistanceToGolfBall() < maximumDistanceToGolfBall &&
								touchDistance < lastTouchDistance) {
								Vector3 routeToGolfBall = Zoom();
								// Moves the camera away from the golf ball.
								transform.position -= routeToGolfBall;
								cameraAnchor.SendMessage("MoveAwayFromGolfBall", routeToGolfBall);
							}

							CalculateDistances();
							break;
						}
						default: {
							break;
						}
					}

					lastTouchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
				}
			}
		}
	}

	/// <summary>
	/// Moves the camera so it follows the golfBall's position.
	/// </summary>
	private void MoveCamera() {
		transform.position = golfBallRigidbody.position + ballOffset;
	}

	/// <summary>
	/// Rotates the camera around the golfBall.
	/// </summary>
	private void RotateCamera() {
		transform.RotateAround(golfBallRigidbody.position, Vector3.up, xTranslation);
		transform.RotateAround(golfBallRigidbody.position, transform.right, -yTranslation);
	}

	/// <summary>
	/// Returns a Vector3 for moving the camera towards or away from the 
	/// golfBall.
	/// </summary>
	/// <returns> The route from the camera to the golfBall. </returns>
	private Vector3 Zoom() {
		const float zoomSpeed = 8.0f;
		Vector3 cameraPosition = transform.position;
		Vector3 golfBallPosition = golfBallRigidbody.position;
		Vector3 displacement = cameraPosition - golfBallPosition;
		return displacement * -zoomSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Clamps the camera's y position so it can't rotate over or under the golfBall.
	/// </summary>
	private void ClampCameraPosition() {
		if (transform.position.y > cameraAnchor.transform.position.y) {
			if (Vector3.Distance(transform.position, cameraAnchor.transform.position) > maximumDistanceToAnchor) {
				// Sets the camera's position to be vertically alligned with the golf ball's position.
				transform.position = new Vector3(golfBallRigidbody.position.x, 
					golfBallRigidbody.position.y + anchorDistanceToGolfBall, 
					golfBallRigidbody.position.z);
			}
		} else if (transform.position.y < cameraAnchor.transform.position.y) {
			transform.position = new Vector3(cameraAnchor.transform.position.x, 
				cameraAnchor.transform.position.y, 
				cameraAnchor.transform.position.z);
		}
	}

	/// <summary>
	/// Calculates the new camera to golf ball offset, camera anchor to golf ball distance and the
	/// camera's maximum possible distance to the camera anchor.
	/// </summary>
	private void CalculateDistances() {
		ballOffset = transform.position - golfBallRigidbody.position;
		anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBallRigidbody.position);
		// Finds the maximum possible distance between the camera and camera anchor without the
		// camera rotating past the golf ball's global y axis.
		maximumDistanceToAnchor = Vector3.Distance(cameraAnchor.transform.position,
		(new Vector3(golfBallRigidbody.position.x,
			golfBallRigidbody.position.y + anchorDistanceToGolfBall,
			golfBallRigidbody.position.z)));
	}

	private float DistanceToGolfBall() {
		return Vector3.Distance(transform.position, golfBallRigidbody.position);
	}
}