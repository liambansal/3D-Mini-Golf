using UnityEngine;

public class MainCamera : MonoBehaviour {
	[SerializeField]
	private GameObject golfBall = null;

	private float translationBuffer = 10,
		xTranslation = 0,
		yTranslation = 0,
		zoomSpeed = 10.0f;

	private bool canRotate = false;

	private Vector3 offset; // Used to determine the route from the camera to the golf ball.

	/// <summary>
	/// Sets the offset position between the camera and golf ball.
	/// </summary>
	private void Start() {
		offset = transform.position - golfBall.transform.position;
	}

	private void FixedUpdate() {
		// Enables camera rotations while the player is inputting from the mouse.
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
			canRotate = true;
		} else {
			canRotate = false;
		}

		if (canRotate && (golfBall.GetComponent<Rigidbody>().velocity.magnitude == 0)) { // Golf ball is still.
			// Checks for change in the mouxe's x/y axis values.
			if ((Input.GetAxis("Mouse X") > 0) || (Input.GetAxis("Mouse X") < 0)) {
				xTranslation = (Input.GetAxis("Mouse X") * translationBuffer);
			}

			if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y") < 0)) {
				yTranslation = (Input.GetAxis("Mouse Y") * translationBuffer);
			}

			RotateCamera(xTranslation, yTranslation);
			transform.LookAt(golfBall.transform.position, transform.up);
			offset = transform.position - golfBall.transform.position; // Sets the new offset position.
		} else { // Golf ball is moving.
			MoveCamera();
		}

		// Check input for zooming in/out
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			ZoomIn();
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			ZoomOut();
		}
	}

	/// <summary>
	/// Rotates the camera around the golf ball gameobject.
	/// </summary>
	/// <param name="xAxis"></param>
	/// <param name="yAxis"></param>
	private void RotateCamera(float xAngle, float yAngle) {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xAngle);
		transform.RotateAround(golfBall.transform.position, transform.right, -yAngle);
	}

	/// <summary>
	/// Moves the camera towards the golf ball gameobject.
	/// </summary>
	/// <param name="initialPosition"></param>
	/// <returns></returns>
	private void ZoomIn() {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position += (displacement * zoomSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Moves the camera away from the golf ball gameobject.
	/// </summary>
	/// <param name="initialPosition"></param>
	/// <returns></returns>
	private void ZoomOut() {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position += (displacement * zoomSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Moves the camera so it follows the golf ball's position.
	/// </summary>
	private void MoveCamera() {
		transform.position = golfBall.transform.position + offset;
	}
}