using UnityEngine;
using System.Collections;
using System;

public class StageManager : MonoBehaviour {
	public static event Action StageGameOverEvent;
	public static event Action StageClearedEvent;

	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	public GameObject floorPrefab;
	public GameObject groundPrefab;
	public GameObject backgroundPrefab;
	public GameObject enemyGeneratorPrefab;
	public GameObject stageContainerPrefab;
	private GameObject mStageContainerObject;
	private GameObject mGoLabelObject;
	private bool boss;
	private bool mCompleteGenerate;

	void OnEnable () {
		Player.ClearedEvent += ClearedEvent;
		Player.GameOverEvent += GameoverEvent;
		Boss.bossGenerated += BossGenerated;
		Boss.bossDestroyed += BossDestroyed;
		TimeKeeper.TimeUpEvent += TimeUpEvent;
		EnemyGenerator.CompleteGenerate += CompleteGenerateEvent;
	}

	void OnDisable () {
		Player.ClearedEvent -= ClearedEvent;
		Player.GameOverEvent -= GameoverEvent;
		Boss.bossGenerated -= BossGenerated;
		Boss.bossDestroyed -= BossDestroyed;
		TimeKeeper.TimeUpEvent -= TimeUpEvent;
		EnemyGenerator.CompleteGenerate -= CompleteGenerateEvent;
	}

	void Start () {
		mStageContainerObject = Instantiate (stageContainerPrefab)as GameObject;
		mGoLabelObject = mStageContainerObject.transform.Find ("GoLabel").gameObject;
		GameObject uiRootObject = GameObject.FindGameObjectWithTag ("UIRoot");
		mStageContainerObject.transform.parent = uiRootObject.transform;
		mStageContainerObject.transform.localScale = new Vector3 (1,1,1);
		GameObject cameraObject = Instantiate (cameraPrefab) as GameObject;
		cameraObject.transform.parent = transform;
		GameObject groundObject =  Instantiate (groundPrefab) as GameObject;
		groundObject.transform.parent = gameObject.transform;
		float[] floorX = { -10, 0, 10 };
		for (int i = 0; i < 3; i++) {
			GameObject floorObject = Instantiate (floorPrefab, new Vector3 (floorX [i], 0, 0), Quaternion.identity) as GameObject;
			floorObject.transform.parent = transform;
		}
		GameObject backGroundObject = Instantiate (backgroundPrefab) as GameObject;
		backGroundObject.transform.parent = cameraObject.transform;
		GameObject enemyGeneratorObject = Instantiate (enemyGeneratorPrefab) as GameObject;
		enemyGeneratorObject.transform.parent = cameraObject.transform;
		GameObject playerObject = Instantiate (playerPrefab) as GameObject;
		playerObject.transform.parent = transform;
		cameraObject.GetComponent<CameraFollow> ().SetPlayerTransform (playerObject.transform);
	}

	void BossGenerated(){
		boss = true;
		SoundManager.instance.PlayBGM (SoundManager.BGMChannel.Boss);
		mGoLabelObject.SetActive (false);
		SoundManager.instance.StopSE (SoundManager.SECannel.Go);
	}

	void BossDestroyed(){
		boss = false;
	}

	void TimeUpEvent () {
		GameOver ();
	}
		
	void ClearedEvent () {
		if(!mCompleteGenerate){
			return;
		}
		if (boss) {
			return;
		}
		SoundManager.instance.PlaySE (SoundManager.SECannel.Step);
		Destroy (gameObject);
		Destroy (mStageContainerObject);
		StageClearedEvent ();
	}

	void GameoverEvent () {
		Invoke ("GameOver", 3.0f);
	}

	void CompleteGenerateEvent(){
		mCompleteGenerate = true;
		mGoLabelObject.SetActive (true);
		SoundManager.instance.PlaySE (SoundManager.SECannel.Go);
	}

	private void GameOver () {
		Destroy (gameObject);
		Destroy (mStageContainerObject);
		StageGameOverEvent ();
	}
}
