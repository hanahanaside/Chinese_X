using UnityEngine;
using System.Collections;

public class EnemyAtack1 : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			collision.gameObject.SendMessage ("ApplyDamage");
		}
	}
}
