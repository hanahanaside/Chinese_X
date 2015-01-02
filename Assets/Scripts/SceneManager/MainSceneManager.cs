using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {

	public bool debug;
	private GameObject mUIRoot;
	private int mStageLevel = 0;

	void OnEnable () {
		TopContainerManager.OnStartButtonClickedEvent += OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent += OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent += OnStartGameEvent;
		StageManager.StageGameOverEvent += OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent += OnFinishGameEvent;
		StageManager.StageClearedEvent += StageClearedEvent;
		GameOverContainerManager.ContinueEvent += ContinueEvent;
		ShopContainerManager.CloseShopEvent += CloseShopEvent;
	}

	void OnDisable () {
		TopContainerManager.OnStartButtonClickedEvent -= OnStartStoryEvent;
		StoryContainerManager.OnStoryFinishedEvent -= OnStoryFinishedEvent;
		StageInfoContainerManager.OnStartButtonClickedEvent -= OnStartGameEvent;
		StageManager.StageGameOverEvent -= OnGameOverEvent;
		GameOverContainerManager.OnFinishGameEvent -= OnFinishGameEvent;
		StageManager.StageClearedEvent -= StageClearedEvent;
		GameOverContainerManager.ContinueEvent -= ContinueEvent;
		ShopContainerManager.CloseShopEvent -= CloseShopEvent;
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
		mStageLevel = 1;
		InstantiateContainer ("Container/StageInfoContainer_" + mStageLevel);
	}

	void OnStartGameEvent () {
		InstantiateStageManager (mStageLevel);
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
		InstantiateContainer ("Container/GameOverContainer");
	}

	void OnFinishGameEvent () {
		LobiUtil.Instance.sendScore (gameObject,LobiUtil.Instance.RANKING_IDS[0],ScoreKeeper.instance.score);
		InstantiateContainer ("Container/TopContainer");
		ScoreKeeper.instance.score = 0;
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

	void CloseShopEvent(){
		if(mStageLevel == 0){
			InstantiateContainer ("Container/TopContainer");
		}else {
			InstantiateContainer ("Container/GameOverContainer");
		}
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
