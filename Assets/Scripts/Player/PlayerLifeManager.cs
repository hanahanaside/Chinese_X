using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoSingleton<PlayerLifeManager> {
	private UISlider mHealthSlider;
	private Animator mAnimator;
	private float mHealth = 100;
	// Use this for initialization
	void Start () {
		mAnimator = transform.parent.gameObject.GetComponent<Animator> ();
		mHealthSlider = GameObject.Find ("PlayerLife").gameObject.GetComponent<UISlider> ();
	}

	public void ApplyDamage () {
		mHealth -= 10;
		mHealthSlider.value = mHealth / 100.0f;
		if (mHealth <= 0) {
			Death ();
		}
	}

	private void Death () {
		mAnimator.SetTrigger ("Death");
	}
}
