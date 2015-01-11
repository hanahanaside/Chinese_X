using UnityEngine;
using System.Collections;

public class AdManager : MonoSingleton<AdManager> {
	private const string PUBLICER_ID = "34257";
	private string mInterstitialSpotId = "344010";
	private int mBannerId;
	private int mIconId;

	public override void OnInitialize () {
		IMobileSdkAdsUnityPlugin.registerInline (PUBLICER_ID, "135714", "344009");
		mBannerId = IMobileSdkAdsUnityPlugin.show ("344009", IMobileSdkAdsUnityPlugin.AdType.BANNER,
			IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER, IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM);

		IMobileSdkAdsUnityPlugin.registerInline (PUBLICER_ID, "131681", "330695");
		IMobileSdkAdsUnityPlugin.start ("330695");
		var iconParams = new IMobileIconParams ();
		iconParams.iconNumber = 2;
		iconParams.iconTitleShadowEnable = false;
		iconParams.iconTitleEnable = false;
		mIconId = IMobileSdkAdsUnityPlugin.show ("330695", IMobileSdkAdsUnityPlugin.AdType.ICON, 0, 10, iconParams);

		IMobileSdkAdsUnityPlugin.registerFullScreen (PUBLICER_ID, "135714", mInterstitialSpotId);
		IMobileSdkAdsUnityPlugin.start (mInterstitialSpotId);
		IMobileSdkAdsUnityPlugin.setAdOrientation (IMobileSdkAdsUnityPlugin.ImobileSdkAdsAdOrientation.IMOBILESDKADS_AD_ORIENTATION_LANDSCAPE);

		HideBannerAd ();
		HideIconAd ();

	}

	public void ShowBannerAd () {
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			AdMobManager.instance.hide ();
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerId, true);
		} else {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerId, false);
			AdMobManager.instance.show ();
		}

	}

	public void HideBannerAd () {
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerId, false);
		}else {
			AdMobManager.instance.hide ();
		}

	}

	public void ShowIconAd () {
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconId, true);
		}
	}

	public void HideIconAd () {
		IMobileSdkAdsUnityPlugin.setVisibility (mIconId, false);
	}

	public void ShowInterstitialAd () {
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			IMobileSdkAdsUnityPlugin.show (mInterstitialSpotId);
		}
	}
}
