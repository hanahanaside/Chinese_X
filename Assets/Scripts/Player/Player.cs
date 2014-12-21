﻿using UnityEngine;
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

	public Vector2 Delta {
		get;
		set;
	}

	void Start () {
		sInstance = this;
		life = 1.0f;
		mAnimator = GetComponent<Animator> ();
		mGroundCheckTransform = transform.Find ("GroundCheck");
		mHighKickObject = transform.Find ("HighKick").gameObject;
		mLowKickObject = transform.Find ("LowKick").gameObject;
		mHighKickObject.SetActive (false);
		mLowKickObject.SetActive (false);
	}

	void Update () {
		mGrounded = Physics2D.Linecast (transform.position, mGroundCheckTransform.position, 1 << LayerMask.NameToLayer ("Ground"));  
	}

	void FixedUpdate () {
		LifeManager.instance.UpdatePlayerLife (life);
		if (life <= 0) {
			mAnimator.SetTrigger ("Death");
			enabled = false;
			rigidbody2D.isKinematic = true;
			GameOverEvent ();
			return;
		}

		#if UNITY_EDITOR
		float h = Input.GetAxis ("Horizontal");
		mAnimator.SetFloat ("Speed", Mathf.Abs (h));

		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.Walk")) {
			Move (h);
		}

		CheckFlip (h);

		#else

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		mAnimator.SetFloat ("Speed", Mathf.Abs (Delta.x));
	
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.Walk")) {
			Move (Delta.x);
		}

		CheckFlip (Delta.x);

		#endif

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

	void OnTriggerEnter2D (Collider2D collider) {
		string tag = collider.gameObject.tag;
		if (tag == "Step") {
			ClearedEvent ();
		}
	}

	public override void ApplyDamage () {
		life -= 0.1f;
	}

	public static Player instance {
		get {
			return sInstance;
		}
	}

	public void Jump () {
		if (!enabled) {
			return;
		}
		if (!mJump && mGrounded) {
			mJump = true;
			mGrounded = false;
		}
	}

	public void Atack () {
		if (!enabled) {
			return;
		}
		if (Delta.y >= 0) {
			HighKick ();
		} else {
			LowKick ();
		}
	}

	private void HighKick () {
		mAnimator.SetTrigger ("HighKick");
		mHighKickObject.SetActive (true);
	}

	private void LowKick () {
		mAnimator.SetTrigger ("LowKick");
		mLowKickObject.SetActive (true);
	}
}
