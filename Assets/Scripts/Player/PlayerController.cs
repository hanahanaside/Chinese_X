using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoSingleton<PlayerController> {
	public static event Action ClearedEvent;

	public float moveForce = 365f;
	// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;
	// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;
	// Amount of force added when the player jumps.
	private float mLife = 1.0f;
	private Animator mAnimator;
	private bool mFacingRight = false;
	private bool mJump = false;
	private bool mGrounded = false;
	private Transform mGroundCheckTransform;
	private GameObject mHighKickObject;
	private GameObject mLowKickObject;

	public override void OnInitialize() {
		mAnimator = GetComponent<Animator> ();
		mGroundCheckTransform = transform.Find ("GroundCheck");
		mHighKickObject = transform.Find ("HighKick").gameObject;
		mLowKickObject = transform.Find ("LowKick").gameObject;
	}

	void Update(){
		mGrounded = Physics2D.Linecast(transform.position, mGroundCheckTransform.position, 1 << LayerMask.NameToLayer("Ground"));  
	}
		
	void FixedUpdate () {
		LifeManager.instance.UpdatePlayerLife (mLife);
		if(mLife <=0){
			mAnimator.SetTrigger ("Death");
			enabled = false;
			Destroy (gameObject,0.7f);
		}

		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (1);
		if(info.nameHash == Animator.StringToHash("Atack Layer.High Kick")){
			mHighKickObject.SetActive (true);
			return;
		}else {
			mHighKickObject.SetActive (false);
		}

		if(info.nameHash == Animator.StringToHash("Atack Layer.Low Kick")){
			mLowKickObject.SetActive (true);
			return;
		}else {
			mLowKickObject.SetActive (false);
		}

		// Cache the horizontal input.
		float h = Input.GetAxis ("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		mAnimator.SetFloat ("Speed", Mathf.Abs (h));

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

		// If the player should jump...
		if (mJump) {
			// Set the Jump animator trigger parameter.
			mAnimator.SetTrigger ("Jump");

			// Add a vertical force to the player.
			rigidbody2D.AddForce (new Vector2 (0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			mJump = false;
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Step"){
			ClearedEvent ();
		}
	}

	void ApplyDamage(){
		mLife -= 0.1f;
	}
				
	public void Jump () {
		if (!mJump && mGrounded) {
			mJump = true;
			mGrounded = false;
		}
	}

	public void HighKick(){
		mAnimator.SetTrigger ("HighKick");
	}

	public void LowKick(){
		mAnimator.SetTrigger ("LowKick");
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
