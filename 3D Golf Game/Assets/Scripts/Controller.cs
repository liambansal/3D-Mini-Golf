using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField]
	private Material mat = null;

	private float hitForce = 2, 
		power = 2, 
		startTime = 0, 
		endTime = 0, 
		minHitForce = 2, 
		maxHitForce = 40;

	private bool moving = false;

	private Rigidbody rigidBody = null;

	private Vector3 direction; // The direction in which the ball will travel.


	private void Awake() {
		rigidBody = GetComponent<Rigidbody>(); // Get the golf ball's rigidbody
	}

    private void Update() {
		if (rigidBody.velocity.magnitude == 0.0f || (rigidBody.velocity.magnitude < 0.5f)) {
			rigidBody.velocity = Vector3.zero;
			moving = false;
			GetComponent<MeshRenderer>().material = mat;
		} else {
			moving = true;
			GetComponent<MeshRenderer>().material = null;
		}

		if (!moving) {
			if (Input.GetKeyDown(KeyCode.M)) {
				startTime = Time.time; // Timestamp for when the player started perssing spacebar.
			}

			if (Input.GetKeyUp(KeyCode.M)) {
				direction = Camera.main.transform.forward;
				endTime = Time.time;// Timestamp for when the player stopped pressing spacebar.
				hitForce *= (endTime - startTime);// Calculates a float relative to the length of time the spacebar was held down.
				hitForce = Mathf.Clamp(hitForce * power, minHitForce, maxHitForce);
				PuttBall();
				hitForce = power;
			}
		}
	}

	private void PuttBall() {
		Debug.Log(hitForce);
		rigidBody.AddForce((direction * hitForce), ForceMode.Impulse);
	}
}