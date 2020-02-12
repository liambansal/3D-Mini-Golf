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

	private void Update() {
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

		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			transform.position = ZoomIn(transform.position);
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			transform.position = ZoomOut(transform.position);
		}

		if (golfBall.GetComponent<Rigidbody>().velocity.magnitude > 0) {
			isMoving = true;
			MoveCamera(transform.position);
		} else {
			isMoving = false;
		}
	}

	private void RotateCamera(float xAxis, float yAxis) {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
		transform.RotateAround(golfBall.transform.position, transform.right, -yTranslation);
	}

	private Vector3 ZoomIn(Vector3 initialPosition) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		Vector3 newPosition = initialPosition + (displacement * zoomSpeed * Time.deltaTime);
		return newPosition;
	}

	private Vector3 ZoomOut(Vector3 initialPosition) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		Vector3 newPosition = initialPosition - (displacement * zoomSpeed * Time.deltaTime);
		return newPosition;
	}
	private Vector3 MoveCamera(Vector3 initialPosition) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		Vector3 newPosition = initialPosition - (displacement * zoomSpeed * Time.deltaTime);
		return newPosition;
	}
}