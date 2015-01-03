using UnityEngine;
using System.Collections;
using System;

public class GameOverContainerManager : MonoBehaviour {

	public static event Action OnFinishGameEvent;
	public static event Action ContinueEvent;

	public GameObject newLabelObject;
	public UILabel scoreLabel;
	public UILabel bestScoreLabel;

	// Use this for initialization
	void Start () {
		AdManager.instance.ShowBannerAd ();
		scoreLabel.text = "" + ScoreKeeper.instance.score;
		int bestScore = PrefsManager.instance.BestScore;
		if(ScoreKeeper.instance.score > bestScore){
			bestScore = ScoreKeeper.instance.score;
			PrefsManager.instance.BestScore = bestScore;
			newLabelObject.SetActive (true);
			LobiUtil.Instance.sendScore (gameObject,LobiUtil.Instance.RANKING_IDS[0],ScoreKeeper.instance.score);
		}
		bestScoreLabel.text = "" + bestScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnNoButtonClicked(){
		OnFinishGameEvent ();
		Destroy (transform.parent.gameObject);
	}

	public void OnYesButtonClicked(){
		AdManager.instance.HideBannerAd ();
		Destroy (transform.parent.gameObject);
		ContinueEvent ();
	}
}
