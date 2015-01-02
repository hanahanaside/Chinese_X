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

	public void OnItemButtonClicked(){
		GameObject containerPrefab = Resources.Load ("Container/ShopContainer") as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = transform.parent.transform.parent;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		Destroy (transform.parent.gameObject);
	}

	public void OnRecommendButtonClicked(){

	}
}
