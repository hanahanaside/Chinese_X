using UnityEngine;
using System.Collections;
using System;

public class EnemyGenerator : MonoBehaviour { 
	public int generateCount;
	public Transform[] generatePosition;
	public GameObject[] enemyPrefabArray;
	public GameObject bossPrefab;
	public float minInterval;
	public float maxInterval;
	private float mInterval;
	private bool mStop;
	private Transform mCameraTransform;

	public static event Action CompleteGenerate;

	void Start () {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		transform.parent = camera.transform;
		mCameraTransform = camera.transform;
		mInterval = UnityEngine.Random.Range(minInterval,maxInterval);
	}
	// Update is called once per frame
	void Update () {
		mInterval -= Time.deltaTime;
		if (mInterval < 0) {
			GenerateEnemy ();
			mInterval = UnityEngine.Random.Range(minInterval,maxInterval);
		}
		if(bossPrefab == null){
			return;
		}
		if(mCameraTransform.position.x < -15){
			Transform generateTransform = generatePosition[0];
			GameObject enemyObject =  Instantiate (bossPrefab, generateTransform.position, Quaternion.identity) as GameObject;
			enemyObject.transform.parent = transform.parent.transform.parent;
			enabled = false;
		}
	}
		
	private void GenerateEnemy () {
		int rand = UnityEngine.Random.Range (0,enemyPrefabArray.Length);
		GameObject enemy = enemyPrefabArray[rand];
		rand = UnityEngine.Random.Range (0,2);
		Transform generateTransform = generatePosition[rand];
		GameObject enemyObject =  Instantiate (enemy, generateTransform.position, Quaternion.identity) as GameObject;
		enemyObject.transform.parent = transform.parent.transform.parent;
		generateCount--;
		if(generateCount <=0){
			enabled = false;
			CompleteGenerate ();
		}
	}
}
