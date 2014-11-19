using UnityEngine;
using System.Collections;
using System;

public class StoryContainerManager : ContainerManager<StoryContainerManager> {

	public static event Action OnStoryFinishedEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSkipButtonClicked(){
		OnStoryFinishedEvent ();
		DestoryContainer ();
	}
}
