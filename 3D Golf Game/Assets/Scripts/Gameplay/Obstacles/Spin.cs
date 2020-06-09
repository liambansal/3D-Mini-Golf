using UnityEngine;

/// <summary>
/// Used to rotate a transform each frame.
/// </summary>
public class Spin : MonoBehaviour {
	[SerializeField]
	private Vector3 torque = new Vector3(0.0f, 0.0f, 0.0f);
	
	/// <summary>
	/// Rotates the gameObject's transform.
	/// </summary>
	private void FixedUpdate() {
		transform.Rotate(torque);
	}
}