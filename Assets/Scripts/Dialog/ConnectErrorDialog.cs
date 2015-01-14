using UnityEngine;
using System.Collections;

public class ConnectErrorDialog : MonoSingleton<ConnectErrorDialog> {

	public void YesClicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		#if UNITY_IPHONE
		IAPManager.instance.RequestProducts ();
		#endif
		Dismiss ();
	}

	public void NoClicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		Dismiss ();
	}

	public void Show(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (true);
		}
	}

	private void Dismiss(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
		}
	}
}
