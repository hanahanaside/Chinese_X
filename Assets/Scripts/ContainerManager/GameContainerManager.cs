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
		Rect gameoverRect = new Rect (10, 10, 150, 50);
		bool gameover = GUI.Button (gameoverRect, "GAME OVER");
		if (gameover) {
			OnGameOverEvent ();
			DestoryContainer ();
		}
		Rect deathRect = new Rect (10,100,150,50);
		bool death = GUI.Button (deathRect,"Death");
		if(death){
			Player.instance.Death ();
		}
	}

	public  void OnAtackButtonClicked () {
		float v = Input.GetAxis ("Vertical");
		if (v < 0) {
			Player.instance.LowKick ();
		} else {
			Player.instance.HighKick ();
		}
	}

	public void OnJumpButtonClicked () {
		Player.instance.Jump ();
	}
}
