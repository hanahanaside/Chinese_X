using UnityEngine;
using System.Collections;
using System;

public class GameOverContainerManager : MonoBehaviour {
	public static event Action OnFinishGameEvent;
	public static event Action ContinueEvent;

	public GameObject newLabelObject;
	public UILabel scoreLabel;
	public UILabel bestScoreLabel;
	public UILabel ticketLabel;
	// Use this for initialization
	void Start () {
		AdManager.instance.ShowBannerAd ();
		scoreLabel.text = "" + ScoreKeeper.instance.score;
		int bestScore = PrefsManager.instance.BestScore;
		if (ScoreKeeper.instance.score > bestScore) {
			bestScore = ScoreKeeper.instance.score;
			PrefsManager.instance.BestScore = bestScore;
			newLabelObject.SetActive (true);
			LobiUtil.Instance.sendScore (gameObject, LobiUtil.Instance.RANKING_IDS [0], ScoreKeeper.instance.score);
		}
		bestScoreLabel.text = "" + bestScore;
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
	}

	public void OnNoButtonClicked () {
		OnFinishGameEvent ();
		Destroy (transform.parent.gameObject);
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}

	public void OnYesButtonClicked () {
		if (PrefsManager.instance.TicketCount <= 0) {
			SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
			Destroy (transform.parent.gameObject);
			GameObject containerPrefab = Resources.Load ("Container/ShopContainer") as GameObject;
			GameObject containerObject = Instantiate (containerPrefab) as GameObject;
			containerObject.transform.parent = transform.parent.transform.parent;
			containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			return;
		}
		PrefsManager.instance.TicketCount = PrefsManager.instance.TicketCount - 1;
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
		AdManager.instance.HideBannerAd ();
		Destroy (transform.parent.gameObject);
		ContinueEvent ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}
}
