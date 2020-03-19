using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField]
	private Material defaultMaterial = null;

	private float hitForce = 2.0f; // The force applied to move the golf ball.
	private float power = 2.0f;
	private float startTime = 0.0f; // Timestamp of when the player starts inputting to move the golf ball.
	private float endTime = 0.0f; // Timestamp of when the player stops inputting to move the golf ball.
	private float minHitForce = 2.0f;
	private float maxHitForce = 40.0f;
	private float minimumSpeed = 1.0f;

	private bool moving = false;

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
		if (rigidBody.velocity.magnitude == 0.0f || (rigidBody.velocity.magnitude < minimumSpeed)) {
			rigidBody.velocity = Vector3.zero;
			moving = false;
			GetComponent<MeshRenderer>().material = defaultMaterial;
		} else {
			moving = true;
			GetComponent<MeshRenderer>().material = null;
		}

		if (!moving) {
			if (Input.GetKeyDown(KeyCode.M)) {
				startTime = Time.time;
			}

			if (Input.GetKeyUp(KeyCode.M)) {
				direction = Camera.main.transform.forward;
				endTime = Time.time;
				hitForce *= (endTime - startTime); // Calculates a float 
				// relative to the length of time the player was inputting to move the golf ball.
				hitForce = Mathf.Clamp(hitForce * 10, minHitForce, maxHitForce);
				PuttBall();
				hitForce = power;
			}
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
