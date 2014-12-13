using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	private UISlider playerLife;
	private UISlider enemyLife;
	private static LifeManager sInstance;

	public static LifeManager instance{
		get{
			return sInstance;
		}
	}

	void Awake(){
		sInstance = this;
	}

	void Start(){
		playerLife = transform.Find ("PlayerLife").GetComponent<UISlider>();
		enemyLife = transform.Find ("EnemyLife").GetComponent<UISlider>();
	}

	public void UpdatePlayerLife(float life){
		playerLife.value = life;
	}

	public void UpdateEnemyLife(float life){
		enemyLife.value = life;
	}
}
