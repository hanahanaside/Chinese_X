using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {

	public bool debug;
	private GameObject mUIRoot;
	private int mStageLevel = 1;

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
		mUIRoot = GameObject.FindGameObjectWithTag ("UIRoot");
		if (!debug) {
			InstantiateContainer ("Container/TopContainer");
		}
	}

	void OnStartStoryEvent () {
		InstantiateContainer ("Container/StoryContainer");
	}

	void OnStoryFinishedEvent () {
		InstantiateContainer ("Container/StageInfoContainer_1");
	}

	void OnStartGameEvent () {
		InstantiateStageManager (mStageLevel);
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
		InstantiateContainer ("Container/GameOverContainer");
	}

	void OnFinishGameEvent () {
		InstantiateContainer ("Container/TopContainer");
	}

	void ContinueEvent(){
		InstantiateContainer ("Container/StageInfoContainer_" + mStageLevel);
	}

	void StageClearedEvent () {
		Debug.Log ("clear");
		AdManager.instance.ShowInterstitialAd ();
		if (mStageLevel >= 3) {
			mStageLevel = 1;
		} else {
			mStageLevel++;
		}
		Debug.Log ("lovel " + mStageLevel);
		InstantiateContainer ("Container/StageInfoContainer_" + mStageLevel);
	}
		
	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = mUIRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	private void InstantiateStageManager(int level){
		GameObject stageManagerPrefab =	Resources.Load ("StageManager/StageManager" + level) as GameObject;
		Instantiate (stageManagerPrefab);
	}
}
