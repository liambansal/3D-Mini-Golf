using UnityEngine;

public class SpinCross : MonoBehaviour {
	Vector3 torque = new Vector3(0.0f, -1.0f, 0.0f);
	Rigidbody crossRigidbody = null;

	/// <summary>
	/// Gets a reference to the object's rigidbody component.
	/// </summary>
	private void Awake() {
		crossRigidbody = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Rotates the object's rigidbody around it's y axis.
	/// </summary>
	private void Update() {
		transform.Rotate(torque);
	}
}