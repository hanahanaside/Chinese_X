using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {

	public float atackDistance;
	public int score;
	public float hp;

	void Update(){
		if(transform.position.y < -10){
			Destroy (gameObject);
		}
	}

	public override void ApplyDamage () {
		life -= 1f / hp;
		LifeManager.instance.UpdateEnemyLife (life);
	}

}