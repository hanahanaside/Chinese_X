using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IAPManager : MonoSingleton<IAPManager> {

	public static event Action UpdateTicketCountEvent;

	public enum productID {
		item_1,
		item_2,
		item_3
	}

	public string[] productIdentifiers;
	#if UNITY_IPHONE
	private List<StoreKitProduct> mProductList;

	void OnEnable () {
		StoreKitManager.productListReceivedEvent += ReceivedProductsList;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailedEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent += purchaseFailedEvent;
	}

	void OnDisable () {
		StoreKitManager.productListReceivedEvent -= ReceivedProductsList;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailedEvent; 
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent -= purchaseFailedEvent;
	}

	void productListRequestFailedEvent (string error) {
		Debug.Log ("productListRequestFailedEvent");
		Debug.Log (error);
	}

	void ReceivedProductsList (List<StoreKitProduct> productsList) {
		mProductList = productsList;
		Debug.Log ("received total products: " + productsList.Count);
	}

	void purchaseFailedEvent (string error) {
		Debug.Log ("purchaseFailedEvent: " + error);
	}

	void purchaseCancelledEvent (string error) {
		Debug.Log ("purchaseCancelledEvent: " + error);
	}

	void purchaseSuccessfulEvent (StoreKitTransaction transaction) {
		Debug.Log ("purchaseSuccessfulEvent: " + transaction);
		string productId = transaction.productIdentifier;
		if (productId == productIdentifiers [0]) {
			PrefsManager.instance.TicketCount = PrefsManager.instance.TicketCount + 1;
		}
		if (productId == productIdentifiers [1]) {

		}
		if(productId == productIdentifiers[2]){
			PrefsManager.instance.TicketCount = PrefsManager.instance.TicketCount + 2;
		}
		UpdateTicketCountEvent ();
	}

	public override void OnInitialize () {
		RequestProducts ();
	}

	public void PurchaseProduct (IAPManager.productID productID) { 
		if (mProductList == null) {
			ConnectErrorDialog.instance.Show ();
			return;
		}
		StoreKitProduct product = mProductList [(int)productID];
		StoreKitBinding.purchaseProduct (product.productIdentifier, 1);
	}

	public void RequestProducts(){
		if (StoreKitBinding.canMakePayments ()) {
			StoreKitBinding.requestProductData (productIdentifiers);
		}
	}

	#endif
}
