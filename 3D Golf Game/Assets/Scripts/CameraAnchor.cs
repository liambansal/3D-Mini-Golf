using UnityEngine;

public class CameraAnchor : MonoBehaviour {
	private GameObject golfBall = null;

	/// <summary>
	/// Finds the golfBall object and sets the offset position 
	/// between the camera anchor and golfBall.
	/// </summary>
	private void Start() {
		golfBall = GameObject.Find("Golf Ball");
	}

	void Update() {
		transform.LookAt(golfBall.transform.position, transform.up);
	}

	/// <summary>
	/// Moves the cameraAnchor towards the golfBall.
	/// </summary>
	/// <param name="initialPosition"></param>
	private void ZoomIn(float zoomSpeed) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position += (displacement * zoomSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Moves the cameraAnchor away from the golfBall.
	/// </summary>
	/// <param name="initialPosition"></param>
	private void ZoomOut(float zoomSpeed) {
		Vector3 vec1 = (transform.position);
		Vector3 vec2 = (golfBall.transform.position);
		Vector3 displacement = vec1 -= vec2;
		transform.position -= (displacement * zoomSpeed * Time.deltaTime);
	}

	private void RotateAlongXAxis(float xTranslation) {
		transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
	}
}
