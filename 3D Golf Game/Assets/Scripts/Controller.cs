using UnityEngine;

public class Controller : MonoBehaviour {
	private int forceMultiplier = 40;

	private float hitForce = 0;
	private float minHitForce = 0;
	private float maxHitForce = 500;

	private Rigidbody rigidBody = null;

	private void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Start() {
		Mathf.Clamp(hitForce, minHitForce, maxHitForce);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
			hitForce = (forceMultiplier * (40 * Time.deltaTime));
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			PuttBall();
		}
	}

	private void PuttBall() {
		rigidBody.AddForce((Vector3.forward * hitForce), ForceMode.Impulse);
	}
}