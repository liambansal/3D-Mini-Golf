using UnityEngine;

/// <summary>
/// Handles moving a firefly between two positions.
/// </summary>
public class Firefly : MonoBehaviour {
	/// <summary>
	/// Length of time before the firefly changes move direction.
	/// </summary>
	private const float moveTime = 1.0f;
	private const float moveSpeedModifier = 5.0f;
	private float minimumYHeight = 0.0f;
	private float maxamimumYHeight = 0.0f;
	/// <summary>
	/// The time spent moving in the current direction.
	/// </summary>
	private float timeMoving = 0.0f;
	/// <summary>
	/// Time before the firefly starts moving.
	/// </summary>
	private float sleepTime = 0.0f;

	/// <summary>
	/// Gets a random float for when the firefly will start moving.
	/// </summary>
	private void Awake() {
		const float minimumRandomTime = 0.0f;
		const float maximumRandomTime = 6.0f;
		sleepTime = Random.Range(minimumRandomTime, maximumRandomTime);
	}

	/// <summary>
	/// Sets the firefly's minimum and maximum y position.
	/// </summary>
	private void Start() {
		minimumYHeight = transform.position.y;
		const float height = 4.0f;
		maxamimumYHeight = transform.position.y + height;
	}

	/// <summary>
	/// Moves the firefly's transform between two points.
	/// </summary>
	private void FixedUpdate() {
		// Check if the firefly should start moving.
		if (sleepTime < 0.0f) {
			transform.position = new Vector3(transform.position.x,
				Mathf.Lerp(minimumYHeight,
					maxamimumYHeight,
					timeMoving),
				transform.position.z);
			timeMoving += Time.deltaTime / moveSpeedModifier;

			// Check if the minimum and maximum height positions should be swapped to change the
			// move direction.
			if (timeMoving > moveTime) {
				float temporaryHeight = minimumYHeight;
				minimumYHeight = maxamimumYHeight;
				maxamimumYHeight = temporaryHeight;
				timeMoving = 0.0f;
			}
		} else {
			sleepTime -= Time.deltaTime;
		}
	}
}