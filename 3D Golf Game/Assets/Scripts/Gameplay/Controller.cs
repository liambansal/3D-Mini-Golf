using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField]
	private Material defaultMaterial = null;

	/// <summary>
	/// The maximum number of finger touches allowed to putt the ball.
	/// </summary>
	private const int maximumPuttTouches = 1;

	private float defaultHitForce = 0.5f;
	/// <summary>
	/// The force applied to move the golf ball.
	/// </summary>
	private float hitForce = 0.5f;
	private float forceMultiplier = 40.0f;
	/// <summary>
	/// Timestamp of when the player starts inputting to move the golf ball.
	/// </summary>
	private float startTime = 0.0f;
	/// <summary>
	/// Timestamp of when the player stops inputting to move the golf ball.
	/// </summary>
	private float endTime = 0.0f;
	private float minHitForce = 0.5f;
	private float maxHitForce = 80.0f;
	private float minimumSpeed = 0.1f;

	private float outOfBounds = -3.0f;

	private bool holding = false;

	/// <summary>
	/// The direction in which the golf ball will move.
	/// </summary>
	private Vector3 direction = new Vector3();
	/// <summary>
	/// The position of the golf ball before it was hit.
	/// </summary>
	private Vector3 lastPosition = new Vector3();

	/// <summary>
	/// The golf ball's rigidbody.
	/// </summary>
	private Rigidbody golfballRigidBody = null;
	private HUD hud = null;

	/// <summary>
	/// Initializes some variables.
	/// </summary>
	private void Awake() {
		hud = FindObjectOfType<HUD>();
		golfballRigidBody = GetComponent<Rigidbody>();
		lastPosition = transform.position;
	}

	private void Update() {
		if (transform.position.y <= outOfBounds) {
			golfballRigidBody.position = lastPosition;
			golfballRigidBody.velocity = Vector3.zero;
		}

		if (golfballRigidBody.velocity.magnitude < minimumSpeed) {
			golfballRigidBody.velocity = Vector3.zero;
		}

		if (golfballRigidBody.velocity == Vector3.zero) {
			GetComponent<MeshRenderer>().material = defaultMaterial;

			if (Input.touchCount == maximumPuttTouches) {
				Touch touch = Input.touches[0];

				switch (touch.phase) {
					case TouchPhase.Began: {
						startTime = Time.time;

						if (touch.tapCount >= 2) {
							holding = true;
						}

						break;
					}
					case TouchPhase.Ended: {
						if (holding) {
							holding = false;
							direction = Camera.main.transform.forward;
							endTime = Time.time;
							// Calculates a float relative to the length of time the player 
							// was inputting to move the golf ball.
							hitForce *= (endTime - startTime);
							hitForce = Mathf.Clamp(hitForce * forceMultiplier, minHitForce, maxHitForce);
							PuttBall();
							hitForce = defaultHitForce;
						}

						break;
					}
					case TouchPhase.Canceled: {
						holding = false;
						break;
					}
					default: {
						break;
					}
				}
			} else {
				holding = false; 
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
		lastPosition = golfballRigidBody.position;
		golfballRigidBody.AddForce((direction * hitForce), ForceMode.Impulse);
		hud.UpdatePuttCounter(1);
	}
}