using UnityEngine;
using System.Collections;

public class HighKick : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Enemy"){
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.ApplyDamage ();
		}
	}
}
