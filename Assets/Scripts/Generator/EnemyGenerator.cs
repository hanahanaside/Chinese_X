using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	public Transform[] generatePosition;
	public GameObject[] enemyPrefab;
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
			mInterval = 3.0f;
		}
	}

	private void GenerateEnemy () {
		int rand = Random.Range (0,1);
		rand = 1;
		GameObject enemy = enemyPrefab[rand];
		rand = Random.Range (0,2);
		Transform generateTransform = generatePosition[rand];
		Instantiate (enemy, generateTransform.position, Quaternion.identity);
	}
}
