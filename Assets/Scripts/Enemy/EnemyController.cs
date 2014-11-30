using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float moveForce = 0.1f;
	// Amount of force added to move the player left and right.
	public float maxSpeed = 0.5f;

	private GameObject mPlayerObject;

	void Start () {
		mPlayerObject = GameObject.FindGameObjectWithTag ("Player");

	}

	void FixedUpdate () {
		if (0.1f * rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce (Vector2.right * 0.1f * moveForce);
		}
	}
}
