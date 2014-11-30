using UnityEngine;
using System.Collections;
using System;

public class Stage1ContainerManager : ContainerManager<Stage1ContainerManager> {
	public static event Action OnGameOverEvent;

	public GameObject playerPrefab;
	public GameObject atackButton;
	public GameObject jumpButton;
	public GameObject backGroundTexture;

	private Transform mCameraTransform;
	private Transform mPlayerTransform;

	void OnEnable(){
		Player.ClearedEvent += ClearedEvent;
	}

	void OnDisable(){
		Player.ClearedEvent -= ClearedEvent;
	}

	void Start () {
		GameObject playerObject = Instantiate (playerPrefab) as GameObject;
		mPlayerTransform = playerObject.transform;
		playerObject.transform.parent = transform.parent;
		playerObject.transform.localScale = new Vector3 (1, 1, 1);
		mCameraTransform = GameObject.Find ("Camera").transform;
		mCameraTransform.parent = transform.parent;
		atackButton.transform.parent = mCameraTransform;
		jumpButton.transform.parent = mCameraTransform;
		backGroundTexture.transform.parent = mCameraTransform;
	}

	void Update(){
		float playerX = mPlayerTransform.localPosition.x;
		if(playerX > 0){
			return;
		}
		mCameraTransform.localPosition = new Vector3 (playerX,0,0);
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

	void ClearedEvent(){
		Debug.Log ("clear");
	}
}
