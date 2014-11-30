using UnityEngine;
using System.Collections;

public class EnemyLifeManager : MonoBehaviour {

	private UISlider mHealthSlider;
	private float mHealth = 100;

	// Use this for initialization
	void Start () {
		mHealthSlider = GameObject.Find ("EnemyLife").gameObject.GetComponent<UISlider> ();
	}
	
	public void ApplyDamage(){
		Debug.Log ("damage");
		mHealth -= 100;
		mHealthSlider.value = mHealth / 100.0f;
		if(mHealth <= 0){
			Destroy (transform.parent.gameObject);
		}
	}
}
