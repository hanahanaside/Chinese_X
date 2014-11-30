using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	private UISlider mHealthSlider;
	private float mHealth = 100;
	// Use this for initialization
	void Start () {
		mHealthSlider = GameObject.Find ("PlayerHealth").gameObject.GetComponent<UISlider> ();
	}
	// Update is called once per frame
	void Update () {
		mHealthSlider.value = mHealth / 100.0f;
	}

	void OnCollisionEnter2D (Collision2D collision) {

	}
}
