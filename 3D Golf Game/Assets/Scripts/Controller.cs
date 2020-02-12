using UnityEngine;

public class Controller : MonoBehaviour {
	private float hitForce = 10,
		startTime,
		endTime,
		minHitForce = 0,
		maxHitForce = 100;

	private Rigidbody rigidBody = null;

	private void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

    private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			startTime = Time.time;
		}

		if (Input.GetKey(KeyCode.Space)) {
			++hitForce;
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			endTime = Time.time;
			hitForce /= (endTime - startTime);
			Mathf.Clamp(hitForce, minHitForce, maxHitForce);
			PuttBall();
		}
	}

	private void PuttBall() {
		rigidBody.AddForce((Vector3.forward * hitForce), ForceMode.Impulse); // TODO: Make ball travel in facing direction of camera
	}
}