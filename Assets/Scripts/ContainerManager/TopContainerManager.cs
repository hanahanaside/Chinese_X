using UnityEngine;
using System.Collections;
using System;

public class TopContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;

	void Start(){
		AdManager.instance.ShowBannerAd ();
		AdManager.instance.ShowIconAd ();
	}

	public void OnStartButtonClicked(){
		AdManager.instance.HideBannerAd ();
		AdManager.instance.HideIconAd ();
		OnStartButtonClickedEvent ();
		Destroy (transform.parent.gameObject);
	}

	public void OnRankingButtonClicked(){
		LobiUtil.Instance.showRanking ();
	}
}
