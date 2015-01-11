using UnityEngine;
using System.Collections;

public class KnifeMan : Enemy {
	public GameObject knifePrefab;
	private Animator mAnimator;
	private Transform mPlayerTransform;
	private Transform mThrowPointTransform;
	private float mMoveSpeed = 0.5f;
	private bool mAtacking = false;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = Player.instance.transform;
		mThrowPointTransform = transform.Find ("ThrowPoint");
	}

	void FixedUpdate () {
		if (life <= 0) {
			CheckFlip (mMoveSpeed);
			Move (-mMoveSpeed * 10);
			mAnimator.SetTrigger ("Death");
			enabled = false;
			ScoreKeeper.instance.AddScore (score);
			Destroy (gameObject, 0.8f);
		}

		if (mPlayerTransform.position.x > transform.position.x) {
			mMoveSpeed = 0.5f;
		} else {
			mMoveSpeed = -0.5f;
		}

		CheckFlip (mMoveSpeed);


		float distance = Vector3.Distance (mPlayerTransform.position, transform.position);
		if (distance <= atackDistance) {
			if(!mAtacking){
				mAtacking = true;
				mAnimator.SetFloat ("Speed", 0);
				Invoke ("SetAtack",1.5f);
			}
		}else {
			mAtacking = false;
			CancelInvoke ();
		}

		if(mAtacking){
			return;
		}

		mAnimator.SetFloat ("Speed", moveForce);

		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);

		if (info.nameHash == Animator.StringToHash ("Base Layer.Walk")) {
			Move (mMoveSpeed);
		}

	}

	private void SetAtack(){
		if(!mAtacking){
			return;
		}
		mAnimator.SetTrigger ("Atack");
		Invoke ("ThrowKnife", 0.3f);
	}

	private void ThrowKnife () {
		if (!mAtacking) {
			return;
		}
		if(enabled){
			Instantiate (knifePrefab, mThrowPointTransform.position, Quaternion.identity);
		}
	}
}
