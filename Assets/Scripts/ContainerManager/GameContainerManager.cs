using UnityEngine;
using System.Collections;
using System;

public class GameContainerManager : ContainerManager<GameContainerManager> {

	public static event Action OnGameOverEvent;

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}

	public  void OnAtackButtonClicked () {
		OnGameOverEvent();
		DestoryContainer ();
	}

	public void OnJumpButtonClicked () {

	}
}
