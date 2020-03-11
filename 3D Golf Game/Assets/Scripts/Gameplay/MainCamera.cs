using UnityEngine;

public class MainCamera : MonoBehaviour {
	private float translationBuffer = 8.0f; // Affects the cameras rotation speed.
	private float xTranslation = 0.0f; // The mouse-x input axis value.
	private float yTranslation = 0.0f; // The mouse-y input axis value.
	private float zoomSpeed = 8.0f;
	private float anchorDistanceToGolfBall = 0.0f;
	private float maxDistanceToAnchor = 0.0f;
	private float minDistanceToGolfBall = 2.0f;
	private float maxDistanceToGolfBall = 12.0f;

	private bool canRotate = false;
	private bool canZoom = true;

	private Vector3 ballOffset = new Vector3(); // Used to determine the route 
	// from the camera to the golf ball.

	private GameObject golfBall = null;
	private GameObject cameraAnchor = null;

	/// <summary>
	/// Finds the golfBall & cameraAnchor gameObjects and makes a call to 
	/// calculate distances for positioning various gameObjects.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
		cameraAnchor = GameObject.Find("Camera Anchor");
		CalculateDistances();
	}

	private void FixedUpdate() {
		if (Input.GetMouseButton(0)) {
			canRotate = true;
			canZoom = false;
		} else {
			canRotate = false;
			canZoom = true;
		}

		if (canRotate && (golfBall.GetComponent<Rigidbody>().velocity.magnitude == 0)) { // Golf ball is still.
			// TODO: Change logic so there's only one if statement for rotating
			// Checks for change in the mouxe's x/y axis values.
			if ((Input.GetAxis("Mouse X") > 0) || (Input.GetAxis("Mouse X") < 0)) {
				xTranslation = (Input.GetAxis("Mouse X") * translationBuffer);
				RotateCamera();
				cameraAnchor.SendMessage("RotateAroundGolfBall", xTranslation); // Rotates the cameraAnchor 
				// relative to the cameras rotation around the golfBall.
				ClampCameraPosition();
				transform.LookAt(golfBall.transform.position, transform.up); // Makes the camera's forward 
				// direction point towards the golfBall.
				CalculateDistances();
			}

			if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y") < 0)) {
				yTranslation = (Input.GetAxis("Mouse Y") * translationBuffer);
				RotateCamera();
				cameraAnchor.SendMessage("RotateAroundGolfBall", xTranslation); // Rotates the cameraAnchor 
				// relative to the cameras rotation around the golfBall.
				ClampCameraPosition();
				transform.LookAt(golfBall.transform.position, transform.up); // Makes the camera's forward 
				// direction point towards the golfBall.
				CalculateDistances();
			}
		} else { // Golf ball is moving.
			MoveCamera();
		}

		// Check input for zooming in/out.
		if (canZoom) {
			if (Input.GetAxis("Mouse ScrollWheel") > 0) {
				if (DistanceToGolfBall() > minDistanceToGolfBall) {
					transform.position += Zoom(); // Zooms in.
					CalculateDistances();
				}
			} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
				if (DistanceToGolfBall() < maxDistanceToGolfBall) {
					transform.position -= Zoom(); // Zooms out.
					CalculateDistances();
				}
			}
		}
	}

	/// <summary>
	/// Moves the camera so it follows the golfBall's position.
	/// </summary>
	private void MoveCamera() {
		transform.position = golfBall.transform.position + ballOffset;
	}

	/// <summary>
	/// Rotates the camera around the golfBall.
	/// </summary>
	private void RotateCamera() {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
		transform.RotateAround(golfBall.transform.position, transform.right, -yTranslation);
	}

	/// <summary>
	/// Returns a Vector3 for moving the camera towards or away from the 
	/// golfBall.
	/// </summary>
	private Vector3 Zoom() {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		return displacement * -zoomSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Clamps the cameras y position so it can't rotate over or under the ball.
	/// </summary>
	private void ClampCameraPosition() {
		if (transform.position.y > cameraAnchor.transform.position.y) {
			if (Vector3.Distance(transform.position, cameraAnchor.transform.position) > maxDistanceToAnchor) {
				transform.position = new Vector3(golfBall.transform.position.x, 
					golfBall.transform.position.y + anchorDistanceToGolfBall, 
					golfBall.transform.position.z);
			}
		} else if (transform.position.y < cameraAnchor.transform.position.y) {
			transform.position = new Vector3(cameraAnchor.transform.position.x, 
				cameraAnchor.transform.position.y, 
				cameraAnchor.transform.position.z);
		}
	}

	/// <summary>
	/// Calculates the new camera to golfBall offset, cameraAnchor to golfBall 
	/// distance and the camera's max possible distance to cameraAnchor.
	/// </summary>
	private void CalculateDistances() {
		ballOffset = transform.position - golfBall.transform.position; // Sets the new offset position.
		// Stores new distance between the camera and golfBall.
		anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position);
		// Finds the maximum possible distance between the camera and 
		// cameraAnchor before the camera rotates past the golfBall's global y 
		// axis.
		maxDistanceToAnchor = Vector3.Distance(cameraAnchor.transform.position,
		(new Vector3(golfBall.transform.position.x,
			golfBall.transform.position.y + anchorDistanceToGolfBall,
			golfBall.transform.position.z)));
	}

	private float DistanceToGolfBall() {
		return Vector3.Distance(transform.position, golfBall.transform.position);
	}
}
