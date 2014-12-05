using UnityEngine;
using System.Collections;
using System;

public class Stage1ContainerManager : ContainerManager<Stage1ContainerManager> {
	public static event Action OnGameOverEvent;

	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	public GameObject floorPrefab;
	public GameObject enemyGeneratorPrefab;
	private Transform mPlayerTransform;

	void OnEnable () {
		PlayerController.ClearedEvent += ClearedEvent;
	}

	void OnDisable () {
		PlayerController.ClearedEvent -= ClearedEvent;
	}

	void Start () {
		Instantiate (playerPrefab);
		Instantiate (cameraPrefab);
		float[] floorX = { -10, 0, 10 };
		for (int i = 0; i < 3; i++) {
			Instantiate (floorPrefab, new Vector3 (floorX [i], 0, 0), Quaternion.identity);
		}
		Instantiate (enemyGeneratorPrefab);
	}

	public  void OnAtackButtonClicked () {
		float v = Input.GetAxis ("Vertical");
		if (v < 0) {
			PlayerController.instance.LowKick ();
		} else {
			PlayerController.instance.HighKick ();
		}
	}

	public void OnJumpButtonClicked () {
		PlayerController.instance.Jump ();
	}

	void ClearedEvent () {
		Debug.Log ("clear");

		OnGameOverEvent ();
		Destroy (transform.parent);
	}
}
