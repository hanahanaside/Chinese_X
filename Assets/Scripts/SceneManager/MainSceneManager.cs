using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoSingleton<MainSceneManager> {
	public bool debug;
	private GameObject mUIRoot;
	private int mStageLevel = 0;
	private int mGameLevel = 1;

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
			SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Opening);
		}
	}

	void OnStartStoryEvent () {
		InstantiateContainer ("Container/StoryContainer");
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Story);
	}

	void OnStoryFinishedEvent () {
		InstantiateContainer ("Container/StageInfoContainer");
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.StageInfo);
	}

	void OnStartGameEvent () {
		InstantiateStageManager (mStageLevel);
	}

	void OnGameOverEvent () {
		Debug.Log ("game over");
		SoundManager.instance.StopSE (SoundManager.SECannel.Go);
		InstantiateContainer ("Container/GameOverContainer");
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Gameover);
	}

	void OnFinishGameEvent () {
		InstantiateContainer ("Container/TopContainer");
		ScoreKeeper.instance.score = 0;
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Opening);
	}

	void ContinueEvent () {
		InstantiateContainer ("Container/StageInfoContainer");
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.StageInfo);
	}

	void StageClearedEvent () {
		Debug.Log ("clear");
		SoundManager.instance.StopSE (SoundManager.SECannel.Go);
		AdManager.instance.ShowInterstitialAd ();
		if (mStageLevel >= 3) {
			mGameLevel++;
		}
		mStageLevel++;
		InstantiateContainer ("Container/StageInfoContainer");
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.StageInfo);
	}

	void CloseShopEvent () {
		if (mStageLevel == 0) {
			InstantiateContainer ("Container/TopContainer");
		} else {
			InstantiateContainer ("Container/GameOverContainer");
		}
	}

	public int GameLevel {
		get {
			return mGameLevel;
		}
	}

	public int StageLevel {
		get {
			return mStageLevel;
		}
	}

	private void InstantiateContainer (string path) {
		GameObject containerPrefab = Resources.Load (path) as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = mUIRoot.transform;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	private void InstantiateStageManager (int level) {
		GameObject stageManagerPrefab =	Resources.Load ("StageManager/StageManager" + level) as GameObject;
		Instantiate (stageManagerPrefab);
		switch (level) {
		case 1:
			SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Stage_1);
			break;
		case 2:
			SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Stage_2);
			break;
		case 3:
			SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Stage_3);
			break;
		}
	}
}
