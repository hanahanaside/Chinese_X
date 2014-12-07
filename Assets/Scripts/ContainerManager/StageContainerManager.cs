using UnityEngine;
using System.Collections;
using System;

public class StageContainerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	public GameObject floorPrefab;
	public GameObject backgroundPrefab;
	public GameObject enemyGeneratorPrefab;
	public int stageLevel;
	private Transform mPlayerTransform;

	void Start () {
		Instantiate (playerPrefab);
		Instantiate (cameraPrefab);
		float[] floorX = { -10, 0, 10 };
		for (int i = 0; i < 3; i++) {
			Instantiate (floorPrefab, new Vector3 (floorX [i], 0, 0), Quaternion.identity);
		}
		Instantiate (backgroundPrefab);
		Instantiate (enemyGeneratorPrefab);
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
