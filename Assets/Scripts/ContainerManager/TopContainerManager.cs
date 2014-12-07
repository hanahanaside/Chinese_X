using UnityEngine;
using System.Collections;
using System;

public class TopContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;

	public void OnStartButtonClicked(){
		OnStartButtonClickedEvent ();
		Destroy (transform.parent.gameObject);
	}
}
