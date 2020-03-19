using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField]
	private Material defaultMaterial = null;

	private float hitForce = 2.0f; // The force applied to move the golf ball.
	private float power = 2.0f;
	private float startTime = 0.0f; // Timestamp of when the player starts inputting to move the golf ball.
	private float endTime = 0.0f; // Timestamp of when the player stops inputting to move the golf ball.
	private float minHitForce = 2.0f;
	private float maxHitForce = 60.0f;
	private float minimumSpeed = 1.0f;

	private bool pressed = false;

	private Vector3 direction = new Vector3(); // The direction in which the golf ball will move.

	private Rigidbody rigidBody = null; // The golf ball's rigidbody.

	private HUD hud = null;

	/// <summary>
	/// Gets the rigidbody of this gameObject.
	/// </summary>
	private void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Finds the HUD object.
	/// </summary>
	private void Start() {
		hud = FindObjectOfType<HUD>();
	}

	private void Update() {
		if (rigidBody.velocity.magnitude < minimumSpeed) {
			rigidBody.velocity = Vector3.zero;
		}

		if (rigidBody.velocity == Vector3.zero) {
			GetComponent<MeshRenderer>().material = defaultMaterial;

			if (Input.GetKeyDown(KeyCode.Space)) {
				startTime = Time.time;
				pressed = true;
			}

			if (pressed && Input.GetKeyUp(KeyCode.Space)) {
				pressed = false;
				direction = Camera.main.transform.forward;
				endTime = Time.time;
				hitForce *= (endTime - startTime); // Calculates a float 
												   // relative to the length of time the player was inputting to move the golf ball.
				hitForce = Mathf.Clamp(hitForce * 10, minHitForce, maxHitForce);
				PuttBall();
				hitForce = power;
			}
		} else {
			GetComponent<MeshRenderer>().material = null;
		}
	}

	/// <summary>
	/// Adds an instant force impulse to the rigidbody of the golf ball which 
	/// causes it to move along the main camera's forward direction and makes 
	/// a call to update the putt count text.
	/// </summary>
	private void PuttBall() {
		rigidBody.AddForce((direction * hitForce), ForceMode.Impulse);
		hud.UpdatePuttCounter(1);
	}
}
