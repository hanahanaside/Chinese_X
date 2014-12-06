using UnityEngine;
using System.Collections;

public class RollingManAtack : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			Player.instance.ApplyDamage ();
		}
	}
}
