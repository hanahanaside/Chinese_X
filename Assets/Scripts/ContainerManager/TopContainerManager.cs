using UnityEngine;
using System.Collections;
using System;
using MiniJSON;

public class TopContainerManager : MonoBehaviour {
	public static event Action OnStartButtonClickedEvent;
	public GameObject recommendButtonObject;

	void Start () {
		if (Application.systemLanguage != SystemLanguage.Japanese) {
			recommendButtonObject.SetActive (false);
		}
		AdManager.instance.ShowBannerAd ();
		#if UNITY_IPHONE
		string url = "https://dl.dropboxusercontent.com/u/66223745/App/Sparutan/environment.json";
		WWWClient wwwClient = new WWWClient (this, url);
		wwwClient.OnSuccess = (WWW response) => {
			string json = response.text;
			IDictionary parentObject = (IDictionary)Json.Deserialize (json);
			IDictionary childObject = (IDictionary)parentObject ["environments"];
			string status = childObject ["1"].ToString();
			Debug.Log("status " + status);
			if (status == "production") {
				AdManager.instance.ShowIconAd ();
			}
		};
		wwwClient.Request ();
		#else 
		AdManager.instance.ShowIconAd();
		#endif
	}

	void Update(){
		#if UNITY_ANDROID
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		#endif
	}

	public void OnStartButtonClicked () {
		AdManager.instance.HideBannerAd ();
		AdManager.instance.HideIconAd ();
		OnStartButtonClickedEvent ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		Destroy (transform.parent.gameObject);
	}

	public void OnRankingButtonClicked () {
		LobiUtil.Instance.showRanking ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}

	public void OnItemButtonClicked () {
		AdManager.instance.HideIconAd ();
		GameObject containerPrefab = Resources.Load ("Container/ShopContainer") as GameObject;
		GameObject containerObject = Instantiate (containerPrefab) as GameObject;
		containerObject.transform.parent = transform.parent.transform.parent;
		containerObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		Destroy (transform.parent.gameObject);
	}

	public void OnRecommendButtonClicked () {
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		AdManager.instance.ShowWallAd ();
	}
}
