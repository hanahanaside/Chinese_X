﻿using UnityEngine;
using System.Collections;
using System;

public class StageContainerManager : MonoBehaviour {
	public static event Action OnGameOverEvent;
	public static event Action<int> StageClearedEvent;

	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	public GameObject floorPrefab;
	public GameObject backgroundPrefab;
	public GameObject enemyGeneratorPrefab;
	public int stageLevel;
	private Transform mPlayerTransform;

	void OnEnable () {
		Player.ClearedEvent += ClearedEvent;
	}

	void OnDisable () {
		Player.ClearedEvent -= ClearedEvent;
	}

	void Start () {
		Instantiate (playerPrefab);
		Instantiate (cameraPrefab);
		float[] floorX = { -10, 0, 10 };
		for (int i = 0; i < 3; i++) {
			Instantiate (floorPrefab, new Vector3 (floorX [i], 0, 0), Quaternion.identity);
		}
		Instantiate (backgroundPrefab);
		Instantiate (enemyGeneratorPrefab);
	}

	public  void OnAtackButtonClicked () {
		float v = Input.GetAxis ("Vertical");
		if (v < 0) {
			Player.instance.LowKick ();
		} else {
			Player.instance.HighKick ();
		}
	}

	public void OnJumpButtonClicked () {
		Player.instance.Jump ();
	}

	void ClearedEvent () {

		Destroy (transform.parent.gameObject);
		StageClearedEvent (stageLevel);
	}
}
