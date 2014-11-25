using UnityEngine;
using System.Collections;
using System;

public class GameContainerManager : ContainerManager<GameContainerManager> {
	public static event Action OnGameOverEvent;

	public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		GameObject playerObject = Instantiate (playerPrefab) as GameObject;
		playerObject.transform.parent = transform.parent;
		playerObject.transform.localScale = new Vector3 (1, 1, 1);
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		Rect rect = new Rect (10, 10, 150, 50);
		bool isClicked = GUI.Button (rect, "Set UP!!");
		if (isClicked) {
			print ("Stand by Ready!");
		}
	}

	public  void OnAtackButtonClicked () {
		//	OnGameOverEvent();
		//	DestoryContainer ();
		Player.instance.HighKick ();
	}

	public void OnJumpButtonClicked () {
		Player.instance.Jump ();
	}
}
