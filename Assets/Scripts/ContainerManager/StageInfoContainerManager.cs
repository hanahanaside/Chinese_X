using UnityEngine;
using System.Collections;
using System;

public class StageInfoContainerManager : ContainerManager<StageInfoContainerManager> {

	public static event Action OnStartButtonClickedEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClicked(){
		OnStartButtonClickedEvent ();
		DestoryContainer ();
	}
}
