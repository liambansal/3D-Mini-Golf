using UnityEngine;

/// <summary>
/// Controls when to use the explosive power up and on what object(s).
/// </summary>
public class PowerUpController : MonoBehaviour {
	private const string boxTag = "Box";
	private ExplosivePowerUp explosivePowerUp = null;

	/// <summary>
	/// Stores a reference to the power-up script.
	/// </summary>
	/// <param name="powerUp"> The power script to get a reference to. </param>
	public void EnablePowerUp(ExplosivePowerUp powerUp) {
		explosivePowerUp = powerUp;
	}

	/// <summary>
	/// Uses and disables the explosive power-up upon colliding with a box.
	/// </summary>
	/// <param name="collision"> Data about the collision between the golf ball and colliding game object. </param>
	private void OnCollisionEnter(Collision collision) {
		if (explosivePowerUp && collision.gameObject.CompareTag(boxTag)) {
			// Check if the box is part of a collection of boxes.
			if (collision.gameObject.transform.parent) {
				foreach (Rigidbody boxRigidbody in collision.gameObject.transform.parent.GetComponentsInChildren<Rigidbody>()) {
					explosivePowerUp.UsePowerUp(boxRigidbody);
				}
			} else {
				explosivePowerUp.UsePowerUp(collision.gameObject.GetComponent<Rigidbody>());
			}

			Destroy(explosivePowerUp.gameObject);
			explosivePowerUp = null;
		}
	}
}