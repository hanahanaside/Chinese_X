using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public UISlider playerLife;
	public UISlider enemyLife;
	private static LifeManager sInstance;

	public static LifeManager instance{
		get{
			return sInstance;
		}
	}

	void Awake(){
		sInstance = this;
	}
		
	public void UpdatePlayerLife(float life){
		playerLife.value = life;
	}

	public void UpdateEnemyLife(float life){
		enemyLife.value = life;
	}
}
