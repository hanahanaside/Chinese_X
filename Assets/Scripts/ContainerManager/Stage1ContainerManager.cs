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
		PlayerController.ClearedEvent += ClearedEvent;
	}

	void OnDisable(){
		PlayerController.ClearedEvent -= ClearedEvent;
	}

	void Start () {
		GameObject playerObject = Instantiate (playerPrefab) as GameObject;
		mPlayerTransform = playerObject.transform;
		playerObject.transform.parent = transform.parent;
		playerObject.transform.localScale = new Vector3 (1, 1, 1);
		playerObject.transform.localPosition = new Vector3 (600,0,0);
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
		
	public  void OnAtackButtonClicked () {
		float v = Input.GetAxis ("Vertical");
		if (v < 0) {
			PlayerAtackController.instance.LowKick ();
		} else {
			PlayerAtackController.instance.HighKick ();
		}
	}

	public void OnJumpButtonClicked () {
		PlayerController.instance.Jump ();
	}

	void ClearedEvent(){
		Debug.Log ("clear");
	}
}
