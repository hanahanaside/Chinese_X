using UnityEngine;
using System.Collections;
using System;

public class GameOverContainerManager : MonoBehaviour {

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
		Destroy (transform.parent.gameObject);
	}

	public void OnYesButtonClicked(){
		Destroy (transform.parent.gameObject);
		ContinueEvent ();
	}
}
