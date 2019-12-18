using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGoalCollisions : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Pinball")) {
			Destroy(other.gameObject);
		}
	}
}