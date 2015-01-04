using UnityEngine;
using System.Collections;

public class HighKick : MonoBehaviour {

	void OnEnable(){
		Invoke ("Hide",0.3f);
	}

	void OnTriggerEnter2D (Collider2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Enemy"){
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.ApplyDamage ();
			Hide ();
		}
	}

	private void Hide(){
		gameObject.SetActive (false);
	}
}
