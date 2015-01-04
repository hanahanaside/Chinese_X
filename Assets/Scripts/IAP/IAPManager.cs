using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAPManager : MonoSingleton<IAPManager> {
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
	}

	public override void OnInitialize () {
		if (StoreKitBinding.canMakePayments ()) {
			StoreKitBinding.requestProductData (productIdentifiers);
		}
	}

	public void PurchaseProduct (IAPManager.productID productID) { 
		if (mProductList == null) {
			return;
		}
		StoreKitProduct product = mProductList [(int)productID];
		StoreKitBinding.purchaseProduct (product.productIdentifier, 1);
	}

	#endif
}
