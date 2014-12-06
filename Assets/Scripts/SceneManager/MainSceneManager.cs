using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {
	public GameObject uiRoot;
	public bool debug;

	void OnEnable () {
		TopContainerManager.OnStartButtonClickedEvent += OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent += OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent += OnStartGameEvent;
		Stage1ContainerManager.OnGameOverEvent += OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent += OnFinishGameEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		Stage1ContainerManager.OnGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
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

	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = uiRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
