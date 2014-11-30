using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoSingleton<EnemyGenerator> {
	public GameObject enemyPrefab;
	private float mInterval;

	void Start () {
		mInterval = 3.0f;
	}
	// Update is called once per frame
	void Update () {
		mInterval -= Time.deltaTime;
		if(mInterval < 0){
			GenerateEnemy ();
			mInterval = 3.0f;
		}
	}

	private void GenerateEnemy(){
		GameObject enemyObject = Instantiate (enemyPrefab) as GameObject;
		enemyObject.transform.parent = transform.parent;
		enemyObject.transform.localScale = new Vector3 (1, 1, 1);
		enemyObject.transform.localPosition = new Vector3 (-1000,0,0);
	}
}
