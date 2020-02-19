using UnityEngine;

public class MainCamera : MonoBehaviour {
	[SerializeField]
	private GameObject golfBall = null;

	private float translationBuffer = 10,
		xTranslation = 0,
		yTranslation = 0,
		zoomSpeed = 10.0f;

	private bool canRotate = false,
		isMoving = false;

	private Vector3 offset;

	private void FixedUpdate() {
		// Enables camera rotations while the player is inputting from the mouse.
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
			canRotate = true;
		} else {
			canRotate = false;
		}

		if (canRotate && !isMoving) {
			if ((Input.GetAxis("Mouse X") > 0) || (Input.GetAxis("Mouse X") < 0)) {
				xTranslation = (Input.GetAxis("Mouse X") * translationBuffer);
			}

			if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y") < 0)) {
				yTranslation = (Input.GetAxis("Mouse Y") * translationBuffer);
			}

			RotateCamera(xTranslation, yTranslation);
			transform.LookAt(golfBall.transform.position, transform.up);
		}

		// Check input for zooming in/out
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			ZoomIn(transform.position);
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			ZoomOut(transform.position);
		}

		// Tells us if the golf ball is moving and makes the camera follow it.
		if (golfBall.GetComponent<Rigidbody>().velocity.magnitude > 0) {
			isMoving = true;
			MoveCamera();
		} else {
			isMoving = false;
		}
	}

	private void Update() {
		offset = transform.position - golfBall.transform.position;
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
	private void ZoomIn(Vector3 initialPosition) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position = initialPosition + (displacement * zoomSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Moves the camera away from the golf ball gameobject.
	/// </summary>
	/// <param name="initialPosition"></param>
	/// <returns></returns>
	private void ZoomOut(Vector3 initialPosition) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position = initialPosition - (displacement * zoomSpeed * Time.deltaTime);
	}

	private void MoveCamera() {
		transform.position = golfBall.transform.position - offset;
	}
}