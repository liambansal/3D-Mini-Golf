using UnityEngine;

/// <summary>
/// Defines the use of the explosive power up and when it should be enabled.
/// </summary>
public class ExplosivePowerUp : MonoBehaviour {
	public float ExplosionForce { get; private set; } = 100.0f;
	public float ExplosionRadius { get; private set;} = 10.0f;
	/// <summary>
	/// Affects how high along the y axis the rigidbody will travel.
	/// </summary>
	public float ExplosionLift { get; private set;} = 5.0f;
	/// <summary>
	/// How the explosion force is applied to a rigidbody.
	/// </summary>
	public ForceMode ExplosionForceMode	{ get; private set;} = ForceMode.Impulse;

	private const float minimumMoveRange = -3.0f;
	private const float maximumMoveRange = 3.0f;
	private const string golfBallTag = "GolfBall";
	/// <summary>
	/// Moves the explosion position from within the affected rigidbody.
	/// </summary>
	private Vector3 explosionOffset = new Vector3();

	/// <summary>
	/// Enables the explosive power-up for the golf ball when colliding with it.
	/// </summary>
	/// <param name="other"> The collider that entered the explosive power-up's trigger collider. </param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(golfBallTag)) {
			other.GetComponent<PowerUpController>().EnablePowerUp(this);
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Applies an explosive force to a rigidbody.
	/// </summary>
	/// <param name="affectedRigidbody"> The rigidbody affected by the explosion. </param>
	public void UsePowerUp(Rigidbody affectedRigidbody) {
		explosionOffset = new Vector3(Random.Range(minimumMoveRange,
			maximumMoveRange),
		0.0f,
		Random.Range(minimumMoveRange,
			maximumMoveRange));

		affectedRigidbody.AddExplosionForce(ExplosionForce,
			affectedRigidbody.position - explosionOffset,
			ExplosionRadius,
			ExplosionLift,
			ExplosionForceMode);
	}
}