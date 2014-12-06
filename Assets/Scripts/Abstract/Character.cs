using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	[HideInInspector]
	public float life = 1.0f;
	private bool mFacingRight;

	public abstract void ApplyDamage ();

	public void Move(float h){
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce (Vector2.right * h * moveForce);
		}

		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
	}

	public void CheckFlip(float h){
		if (h > 0 && !mFacingRight) {
			// ... flip the player.
			Flip ();
		}

		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && mFacingRight) {
			// ... flip the player.
			Flip ();
		}
	}

	private void Flip () {
		// Switch the way the player is labelled as facing.
		mFacingRight = !mFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
