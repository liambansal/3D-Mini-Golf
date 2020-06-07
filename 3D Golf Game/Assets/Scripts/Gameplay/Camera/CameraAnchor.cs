using UnityEngine;

public class CameraAnchor : MonoBehaviour {
	private float minDistanceToGolfBall = 2.0f;
	private float maxDistanceToGolfBall = 12.0f;

	private bool canZoom = true;

	private GameObject golfBall = null;

	/// <summary>
	/// Finds the golf ball GameObject.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
	}

	void FixedUpdate() {
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
	/// Returns a Vector3 for moving the camera anchor towards or away from the 
	/// golf ball.
	/// </summary>
	/// <returns> The route as a Vector3 from the camera anchor to the golf ball. </returns>
	private Vector3 Zoom() {
		const float zoomSpeed = 8.0f;
		Vector3 vec1 = transform.position;
		Vector3 vec2 = golfBall.transform.position;
		Vector3 displacement = vec1 -= vec2;
		return displacement * -zoomSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Rotates the camera anchor around the golf ball's global y axis.
	/// </summary>
	private void RotateAroundGolfBall(float xTranslation) {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
	}

	private float DistanceToGolfBall() {
		return Vector3.Distance(transform.position, golfBall.transform.position);
	}
}
