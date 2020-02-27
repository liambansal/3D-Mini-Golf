using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField]
	private Material defaultMaterial = null;

	private float hitForce = 2; // Used in applying a force to move the ball.
	private float power = 2; // Used to multiply hitForce's value, to exaggerate the result.
	private float startTime = 0;
	private float endTime = 0;
	private float minHitForce = 2;
	private float maxHitForce = 40;

	private bool moving = false;

	private Vector3 direction = new Vector3(); // The direction in which the ball will travel.

	private Rigidbody rigidBody = null; // The rigidbody of this gameObject.

	/// <summary>
	/// Gets the rigidbody of this gameObject.
	/// </summary>
	private void Awake() {
		rigidBody = GetComponent<Rigidbody>(); // Get the golf ball's rigidbody
	}

    private void Update() {
		if (rigidBody.velocity.magnitude == 0.0f || (rigidBody.velocity.magnitude < 0.5f)) {
			rigidBody.velocity = Vector3.zero;
			moving = false;
			GetComponent<MeshRenderer>().material = defaultMaterial;
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

	/// <summary>
	/// Adds an instant force impulse to the rigidbody of this gameObject 
	/// that causes it to travel in the main camera's forward direction.
	/// </summary>
	private void PuttBall() {
		rigidBody.AddForce((direction * hitForce), ForceMode.Impulse);
	}
}
