using UnityEngine;
using System.Collections;

public class LifeManager : MonoSingleton<LifeManager> {

	public UISlider playerLife;
	public UISlider enemyLife;

	public void UpdatePlayerLife(float life){
		playerLife.value = life;
	}

	public void UpdateEnemyLife(float life){
		enemyLife.value = life;
	}
}
