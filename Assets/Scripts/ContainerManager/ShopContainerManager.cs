using UnityEngine;
using System.Collections;
using System;

public class ShopContainerManager : MonoBehaviour {

	public static event Action CloseShopEvent;

	public void TicketButton1Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}

	public void TicketButton2Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}

	public void BackButtonClicked(){
		Destroy (transform.parent.gameObject);
		CloseShopEvent ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}
}
