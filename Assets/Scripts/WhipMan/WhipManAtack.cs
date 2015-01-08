using UnityEngine;
using System.Collections;

public class WhipManAtack : MonoBehaviour {

	void OnEnable(){
		Invoke ("Hide",0.5f);
	}

	void OnTriggerEnter2D (Collider2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			Player.instance.ApplyDamage ();
		}
	}

	private void Hide(){
		gameObject.SetActive (false);
	}
}
