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
		StageContainerManager.OnGameOverEvent += OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent += OnFinishGameEvent;
		StageContainerManager.StageClearedEvent += StageClearedEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		StageContainerManager.OnGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
		StageContainerManager.StageClearedEvent -= StageClearedEvent;
	}

	void Start () {
		if(!debug){
			InstantiateContainer ("Container/TopContainer");
		}
	}

	void OnStartStoryEvent () {
		InstantiateContainer ("Container/StoryContainer");
	}

	void OnStoryFinishedEvent(){
		InstantiateContainer ("Container/StageInfoContainer");
	}

	void OnStartGameEvent(){
		InstantiateContainer ("Container/Stage1Container");
	}

	void OnGameOverEvent(){
		InstantiateContainer ("Container/GameOverContainer");
	}

	void OnFinishGameEvent(){
		InstantiateContainer ("Container/TopContainer");
	}

	void StageClearedEvent(int stageLevel){
		Debug.Log ("clear");
		loadingObject.SetActive (true);
		GameObject[] floorArray = GameObject.FindGameObjectsWithTag ("Floor");
		foreach(GameObject floor in floorArray){
			Destroy (floor);
		}
		Destroy (GameObject.FindGameObjectWithTag("MainCamera"));
		Destroy (GameObject.FindGameObjectWithTag("Player"));
	}

	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = uiRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
