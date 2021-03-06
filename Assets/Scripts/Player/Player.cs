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
		if (life <= 0) {
			enabled = false;
			mAnimator.SetTrigger ("Death");
			rigidbody2D.isKinematic = true;
			GameOverEvent ();
			SoundManager.instance.PlaySE (SoundManager.SECannel.Death_Player);
			return;
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Damage")){
			collider2D.enabled = false;
			return;
		}
		if(!collider2D.enabled){
			collider2D.enabled = true;
		}
		float h = 0;
		#if UNITY_EDITOR
		h = Input.GetAxis ("Horizontal");
		#else
		h = Delta.x;
		#endif
		mAnimator.SetFloat ("Speed", Mathf.Abs (h));


		if (info.nameHash == Animator.StringToHash ("Base Layer.Walk") || info.nameHash == Animator.StringToHash ("Base Layer.Jump")) {
			Move (h);
		}

		CheckFlip (h);

		// If the player should jump...
		if (mJump) {
			// Set the Jump animator trigger parameter.
			mAnimator.SetTrigger ("Jump");

			// Add a vertical force to the player.
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			mJump = false;
		}
	}

	void OnCollisionEnter2D (Collision2D collider) {
		string tag = collider.gameObject.tag;
		if (tag == "Step") {
			ClearedEvent ();
		}
	}

	public override void ApplyDamage () {
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Damage")){
			return;
		}
		if (life > 0) {
			float fromLife = life;
			life -= 0.1f;
			if(life > 0){
				mAnimator.SetTrigger ("Damage");
			}

			LifeManager.instance.UpdatePlayerLife (fromLife, life);
			SoundManager.instance.PlaySE (SoundManager.SECannel.Damage_Player);
		}
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
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Damage")){
			return;
		}

		if (!mJump && mGrounded) {
			mJump = true;
			mGrounded = false;
			SoundManager.instance.PlaySE (SoundManager.SECannel.Jump);
		}
	}

	public void Atack () {
		if (!enabled) {
			return;
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Damage")){
			return;
		}
		float v = 0;
		#if UNITY_EDITOR
		v = Input.GetAxis ("Vertical");
		#else
		v = Delta.y;
		#endif
		if (v >= 0) {
			HighKick ();
		} else {
			LowKick ();
		}
	}

	private void HighKick () {
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.HighKick") || info.nameHash == Animator.StringToHash ("Base Layer.LowKick")) {
			return;
		}
		mAnimator.SetTrigger ("HighKick");
		mHighKickObject.SetActive (true);
		SoundManager.instance.PlaySE (SoundManager.SECannel.HighKick);
	}

	private void LowKick () {
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.HighKick") || info.nameHash == Animator.StringToHash ("Base Layer.LowKick")) {
			return;
		}
		mAnimator.SetTrigger ("LowKick");
		mLowKickObject.SetActive (true);
		SoundManager.instance.PlaySE (SoundManager.SECannel.LowKick);
	}
}
