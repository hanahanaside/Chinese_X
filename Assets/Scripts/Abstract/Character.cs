using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	[HideInInspector]
	public float life = 1.0f;
	private bool mFacingRight;

	public abstract void ApplyDamage ();

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
