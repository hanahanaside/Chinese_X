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
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		Destroy (transform.parent.gameObject);
	}

	public void OnRankingButtonClicked(){
		LobiUtil.Instance.showRanking ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}

	public void OnItemButtonClicked(){
		GameObject containerPrefab = Resources.Load ("Container/ShopContainer") as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = transform.parent.transform.parent;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		Destroy (transform.parent.gameObject);
	}

	public void OnRecommendButtonClicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		ConnectErrorDialog.instance.Show ();
	}
}
