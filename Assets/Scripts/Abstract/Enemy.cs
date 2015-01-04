using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {

	public float atackDistance;
	public int score;
	public float hp;

	void Awake(){
		hp = hp * MainSceneManager.instance.GameLevel;
		score = score * MainSceneManager.instance.GameLevel;
	}

	void Update(){
		if(transform.position.y < -10){
			Destroy (gameObject);
		}
	}

	public override void ApplyDamage () {
		float fromLife = life;
		life -= 1f / hp;
		LifeManager.instance.UpdateEnemyLife (fromLife,life);
	}

}