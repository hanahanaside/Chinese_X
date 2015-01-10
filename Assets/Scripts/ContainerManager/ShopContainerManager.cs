using UnityEngine;
using System.Collections;
using System;

public class ShopContainerManager : MonoBehaviour {

	public static event Action CloseShopEvent;
	public UILabel ticketLabel;

	void OnEnable(){
		IAPManager.UpdateTicketCountEvent += UpdateTicketCountEvent;
	}

	void OnDisable(){
		IAPManager.UpdateTicketCountEvent -= UpdateTicketCountEvent;
	}

	void Start(){
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
	}

	void UpdateTicketCountEvent(){
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
	}

	public void TicketButton1Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button); 
		//		IAPManager.instance.PurchaseProduct (IAPManager.productID.item_1);
	}

	public void TicketButton2Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		IAPManager.instance.PurchaseProduct (IAPManager.productID.item_2);
	}

	public void BackButtonClicked(){
		Destroy (transform.parent.gameObject);
		CloseShopEvent ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}
}
