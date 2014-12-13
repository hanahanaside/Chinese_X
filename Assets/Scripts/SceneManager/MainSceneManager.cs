using UnityEngine;
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
		GameOverContainerManager.ContinueEvent += ContinueEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		StageContainerManager.StageGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
		StageContainerManager.StageClearedEvent -= StageClearedEvent;
		GameOverContainerManager.ContinueEvent -= ContinueEvent;
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
		InstantiateContainer ("Container/StageContainer1");
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
		DestroyObjects ();
		InstantiateContainer ("Container/GameOverContainer");
	}

	void OnFinishGameEvent () {
		InstantiateContainer ("Container/TopContainer");
	}

	void ContinueEvent(){
		InstantiateContainer ("Container/StageInfoContainer");
	}

	void StageClearedEvent (int stageLevel) {
		Debug.Log ("clear");
		loadingObject.SetActive (true);
		DestroyObjects ();
		if (stageLevel >= 3) {
			stageLevel = 1;
		} else {
			stageLevel++;
		}
		InstantiateContainer ("Container/StageContainer" + stageLevel);
		loadingObject.SetActive (false);
	}

	private void DestroyObjects () {
		GameObject[] floorArray = GameObject.FindGameObjectsWithTag ("Floor");
		foreach (GameObject floor in floorArray) {
			Destroy (floor);
		}
		GameObject[] enemyArray = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemyArray) {
			Destroy (enemy);
		}
		Destroy (GameObject.FindGameObjectWithTag ("MainCamera"));
		Destroy (GameObject.FindGameObjectWithTag ("Player"));
	}

	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = uiRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
