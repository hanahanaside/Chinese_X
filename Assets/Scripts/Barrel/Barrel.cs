using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	private Transform mPlayerTransform;
	private float mMoveSpeed;

	void Start () {
		mPlayerTransform = Player.instance.transform;
		if(transform.position.x > mPlayerTransform.position.x){
			moveForce = -moveForce;
		}
	}

	void Update(){
		rigidbody2D.AddForce (Vector2.right *  moveForce);
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

	}

	void OnCollisionEnter2D (Collision2D collision) {
		string tag = collision.gameObject.tag;
		if(tag == "Player"){
			Player.instance.ApplyDamage ();
			Destroy (gameObject);
		}
	}
}
