﻿using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	public GameObject enemyPrefab;
	private float mInterval;

	void Start () {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		transform.parent = camera.transform;
		mInterval = 3.0f;
		GenerateEnemy ();
	}
	// Update is called once per frame
	void Update () {
		mInterval -= Time.deltaTime;
		if(mInterval < 0){

			mInterval = 3.0f;
		}
	}

	private void GenerateEnemy(){
		Instantiate (enemyPrefab);
	}
}
