using UnityEngine;
using System.Collections;
using System;

public class GameOverContainerManager : ContainerManager<GameOverContainerManager> {

	public static event Action OnFinishGameEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFinishGameButtonClicked(){
		OnFinishGameEvent ();
		DestoryContainer ();
	}
}
