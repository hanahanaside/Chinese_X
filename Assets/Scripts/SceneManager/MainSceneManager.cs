﻿using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {
	public GameObject uiRoot;
	public GameObject loadingObject;
	public bool debug;

	void OnEnable () {
		TopContainerManager.OnStartButtonClickedEvent += OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent += OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent += OnStartGameEvent;
		StageContainerManager.StageGameOverEvent += OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent += OnFinishGameEvent;
		StageContainerManager.StageClearedEvent += StageClearedEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		StageContainerManager.StageGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
		StageContainerManager.StageClearedEvent -= StageClearedEvent;
	}

	void Start () {
		if (!debug) {
			InstantiateContainer ("Container/TopContainer");
		}
	}

	void OnStartStoryEvent () {
		InstantiateContainer ("Container/StoryContainer");
	}

	void OnStoryFinishedEvent () {
		InstantiateContainer ("Container/StageInfoContainer");
	}

	void OnStartGameEvent () {
		InstantiateContainer ("Container/Stage1Container");
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
		//	InstantiateContainer ("Container/GameOverContainer");
	}

	void OnFinishGameEvent () {
		InstantiateContainer ("Container/TopContainer");
	}

	void StageClearedEvent (int stageLevel) {
		Debug.Log ("clear");
		loadingObject.SetActive (true);
		GameObject[] floorArray = GameObject.FindGameObjectsWithTag ("Floor");
		foreach (GameObject floor in floorArray) {
			Destroy (floor);
		}
		GameObject[] enemyArray = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject enemy in enemyArray){
			Destroy (enemy);
		}
		Destroy (GameObject.FindGameObjectWithTag ("MainCamera"));
		Destroy (GameObject.FindGameObjectWithTag ("Player"));
		if(stageLevel >= 3){
			stageLevel = 1;
		}else {
			stageLevel++;
		}
		InstantiateContainer ("Container/Stage" + stageLevel + "Container");
		loadingObject.SetActive (false);
	}

	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = uiRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
