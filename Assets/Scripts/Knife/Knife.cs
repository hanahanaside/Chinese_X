using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	public float moveForce;

	void Start () {
		Transform playerTransform = Player.instance.transform;
		if(transform.position.x < playerTransform.position.x){
			rigidbody2D.AddForce (Vector2.right * moveForce);
		}else {
			transform.localScale = new Vector3 (1,1,1);
			rigidbody2D.AddForce (Vector2.right * -moveForce);
		}
	}

	void OnTriggerEnter2D (Collider2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			Player.instance.ApplyDamage ();
			Destroy (gameObject);
		}
	}
}
