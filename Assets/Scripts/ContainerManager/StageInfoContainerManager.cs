using UnityEngine;
using System.Collections;
using System;

public class StageInfoContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClicked(){
		OnStartButtonClickedEvent ();
		Destroy (transform.parent.gameObject);
	}
}
