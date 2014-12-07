using UnityEngine;
using System.Collections;
using System;

public class GameOverContainerManager : ContainerManager<GameOverContainerManager> {

	public static event Action OnFinishGameEvent;
	public static event Action ContinueEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnNoButtonClicked(){
		OnFinishGameEvent ();
		DestoryContainer ();
	}

	public void OnYesButtonClicked(){
		DestoryContainer ();
		ContinueEvent ();
	}
}
