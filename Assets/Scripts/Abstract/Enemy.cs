using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {

	public float atackDistance;

	void Update(){
		if(transform.position.y < -10){
			Destroy (gameObject);
		}
	}

}