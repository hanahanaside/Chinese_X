using UnityEngine;
using System.Collections;

public class RollingMan : Enemy {

	private Animator mAnimator;
	private Transform mPlayerTransform;
	private GameObject mAtackObject;
	private float mH;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		mAtackObject = transform.Find ("Atack").gameObject;
		mAtackObject.SetActive (false);
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
			return;
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.Idle")) {
			return;
		}

		if (info.nameHash == Animator.StringToHash ("Base Layer.Atack")) {
			mAtackObject.SetActive (true);
			collider2D.enabled = false;
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
