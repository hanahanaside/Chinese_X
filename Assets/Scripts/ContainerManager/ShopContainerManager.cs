using UnityEngine;
using System.Collections;
using System;

public class ShopContainerManager : MonoBehaviour {

	public static event Action CloseShopEvent;
	public UILabel ticketLabel;

	void OnEnable(){
		#if UNITY_IPHONE
		IAPManager.UpdateTicketCountEvent += UpdateTicketCountEvent;
		#endif
	}

	void OnDisable(){
		#if UNITY_IPHONE
		IAPManager.UpdateTicketCountEvent -= UpdateTicketCountEvent;
		#endif
	}

	void Start(){
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
	}

	void UpdateTicketCountEvent(){
		ticketLabel.text = "×" + PrefsManager.instance.TicketCount;
	}

	public void TicketButton1Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button); 
		#if UNITY_IPHONE
		IAPManager.instance.PurchaseProduct (IAPManager.productID.item_1);
		#endif 

		#if UNITY_ANDROID
		IABManager.instance.PurchaseProduct(IABManager.Products.Ticket_1);
		#endif
	}

	public void TicketButton2Clicked(){
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		#if UNITY_IPHONE
		IAPManager.instance.PurchaseProduct (IAPManager.productID.item_2);
		#endif
	}

	public void BackButtonClicked(){
		Destroy (transform.parent.gameObject);
		CloseShopEvent ();
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}
}
