using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles touch input to putt the golf ball and update the power bar.
/// </summary>
public class Controller : MonoBehaviour {
	/// <summary>
	/// The maximum number of finger touches allowed to putt the ball.
	/// </summary>
	private const int maximumPuttTouches = 1;

	private const float defaultPuttForce = 1.0f;
	private const float minimumPuttForce = 1.0f;
	private const float maximumPuttForce = 80.0f;
	private const float minimumSpeed = 0.15f;
	/// <summary>
	/// Speeds up the time for holding a finger down.
	/// </summary>
	private const float timeMultiplier = 22.0f;
	/// <summary>
	/// The y position a golf ball must pass for it to be reset to it's last position.
	/// </summary>
	private const float outOfBounds = -2.0f;
	private float puttForce = 0.0f;
	/// <summary>
	/// Length of time the player holds their finger down on the screen.
	/// </summary>
	private float holdLength = 0.0f;

	/// <summary>
	/// Is the player holding down a finger on the screen.
	/// </summary>
	private bool holding = false;

	private Vector3 moveDirection = new Vector3();
	/// <summary>
	/// The position of the golf ball before it was hit.
	/// </summary>
	private Vector3 lastPosition = new Vector3();
	private Color stationaryColour = Color.white;
	private Color movingColour = Color.gray;

	private Rigidbody golfBallRigidBody = null;
	private HUD hud = null;
	private Slider powerBar = null;
	private GameObject mainCamera = null;
	private GameObject cameraAnchor = null;
	private MeshRenderer golfBallMeshRenderer = null;

	/// <summary>
	/// Assigns some unassigned variables.
	/// </summary>
	private void Awake() {
		hud = FindObjectOfType<HUD>();
		const string powerBarTag = "PowerBar";
		powerBar = GameObject.FindGameObjectWithTag(powerBarTag).GetComponent<Slider>();
		const string mainCameraTag = "MainCamera";
		mainCamera = GameObject.FindGameObjectWithTag(mainCameraTag);
		const string CameraAnchorTag = "CameraAnchor";
		cameraAnchor = GameObject.FindGameObjectWithTag(CameraAnchorTag);
		golfBallRigidBody = GetComponent<Rigidbody>();
		golfBallMeshRenderer = GetComponent<MeshRenderer>();
		lastPosition = transform.position;
	}

	/// <summary>
	/// Handles input to putt the golf ball, setting it's material colour and updating the power bar.
	/// </summary>
	private void Update() {
		if (golfBallRigidBody.velocity.magnitude < minimumSpeed) {
			golfBallMeshRenderer.material.color = stationaryColour;

			if (Input.touchCount == maximumPuttTouches) {
				Touch touch = Input.touches[0];

				switch (touch.phase) {
					case TouchPhase.Began: {
						// Makes sure the player double taps before putting.
						if (touch.tapCount >= 2) {
							holding = true;
							holdLength = 0.0f;
						}

						break;
					}
					case TouchPhase.Ended: {
						if (holding) {
							holding = false;
							moveDirection = Camera.main.transform.forward;
							// Exaggerates a float that is relative to the length of time the
							// player was holding a finger on the screen and clamps the value.
							puttForce = Mathf.Clamp(holdLength * timeMultiplier,
								minimumPuttForce,
								maximumPuttForce);
							lastPosition = golfBallRigidBody.position;
							PuttGolfBall();
							hud.IncreasePuttCounter();
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

				if (holding) {
					holdLength += Time.deltaTime;
					powerBar.value = holdLength * timeMultiplier;
				}
			} else {
				holding = false;
				holdLength = 0.0f;
			}
		} else {
			golfBallMeshRenderer.material.color = movingColour;
		}
	}

	/// <summary>
	/// Stops the golf ball's movement when necessary.
	/// </summary>
	private void FixedUpdate() {
		if (transform.position.y <= outOfBounds) {
			// Reset the golf ball's position to its last valid position.
			golfBallRigidBody.position = lastPosition;
			golfBallRigidBody.velocity = Vector3.zero;
			golfBallRigidBody.rotation = Quaternion.identity;
			mainCamera.SendMessage("MoveCamera");
			cameraAnchor.SendMessage("MoveAnchor");
		}

		if (golfBallRigidBody.velocity.magnitude < minimumSpeed) {
			// Stops the golf ball's movement after reaching it's minimum speed.
			golfBallRigidBody.velocity = Vector3.zero;
			golfBallRigidBody.rotation = Quaternion.identity;
		}
	}

	private void PuttGolfBall() {
		golfBallRigidBody.AddForce(moveDirection * puttForce, ForceMode.Impulse);
	}
}