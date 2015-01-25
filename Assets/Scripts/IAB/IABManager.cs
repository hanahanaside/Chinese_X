using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABManager : MonoSingleton<IABManager> {
	#if UNITY_ANDROID
	public enum Products {
		Ticket_1,
		Ticket_2}
	;

	private const string PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtePLGnOL5ttlNvWJCKY1lcGqK60bM+Pxlr3KPt9Yz5tlZb5fz7v0UjDNhmzO4pJVQkxfQLJU+dLXkQvMOFuJYyrTl1icaydLYRp8H8NAHKO5KeIi1LeuEJ8DeZkLcdOkcCtGdSQJWQNlQHzKO35QIIltH1p1S++J0h1exVKNIvZ0PGV6QtUhnm1MLhapCDcrFNN866wIb0mHc6BOhs3Bx4pQdkICZeT7h8p4PdCIOIb6E42TnVvKozhAtq/tKk4J0Xvy0nhQtd5/NMAkKxP9g4YRPgOkKZi1vm9/75gBr1MofevN7JIVWw1E3E6MNIbbqeZQNV2CpD7Pa6ESd8C6/wIDAQAB";
	private string[] mSKUArray = {"com.maruiorimono.spartan.item1" };

	public override void OnInitialize () {
		GoogleIAB.init (PUBLIC_KEY);
	}

	public void PurchaseProduct (Products products) {
		int index = (int)products;
		string sku = mSKUArray[index];
		GoogleIAB.purchaseProduct (sku);
	}

	void OnEnable () {
		// Listen to all events for illustration purposes
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}

	void OnDisable () {
		// Remove all event handlers
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	void billingSupportedEvent () {
		Debug.Log ("billingSupportedEvent");
		GoogleIAB.queryInventory (mSKUArray);
	}

	void billingNotSupportedEvent (string error) {
		Debug.Log ("billingNotSupportedEvent: " + error);
	}

	void queryInventorySucceededEvent (List<GooglePurchase> purchases, List<GoogleSkuInfo> skus) {
		Debug.Log (string.Format ("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
		Prime31.Utils.logObject (purchases);
		Prime31.Utils.logObject (skus);
	}

	void queryInventoryFailedEvent (string error) {
		Debug.Log ("queryInventoryFailedEvent: " + error);
	}

	void purchaseCompleteAwaitingVerificationEvent (string purchaseData, string signature) {
		Debug.Log ("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
	}

	void purchaseSucceededEvent (GooglePurchase purchase) {
		Debug.Log ("purchaseSucceededEvent: " + purchase);
		GoogleIAB.consumeProduct ("android.test.purchased");
	}

	void purchaseFailedEvent (string error, int response) {
		Debug.Log ("purchaseFailedEvent: " + error + ", response: " + response);
	}

	void consumePurchaseSucceededEvent (GooglePurchase purchase) {
		Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
	}

	void consumePurchaseFailedEvent (string error) {
		Debug.Log ("consumePurchaseFailedEvent: " + error);
	}

	#endif
}
