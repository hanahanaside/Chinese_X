using UnityEngine;
using System.Collections;

public class KickMan :  Enemy {

	private Animator mAnimator;
	private GameObject mAtackObject;
	private Transform mPlayerTransform;
	private float mMoveSpeed = 0.5f;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAtackObject = transform.Find ("Atack").gameObject;
		mAtackObject.SetActive (false);
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = Player.instance.transform;
	}

	void FixedUpdate () {
		if(life <=0){
			CheckFlip (mMoveSpeed);
			Move (-mMoveSpeed * 10);
			mAnimator.SetTrigger ("Death");
			enabled = false;
			ScoreKeeper.instance.AddScore (score);
			Destroy (gameObject,0.6f);
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if(info.nameHash == Animator.StringToHash ("Base Layer.Atack")){
			mAtackObject.SetActive (true);
		}else {
			mAtackObject.SetActive (false);
		}			

		if (mPlayerTransform.position.x > transform.position.x) {
			mMoveSpeed = 0.5f;
		} else {
			mMoveSpeed = -0.5f;
		}

		CheckFlip (mMoveSpeed);

		float distance = Vector3.Distance (mPlayerTransform.position,transform.position);
		if(distance <= atackDistance){
			mAnimator.SetFloat ("Speed", 0);
		}else {
			mAnimator.SetFloat ("Speed", moveForce);
		}

		if(info.nameHash == Animator.StringToHash("Base Layer.Walk")){
			Move (mMoveSpeed);
		}

	}
}
