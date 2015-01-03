using UnityEngine;
using System.Collections;
using System;

public class Boss : Enemy {

	public static event Action bossGenerated;
	public static event Action bossDestroyed;

	public GameObject barrelPrefab;
	public float maxAtackInterval;
	private Animator mAnimator;
	private GameObject mKickObject;
	private Transform mThrowPosition;
	private Transform mPlayerTransform;
	private float mMoveSpeed = 0.5f;
	private float mAtackInterval;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mKickObject = transform.Find ("Kick").gameObject;
		mThrowPosition = transform.Find ("Throw");
		mKickObject.SetActive (false);
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = Player.instance.transform;
		mAtackInterval = 2.0f;
		bossGenerated();
	}

	void FixedUpdate () {
		if (life <= 0) {
			CheckFlip (mMoveSpeed);
			Move (-mMoveSpeed * 10);
			mAnimator.SetTrigger ("Death");
			enabled = false;
			ScoreKeeper.instance.AddScore (score);
			bossDestroyed ();
			Destroy (gameObject, 2.1f);
		}

		mAtackInterval -= Time.deltaTime;

		if (mAtackInterval < 0) {
			Atack ();
		}
			
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.Kick")) {
			mKickObject.SetActive (true);
		} else {
			mKickObject.SetActive (false);
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

	private void Atack(){
		float distance = Vector3.Distance (mPlayerTransform.position,transform.position);
		if(distance >= 3){
			mAnimator.SetTrigger ("Throw");
			Invoke ("ThrowBarrel",0.3f);
		}else {
			mAnimator.SetTrigger ("Kick");
		}

		mAtackInterval = 2.0f;
	}

	private void ThrowBarrel(){

		GameObject barrelObject =  Instantiate (barrelPrefab) as GameObject;
		barrelObject.transform.parent = mThrowPosition;

		barrelObject.transform.localPosition= new Vector3 (0,0,0);
		barrelObject.transform.localScale = new Vector3 (1f,1f,1f);
	}
}
