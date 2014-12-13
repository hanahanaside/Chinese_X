using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {
	public GameObject uiRoot;
	public bool debug;

	void OnEnable () {
		TopContainerManager.OnStartButtonClickedEvent += OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent += OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent += OnStartGameEvent;
		StageManager.StageGameOverEvent += OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent += OnFinishGameEvent;
		StageManager.StageClearedEvent += StageClearedEvent;
		GameOverContainerManager.ContinueEvent += ContinueEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		StageManager.StageGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
		StageManager.StageClearedEvent -= StageClearedEvent;
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
		InstantiateStageManager (1);
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
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
		if (stageLevel >= 3) {
			stageLevel = 1;
		} else {
			stageLevel++;
		}
		InstantiateStageManager (stageLevel);
	}
		
	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = uiRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	private void InstantiateStageManager(int level){
		GameObject stageManagerPrefab =	Resources.Load ("StageManager/StageManager" + level) as GameObject;
		Instantiate (stageManagerPrefab);
	}
}
