using UnityEngine;
using System.Collections;

public class LowKick : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Enemy"){

		}
	}
}
