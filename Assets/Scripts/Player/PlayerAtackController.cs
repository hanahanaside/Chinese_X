using UnityEngine;
using System.Collections;

public class PlayerAtackController : MonoSingleton<PlayerAtackController> {

	public BoxCollider2D highKickCollider;
	public BoxCollider2D lowKickCollider;
	private Animator mAnimator;

	void Start () {
		mAnimator = transform.parent.gameObject.GetComponent<Animator> ();
		HideCollider ();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Enemy"){
			Debug.Log ("hit");
			EnemyLifeManager manager = collision.gameObject.GetComponentInChildren<EnemyLifeManager>();
			manager.ApplyDamage ();
			HideCollider ();
		}
	}

	public void HighKick(){
		mAnimator.SetTrigger ("HighKick");
		highKickCollider.enabled = true;
		Invoke ("HideCollider",0.3f);
	}

	public void LowKick(){
		mAnimator.SetTrigger ("LowKick");
		lowKickCollider.enabled = true;
		Invoke ("HideCollider",0.3f);
	}

	private void HideCollider(){
		highKickCollider.enabled = false;
		lowKickCollider.enabled = false;
	}
}
