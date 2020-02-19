using UnityEngine;

public class Controller : MonoBehaviour {
	private float hitForce = 2, 
		power = 4, 
		startTime, 
		endTime, 
		minHitForce = 0, 
		maxHitForce = 20;

	private Rigidbody rigidBody = null;

	private Vector3 direction; // The direction in which the ball will travel.

	private void Awake() {
		rigidBody = GetComponent<Rigidbody>(); // Get the golf ball's rigidbody
	}

    private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			startTime = Time.time; // Timestamp for when the player started perssing spacebar.
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			direction = Camera.main.transform.forward;
			endTime = Time.time;// Timestamp for when the player stopped pressing spacebar.
			hitForce *= (endTime - startTime);// Calculates a float relative to the length of time the spacebar was held down.
			hitForce = Mathf.Clamp(hitForce * power, minHitForce, maxHitForce);
			PuttBall();
		}
	}

	private void PuttBall() {
		rigidBody.AddForce((direction * hitForce), ForceMode.Impulse); // TODO: Make ball travel in facing direction of camera
	}
}