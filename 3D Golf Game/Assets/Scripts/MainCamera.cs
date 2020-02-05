using UnityEngine;

public class MainCamera : MonoBehaviour {
	[SerializeField]
	private GameObject golfBall = null;

	private float translationBuffer = 10;
	private float xTranslation = 0;
	private float yTranslation = 0;

	//private Vector3 offset;

	//private void Start() {
	//	offset = transform.position - golfBall.transform.position;
	//}

	private void Update() {
		//transform.position = offset + golfBall.transform.position;

		if ((Input.GetAxis("Mouse X") > 0) || (Input.GetAxis("Mouse X") < 0)) {
			xTranslation = (Input.GetAxis("Mouse X") * translationBuffer);
			transform.RotateAround(golfBall.transform.position, Vector3.up, xTranslation);
		}

		if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y") < 0)) {
			yTranslation = (Input.GetAxis("Mouse Y") * translationBuffer);
			transform.RotateAround(golfBall.transform.position, transform.right, -yTranslation);
		}

		transform.LookAt(golfBall.transform, transform.up);
		Mathf.Clamp(transform.rotation.x, 0, 70);
	}
}