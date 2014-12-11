using UnityEngine;
using System.Collections;

public class HighKick : MonoBehaviour {

	void OnEnable(){
		Invoke ("Hide",0.4f);
	}

	void OnTriggerEnter2D (Collider2D collision) {
		string tag = collision.gameObject.tag;
		Debug.Log ("hit");
		if(tag == "Enemy"){
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.ApplyDamage ();
			gameObject.SetActive (false);
		}
	}

	private void Hide(){
		gameObject.SetActive (false);
	}
}
