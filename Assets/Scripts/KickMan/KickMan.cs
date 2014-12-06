﻿using UnityEngine;
using System.Collections;

public class KickMan :  Enemy {

	private Animator mAnimator;
	private GameObject mAtackObject;
	private Transform mPlayerTransform;
	private float mH;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAtackObject = transform.Find ("Atack").gameObject;
		mAtackObject.SetActive (false);
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		if (mPlayerTransform.position.x > transform.position.x) {
			mH = 0.5f;
		} else {
			mH = -0.5f;
		}
		CheckFlip (mH);
	}

	void FixedUpdate () {
		if(life <=0){
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
			

		Move (mH);



		float distance = Vector3.Distance (mPlayerTransform.position,transform.position);
		if(distance < atackDistance){
			mAnimator.SetFloat ("Speed", 0);
		}
	}

	public override void ApplyDamage () {
		life -= 1.0f;
		LifeManager.instance.UpdateEnemyLife (life);
	}

}
