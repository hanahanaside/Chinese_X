using UnityEngine;
using System.Collections;
using System;

public class ShopContainerManager : MonoBehaviour {

	public static event Action CloseShopEvent;

	public void TicketButton1Clicked(){

	}

	public void TicketButton2Clicked(){

	}

	public void BackButtonClicked(){
		Destroy (transform.parent.gameObject);
		CloseShopEvent ();
	}
}
