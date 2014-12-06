using UnityEngine;
using System.Collections;

public class EnemyController1 : MonoBehaviour {
	public float moveForce = 1.0f;
	public float maxSpeed = 5f;
	public float atackDistance = 2.1f;
	private float mLife = 1.0f;
	private bool mFacingRight = false;
	private Animator mAnimator;
	private GameObject mAtackObject;
	private Transform mPlayerTransform;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAtackObject = transform.Find ("Atack").gameObject;
		mAtackObject.SetActive (false);
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void FixedUpdate () {
		if(mLife <=0){
			mAnimator.SetTrigger ("Death");
			enabled = false;
			Destroy (gameObject,0.8f);
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Atack")){
			mAtackObject.SetActive (true);
		}else {
			mAtackObject.SetActive (false);
		}
		if (info.nameHash != Animator.StringToHash ("Base Layer.Walk")) {
			return;
		}

		float h = 0;
		if (mPlayerTransform.position.x > transform.position.x) {
			h = 0.5f;
		} else {
			h = -0.5f;
		}

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce (Vector2.right * h * moveForce);
		}

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !mFacingRight) {
			// ... flip the player.
			Flip ();
		}

		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && mFacingRight) {
			// ... flip the player.
			Flip ();
		}

		float distance = Vector3.Distance (mPlayerTransform.position,transform.position);
		if(distance < atackDistance){
			mAnimator.SetFloat ("Speed", 0);
		}
	}

	void ApplyDamage () {
		mLife -= 1.0f;
		LifeManager.instance.UpdateEnemyLife (mLife);
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
