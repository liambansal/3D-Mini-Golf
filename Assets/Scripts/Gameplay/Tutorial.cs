using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the text display for the tutorial scene.
/// </summary>
public class Tutorial : MonoBehaviour {
	/// <summary>
	/// Text telling the player what to do.
	/// </summary>
	[SerializeField]
	private Text[] stepText = new Text[3];

	/// <summary>
	/// Touches needed for rotating the camera.
	/// </summary>
	private const int rotateTouchCount = 1;
	/// <summary>
	/// Touches needed for zooming the camera.
	/// </summary>
	private const int zoomTouchCount = 2;
	private int currentStepIndex = 0;
	private const string golfBallTag = "GolfBall";

	private GameObject golfBall = null;
	private enum TutorialSteps {
		RotateCamera,
		ZoomCamera,
		PuttBall
	}
	TutorialSteps currentStep = TutorialSteps.RotateCamera;

	/// <summary>
	/// Finds and disables the golf ball's "Controller" script component.
	/// </summary>
	private void Awake() {
		golfBall = GameObject.FindGameObjectWithTag(golfBallTag);
		golfBall.GetComponent<Controller>().enabled = false;
	}

	/// <summary>
	/// Steps through each tutorial step as they're completed.
	/// </summary>
	private void Update() {
		switch (currentStep) {
			case TutorialSteps.RotateCamera: {
				stepText[currentStepIndex].gameObject.SetActive(true);

				if (Input.touchCount == rotateTouchCount) {
					Touch touch = Input.touches[0];

					switch (touch.phase) {
						case TouchPhase.Moved: {
							// At this point in the "MainCamera" script the camera will rotate,
							// so complete this step.
							stepText[currentStepIndex].gameObject.SetActive(false);
							++currentStepIndex;
							++currentStep;
							break;
						}
						default: {
							break;
						}
					}
				}

				break;
			}
			case TutorialSteps.ZoomCamera: {
				stepText[currentStepIndex].gameObject.SetActive(true);

				if (Input.touchCount == zoomTouchCount) {
					bool fingersMoved = false;

					for (int i = 0; i < zoomTouchCount; ++i) {
						Touch touch = Input.touches[i];

						switch (touch.phase) {
							case TouchPhase.Moved: {
								// At this point in the "MainCamera" script the camera will zoom,
								// so complete this step.
								fingersMoved = true;
								break;
							}
							default: {
								break;
							}
						}
					}

					if (fingersMoved) {
						stepText[currentStepIndex].gameObject.SetActive(false);
						++currentStepIndex;
						++currentStep;
					}
				}

				break;
			}
			case TutorialSteps.PuttBall: {
				stepText[currentStepIndex].gameObject.SetActive(true);
				// Enabling will allow the player to control the golf ball.
				golfBall.GetComponent<Controller>().enabled = true;
				break;
			}
			default: {
				break;
			}
		}
	}

	/// <summary>
	/// Loads the main menu upon colliding with a golf ball.
	/// </summary>
	/// <param name="other"> The collider that entered this gameObject's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
		}
	}
}