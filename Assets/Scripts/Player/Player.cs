using UnityEngine;
using System.Collections;
using System;

public class Player : Character {
	public static event Action ClearedEvent;
	public static event Action GameOverEvent;

	public float jumpForce = 1000f;
	// Amount of force added when the player jumps.
	private Animator mAnimator;
	private bool mJump = false;
	private bool mGrounded = false;
	private Transform mGroundCheckTransform;
	private GameObject mHighKickObject;
	private GameObject mLowKickObject;
	private static Player sInstance;

	void Start(){
		sInstance = this;
		mAnimator = GetComponent<Animator> ();
		mGroundCheckTransform = transform.Find ("GroundCheck");
		mHighKickObject = transform.Find ("HighKick").gameObject;
		mLowKickObject = transform.Find ("LowKick").gameObject;
	}

	void Update(){
		mGrounded = Physics2D.Linecast(transform.position, mGroundCheckTransform.position, 1 << LayerMask.NameToLayer("Ground"));  
	}
		
	void FixedUpdate () {
		LifeManager.instance.UpdatePlayerLife (life);
		if(life <=0){
			mAnimator.SetTrigger ("Death");
			enabled = false;
			rigidbody2D.isKinematic = true;
			GameOverEvent ();
			return;
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

		Move (h);

		CheckFlip (h);

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

	public override void ApplyDamage(){
		life -= 0.1f;
	}

	public static Player instance{
		get{
			return sInstance;
		}
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
		
}
