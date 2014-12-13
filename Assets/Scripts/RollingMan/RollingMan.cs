using UnityEngine;
using System.Collections;

public class RollingMan : Enemy {
	private Animator mAnimator;
	private Transform mPlayerTransform;
	private float mMoveSpeed;
	private BoxCollider2D mBoxCollider2D;

	void Start () {
		mAnimator = GetComponent<Animator> ();
		mAnimator.SetFloat ("Speed", moveForce);
		mPlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		mBoxCollider2D = GetComponent<BoxCollider2D> ();
		if (mPlayerTransform.position.x > transform.position.x) {
			mMoveSpeed = 0.5f;
		} else {
			mMoveSpeed = -0.5f;
		}
		CheckFlip (mMoveSpeed);
	}

	void FixedUpdate () {
		if (life <= 0) {
			mAnimator.SetTrigger ("Death");
			enabled = false;
			Destroy (gameObject, 0.8f);
			return;
		}
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash == Animator.StringToHash ("Base Layer.Idle")) {
			return;
		}

		if (info.nameHash == Animator.StringToHash ("Base Layer.Atack")) {
			mBoxCollider2D.size = new Vector2 (1, 0.8f);
			mBoxCollider2D.center = new Vector2 (-0.05f, -0.63f);
		}
			
		Move (mMoveSpeed);

		float distance = Vector3.Distance (mPlayerTransform.position, transform.position);
		if (distance < atackDistance) {
			mAnimator.SetFloat ("Speed", 0);
		}
	}

	void OnTriggerEnter2D (Collider2D collision) {
		AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo (0);
		if (info.nameHash != Animator.StringToHash ("Base Layer.Atack")) {
			return;
		}
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			Player.instance.ApplyDamage ();
		}
	}

	public override void ApplyDamage () {
		life -= 1.0f;
		LifeManager.instance.UpdateEnemyLife (life);
	}
}
