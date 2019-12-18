using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
	[SerializeField]
	private string paddlePrefix = "";

	private Rigidbody paddleRigidbody = null;

	private float forceMultiplier = 30;

	private void Start() {
		paddleRigidbody = GetComponent<Rigidbody>();
	}

	void Update() {
        if (Input.GetAxis(paddlePrefix + "Vertical") > 0) {
			paddleRigidbody.AddForce((Vector2.up * forceMultiplier), ForceMode.Impulse);
		}
    }
}