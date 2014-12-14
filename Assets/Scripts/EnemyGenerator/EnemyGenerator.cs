using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	public Transform[] generatePosition;
	public GameObject[] enemyPrefabArray;
	public float minInterval;
	public float maxInterval;
	private float mInterval;

	void Start () {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		transform.parent = camera.transform;
		mInterval = 5.0f;
	}
	// Update is called once per frame
	void Update () {
		mInterval -= Time.deltaTime;
		if (mInterval < 0) {
			GenerateEnemy ();
			mInterval = Random.Range(minInterval,maxInterval);
		}
	}

	private void GenerateEnemy () {
		int rand = Random.Range (0,enemyPrefabArray.Length);
		GameObject enemy = enemyPrefabArray[rand];
		rand = Random.Range (0,2);
		Transform generateTransform = generatePosition[rand];
		GameObject enemyObject =  Instantiate (enemy, generateTransform.position, Quaternion.identity) as GameObject;
		enemyObject.transform.parent = transform.parent.transform.parent;
	}
}
