using UnityEngine;
using System.Collections;

public class AdManager : MonoSingleton<AdManager> {

	#if !UNITY_EDITOR
	private string mPublisherId = "34257";
	private string mBannerMediaId = "135714";
	private string mBannerSpotId = "344009";
	private string mIconMediaId = "131681";
	private string mIconSpotId = "330695";
	private string mInterstitialMediaId = "135714";
	private string mInterstitialSpotId = "344010";
	private string mWallMediaId = "135714";
	private string mWallSpotId = "353732";


	private int mBannerViewId;
	private int mIconViewId;
	#endif

	public override void OnInitialize () {
	
		#if !UNITY_EDITOR
		if (Application.systemLanguage != SystemLanguage.Japanese) {
			return;
		}
	

		IMobileSdkAdsUnityPlugin.registerInline (mPublisherId, mBannerMediaId, mBannerSpotId);
		mBannerViewId = IMobileSdkAdsUnityPlugin.show (mBannerSpotId, IMobileSdkAdsUnityPlugin.AdType.BANNER,
		IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER, IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM);

		IMobileSdkAdsUnityPlugin.registerInline (mPublisherId, mIconMediaId, mIconSpotId);
		IMobileSdkAdsUnityPlugin.start (mIconSpotId);
		var iconParams = new IMobileIconParams ();
		iconParams.iconNumber = 2;
		iconParams.iconTitleShadowEnable = false;

		iconParams.iconTitleEnable = false;
		#if UNITY_IPHONE
		mIconViewId = IMobileSdkAdsUnityPlugin.show (mIconSpotId, IMobileSdkAdsUnityPlugin.AdType.ICON, 0, 10, iconParams);
		#endif
		#if UNITY_ANDROID
		mIconViewId = IMobileSdkAdsUnityPlugin.show (mIconSpotId, IMobileSdkAdsUnityPlugin.AdType.ICON, 0, 50, iconParams);
		#endif
		IMobileSdkAdsUnityPlugin.registerFullScreen (mPublisherId, mInterstitialMediaId, mInterstitialSpotId);
		IMobileSdkAdsUnityPlugin.start (mInterstitialSpotId);
		IMobileSdkAdsUnityPlugin.setAdOrientation (IMobileSdkAdsUnityPlugin.ImobileSdkAdsAdOrientation.IMOBILESDKADS_AD_ORIENTATION_LANDSCAPE);

		IMobileSdkAdsUnityPlugin.registerFullScreen (mPublisherId, mWallMediaId, mWallSpotId);
		IMobileSdkAdsUnityPlugin.start (mWallSpotId);
		IMobileSdkAdsUnityPlugin.setAdOrientation (IMobileSdkAdsUnityPlugin.ImobileSdkAdsAdOrientation.IMOBILESDKADS_AD_ORIENTATION_LANDSCAPE);

		HideBannerAd ();
		HideIconAd ();

		#endif
	}

	public void ShowBannerAd () {
		#if !UNITY_EDITOR
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			AdMobManager.instance.hide ();
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, true);
		} else {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, false);
			AdMobManager.instance.show ();
		}
		#endif
	}

	public void HideBannerAd () {
		#if !UNITY_EDITOR
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, false);
		} else {
			AdMobManager.instance.hide ();
		}
		#endif
	}

	public void ShowIconAd () {
		#if !UNITY_EDITOR
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, true);
		}
		#endif
	}

	public void HideIconAd () {
		#if !UNITY_EDITOR
		IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, false);
		#endif
	}

	public void ShowInterstitialAd () {
		#if !UNITY_EDITOR
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.show (mInterstitialSpotId);
		}
		#endif
	}

	public void ShowWallAd () {
		#if !UNITY_EDITOR
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.show (mWallSpotId);
		}
		#endif
	}
}
