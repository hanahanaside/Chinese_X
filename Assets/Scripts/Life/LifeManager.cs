using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public UISlider playerLife;
	public UISlider enemyLife;
	private static LifeManager sInstance;
	private float mEnemyToLife = 1.0f;
	private float mPlayerToLife = 1.0f;

	public static LifeManager instance{
		get{
			return sInstance;
		}
	}

	void Awake(){
		sInstance = this;
	}

	void Update(){
		if(enemyLife.value > mEnemyToLife){
			enemyLife.value -= 0.04f;
		}
		if(playerLife.value > mPlayerToLife){
			playerLife.value -= 0.02f;
		}
	}
		
	public void UpdatePlayerLife(float fromLife,float toLife){
		playerLife.value = fromLife;
		mPlayerToLife = toLife;
	}

	public void UpdateEnemyLife(float fromLife,float toLife){
		enemyLife.value = fromLife;
		mEnemyToLife = toLife;
	}
}
