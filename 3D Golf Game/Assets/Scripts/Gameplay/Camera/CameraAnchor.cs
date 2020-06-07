using UnityEngine;

public class CameraAnchor : MonoBehaviour {
	private float minDistanceToGolfBall = 2.0f;
	private float maxDistanceToGolfBall = 12.0f;
	private GameObject golfBall = null;

	/// <summary>
	/// Finds the golf ball GameObject.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
	}

	void FixedUpdate() {
		transform.LookAt(golfBall.transform.position, transform.up);
	}

	private void ZoomIn(Vector3 zoomDirection) {
		if (DistanceToGolfBall() > minDistanceToGolfBall) {
			transform.position += zoomDirection;
		}
	}

	private void ZoomOut(Vector3 zoomDirection) {
		if (DistanceToGolfBall() < maxDistanceToGolfBall) {
			transform.position -= zoomDirection;
		}
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
