using UnityEngine;

public class MainCamera : MonoBehaviour {
	private float translationBuffer = 10;
	private float xTranslation = 0;
	private float yTranslation = 0;
	private float zoomSpeed = 8.0f;
	private float anchorDistanceToGolfBall = 0.0f;
	private float maxDistanceToAnchor = 0.0f;

	private bool canRotate = false;
	private bool canZoom = true;

	private Vector3 ballOffset = new Vector3(); // Used to determine the route from the camera to the golf ball.

	private GameObject golfBall = null;
	private GameObject cameraAnchor = null;

	/// <summary>
	/// Finds the golfBall & cameraAnchor objects and sets the offset position 
	/// between the camera and golfBall.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
		cameraAnchor = GameObject.Find("Camera Anchor");
		ballOffset = transform.position - golfBall.transform.position;
		anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position);
		// Finds the maximum possible distance between the camera and 
		// cameraAnchor before the camera rotates past the golfBall's global y 
		// axis.
		maxDistanceToAnchor = Vector3.Distance(transform.position, 
			(new Vector3(golfBall.transform.position.x,
			golfBall.transform.position.y + anchorDistanceToGolfBall,
			golfBall.transform.position.z)));
	}

	private void FixedUpdate() {
		// Enables camera rotations while the player is inputting from the mouse.
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
				cameraAnchor.SendMessage("RotateAlongXAxis", xTranslation); // Rotates the cameraAnchor relative to the cameras rotation around the golfBall.
				ClampCameraPosition();
				transform.LookAt(golfBall.transform.position, transform.up); // Makes the camera's forward direction point towards the golfBall.
				ballOffset = transform.position - golfBall.transform.position; // Sets the new offset position.
				anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position); // Stores new distance between the camera and golfBall.
			}

			if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y") < 0)) {
				yTranslation = (Input.GetAxis("Mouse Y") * translationBuffer);
				RotateCamera();
				cameraAnchor.SendMessage("RotateAlongXAxis", xTranslation); // Rotates the cameraAnchor relative to the cameras rotation around the golfBall.
				ClampCameraPosition();
				transform.LookAt(golfBall.transform.position, transform.up); // Makes the camera's forward direction point towards the golfBall.
				ballOffset = transform.position - golfBall.transform.position; // Sets the new offset position.
				anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position); // Stores new distance between the camera and golfBall.
			}
		} else { // Golf ball is moving.
			MoveCamera();
		}

		// Check input for zooming in/out.
		if (canZoom) {
			// TODO: Change logic so there's only one if statement for zooming
			if (Input.GetAxis("Mouse ScrollWheel") < 0) {
				transform.position += ZoomIn();
				cameraAnchor.SendMessage("ZoomIn");
				ballOffset = transform.position - golfBall.transform.position; // Sets the new offset position.
				anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position); // Stores new distance between the camera and golfBall.
				// Finds the maximum possible distance between the camera and 
				// cameraAnchor before the camera rotates past the golfBall's global y 
				// axis.
				maxDistanceToAnchor = Vector3.Distance(transform.position,
				(new Vector3(golfBall.transform.position.x,
				golfBall.transform.position.y + anchorDistanceToGolfBall,
				golfBall.transform.position.z)));
			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0) {
				transform.position -= ZoomIn();
				cameraAnchor.SendMessage("ZoomOut");
				ballOffset = transform.position - golfBall.transform.position; // Sets the new offset position.
				anchorDistanceToGolfBall = Vector3.Distance(cameraAnchor.transform.position, golfBall.transform.position); // Stores new distance between the camera and golfBall.
				// Finds the maximum possible distance between the camera and 
				// cameraAnchor before the camera rotates past the golfBall's global y 
				// axis.
				maxDistanceToAnchor = Vector3.Distance(transform.position,
				(new Vector3(golfBall.transform.position.x,
				golfBall.transform.position.y + anchorDistanceToGolfBall,
				golfBall.transform.position.z)));
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
	/// Moves the camera towards the golfBall. 
	/// </summary>
	private Vector3 ZoomIn() {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		return displacement * zoomSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Clamps the cameras y position so it can't rotate over or under the ball.
	/// </summary>
	private void ClampCameraPosition() {
		if (transform.position.y > cameraAnchor.transform.position.y) {
			if (Vector3.Distance(transform.position, cameraAnchor.transform.position) >= maxDistanceToAnchor) {
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
}
