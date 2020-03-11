using UnityEngine;

public class CameraAnchor : MonoBehaviour {
	private float minDistanceToGolfBall = 2.0f;
	private float maxDistanceToGolfBall = 12.0f;

	private bool canZoom = true;

	private GameObject golfBall = null;

	/// <summary>
	/// Finds golfBall in the scene.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
	}

	void FixedUpdate() {
		// Makes the gameObject's forward direction face the golfBall.
		transform.LookAt(golfBall.transform.position, transform.up);

		if (Input.GetMouseButton(0)) {
			canZoom = false;
		} else {
			canZoom = true;
		}

		// Check input for zooming in/out.
		if (canZoom) {
			if (Input.GetAxis("Mouse ScrollWheel") > 0) {
				if (DistanceToGolfBall() > minDistanceToGolfBall) {
					transform.position += Zoom();
				}
			} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
				if (DistanceToGolfBall() < maxDistanceToGolfBall) {
					transform.position -= Zoom();
				}
			}
		}
	}

	/// <summary>
	/// Returns a Vector3 for moving the cameraAnchor towards or away from the 
	/// golfBall.
	/// </summary>
	private Vector3 Zoom() {
		float zoomSpeed = 8.0f;
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		return displacement * -zoomSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Rotates the cameraAnchor around the golfBall's global y axis.
	/// </summary>
	/// <param name="xTranslation"></param>
	private void RotateAroundGolfBall(float xTranslation) {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
	}

	private float DistanceToGolfBall() {
		return Vector3.Distance(transform.position, golfBall.transform.position);
	}
}
